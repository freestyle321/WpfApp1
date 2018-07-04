using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp1.Tool;

namespace WpfApp1.Command
{
    public class RelayCommand : NotifyObject, ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public virtual bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            OnExecute(parameter);
        }

        protected virtual void OnExecute(object parameter)
        {
            _execute(parameter);
        }

        private EventHandler _canExecuteChanged;
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (RequerySuggestedController != null)
                    RequerySuggestedController.AddRequerySuggested(value);
                else
                    _canExecuteChanged += value;
            }
            remove
            {
                if (RequerySuggestedController != null)
                    RequerySuggestedController.RemoveRequerySuggested(value);
                else
                    _canExecuteChanged -= value;
            }
        }
        public void OnCanExecuteChanged()
        {
            if (RequerySuggestedController != null)
            {
                RequerySuggestedController.RequerySuggestedAct();
            }
            else
            {
                var canExecuteChanged = _canExecuteChanged;
                if (_canExecuteChanged != null)
                {
                    canExecuteChanged(this, EventArgs.Empty);
                }
            }
        }

        public RequerySuggestedController RequerySuggestedController { get; set; }

    }

    public interface IRequerySuggested
    {
        Action RequerySuggestedAct { get; set; }
        Action<EventHandler> AddRequerySuggested { get; set; }
        Action<EventHandler> RemoveRequerySuggested { get; set; }
    }

    public class RequerySuggestedController : IRequerySuggested
    {
        public Action RequerySuggestedAct { get; set; }
        public Action<EventHandler> AddRequerySuggested { get; set; }
        public Action<EventHandler> RemoveRequerySuggested { get; set; }
    }
}
