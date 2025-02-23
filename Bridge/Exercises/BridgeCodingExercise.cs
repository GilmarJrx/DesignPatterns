using static System.Console;

namespace DesignPatterns.Bridge.Exercises
{
    // Bridge Coding Exercise

    //You are given an example of an inheritance hierarchy which results in Cartesian-product duplication.

    //Please refactor this hierarchy, giving the base class Shape  a constructor that takes an interface IRenderer  defined as

    //interface IRenderer
    //    {
    //        string WhatToRenderAs { get; }
    //    }
    //as well as VectorRenderer and RasterRenderer classes.Each implementer of the Shape  abstract class should have a constructor that takes an IRenderer such that, subsequently, each constructed object's ToString()  operates correctly, for example,

    //new Triangle(new RasterRenderer()).ToString() // returns "Drawing Triangle as pixels" 

    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs => "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "pixels";
    }

    public abstract class Shape
    {
        protected IRenderer renderer;
        public Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public string Name { get; set; }

        public override string ToString() => $"Drawing {Name} as {renderer.WhatToRenderAs}";
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }        
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }
    }

    public class BridgeCodingExercise
    {
        public static void RunExercise()
        {
            WriteLine(new Triangle(new RasterRenderer()).ToString()); // returns "Drawing Triangle as pixels"
        }
    }
}
