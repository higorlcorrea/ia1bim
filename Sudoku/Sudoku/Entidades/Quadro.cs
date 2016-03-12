using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp.Entidades
{
    public class Quadro
    {
        #region Propriedades Publicas

        public int LinhaMaxima { get; set; }

        public int LinhaMinima { get; set; }

        public int ColunaMaxima { get; set; }

        public int ColunaMinima { get; set; }

        #endregion
    }
}
