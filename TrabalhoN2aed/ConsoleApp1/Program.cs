using SistemaEscolar.Controllers;

namespace SistemaEscolar
{
    class Program
    {
        static void Main(string[] args)
        {
            EscolaController app = new EscolaController();
            app.Iniciar();
        }
    }
}