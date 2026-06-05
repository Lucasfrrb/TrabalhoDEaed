using System;
using System.IO;

namespace SistemaEscolar.Models
{
    public class BancoDeDados
    {
        public Aluno[] vetorAlunos = new Aluno[100];
        public int qtdAlunos = 0;
        
        public Disciplina[] vetorDisciplinas = new Disciplina[100];
        public int qtdDisciplinas = 0;
        
        public Matricula[] vetorMatriculas = new Matricula[300];
        public int qtdMatriculas = 0;

        public void CarregarDados()
        {
            if (File.Exists("Alunos.dat"))
            {
                using (StreamReader sr = new StreamReader("Alunos.dat"))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        string[] dados = linha.Split(';');
                        if (dados.Length == 3)
                        {
                            vetorAlunos[qtdAlunos] = new Aluno { Matricula_Aluno = int.Parse(dados[0]), Nome_Aluno = dados[1], Idade = int.Parse(dados[2]) };
                            qtdAlunos++;
                        }
                    }
                }
            }

            if (File.Exists("Disciplinas.dat"))
            {
                using (StreamReader sr = new StreamReader("Disciplinas.dat"))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        string[] dados = linha.Split(';');
                        if (dados.Length == 3)
                        {
                            vetorDisciplinas[qtdDisciplinas] = new Disciplina { Cod_Disciplina = int.Parse(dados[0]), Nome_Disciplina = dados[1], Nota_Minima = double.Parse(dados[2]) };
                            qtdDisciplinas++;
                        }
                    }
                }
            }

            if (File.Exists("Matriculas.dat"))
            {
                using (StreamReader sr = new StreamReader("Matriculas.dat"))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        string[] dados = linha.Split(';');
                        if (dados.Length == 4)
                        {
                            vetorMatriculas[qtdMatriculas] = new Matricula { Cod_Disciplina = int.Parse(dados[0]), Matricula_Aluno = int.Parse(dados[1]), Nota1 = double.Parse(dados[2]), Nota2 = double.Parse(dados[3]) };
                            qtdMatriculas++;
                        }
                    }
                }
            }
        }

        public void SalvarDados()
        {
            using (StreamWriter sw = new StreamWriter("Alunos.dat"))
            {
                for (int i = 0; i < qtdAlunos; i++)
                    sw.WriteLine($"{vetorAlunos[i].Matricula_Aluno};{vetorAlunos[i].Nome_Aluno};{vetorAlunos[i].Idade}");
            }

            using (StreamWriter sw = new StreamWriter("Disciplinas.dat"))
            {
                for (int i = 0; i < qtdDisciplinas; i++)
                    sw.WriteLine($"{vetorDisciplinas[i].Cod_Disciplina};{vetorDisciplinas[i].Nome_Disciplina};{vetorDisciplinas[i].Nota_Minima}");
            }

            using (StreamWriter sw = new StreamWriter("Matriculas.dat"))
            {
                for (int i = 0; i < qtdMatriculas; i++)
                    sw.WriteLine($"{vetorMatriculas[i].Cod_Disciplina};{vetorMatriculas[i].Matricula_Aluno};{vetorMatriculas[i].Nota1};{vetorMatriculas[i].Nota2}");
            }
        }
    }
}