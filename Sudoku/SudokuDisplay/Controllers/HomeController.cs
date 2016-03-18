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
            model.IsBackTrack = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Heuristico(SudokuViewModel model)
        {
            model.SudokuHeuristico.Run();
            model.IsBackTrack = false;

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Backtrack(SudokuViewModel model)
        {
            model.Sudoku.Run();
            model.IsBackTrack = true;

            return View("Index", model);
        }

        public ActionResult Aleatorio(SudokuViewModel model)
        {
            if (model.NumerosAleatorios.HasValue && model.NumerosAleatorios.Value > 0 && model.NumerosAleatorios.Value < 81)
            {
                var linha = 0;
                var coluna = 0;
                var numero = 0;
                var random = new Random();
                var sudoku = new Sudoku();
                var valido = false;
                var tentativas = 0;
                sudoku.InicializarContexto();
                var preenchidas = new List<string>();

                for (int i = 0; i < model.NumerosAleatorios.Value; i++)
                {
                    while (!valido && tentativas < 3000)
                    {

                        linha = random.Next(8);
                        coluna = random.Next(8);

                        numero = random.Next(1, 9);

                        if (preenchidas.Where(x => x == linha + "," + coluna).Count() == 0 && sudoku.ValidarEntrada(linha, coluna, numero))
                        {
                            sudoku.Tabela[linha][coluna] = numero;
                            preenchidas.Add(linha + "," + coluna);
                            valido = true;
                        }
                        else
                        {
                            tentativas++;
                        }
                    }
                    if (tentativas == 3000)
                    {
                        i = 0;
                        preenchidas.Clear();
                        sudoku.InicializarContexto();
                    }

                    tentativas = 0;
                    valido = false;
                }

                if (model.IsBackTrack)
                {
                    model.Sudoku.Tabela = sudoku.Tabela;
                }
                else
                {
                    model.SudokuHeuristico.Tabela = sudoku.Tabela;
                }

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