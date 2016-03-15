using SudokuApp.Entidades;
using System;

namespace SudokuApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudoku = new Sudoku();
            var sudokuEmpirico = new SudokuEmpirico();

            var sudokuInicio = DateTime.Now;
            sudoku.Run();
            var sudokuFim = DateTime.Now;
            
            for (int i = 0; i < sudoku.Linhas; i++)
            {
                for (int j = 0; j < sudoku.Colunas; j++)
                {
                    Console.Write(sudoku.Tabela[i][j].ToString() + "\t");
                }
                Console.Write("\n\n");
            }
            Console.WriteLine("Sudoku Burro demorou "+(sudokuFim-sudokuInicio).TotalMilliseconds.ToString()+"ms para completar");

            Console.Write("\n\n");
            Console.Write("\n\n");

            sudokuInicio = DateTime.Now;
            sudokuEmpirico.Run();
            sudokuFim = DateTime.Now;

            for (int i = 0; i < sudokuEmpirico.Linhas; i++)
            {
                for (int j = 0; j < sudokuEmpirico.Colunas; j++)
                {
                    Console.Write(sudokuEmpirico.Tabela[i][j].ToString() + "\t");
                }
                Console.Write("\n\n");
            }

            Console.WriteLine("Sudoku Empírico demorou " + (sudokuFim - sudokuInicio).TotalMilliseconds.ToString() + "ms para completar");
            Console.ReadLine();

        }
    }
}
