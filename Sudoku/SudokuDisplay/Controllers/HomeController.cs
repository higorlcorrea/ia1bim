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

    }
}