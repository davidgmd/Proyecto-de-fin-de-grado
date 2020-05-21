using Octokit;
using System.Linq;
using System.Threading.Tasks;

namespace ElEscribaDelDJ.Classes
{
    class GitHub
    {
        private GitHubClient cliente;
        private Credentials credenciales;

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
            this.credenciales = new Credentials("f8587aee1fa4b118675143199c7ec7a8da226a91");           
        }

        public async void CrearCredenciales(string nombre, string clave)
        {
            //cliente = new GitHubClient(new ProductHeaderValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName));
            //credenciales = new Credentials("f8587aee1fa4b118675143199c7ec7a8da226a91");
            this.cliente.Credentials = credenciales;
            var user = await this.cliente.User.Get("davidgmd");          
            var repositorio = cliente.Repository.Get("davidgmd", "Proyecto-de-fin-de-grado");

            try {
                var existingFile = await cliente.Repository.Content.GetAllContentsByRef(user.Login, "Proyecto-de-fin-de-grado", nombre + ".json", "master");

                var updateChangeSet = await cliente.Repository.Content.UpdateFile(
                                                user.Login,
                                                "Proyecto-de-fin-de-grado",
                                                nombre + ".json",
                                                new UpdateFileRequest("File update",
                                                                      MainWindow.SesionUsuario.ToString(),
                                                                      existingFile.First().Sha,
                                                                      "master"));
            }
            catch (Octokit.NotFoundException)
            {
                // create file
                var createChangeSet = await cliente.Repository.Content.CreateFile(
                                                user.Login,
                                                "Proyecto-de-fin-de-grado",
                                                nombre + ".json",
                                                new CreateFileRequest("File creation",
                                                                      MainWindow.SesionUsuario.ToString(),
                                                                      "master"));
            }
            

            var request = new SearchCodeRequest("Clave")
            {
                FileName = nombre + ".json"
            };
            var result = await this.cliente.Search.SearchCode(request);
            var cadena = user.Bio;
        }

        public async void ComprobarCredenciales(string nombre, string clave)
        {
            var user = await this.cliente.User.Get("davidgmd");
            var repositorio = cliente.Repository.Get("davidgmd", "Proyecto-de-fin-de-grado");
            var request = new SearchCodeRequest("Clave")
            {
                FileName = nombre + ".json"
            };
            var result = await this.cliente.Search.SearchCode(request);
            var cadena = user.Bio.ToString();
        }
    }
}
