using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuDisplay.Models
{
    public class SudokuViewModel
    {
        #region Propriedades Públicas

        public Sudoku Sudoku { get; set; }

        public SudokuEmpirico SudokuEmpirico { get; set; }

        public bool IsBackTrack { get; set; }

        #endregion

        #region Construtores

        public SudokuViewModel()
        {
            SudokuEmpirico = new SudokuEmpirico();

            Sudoku = new Sudoku();
        }

        #endregion
    }
}