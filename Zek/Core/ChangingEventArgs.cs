
namespace Zek.Core
{
    public class ChangingEventArgs : CancelEventArgs
    {
        public ChangingEventArgs(object oldValue, object newValue) : this(oldValue, newValue, false)
        {
        }
        public ChangingEventArgs(object oldValue, object newValue, bool cancel) : base(cancel)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public object NewValue { get; set; }

        public object OldValue { get; protected set; }
    }


}
