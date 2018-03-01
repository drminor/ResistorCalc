using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace ResistorCalcChallenge
{
    public class ViewModelBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>
            (
            ref T backingStore, T value,
            string propertyName,
            Action onChanged = null,
            Action onChanging = null
            )
        {
            VerifyCallerIsProperty(propertyName);

            if (EqualityComparer<T>.Default.Equals(backingStore, value)) return;

            onChanging?.Invoke();
            OnPropertyChanging(propertyName);

            backingStore = value;

            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
        }

        [Conditional("DEBUG")]
        private void VerifyCallerIsProperty(string propertyName)
        {
            var stackTrace = new StackTrace();
            var frame = stackTrace.GetFrames()[2];
            var caller = frame.GetMethod();

            if (!caller.Name.Equals("set_" + propertyName, StringComparison.InvariantCulture))
                throw new InvalidOperationException(
                    string.Format("Called SetProperty for {0} from {1}", propertyName, caller.Name));
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnPropertyChanging(string name)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));
        }
    }
}
