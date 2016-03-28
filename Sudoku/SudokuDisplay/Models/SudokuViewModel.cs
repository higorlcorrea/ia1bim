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

        public SudokuHeuristico2 SudokuHeuristico { get; set; }

        public int? NumerosAleatorios { get; set; }

        public bool IsBackTrack { get; set; }

        public bool Possivel { get; set; }

        #endregion

        #region Construtores

        public SudokuViewModel()
        {
            SudokuHeuristico = new SudokuHeuristico2();

            Sudoku = new Sudoku();
        }

        #endregion
    }
}