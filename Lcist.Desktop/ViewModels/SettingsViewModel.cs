using System;
using System.Runtime.CompilerServices;
using Lcist.Desktop.Properties;
using Lcist.Desktop.ViewModels.Base;
using Lcist.Resources;
using Microsoft.Win32;

namespace Lcist.Desktop.ViewModels
{
    /// <summary>
    ///     VM для редактирования настроек
    /// </summary>
    public class SettingsViewModel : ViewModel
    {
        #region Properties

        #region Title

        /// <summary>
        ///     Заголовок окна
        /// </summary>
        public override string Title => Strings.Settings;

        #endregion

        #region PeriodLength

        private int _periodLength;
        /// <summary>
        ///      Максимальная длительность периода (месяцы)
        /// </summary>
        public int PeriodLength
        {
            get { return _periodLength; }
            set
            {
                if (_periodLength != value)
                {
                    _periodLength = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region PeriodCount

        private int _periodCount;
        /// <summary>
        ///     количество оцениваемых дней
        /// </summary>
        public int PeriodCount
        {
            get { return _periodCount; }
            set
            {
                if (_periodCount != value)
                {
                    _periodCount = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region LocalDbFile

        private string _localDbFile;
        /// <summary>
        ///     файл локальной БД
        /// </summary>
        public string LocalDbFile
        {
            get { return _localDbFile; }
            set
            {
                if (!string.Equals(_localDbFile, value, StringComparison.InvariantCultureIgnoreCase))
                {
                    _localDbFile = value;
                    OnPropertyChanged();
                }
            }
        
        }

        #endregion

        #endregion

        #region Methods

        #region ReadSettings

        /// <summary>
        ///     Считывает настройки системы
        /// </summary>
        private void ReadSettings()
        {
            PeriodLength = Settings.Default.PeriodLength;
            PeriodCount = Settings.Default.PeriodCount;
            LocalDbFile = Settings.Default.LocalDbFile;

            ClearModified();
        }

        #endregion

        #region SaveSettings

        /// <summary>
        ///     Сохраняет настройки системы
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.PeriodLength = _periodLength;
            Settings.Default.PeriodCount = _periodCount;
            Settings.Default.LocalDbFile = _localDbFile;
            Settings.Default.Save();

            ClearModified();
        }

        #endregion

        #region GetLocalDbFile


        private void GetLocalDbFileFromDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                CheckFileExists = true,
                DefaultExt = "gdb",
                FileName = LocalDbFile,
                Filter = "GDB files|*.gdb|FDB files|*.fdb|All files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LocalDbFile = openFileDialog.FileName;
            }
        }

        #endregion

        #endregion

        #region Constructor

        public SettingsViewModel()
        {
            ReadSettings();
        }

        #endregion

        #region Commands

        #region ReloadSettings

        private RelayCommand _reloadCommand;
        /// <summary>
        ///     команда "Перечитать настройки"
        /// </summary>
        public RelayCommand ReloadCommand
        {
            get
            {
                if (_reloadCommand == null)
                    _reloadCommand = new RelayCommand(param => ReadSettings(), param => IsModified);

                return _reloadCommand;
            }
        }

        #endregion

        #region MyRegion

        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                    _saveCommand = new RelayCommand(param => SaveSettings(), param => IsModified);

                return _saveCommand;
            }
        }

        #endregion

        #region GetLocalDbFileCommand

        private RelayCommand _getLocalDbFileCommand;

        public RelayCommand GetLocalDbFileCommand
        {
            get
            {
                if (_getLocalDbFileCommand == null)
                    _getLocalDbFileCommand = new RelayCommand(param => GetLocalDbFileFromDialog());

                return _getLocalDbFileCommand;
            }
        }

        #endregion

        #endregion
    }
}
