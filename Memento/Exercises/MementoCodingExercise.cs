using System;

namespace DesignPatterns.Memento.Exercises
{
    //Memento Coding Exercise
    //A TokenMachine  is in charge of keeping tokens.Each Token  is a reference type with a single numerical value. The machine supports adding tokens and, when it does, it returns a memento representing the state of that system at that given time.

    //You are asked to fill in the gaps and implement the Memento design pattern for this scenario.Pay close attention to the situation where a token is fed in as a reference and its value is subsequently changed on that reference - you still need to return the correct system snapshot!

    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }

        public Token(Token other)
        {
            this.Value = other.Value;
        }
    }

    public class Memento
    {
        public List<Token> TokensSnapshot = new List<Token>();

        public Memento(List<Token> tokens)
        {
            TokensSnapshot = tokens.Select(t => new Token(t)).ToList();
        }
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            var token = new Token(value);
            Tokens.Add(token);
            return new Memento(Tokens);
        }

        public Memento AddToken(Token token)
        {
            var copy = new Token(token);
            Tokens.Add(copy);
            return new Memento(Tokens);
        }

        public void Revert(Memento m)
        {
            Tokens = m.TokensSnapshot.Select(t => new Token(t)).ToList();
        }
    }

    public class MementoCodingExercise
    {
        public static void RunExercise()
        {
            var machine = new TokenMachine();

            var m1 = machine.AddToken(10);
            Console.WriteLine("Após AddToken(10): " + string.Join(", ", machine.Tokens.Select(t => t.Value)));

            var token = new Token(20);
            var m2 = machine.AddToken(token);
            Console.WriteLine("Após AddToken(token(20)): " + string.Join(", ", machine.Tokens.Select(t => t.Value)));

            token.Value = 999;
            Console.WriteLine("Após token.Value = 999: " + string.Join(", ", machine.Tokens.Select(t => t.Value)));

            machine.Revert(m1);
            Console.WriteLine("Após Revert(m1): " + string.Join(", ", machine.Tokens.Select(t => t.Value)));

            machine.Revert(m2);
            Console.WriteLine("Após Revert(m2): " + string.Join(", ", machine.Tokens.Select(t => t.Value)));
        }
    }
}
