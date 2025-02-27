using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DesignPatterns.Flyweight.Exercises
{
    // Flyweight Coding Exercise
    //    You are given a class called Sentence , which takes a string such as "hello world". You need to provide an interface such that the indexer returns a WordToken  which can be used to capitalize a particular word in the sentence.

    //    Typical use would be something like:


    //    var sentence = new Sentence("hello world");
    //    sentence[1].Capitalize = true;
    //WriteLine(sentence); // writes "hello WORLD"

    public class Sentence
    {
        private readonly string[] words;
        private readonly Dictionary<int, WordToken> formatting = new Dictionary<int, WordToken>();

        public Sentence(string plainText)
        {
            words = plainText.Split(' ');
        }

        public WordToken this[int index]
        {
            get
            {
                if (!formatting.ContainsKey(index))
                    formatting[index] = new WordToken();

                return formatting[index];
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < words.Length; i++)
            {
                if (formatting.ContainsKey(i) && formatting[i].Capitalize)
                    sb.Append(words[i].ToUpper());
                else
                    sb.Append(words[i]);

                if (i < words.Length - 1)
                    sb.Append(' ');
            }

            return sb.ToString();
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }

    public class FlyweightCodingExercise
    {
        public static void RunExercise()
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            WriteLine(sentence); // Deve imprimir "hello WORLD"
        }
    }
}
