using System.Collections.Generic;
using System.Linq;

namespace SudokuDisplay.Models
{
    public class SudokuEmpirico : Sudoku
    {
        #region Construtores

        public SudokuEmpirico()
        {
            PossiveisQuadros = new List<Quadro>
            {
                //PRIMEIRA LINHA
                new Quadro{ LinhaMinima = 0, LinhaMaxima = 2, ColunaMinima = 0, ColunaMaxima = 2},
                new Quadro{ LinhaMinima = 0, LinhaMaxima = 2, ColunaMinima = 3, ColunaMaxima = 5},
                new Quadro{ LinhaMinima = 0, LinhaMaxima = 2, ColunaMinima = 6, ColunaMaxima = 8},

                //SEGUNDA LINHA
                new Quadro{ LinhaMinima = 3, LinhaMaxima = 5, ColunaMinima = 0, ColunaMaxima = 2},
                new Quadro{ LinhaMinima = 3, LinhaMaxima = 5, ColunaMinima = 3, ColunaMaxima = 5},
                new Quadro{ LinhaMinima = 3, LinhaMaxima = 5, ColunaMinima = 6, ColunaMaxima = 8},

                //TERCEIRA LINHA
                new Quadro{ LinhaMinima = 6, LinhaMaxima = 8, ColunaMinima = 0, ColunaMaxima = 2},
                new Quadro{ LinhaMinima = 6, LinhaMaxima = 8, ColunaMinima = 3, ColunaMaxima = 5},
                new Quadro{ LinhaMinima = 6, LinhaMaxima = 8, ColunaMinima = 6, ColunaMaxima = 8}
            };
        }

        #endregion

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
                if (Tabela[i][coluna].HasValue)
                {
                    numeros.Remove(Tabela[i][coluna].Value);
                }
            }

            var quadro = RetornarNumeroQuadro(linha, coluna);

            for (int i = quadro.LinhaMinima; i <= quadro.LinhaMaxima; i++)
            {
                for (int j = quadro.ColunaMinima; j <= quadro.ColunaMaxima; j++)
                {
                    if (Tabela[i][j].HasValue)
                    {
                        numeros.Remove(Tabela[i][j].Value);
                    }
                }
            }
        }

        #endregion

        #region Métodos Públicos

        public override void Run()
        {
            var coluna = 0;
            var linha = 0;
            Preencher(linha, coluna);
        }

        public bool Preencher(int linha, int coluna)
        {
            var proximaLinha = 0;
            var proximaColuna = 0;

            if (Tabela[linha][coluna].HasValue)
            {
                proximaLinha = ProximaLinha(linha, coluna);
                proximaColuna = ProximaColuna(linha, coluna);
                if (TodosPreenchidos())
                {
                    return true;
                }
                else
                {
                    Preencher(proximaLinha, proximaColuna);
                }
                return true;
            }
            else
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
                    proximaLinha = ProximaLinha(linha, coluna);
                    proximaColuna = ProximaColuna(linha, coluna);

                    if (!Preencher(proximaLinha, proximaColuna))
                    {
                        Tabela[linha][coluna] = null;
                    }
                    else
                    {
                        return true;
                    }
                }
                return false;
            }

        }

        #endregion

    }
}
