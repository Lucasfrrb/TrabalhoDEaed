using System;
using SistemaEscolar.Models;
using SistemaEscolar.Views;

namespace SistemaEscolar.Controllers
{
    public class EscolaController
    {
        private BancoDeDados _db;
        private EscolaView _view;

        public EscolaController()
        {
            _db = new BancoDeDados();
            _view = new EscolaView();
            _db.CarregarDados(); // Carrega os dados logo ao abrir o programa
        }

        public void Iniciar()
        {
            bool rodando = true;
            while (rodando)
            {
                string opcao = _view.MostrarMenuPrincipal();

                switch (opcao)
                {
                    case "1": SubMenuConsultas(); break;
                    case "2": SubMenuCadastros(); break;
                    case "3":
                        _db.SalvarDados();
                        _view.MostrarMensagem("Dados salvos com sucesso!");
                        break;
                    case "4":
                        _db.SalvarDados();
                        rodando = false;
                        break;
                    default:
                        _view.MostrarMensagem("Opcao invalida!");
                        break;
                }
            }
        }

        private void SubMenuConsultas()
        {
            string opcao = _view.MostrarMenuConsultas();
            switch (opcao)
            {
                case "1": ListarAlunos(); break;
                case "2": ListarDisciplinas(); break;
                case "3": ListarAlunosPorDisciplina(); break;
                case "4": ListarDisciplinasPorAluno(); break;
                case "5": break;
                default: _view.MostrarMensagem("Opcao invalida!"); break;
            }
        }

        private void SubMenuCadastros()
        {
            string opcao = _view.MostrarMenuCadastros();
            switch (opcao)
            {
                case "1": CadastrarAluno(); break;
                case "2": CadastrarDisciplina(); break;
                case "3": CadastrarMatricula(); break;
                case "4": AtribuirNota(); break;
                case "5": break;
                default: _view.MostrarMensagem("Opcao invalida!"); break;
            }
        }

        private void CadastrarAluno()
        {
            if (_db.qtdAlunos >= 100)
            {
                _view.MostrarMensagem("Erro: Limite maximo de alunos atingido!");
                return;
            }

            string nome = _view.LerString("Digite o nome do aluno: ");
            int idade = _view.LerInt("Digite a idade: ");

            int novaMatricula = _db.qtdAlunos > 0 ? _db.vetorAlunos[_db.qtdAlunos - 1].Matricula_Aluno + 1 : 1;

            Aluno a = new Aluno { Matricula_Aluno = novaMatricula, Nome_Aluno = nome, Idade = idade };
            _db.vetorAlunos[_db.qtdAlunos++] = a;

            _view.MostrarMensagem($"Aluno cadastrado com sucesso! Matricula: {novaMatricula}");
        }

        private void CadastrarDisciplina()
        {
            if (_db.qtdDisciplinas >= 100)
            {
                _view.MostrarMensagem("Erro: Limite maximo de disciplinas atingido!");
                return;
            }

            string nome = _view.LerString("Digite o nome da disciplina: ");
            double notaMinima = _view.LerDouble("Digite a nota minima: ");

            int novoCodigo = _db.qtdDisciplinas > 0 ? _db.vetorDisciplinas[_db.qtdDisciplinas - 1].Cod_Disciplina + 1 : 1;

            Disciplina d = new Disciplina { Cod_Disciplina = novoCodigo, Nome_Disciplina = nome, Nota_Minima = notaMinima };
            _db.vetorDisciplinas[_db.qtdDisciplinas++] = d;

            _view.MostrarMensagem($"Disciplina cadastrada com sucesso! Codigo: {novoCodigo}");
        }

        private void CadastrarMatricula()
        {
            if (_db.qtdMatriculas >= 300)
            {
                _view.MostrarMensagem("Erro: Limite maximo de matriculas atingido!");
                return;
            }

            Aluno aluno = null;
            while (aluno == null)
            {
                string busca = _view.LerString("Digite a matricula ou nome do aluno: ");
                aluno = BuscarAluno(busca);
                if (aluno == null) _view.MostrarMensagem("Aluno nao encontrado.", false);
            }

            Disciplina disciplina = null;
            while (disciplina == null)
            {
                string busca = _view.LerString("Digite o codigo ou nome da disciplina: ");
                disciplina = BuscarDisciplina(busca);
                if (disciplina == null) _view.MostrarMensagem("Disciplina nao encontrada.", false);
            }

            for (int i = 0; i < _db.qtdMatriculas; i++)
            {
                if (_db.vetorMatriculas[i].Matricula_Aluno == aluno.Matricula_Aluno && _db.vetorMatriculas[i].Cod_Disciplina == disciplina.Cod_Disciplina)
                {
                    _view.MostrarMensagem("Este aluno ja esta matriculado nesta disciplina.");
                    return;
                }
            }

            Matricula m = new Matricula { Matricula_Aluno = aluno.Matricula_Aluno, Cod_Disciplina = disciplina.Cod_Disciplina, Nota1 = 0, Nota2 = 0 };
            _db.vetorMatriculas[_db.qtdMatriculas++] = m;

            _view.MostrarMensagem("Matricula cadastrada com sucesso!");
        }

