using SudokuApp.Entidades;
using System;

namespace SudokuApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudoku = new Sudoku();

            sudoku.Run();

            for (int i = 0; i < sudoku.Linhas; i++)
            {
                for (int j = 0; j < sudoku.Colunas; j++)
                {
                    Console.Write(sudoku.Tabela[i][j].ToString() + "\t");
                }
                Console.Write("\n\n");
            }
            Console.Read();
        }
    }
}
