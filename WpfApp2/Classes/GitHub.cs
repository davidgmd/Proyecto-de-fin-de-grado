using Octokit;
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
            cliente = new GitHubClient(new ProductHeaderValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName));
            credenciales = new Credentials("aa7f94ddd461fb37d47e6db87527589a7a7529c4");
            ObtenerDatos();
            
        }

        public async Task ObtenerDatos()
        {
            var user = await this.cliente.User.Get("davidgmd");
            var repositorio = cliente.Repository.Get("davidgmd", "Proyecto-de-fin-de-grado");
            repositorio.
            var cadena = user.Bio.ToString();
        }
    }
}
