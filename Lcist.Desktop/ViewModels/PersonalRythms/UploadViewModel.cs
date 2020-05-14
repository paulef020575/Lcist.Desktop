using System;
using System.Collections.ObjectModel;
using System.Data;
using Lcist.Classes;
using Lcist.Classes.BaseClasses;
using Lcist.Classes.PersonalRhythms;
using Lcist.Desktop.Properties;
using Lcist.Desktop.ViewModels.Base;
using MySql.Data.MySqlClient;

namespace Lcist.Desktop.ViewModels.PersonalRythms
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
                    RefreshResults();
                    OnPropertyChanged();
                }
            }

    }


        #endregion

        #region UserDays

        public ObservableCollection<PersonalDayViewModel> UserDays { get; }

        #endregion

        #region UserResults

        /// <summary>
        ///     Коллекция рассчитанных биоритмов
        /// </summary>
        public ObservableCollection<PersonalResultViewModel> UserResults { get; }

        #endregion

        #endregion

        public UploadViewModel()
        {
            UserDays = new ObservableCollection<PersonalDayViewModel>();
            UserResults = new ObservableCollection<PersonalResultViewModel>();
        }

        #region Methods

        #region LoadUserList

        private ObservableCollection<LcistUser> LoadUserList()
        {
            ObservableCollection<LcistUser> result = new ObservableCollection<LcistUser>();

            foreach (LcistUser user in FirebirdDataProvider.GetLcistUsers(Settings.Default.LocalDbFile))
            {
                result.Add(user);
                if (user.Id == 23) CurrentUser = user;
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

        #region RefreshResults

        private void RefreshResults()
        {
            UserResults.Clear();
            foreach (PersonalResult personalResult in FirebirdDataProvider.GetUserResults(Settings.Default.LocalDbFile, CurrentUser))
                UserResults.Add(new PersonalResultViewModel(personalResult));
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

                MySqlCommand command1 = new MySqlCommand(Resources.MySqlQueries.CheckPersonalResultId, connection);
                command1.Parameters.Add("Id", MySqlDbType.Int32);

                foreach (PersonalResultViewModel result in UserResults)
                {
                    command1.Parameters["Id"].Value = result.Id;
                    object commandResult = command1.ExecuteScalar();
                    result.CanAdded = (commandResult == null || DBNull.Value.Equals(commandResult) ||
                                       (long) commandResult == 0);
                }

                connection.Close();
            }

        }

        #endregion

        #region Upload

        private void Upload()
        {
            UploadDays();
            UploadResults();
        }

        private void UploadResults()
        {
            using (MySqlConnection connection = MySqlDataProvider.GetConnection())
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.InsertPersonalResult, connection, transaction);
                command.Parameters.Add("id", MySqlDbType.Int32);
                command.Parameters.AddWithValue("user", CurrentUser.Id);
                command.Parameters.Add("dateFrom", MySqlDbType.DateTime);
                command.Parameters.Add("length", MySqlDbType.Int16);
                command.Parameters.Add("date1", MySqlDbType.DateTime);
                command.Parameters.Add("stage", MySqlDbType.Int16);
                command.Parameters.Add("date2", MySqlDbType.DateTime);
                command.Parameters.Add("date3", MySqlDbType.DateTime);

                foreach (PersonalResultViewModel viewModel in UserResults)
                    viewModel.Upload(command);

                transaction.Commit();
                connection.Close();
            }
        }

        private void UploadDays()
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
