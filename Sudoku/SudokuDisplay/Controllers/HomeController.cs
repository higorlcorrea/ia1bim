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
        public ActionResult Empirico(SudokuViewModel model)
        {
            model.SudokuEmpirico.Run();
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
                var numero = model.SudokuEmpirico.Tabela[linha][coluna];
                model.SudokuEmpirico.Tabela[linha][coluna] = null;
                if (numero.HasValue)
                {
                    if (model.SudokuEmpirico.ValidarEntrada(linha, coluna, numero.Value))
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