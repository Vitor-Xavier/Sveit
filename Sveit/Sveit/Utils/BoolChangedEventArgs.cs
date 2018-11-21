using System;

namespace Sveit.Utils
{
    public class BoolChangedEventArgs : EventArgs
    {
        public bool OldValue { get; private set; }

        public bool NewValue { get; private set; }

        public BoolChangedEventArgs(bool oldValue, bool newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
