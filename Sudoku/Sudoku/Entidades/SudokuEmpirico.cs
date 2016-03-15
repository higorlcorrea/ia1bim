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

        protected void VerificarNumeros(int linha, int coluna, List<int> numeros)
        {
            numeros.RemoveAll(x => Tabela[linha].Contains(x));

            for (int i = 0; i < Linhas; i++)
            {
                numeros.Remove(Tabela[i][coluna]);
            }

            var quadro = RetornarNumeroQuadro(linha, coluna);

            for (int i = quadro.LinhaMinima; i <= quadro.LinhaMaxima; i++)
            {
                for (int j = quadro.ColunaMinima; j <= quadro.ColunaMaxima; j++)
                {
                    numeros.Remove(Tabela[i][j]);
                }
            }
        }

        #endregion

        #region Métodos Públicos

        public override void Run()
        {
            InicializarContexto();
            var coluna = 0;
            var linha = 0;
            Preencher(linha, coluna);
        }

        public bool Preencher(int linha, int coluna)
        {
            var numeros = TodosNumeros();
            VerificarNumeros(linha, coluna, numeros);

            while (numeros.Count > 0)
            {
                var numero = numeros.First();
                numeros.Remove(numero);

                Tabela[linha][coluna] = numero;
                if (TodosPreenchidos())
                {
                    return true;
                }
                var proximaLinha = ProximaLinha(linha, coluna);
                var proximaColuna = ProximaColuna(linha, coluna);

                if (!Preencher(proximaLinha, proximaColuna))
                {
                    Tabela[linha][coluna] = 0;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
