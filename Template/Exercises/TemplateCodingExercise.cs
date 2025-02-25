using static System.Console;

namespace DesignPatterns.Template.Exercises
{
    // Template Method Coding Exercise
    //Imagine a typical collectible card game which has cards representing creatures.Each creature has two values: Attack and Health.Creatures can fight each other, dealing their Attack damage, thereby reducing their opponent's health.

    //The class CardGame implements the logic for two creatures fighting one another.However, the exact mechanics of how damage is dealt is different:

    //TemporaryCardDamage : In some games(e.g., Magic: the Gathering), unless the creature has been killed, its health returns to the original value at the end of combat.
    //PermanentCardDamage : In other games(e.g., Hearthstone), health damage persists.
    //You are asked to implement classes TemporaryCardDamageGame and PermanentCardDamageGame that would allow us to simulate combat between creatures.
    //    Some examples:

    //With temporary damage, creatures 1/2 and 1/3 can never kill one another.With permanent damage, second creature will win after 2 rounds of combat.
    //With either temporary or permanent damage, two 2/2 creatures kill one another.

    public class Creature
    {
        public int Attack, Health;

        public Creature(int attack, int health)
        {
            Attack = attack;
            Health = health;
        }
    }

    public abstract class CardGame
    {
        public Creature[] Creatures;

        public CardGame(Creature[] creatures)
        {
            Creatures = creatures;
        }

        // returns -1 if no clear winner (both alive or both dead)
        public int Combat(int creature1, int creature2)
        {
            Creature first = Creatures[creature1];
            Creature second = Creatures[creature2];
            Hit(first, second);
            Hit(second, first);
            bool firstAlive = first.Health > 0;
            bool secondAlive = second.Health > 0;
            if (firstAlive == secondAlive) return -1;
            return firstAlive ? creature1 : creature2;
        }

        // attacker hits other creature
        protected abstract void Hit(Creature attacker, Creature other);
    }

    public class TemporaryCardDamageGame : CardGame
    {
        public TemporaryCardDamageGame(Creature[] creatures) : base(creatures)
        {
        }

        protected override void Hit(Creature attacker, Creature other)
        {
            var oldHealth = other.Health;
            other.Health -= attacker.Attack;

            if (other.Health > 0)
                other.Health = oldHealth;
        }
    }

    public class PermanentCardDamage : CardGame
    {
        public PermanentCardDamage(Creature[] creatures) : base(creatures)
        {
        }

        protected override void Hit(Creature attacker, Creature other)
        {
            other.Health -= attacker.Attack;
        }
    }

    public class TemplateCodingExercise
    {
        public static void RunExercise()
        {
            var tCard = new TemporaryCardDamageGame(
            [
                new Creature(1, 2),
                new Creature(1, 2),

                new Creature(1, 2),
                new Creature(1, 3),

                new Creature(2, 2),
                new Creature(2, 2)
            ]);

            var lastAliveT = tCard.Combat(0, 1);
            WriteLine($"Temporary Card Damage Game Winner 1: {lastAliveT}");

            var lastAliveT2 = tCard.Combat(2, 3);
            WriteLine($"Temporary Card Damage Game Winner 2: {lastAliveT2}");

            var lastAliveT3 = tCard.Combat(4, 5);
            WriteLine($"Temporary Card Damage Game Winner 3: {lastAliveT3}");

            var pCard = new PermanentCardDamage(
            [
                new Creature(1, 2),
                new Creature(1, 2),

                new Creature(3, 2),
                new Creature(1, 3),

                new Creature(2, 2),
                new Creature(2, 2)
            ]);

            var lastAliveP = pCard.Combat(0, 1);
            WriteLine($"Permanent Card Damage Game Winner 1: {lastAliveP}");

            var lastAliveP2 = pCard.Combat(2, 3);
            WriteLine($"Permanent Card Damage Game Winner 2: {lastAliveP2}");

            var lastAliveP3 = pCard.Combat(4, 5);
            WriteLine($"Permanent Card Damage Game Winner 3: {lastAliveP3}");
        }
    }
}
