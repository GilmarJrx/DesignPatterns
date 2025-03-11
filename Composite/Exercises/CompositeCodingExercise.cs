using System.Collections;
using static System.Console;

namespace DesignPatterns.Composite.Exercises
{
    // Composite Coding Exercise

    //Consider the code presented below.The Sum()  extension method adds up all the values in a list of IValueContainer  elements it gets passed.We can have a single value or a set of values.
    //Complete the implementation of the interfaces so that Sum() begins to work correctly.

    public interface IValueContainer : IEnumerable<int>
    {

    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {

    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }

    public class CompositeCodingExercise
    {
        public static void RunExercise()
        {
            var single = new SingleValue { Value = 5 };
            var many = new ManyValues { 1, 2, 3 };

            var all = new List<IValueContainer> { single, many };
            WriteLine(all.Sum()); // Output: 11
        }
    }
}
