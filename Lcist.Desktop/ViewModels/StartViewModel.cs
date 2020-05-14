using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using Lcist.Desktop.ViewModels.Base;
using Lcist.Desktop.ViewModels.PersonalRythms;
using Lcist.Resources;

namespace Lcist.Desktop.ViewModels
{
    public class StartViewModel : ViewModel
    {
        #region Properties

        #region CurrentViewModel

        public ViewModel CurrentViewModel { get; private set; }

        #endregion

        #region Title

        public override string Title => CurrentViewModel?.Title ?? Strings.StartTitle;

        #endregion

        #region CommandList

        public ReadOnlyCollection<CommandViewModel> MainMenu { get; private set; }

        #endregion

        #endregion

        #region Constructor

        public StartViewModel()
        {
            CreateMainMenu();
            SetCurrentViewModel(new UploadViewModel());
        }


        #endregion

        #region Methods

        #region SetCurrentViewModel

        public void SetCurrentViewModel(ViewModel viewModel)
        {
            if (viewModel != null)
                viewModel.PreviousViewModel = CurrentViewModel;
            CurrentViewModel = viewModel;

            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(Title));
        }

        #endregion

        #region CreateMainMenu

        private void CreateMainMenu()
        {
            List<CommandViewModel> commands = new List<CommandViewModel>
            {
                new CommandViewModel(Strings.Upload, UploadCommand),
                new CommandViewModel(Strings.RhythmTitle, PersonalRhythmsCommand),
                new CommandViewModel(Strings.Settings, SettingsCommand)
            };

            MainMenu = new ReadOnlyCollection<CommandViewModel>(commands);
        }


        #endregion

        #endregion

        #region Commands

        #region PersonalRhythms

        private RelayCommand _personalRhythmsCommand;
        /// <summary>
        ///     Команда "Расчет персональных биоритмов"
        /// </summary>
        public RelayCommand PersonalRhythmsCommand
        {
            get
            {
                if (_personalRhythmsCommand == null)
                    _personalRhythmsCommand = new RelayCommand(param => SetCurrentViewModel(new PersonalRhythmsVM()));

                return _personalRhythmsCommand;
            }
        }

        #endregion

        #region SettingsCommand;

        private RelayCommand _settingsCommand;
        /// <summary>
        ///     Команда "Настройки"
        /// </summary>
        public RelayCommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                    _settingsCommand = new RelayCommand(param => SetCurrentViewModel(new SettingsViewModel()));

                return _settingsCommand;
            }
        }

        #endregion

        #region UploadCommand

        private RelayCommand _uploadCommand;

        public RelayCommand UploadCommand
        {
            get
            {
                if (_uploadCommand == null)
                    _uploadCommand = new RelayCommand(param => SetCurrentViewModel(new UploadViewModel()));

                return _uploadCommand;
            }
        }

        #endregion

        #endregion
    }
}
