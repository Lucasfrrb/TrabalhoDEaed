using System;

namespace SistemaEscolar.Views
{
    public class EscolaView
    {
        public void LimparTela()
{
    try
    {
        Console.Clear();
    }
    catch (System.IO.IOException)
    {
        // Se o terminal não suportar limpar a tela, imprime linhas em branco para separar visualmente
        Console.WriteLine("\n\n===================================\n\n");
    }
}
        
        public void MostrarMensagem(string msg, bool pausar = true)
        {
            Console.WriteLine(msg);
            if (pausar) Console.ReadKey();
        }

        public string MostrarMenuPrincipal()
        {
            LimparTela();
            Console.WriteLine("=== SISTEMA ESCOLAR ===");
            Console.WriteLine("1 - Consultas");
            Console.WriteLine("2 - Cadastros");
            Console.WriteLine("3 - Salvar");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opcao: ");
            return Console.ReadLine();
        }

        public string MostrarMenuConsultas()
        {
            LimparTela();
            Console.WriteLine("=== CONSULTAS ===");
            Console.WriteLine("1 - Alunos");
            Console.WriteLine("2 - Disciplinas");
            Console.WriteLine("3 - Alunos das Disciplinas");
            Console.WriteLine("4 - Disciplinas do Aluno");
            Console.WriteLine("5 - Voltar");
            Console.Write("Escolha uma opcao: ");
            return Console.ReadLine();
        }

        public string MostrarMenuCadastros()
        {
            LimparTela();
            Console.WriteLine("=== CADASTROS ===");
            Console.WriteLine("1 - Alunos");
            Console.WriteLine("2 - Disciplinas");
            Console.WriteLine("3 - Matriculas");
            Console.WriteLine("4 - Atribuir Nota a Aluno");
            Console.WriteLine("5 - Voltar");
            Console.Write("Escolha uma opcao: ");
            return Console.ReadLine();
        }

        public string LerString(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine();
        }

        public int LerInt(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                if (int.TryParse(Console.ReadLine(), out int valor)) return valor;
                Console.WriteLine("Valor invalido. Digite um numero inteiro.");
            }
        }

        public double LerDouble(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                if (double.TryParse(Console.ReadLine(), out double valor)) return valor;
                Console.WriteLine("Valor invalido. Digite um numero valido.");
            }
        }
    }
}