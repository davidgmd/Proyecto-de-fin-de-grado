﻿using ElEscribaDelDJ.Classes.Utilidades.Aplicacion;
using Newtonsoft.Json;
using Octokit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ElEscribaDelDJ.Classes
{
    public sealed class GitHub
    {
        private GitHubClient cliente;
        private Credentials credenciales;
        private Repository repositorio;
        private User usuario;
        private int fileexist;
        private readonly static GitHub githubInstancia = new GitHub();

        public static GitHub GithubInstancia 
        {
            get { return githubInstancia; }
        }

        public int FileExist
        {
            get { return fileexist; }
            set { fileexist = value; }
        }


        public User Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }


        public Repository Repositorio
        {
            get { return repositorio; }
            set { repositorio = value; }
        }

        public Credentials Credenciales
        {
            get { return credenciales; }
            set { credenciales = value; }
        }

        public GitHubClient Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }


        private GitHub()
        {
            this.cliente = new GitHubClient(new ProductHeaderValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName));

            //Se leen las claves de desencriptación del fichero encriptkeys
            AESencription encriptar = new AESencription();
            //cambiar a .user al finalizar las pruebas
            string path = RecursosAplicacion.DireccionBase + "\\Classes\\Keys\\encriptkeys.txt";
            encriptar = JsonConvert.DeserializeObject<AESencription>(File.ReadAllText(path));

           //cambiar a .user al finalizar las pruebas
            path = RecursosAplicacion.DireccionBase + "gitcredentials.txt";

            //Encriptar las credenciales
            //var plaintText = System.IO.File.ReadAllText(path);
            //var bytes = encriptar.Encrypt(path, plaintText, encriptar.AesKey, encriptar.AesIv);
            //System.IO.File.WriteAllBytes(path, bytes);
            
            //Desencriptar las credenciales
            var bytes2 = System.IO.File.ReadAllBytes(path);

            encriptar.Decrypt(bytes2, encriptar.AesKey, encriptar.AesIv);

            this.credenciales = new Credentials(encriptar.Decrypt(bytes2, encriptar.AesKey, encriptar.AesIv));
            this.cliente.Credentials = credenciales;
            this.repositorio = cliente.Repository.Get("davidgmd", "Proyecto-de-fin-de-grado").Result;
        }

        public async Task CrearCredenciales(string nombre, string clave)
        {
            if (await UsuarioExiste(nombre) == true)
            {
                System.Windows.MessageBox.Show("Error el usuario ya existe");
                return;
            }
            else
            {
                var createChangeSet = await cliente.Repository.Content.CreateFile(
                            this.repositorio.Id,
                            "Usuarios/" + nombre + ".json",
                            new CreateFileRequest("File creation",
                                                    MainWindow.SesionUsuario.ToString(),
                                                    "master"));
                return;
            }
        }

        public async Task<Boolean> ComprobarCredenciales(string nombre, string clave)
        {
            try
            {
                if (await UsuarioExiste(nombre) == true)
                {
                    var existingFile = await cliente.Repository.Content.GetAllContentsByRef(this.repositorio.Id, "Usuarios/" + nombre + ".json", "master");
                    existingFile.ToString();
                    Usuario usuario = JsonConvert.DeserializeObject<Usuario>(existingFile[0].Content);
                    if (usuario.Clave.Equals(clave))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Octokit.NotFoundException)
            {
                return false;
            }
        }

        public async void ActualizarCredenciales(string nombre, string clave)
        {
            try
            {
                if (await UsuarioExiste(nombre) == true) 
                {
                    var existingFile = await cliente.Repository.Content.GetAllContentsByRef(this.repositorio.Id, "Usuarios/" + nombre + ".json", "master");
                    var updateChangeSet = await cliente.Repository.Content.UpdateFile(
                                                repositorio.Id,
                                                nombre + ".json",
                                                new UpdateFileRequest("File update",
                                                                      MainWindow.SesionUsuario.ToString(),
                                                                      existingFile.First().Sha,
                                                                      "master"));
                }
            }
            catch (Octokit.NotFoundException)
            {
                System.Windows.MessageBox.Show("Error el fichero no existe");
                return;
            }
        }

        public async Task<Boolean> UsuarioExiste(String nombre)
        {
            try {
                var existingFile = await cliente.Repository.Content.GetAllContentsByRef(this.repositorio.Id, "Usuarios/" + nombre + ".json", "master");
                return true;
            }
            catch (Octokit.NotFoundException)
            {
                return false;
            }

        }        
    }
}