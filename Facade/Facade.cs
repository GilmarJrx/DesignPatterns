namespace DesignPatterns.Facade
{
    public class Facade
    {
        public static void RunDemo()
        {
            Console.WriteLine("That's a really simple Facade Demo: 'Console.WriteLine()'.");
            Console.WriteLine("We can't see how big and complex Console class is, but on call WriteLine, magically, Write a Line on Console!");
        }
    }
}