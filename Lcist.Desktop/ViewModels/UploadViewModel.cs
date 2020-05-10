using System;
using System.Collections.ObjectModel;
using System.Linq;
using Lcist.Classes;
using Lcist.Classes.BaseClasses;
using Lcist.Classes.PersonalRhythms;
using Lcist.Desktop.Properties;
using Lcist.Desktop.ViewModels.Base;
using MySql.Data.MySqlClient;

namespace Lcist.Desktop.ViewModels
{
    /// <summary>
    ///     Модуль "Восстановление данных"
    /// </summary>
    public class UploadViewModel : ViewModel
    {
        #region Properties

        #region Title

        /// <summary>
        ///     Заголовок окна
        /// </summary>
        public override string Title => "Восстановление данных";

        #endregion

        #region UserList

        private ObservableCollection<LcistUser> _userList;
        /// <summary>
        ///     Список пользователей из локальной БД
        /// </summary>
        public ObservableCollection<LcistUser> UserList
        {
            get
            {
                if (_userList == null)
                    _userList = LoadUserList();

                return _userList;
            }
        }

        #endregion

        #region CurrentUser

        private LcistUser _currentUser;

        public LcistUser CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (!value.Equals(_currentUser))
                {
                    _currentUser = value;
                    RefreshDays();
                    OnPropertyChanged();
                }
            }

    }


        #endregion

        #region UserDays

        public ObservableCollection<PersonalDayViewModel> UserDays { get; }

        #endregion

        #endregion

        public UploadViewModel()
        {
            UserDays = new ObservableCollection<PersonalDayViewModel>();
        }

        #region Methods

        #region LoadUserList

        private ObservableCollection<LcistUser> LoadUserList()
        {
            ObservableCollection<LcistUser> result = new ObservableCollection<LcistUser>();

            foreach (LcistUser user in FirebirdDataProvider.GetLcistUsers(Settings.Default.LocalDbFile))
            {
                result.Add(user);
            }

            return result;
        }

        #endregion

        #region RefreshDays

        private void RefreshDays()
        {
            UserDays.Clear();
            foreach (PersonalDay personalDay in FirebirdDataProvider.GetUserDays(Settings.Default.LocalDbFile, CurrentUser))
                UserDays.Add(new PersonalDayViewModel(personalDay));
        }

        #endregion


        #region CheckUploading

        private void CheckUploading()
        {
            using (MySqlConnection connection = MySqlDataProvider.GetConnection())
            {
                MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.CheckId, connection);
                command.Parameters.AddWithValue("user", CurrentUser.Id);
                command.Parameters.Add("date", MySqlDbType.Date);

                connection.Open();
                foreach (PersonalDayViewModel day in UserDays)
                {
                    command.Parameters["date"].Value = day.Date;
                    object commandResult = command.ExecuteScalar();
                    day.CanAdded = (commandResult == null || DBNull.Value.Equals(commandResult));
                }
                connection.Close();
            }
            
        }

        #endregion

        #region Upload

        private void Upload()
        {
            using (MySqlConnection connection = MySqlDataProvider.GetConnection())
            {
                MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.InsertDay, connection);
                command.Parameters.AddWithValue("user", CurrentUser.Id);

                command.Parameters.Add("date", MySqlDbType.Date);
                command.Parameters.Add("mark1", MySqlDbType.Int32);
                command.Parameters.Add("mark2", MySqlDbType.Int32);

                connection.Open();

                foreach (PersonalDayViewModel personalDay in UserDays)
                {
                    if (!personalDay.CanAdded) continue;

                    command.Parameters["date"].Value = personalDay.Date;
                    command.Parameters["mark1"].Value = personalDay.DataItem.Mark1;
                    command.Parameters["mark2"].Value = personalDay.DataItem.Mark2;

                    command.ExecuteNonQuery();

                    personalDay.CanAdded = false;
                }

                connection.Close();
            }
        }

        #endregion

        #endregion

        #region Commands

        #region CheckUploadingCommand

        private RelayCommand _checkUploadingCommand;

        public RelayCommand CheckUploadingCommand
        {
            get
            {
                if (_checkUploadingCommand == null)
                    _checkUploadingCommand = new RelayCommand(param => CheckUploading());

                return _checkUploadingCommand;
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
                    _uploadCommand = new RelayCommand(param => Upload());

                return _uploadCommand;
            }
        }

        #endregion

        #endregion
    }
}
