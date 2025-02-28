using System.Numerics;
using static System.Console;

namespace DesignPatterns.Strategy.Exercises
{
    // Strategy Coding Exercise

    //    Consider the quadratic equation and its canonical solution:
    //    ax^2 + bx + c = 0
    //    x = (-b +- sqrt(b^2 - 4ac)) / 2a

    //The part b^2-4* a* c is called the discriminant.Suppose we want to provide an API with two different strategies for calculating the discriminant:

    //In OrdinaryDiscriminantStrategy, If the discriminant is negative, we return it as-is. This is OK, since our main API returns Complex  numbers anyway.
    //In RealDiscriminantStrategy , if the discriminant is negative, the return value is NaN (not a number). NaN propagates throughout the calculation, so the equation solver gives two NaN values.
    //Please implement both of these strategies as well as the equation solver itself. With regards to plus-minus in the formula, please return the + result as the first element and - as the second.

    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            return b * b - 4 * a * c;
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var discriminant = b * b - 4 * a * c;
            return discriminant < 0 ? double.NaN : discriminant;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var discriminant = strategy.CalculateDiscriminant(a, b, c);
            var sqrtDiscriminant = Complex.Sqrt(new Complex(discriminant, 0));

            var root1 = (-b + sqrtDiscriminant) / (2 * a);
            var root2 = (-b - sqrtDiscriminant) / (2 * a);

            return Tuple.Create(root1, root2);
        }
    }

    public class StrategyCodingExercise
    {
        public static void RunExercise()
        {
            var solver1 = new QuadraticEquationSolver(new OrdinaryDiscriminantStrategy());
            var solver2 = new QuadraticEquationSolver(new RealDiscriminantStrategy());

            var roots1 = solver1.Solve(1, 4, 4);
            var roots2 = solver2.Solve(1, 2, 5);

            WriteLine($"Ordinary Strategy: Roots = {roots1.Item1}, {roots1.Item2}");
            WriteLine($"Real Strategy: Roots = {roots2.Item1}, {roots2.Item2}");
        }
    }
}
