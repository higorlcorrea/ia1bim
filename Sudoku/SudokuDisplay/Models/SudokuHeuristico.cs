﻿using System.Collections.Generic;
using System.Linq;

namespace SudokuDisplay.Models
{
    public class SudokuHeuristico : Sudoku
    {
        #region Construtores

        public SudokuHeuristico()
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

        public void VerificarNumeros(int linha, int coluna, List<int> numeros)
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

        public List<int> TodosNumeros()
        {
            return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

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
                Preencher(proximaLinha, proximaColuna);

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

        //public bool ValidarEntrada(int linha, int coluna, int numero)
        //{
        //    var contemLinhaColuna = Tabela[linha].Contains(numero);
        //    if (contemLinhaColuna)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < Linhas; i++)
        //        {
        //            if (Tabela[i][coluna] == numero)
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    var quadro = RetornarNumeroQuadro(linha, coluna);

        //    for (int i = quadro.LinhaMinima; i <= quadro.LinhaMaxima; i++)
        //    {
        //        for (int j = quadro.ColunaMinima; j <= quadro.ColunaMaxima; j++)
        //        {
        //            if (Tabela[i][j] == numero)
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

        #endregion

    }
}
