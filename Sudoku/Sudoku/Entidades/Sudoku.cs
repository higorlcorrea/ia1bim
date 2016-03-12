using System.Collections.Generic;
using System.Linq;

namespace SudokuApp.Entidades
{
    public class Sudoku
    {
        #region Atributos Privados

        public readonly int Linhas = 9;

        public readonly int Colunas = 9;

        #endregion

        #region Propriedades Publicas

        public int[][] Tabela { get; set; }

        #endregion

        #region Propriedades Protegidas

        public List<Quadro> PossiveisQuadros { get; set; }

        #endregion

        #region Construtores

        public Sudoku()
        {
            InicializarContexto();
        }

        #endregion

        #region Métodos Públicos

        public bool ValidarEntrada(int linha, int coluna, int numero)
        {
            var contemLinhaColuna = Tabela[linha].Contains(numero);
            if (contemLinhaColuna)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Colunas; i++)
                {
                    if (Tabela[linha][i] == numero)
                    {
                        return false;
                    }
                }
            }

            var quadro = RetornarNumeroQuadro(linha, coluna);

            for (int i = quadro.LinhaMinima; i <= quadro.LinhaMaxima; i++)
            {
                for (int j = quadro.ColunaMinima; j <= quadro.ColunaMaxima; j++)
                {
                    if (Tabela[i][j] == numero)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Run()
        {
            InicializarContexto();
            var coluna = 0;
            var linha = 0;
            var numero = 1;
            Preencher(linha, coluna, numero);
        }

        public bool Preencher(int linha, int coluna, int numero)
        {
            var possivel = ValidarEntrada(linha, coluna, numero);
            var inserido = false;
            if (possivel)
            {
                Tabela[linha][coluna] = numero;
                inserido = true;
                coluna++;
                if (coluna >= 9)
                {
                    linha++;
                    if (linha >= 9)
                    {
                        return true;
                    }
                    coluna = 0;
                }
                numero = 1;
                Preencher(linha, coluna, numero);
            }
            else
            {
                numero++;
                if (numero <= 9)
                {
                    inserido = Preencher(linha, coluna, numero);
                    
                }
            }
            return inserido;
        }
        #endregion

        #region Métodos Privados

        private void InicializarContexto()
        {
            Tabela = new int[Linhas][];
            for (int i = 0; i < Colunas; i++)
            {
                Tabela[i] = new int[Colunas];
            }

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

        protected Quadro RetornarNumeroQuadro(int linha, int coluna)
        {
            return PossiveisQuadros.Where(x => linha <= x.LinhaMaxima && linha >= x.LinhaMinima && coluna >= x.ColunaMinima && coluna <= x.ColunaMaxima).FirstOrDefault();
        }

        #endregion

    }
}
