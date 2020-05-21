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
    class GitHub
    {
        private GitHubClient cliente;
        private Credentials credenciales;
        private Repository repositorio;
        private User usuario;

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

        public GitHub()
        {
            this.cliente = new GitHubClient(new ProductHeaderValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName));

            //Se leen las claves de desencriptación del fichero encriptkeys
            AESencription encriptar = new AESencription();
            var localDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            //cambiar a .user al finalizar las pruebas
            string path = localDirectory + "\\Classes\\Keys\\encriptkeys.txt";
            encriptar = JsonConvert.DeserializeObject<AESencription>(File.ReadAllText(path));

            localDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
           //cambiar a .user al finalizar las pruebas
            path = localDirectory + "gitcredentials.txt";

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
            try {
                // create file
                var existingFile = await cliente.Repository.Content.GetAllContentsByRef(this.repositorio.Id, nombre + ".json", "master");
                System.Windows.MessageBox.Show("Error el usuario ya existe");
                return;
            }
            catch (Octokit.NotFoundException)
            {                             
                var createChangeSet = await cliente.Repository.Content.CreateFile(
                                                this.repositorio.Id,
                                                nombre + ".json",
                                                new CreateFileRequest("File creation",
                                                                      MainWindow.SesionUsuario.ToString(),
                                                                      "master"));
                return;
            }
            

            var request = new SearchCodeRequest("Clave")
            {
                FileName = nombre + ".json"
            };
            var result = await this.cliente.Search.SearchCode(request);
            var cadena = this.usuario.Bio;
        }

        public async Task ComprobarCredenciales(string nombre, string clave)
        {
            try
            {
                var existingFile = await cliente.Repository.Content.GetAllContentsByRef(this.repositorio.Id, nombre + ".json", "master");
                existingFile.ToString();
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(existingFile[0].Content);
                if (usuario.Clave.Equals(clave))
                {
                    System.Windows.MessageBox.Show("Datos introducidos correctamente");
                }
                else
                {
                    System.Windows.MessageBox.Show("Usuario o contraseña incorrecta");
                }
            }
            catch (Octokit.NotFoundException)
            {
                System.Windows.MessageBox.Show("Error el usuario no existe");
            }
        }

        public async void ActualizarCredenciales(string nombre, string clave)
        {
            try
            {
                var existingFile = await cliente.Repository.Content.GetAllContentsByRef(this.repositorio.Id, nombre + ".json", "master");
                var updateChangeSet = await cliente.Repository.Content.UpdateFile(
                                                repositorio.Id,
                                                nombre + ".json",
                                                new UpdateFileRequest("File update",
                                                                      MainWindow.SesionUsuario.ToString(),
                                                                      existingFile.First().Sha,
                                                                      "master"));
            }
            catch (Octokit.NotFoundException)
            {

            }
        }
    }
}
