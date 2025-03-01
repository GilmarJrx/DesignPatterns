using ImpromptuInterface;
using System.Dynamic;
using static System.Console;

namespace DesignPatterns.NullObject.Dynamic
{
    public class Null<TInterface> : DynamicObject where TInterface : class
    {
        public static TInterface Instance =>
            new Null<TInterface>().ActLike<TInterface>();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args,
            out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }

    public class DynamicNullObject
    {
        public static void RunDemo()
        {
            var log = Null<ILog>.Instance;
            var ba = new BankAccount(log);
            ba.Deposit(100);
        }
    }
}