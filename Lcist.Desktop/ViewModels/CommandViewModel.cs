using System.Windows.Input;
using Lcist.Desktop.ViewModels.Base;

namespace Lcist.Desktop.ViewModels
{
    public class CommandViewModel : ViewModel
    {
        #region Properties

        #region Title

        /// <summary>
        ///     Описание команды на форме
        /// </summary>
        public override string Title { get; }

        #endregion

        #region Command

        /// <summary>
        ///     Команда
        /// </summary>
        public ICommand Command { get; }

        #endregion

        #endregion

        #region Constructors

        private CommandViewModel() { }

        public CommandViewModel(string title, ICommand command)
        {
            Title = title;
            Command = command;
        }

        #endregion
    }
}