        private void AtribuirNota()
        {
            _view.LimparTela();
            Aluno aluno = BuscarAluno(_view.LerString("Digite a matricula/nome do aluno: "));
            Disciplina disciplina = BuscarDisciplina(_view.LerString("Digite o codigo/nome da disciplina: "));

            if (aluno == null || disciplina == null)
            {
                _view.MostrarMensagem("Aluno ou Disciplina nao encontrados.");
                return;
            }

            Matricula matricula = null;
            for (int i = 0; i < _db.qtdMatriculas; i++)
            {
                if (_db.vetorMatriculas[i].Matricula_Aluno == aluno.Matricula_Aluno && _db.vetorMatriculas[i].Cod_Disciplina == disciplina.Cod_Disciplina)
                {
                    matricula = _db.vetorMatriculas[i];
                    break;
                }
            }

            if (matricula == null)
            {
                _view.MostrarMensagem("Aluno nao esta matriculado nesta disciplina.");
                return;
            }

            matricula.Nota1 = _view.LerDouble("Digite a nota 1: ");
            matricula.Nota2 = _view.LerDouble("Digite a nota 2: ");
            _view.MostrarMensagem($"Notas atribuidas para {aluno.Nome_Aluno}.");
        }

        private void ListarAlunos()
        {
            _view.LimparTela();
            _view.MostrarMensagem("=== LISTA DE ALUNOS ===", false);
            if (_db.qtdAlunos == 0) _view.MostrarMensagem("Nenhum aluno cadastrado.", false);

            for (int i = 0; i < _db.qtdAlunos; i++)
                _view.MostrarMensagem($"Matricula: {_db.vetorAlunos[i].Matricula_Aluno} | Nome: {_db.vetorAlunos[i].Nome_Aluno} | Idade: {_db.vetorAlunos[i].Idade}", false);
            
            Console.ReadKey();
        }

        private void ListarDisciplinas()
        {
            _view.LimparTela();
            _view.MostrarMensagem("=== LISTA DE DISCIPLINAS ===", false);
            if (_db.qtdDisciplinas == 0) _view.MostrarMensagem("Nenhuma disciplina cadastrada.", false);

            for (int i = 0; i < _db.qtdDisciplinas; i++)
                _view.MostrarMensagem($"Codigo: {_db.vetorDisciplinas[i].Cod_Disciplina} | Nome: {_db.vetorDisciplinas[i].Nome_Disciplina} | Nota min: {_db.vetorDisciplinas[i].Nota_Minima}", false);
            
            Console.ReadKey();
        }

        private void ListarAlunosPorDisciplina()
        {
            _view.LimparTela();
            Disciplina disciplina = BuscarDisciplina(_view.LerString("Digite a disciplina: "));
            if (disciplina == null) { _view.MostrarMensagem("Disciplina nao encontrada."); return; }

            _view.MostrarMensagem($"=== ALUNOS EM {disciplina.Nome_Disciplina} ===", false);
            bool encontrou = false;

            for (int i = 0; i < _db.qtdMatriculas; i++)
            {
                if (_db.vetorMatriculas[i].Cod_Disciplina == disciplina.Cod_Disciplina)
                {
                    Aluno aluno = BuscarAluno(_db.vetorMatriculas[i].Matricula_Aluno.ToString());
                    double media = (_db.vetorMatriculas[i].Nota1 + _db.vetorMatriculas[i].Nota2) / 2.0;
                    string status = media >= disciplina.Nota_Minima ? "APROVADO" : "REPROVADO";
                    _view.MostrarMensagem($"{aluno.Nome_Aluno} | Media: {media} | {status}", false);
                    encontrou = true;
                }
            }
            if (!encontrou) _view.MostrarMensagem("Nenhum aluno nesta disciplina.", false);
            Console.ReadKey();
        }

        private void ListarDisciplinasPorAluno()
        {
            _view.LimparTela();
            Aluno aluno = BuscarAluno(_view.LerString("Digite o aluno: "));
            if (aluno == null) { _view.MostrarMensagem("Aluno nao encontrado."); return; }

            _view.MostrarMensagem($"=== DISCIPLINAS DE {aluno.Nome_Aluno} ===", false);
            bool encontrou = false;

            for (int i = 0; i < _db.qtdMatriculas; i++)
            {
                if (_db.vetorMatriculas[i].Matricula_Aluno == aluno.Matricula_Aluno)
                {
                    Disciplina disciplina = BuscarDisciplina(_db.vetorMatriculas[i].Cod_Disciplina.ToString());
                    double media = (_db.vetorMatriculas[i].Nota1 + _db.vetorMatriculas[i].Nota2) / 2.0;
                    string status = media >= disciplina.Nota_Minima ? "APROVADO" : "REPROVADO";
                    _view.MostrarMensagem($"{disciplina.Nome_Disciplina} | Media: {media} | {status}", false);
                    encontrou = true;
                }
            }
            if (!encontrou) _view.MostrarMensagem("Nenhuma disciplina para este aluno.", false);
            Console.ReadKey();
        }

        private Aluno BuscarAluno(string busca)
        {
            bool ehNumero = int.TryParse(busca, out int matricula);
            for (int i = 0; i < _db.qtdAlunos; i++)
            {
                if (ehNumero && _db.vetorAlunos[i].Matricula_Aluno == matricula) return _db.vetorAlunos[i];
                if (!ehNumero && _db.vetorAlunos[i].Nome_Aluno.Equals(busca, StringComparison.OrdinalIgnoreCase)) return _db.vetorAlunos[i];
            }
            return null;
        }

        private Disciplina BuscarDisciplina(string busca)
        {
            bool ehNumero = int.TryParse(busca, out int codigo);
            for (int i = 0; i < _db.qtdDisciplinas; i++)
            {
                if (ehNumero && _db.vetorDisciplinas[i].Cod_Disciplina == codigo) return _db.vetorDisciplinas[i];
                if (!ehNumero && _db.vetorDisciplinas[i].Nome_Disciplina.Equals(busca, StringComparison.OrdinalIgnoreCase)) return _db.vetorDisciplinas[i];
            }
            return null;
        }
    }
}