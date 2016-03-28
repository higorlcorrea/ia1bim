using SudokuDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuDisplay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new SudokuViewModel();
            model.Possivel = true;
            model.IsBackTrack = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Heuristico(SudokuViewModel model)
        {
            model.SudokuHeuristico.Run();
            if (model.SudokuHeuristico.TodosPreenchidos())
            {
                model.Possivel = true;
            }

            model.IsBackTrack = false;

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Backtrack(SudokuViewModel model)
        {
            model.Sudoku.Run();
            if (model.Sudoku.TodosPreenchidos())
            {
                model.Possivel = true;
            }

            model.IsBackTrack = true;

            return View("Index", model);
        }

        public ActionResult Aleatorio(SudokuViewModel model)
        {
            if (model.NumerosAleatorios.HasValue && model.NumerosAleatorios.Value > 0 && model.NumerosAleatorios.Value < 81)
            {
                var random = new Random();
                var sudoku = new SudokuHeuristico();
                sudoku.InicializarContexto();
                var preenchidas = new List<string>();

                for (int i = 0; i < model.NumerosAleatorios.Value; i++)
                {
                    var linha = random.Next(8);
                    var coluna = random.Next(8);

                    if (preenchidas.All(x => x != linha + "," + coluna))
                    {
                        var numeros = sudoku.TodosNumeros();

                        sudoku.VerificarNumeros(linha, coluna, numeros);

                        if (numeros.Count > 0)
                        {
                            var index = random.Next(0, numeros.Count - 1);

                            sudoku.Tabela[linha][coluna] = numeros.ElementAt(index);

                            preenchidas.Add(linha + "," + coluna);
                        }
                    }
                    else
                    {
                        i--;
                    }
                }

                if (model.IsBackTrack)
                {
                    model.Sudoku.Tabela = sudoku.Tabela;
                }
                else
                {
                    model.SudokuHeuristico.Tabela = sudoku.Tabela;
                }
                model.Possivel = true;
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult ValidarEntrada(SudokuViewModel model, int linha, int coluna)
        {
            var erro = "";
            var sucesso = false;
            if (model.IsBackTrack)
            {
                var numero = model.Sudoku.Tabela[linha][coluna];
                model.Sudoku.Tabela[linha][coluna] = null;
                if (numero.HasValue)
                {
                    if (model.Sudoku.ValidarEntrada(linha, coluna, numero.Value))
                    {
                        sucesso = true;
                    }
                    else
                    {
                        erro = "Você violou as regras do sudoku!";
                    }
                }
            }
            else
            {
                var numero = model.SudokuHeuristico.Tabela[linha][coluna];
                model.SudokuHeuristico.Tabela[linha][coluna] = null;
                if (numero.HasValue)
                {
                    if (model.SudokuHeuristico.ValidarEntrada(linha, coluna, numero.Value))
                    {
                        sucesso = true;
                    }
                    else
                    {
                        erro = "Você violou as regras do sudoku!";
                    }
                }
            }

            return Json(new { Success = sucesso, Erro = erro });
        }
    }
}