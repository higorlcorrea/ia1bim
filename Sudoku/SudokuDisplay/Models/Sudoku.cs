using System.Collections.Generic;
using System.Linq;

namespace SudokuDisplay.Models
{
    public class Sudoku
    {
        #region Atributos Privados

        public int Linhas = 9;

        public int Colunas = 9;

        #endregion

        #region Propriedades Publicas

        public int?[][] Tabela { get; set; }

        #endregion

        #region Propriedades Protegidas

        public List<Quadro> PossiveisQuadros { get; set; }

        #endregion

        #region Construtores

        public Sudoku()
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

        #region Métodos Públicos

        public virtual void Run()
        {
            var coluna = 0;
            var linha = 0;
            var numero = 1;
            Preencher(linha, coluna, numero);
        }

        #endregion

        #region Métodos Protegidos

        public bool ValidarEntrada(int linha, int coluna, int numero)
        {
            var contemLinhaColuna = Tabela[linha].Contains(numero);
            if (contemLinhaColuna)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < Linhas; i++)
                {
                    if (Tabela[i][coluna] == numero)
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

        public virtual bool Preencher(int linha, int coluna, int numero)
        {
            int proximaLinha = 0;
            int proximaColuna = 0;
            if (linha < 9 && coluna < 9)
            {

                if (Tabela[linha][coluna].HasValue)
                {
                    proximaLinha = ProximaLinha(linha, coluna);
                    proximaColuna = ProximaColuna(linha, coluna);
                    Preencher(proximaLinha, proximaColuna, 1);
                    if (TodosPreenchidos())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    while (numero <= 9)
                    {

                        var possivel = ValidarEntrada(linha, coluna, numero);
                        if (possivel)
                        {
                            Tabela[linha][coluna] = numero;
                            if (TodosPreenchidos())
                            {
                                return true;
                            }
                            proximaLinha = ProximaLinha(linha, coluna);
                            proximaColuna = ProximaColuna(linha, coluna);

                            if (!Preencher(proximaLinha, proximaColuna, 1))
                            {
                                Tabela[linha][coluna] = null;
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
            }
            return true;
        }

        public void InicializarContexto()
        {
            Tabela = new int?[Linhas][];
            for (int i = 0; i < Colunas; i++)
            {
                Tabela[i] = new int?[Colunas];
            }
        }

        protected int ProximaLinha(int linha, int coluna)
        {
            return coluna >= 8 ? linha + 1 : linha;
        }

        protected int ProximaColuna(int linha, int coluna)
        {
            return coluna >= 8 ? 0 : coluna + 1;
        }

        protected Quadro RetornarNumeroQuadro(int linha, int coluna)
        {
            return PossiveisQuadros.Where(x => linha <= x.LinhaMaxima && linha >= x.LinhaMinima && coluna >= x.ColunaMinima && coluna <= x.ColunaMaxima).FirstOrDefault();
        }

        protected bool TodosPreenchidos()
        {
            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                {
                    if (!Tabela[i][j].HasValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

    }
}
