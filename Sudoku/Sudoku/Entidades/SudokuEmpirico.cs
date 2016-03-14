using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuApp.Entidades
{
    public class SudokuEmpirico : Sudoku
    {
        #region Métodos Protegidos

        protected List<int> TodosNumeros()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        #endregion

        #region Métodos Públicos

        public override void Run()
        {
            base.Run();
        }

        public override bool Preencher(int linha, int coluna, int numero)
        {
            while (numero <= 9)
            {
                //PAREI AQUI!
                var possivel = ValidarEntrada(linha, coluna, numero);
                if (possivel)
                {
                    Tabela[linha][coluna] = numero;
                    if (TodosPreenchidos())
                    {
                        return true;
                    }
                    var proximaLinha = ProximaLinha(linha, coluna);
                    var proximaColuna = ProximaColuna(linha, coluna);

                    if (!Preencher(proximaLinha, proximaColuna, 1))
                    {
                        Tabela[linha][coluna] = 0;
                    }
                    else
                    {
                        return true;
                    }
                }
                numero++;
            }

            return false;
        }

        #endregion
    }
}
