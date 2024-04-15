using System.Windows.Input;

namespace ViewModelApi {
    public class Commands : ICommand {
        private readonly Action execute;
        private bool isEnabled;

        public Commands(Action execute)
        {
            this.execute = execute;
            IsEnabled = true;
        }

        public bool IsEnabled {
            get { 
                return isEnabled; 
            }

            set {
                if (value != isEnabled) {
                    isEnabled = value;

                    if (CanExecuteChanged != null) {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            execute();
        }

        internal void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
