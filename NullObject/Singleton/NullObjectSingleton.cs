using static System.Console;

namespace DesignPatterns.NullObject.Singleton
{
    public interface ILog
    {
        public void Warn();

        public static ILog Null => NullLog.Instance;

        private sealed class NullLog : ILog
        {
            private NullLog() { }

            private static Lazy<NullLog> instance =
              new Lazy<NullLog>(() => new NullLog());

            public static ILog Instance => instance.Value;

            public void Warn()
            {

            }
        }
    }

    public class BankAccount
    {
        private readonly ILog log;

        public BankAccount(ILog log = null)
        {
            this.log = log ?? ILog.Null;
        }
    }

    public class NullObjectSingleton
    {
        public static void RunDemo()
        {
            ILog log = ILog.Null;
        }
    }
}