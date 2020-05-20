using Octokit;

namespace ElEscribaDelDJ.Classes
{
    class GitHub
    {
        private GitHubClient cliente;

        public GitHubClient Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        public GitHub()
        {
            cliente = new GitHubClient(new ProductHeaderValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName));
            cliente = cliente;
        }
    }
}
