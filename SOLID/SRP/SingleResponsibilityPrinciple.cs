using System.Diagnostics;
using static System.Console;

namespace DesignPatterns.SOLID.SRP
{
    // just stores a couple of comment entries and ways of
    // working with them
    public class Comment
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; // memento pattern!
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        // breaks single responsibility principle
        public void Save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename, ToString());
        }

        public void Load(string filename)
        {

        }

        public void Load(Uri uri)
        {

        }
    }

    // handles the responsibility of persisting objects
    public class Persistence
    {
        public void SaveToFile(Comment comment, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, comment.ToString());
        }
    }

    public static class SingleResponsibilityPrinciple
    {
        public static void RunDemo()
        {
            var j = new Comment();
            j.AddEntry("This is the Single Responsibility Principle.");
            j.AddEntry("It makes part of SOLID Design Principles.");
            WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\comment.txt";
            p.SaveToFile(j, filename);

            Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
        }
    }
}
