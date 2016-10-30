using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calendar
{
    public class MainMenuModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<ICommand> _commands = new ObservableCollection<ICommand>();

        public MainMenuModel()
        {
            for (int i = 1; i < 26; i++)
            {
                try
                {
                    DayBase dayclass = System.Activator.CreateInstance(Type.GetType(string.Format("{0}{1}", "Calendar.Day", i))) as DayBase;
                    if (dayclass != null)
                    {
                        _commands.Add(new CommandHandler(() => { InvokeFirstAnwer(dayclass); }, string.Format("Get Day{0} Answer 1", i)));
                        _commands.Add(new CommandHandler(() => { InvokeSecondAnwer(dayclass);  }, string.Format("Get Day{0} Answer 2", i)));
                    }
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            
        }

        private void InvokeFirstAnwer(DayBase day)
        {
            try
            {
                MessageBox.Show(day.GetFirstAnwer(day.GetQuestion1()).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InvokeSecondAnwer(DayBase day)
        {
            try
            {
                MessageBox.Show(day.GetSecondAnwer(day.GetQuestion2()).ToString());
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<ICommand> Commands
        {
            get
            {
                return _commands;
            }
        }

    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        public CommandHandler(Action action,  string caption)
        {
            _action = action;
            Caption = caption;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

        public string Caption { get; set; }
    }
}
