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

        public SudokuHeuristico SudokuHeuristico { get; set; }

        public int? NumerosAleatorios { get; set; }

        public bool IsBackTrack { get; set; }

        #endregion

        #region Construtores

        public SudokuViewModel()
        {
            SudokuHeuristico = new SudokuHeuristico();

            Sudoku = new Sudoku();
        }

        #endregion
    }
}