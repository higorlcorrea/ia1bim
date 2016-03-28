using System.Collections.Generic;
using System.Linq;

namespace SudokuDisplay.Models
{
    public class SudokuHeuristico2 : Sudoku
    {
        #region Propriedades Privadas

        private List<Possibilidades> Possibilidades { get; set; }

        #endregion

        #region Construtores

        public SudokuHeuristico2()
        {
            Possibilidades = new List<Possibilidades>();

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

        #region Métodos Privados

        private List<int> TodosNumeros()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        private void VerificarNumeros(int linha, int coluna, List<int> numeros)
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

        private void VarrerPossibilidades()
        {
            for (int linha = 0; linha < Linhas; linha++)
            {
                for (int coluna = 0; coluna < Colunas; coluna++)
                {
                    if (!Tabela[linha][coluna].HasValue)
                    {
                        var numeros = TodosNumeros();
                        VerificarNumeros(linha, coluna, numeros);
                        Possibilidades.Add(new Possibilidades { Coluna = coluna, Linha = linha, Numeros = numeros.Count });
                    }
                }
            }
        }

        #endregion

        #region Métodos Públicos

        public override void Run()
        {
            VarrerPossibilidades();
            Possibilidades = Possibilidades.OrderBy(p => p.Numeros).ToList();
            var possibilidade = Possibilidades.First();

            Preencher(possibilidade, 1);
        }

        private bool Preencher(Possibilidades possibilidade, int proximo)
        {
            var numeros = TodosNumeros();
            VerificarNumeros(possibilidade.Linha, possibilidade.Coluna, numeros);


            Possibilidades proximaPossibilidade = null;

            if (proximo < Possibilidades.Count)
            {
                proximaPossibilidade = Possibilidades.ElementAt(proximo);
            }

            while (numeros.Count > 0)
            {
                var numero = numeros.First();
                numeros.Remove(numero);

                Tabela[possibilidade.Linha][possibilidade.Coluna] = numero;
                if (TodosPreenchidos())
                {
                    return true;
                }


                if (proximaPossibilidade != null && !Preencher(proximaPossibilidade, proximo + 1))
                {
                    Tabela[possibilidade.Linha][possibilidade.Coluna] = null;
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
