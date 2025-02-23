using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Reflection.Emit;
using static System.Console;

namespace DesignPatterns.Facade.Exercises
{
    // Facade Coding Exercise

    //    A magic square is a square matrix whose rows, columns and diagonals add up to the same value.

    //    I have built a system that helps us construct magic squares, but it's a little bit complicated. At the moment, it is composed of three classes:

    //Generator makes an array of random digits(suitably constrained) of a particular length.You can use this generator several times to build a square matrix of required size.
    //    Splitter splits a 2D square matrix into several lists containing all rows, all columns and all diagonals.
    //    Verifier ensures that, given a list of lists, every single list adds up to the same value.

    //Using all of the above, please implement a MagicSquareGenerator facade that uses all these three components to generate a valid magic square of the required size.

    public class Generator
    {
        private static readonly Random random = new Random();

        public List<int> Generate(int count)
        {
            return Enumerable.Range(0, count)
              .Select(_ => random.Next(1, 6))
              .ToList();
        }
    }

    public class Splitter
    {
        public List<List<int>> Split(List<List<int>> array)
        {
            var result = new List<List<int>>();

            var rowCount = array.Count;
            var colCount = array[0].Count;

            // get the rows
            for (int r = 0; r < rowCount; ++r)
            {
                var theRow = new List<int>();
                for (int c = 0; c < colCount; ++c)
                    theRow.Add(array[r][c]);
                result.Add(theRow);
            }

            // get the columns
            for (int c = 0; c < colCount; ++c)
            {
                var theCol = new List<int>();
                for (int r = 0; r < rowCount; ++r)
                    theCol.Add(array[r][c]);
                result.Add(theCol);
            }

            // now the diagonals
            var diag1 = new List<int>();
            var diag2 = new List<int>();
            for (int c = 0; c < colCount; ++c)
            {
                for (int r = 0; r < rowCount; ++r)
                {
                    if (c == r)
                        diag1.Add(array[r][c]);
                    var r2 = rowCount - r - 1;
                    if (c == r2)
                        diag2.Add(array[r][c]);
                }
            }

            result.Add(diag1);
            result.Add(diag2);

            return result;
        }
    }

    public class Verifier
    {
        public bool Verify(List<List<int>> array)
        {
            if (!array.Any()) return false;

            var expected = array.First().Sum();

            return array.All(t => t.Sum() == expected);
        }
    }

    public class MagicSquareGenerator
    {
        private readonly Generator _generator;
        private readonly Splitter _splitter;
        private readonly Verifier _verifier;

        public MagicSquareGenerator()
        {
            _generator = new Generator();
            _splitter = new Splitter();
            _verifier = new Verifier();
        }

        public List<List<int>> Generate(int size)
        {
            List<List<int>> matrix;

            do
            {
                matrix = new List<List<int>>();
                for (int i = 0; i < size; i++)
                    matrix.Add(_generator.Generate(size));

            } while (!_verifier.Verify(_splitter.Split(matrix)));

            return matrix;
        }
    }

    public class FacadeCodingExercise
    {
        public static void RunExercise()
        {
            foreach (var square in new MagicSquareGenerator().Generate(3))
                WriteLine(string.Join(' ', square));
        }
    }
}
