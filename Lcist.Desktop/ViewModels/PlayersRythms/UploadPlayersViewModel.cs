using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using Lcist.Classes;
using Lcist.Classes.BaseClasses;
using Lcist.Classes.PlayersRhythms;
using Lcist.Desktop.ViewModels.Base;
using MySql.Data.MySqlClient;

namespace Lcist.Desktop.ViewModels.PlayersRythms
{
    /// <summary>
    ///     Класс для восстановления игроков
    /// </summary>
    public class UploadPlayersViewModel : ViewModelWithUsers
    {
        #region Properties

        #region Title

        /// <summary>
        ///     Заголовок окна
        /// </summary>
        public override string Title => "Восстановление данных игроков";

        #endregion

        #region SelectedPlayer

        private PlayerViewModel _selectedPlayer;
        /// <summary>
        ///     Выделенный игрок
        /// </summary>
        public PlayerViewModel SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                if (_selectedPlayer == null || !_selectedPlayer.Equals(value))
                {
                    _selectedPlayer = value;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PlayerMatches));
                    OnPropertyChanged(nameof(PlayerQueries));
                }
            }
        }

        #endregion

        #region UserPlayers

        /// <summary>
        ///     игроки, зарегистрированные пользователем
        /// </summary>
        public ObservableCollection<PlayerViewModel> UserPlayers => CurrentUser.UserPlayers;

        #endregion

        #region PlayerMatches

        /// <summary>
        ///     матчи, сохраненные для выделенного игрока
        /// </summary>
        public ObservableCollection<MatchViewModel> PlayerMatches => SelectedPlayer.Matches;

        #endregion

        #region PlayerQueries

        /// <summary>
        ///     запросы,созданные для выбранного игрока
        /// </summary>
        public ObservableCollection<QueryViewModel> PlayerQueries => SelectedPlayer.Queries;

        #endregion

        #endregion

        #region Methods

        #region UserForViewModel

        /// <inheritdoc />
        protected override bool UserForViewModel(LcistUser user) => (user.PlayersCount > 0);

        #endregion

        #region RefreshUserData

        /// <inheritdoc />
        protected override void RefreshUserData()
        {
            //CheckPlayers();
            OnPropertyChanged(nameof(UserPlayers));
            SelectedPlayer = UserPlayers.FirstOrDefault();
            OnPropertyChanged(nameof(PlayerMatches));
            OnPropertyChanged(nameof(PlayerQueries));
        }

        #endregion

        #region CheckPlayers

        private void CheckPlayers()
        {
            if (CurrentUser == null) return;
            using (MySqlConnection connection = MySqlDataProvider.GetConnection())
            {
                MySqlCommand checkPlayerCommand = new MySqlCommand(Resources.MySqlQueries.CheckPlayerById, connection);
                checkPlayerCommand.Parameters.Add("id", MySqlDbType.Int32);

                MySqlCommand checkMatchCommand = new MySqlCommand(Resources.MySqlQueries.CheckMatchByDate, connection);
                checkMatchCommand.Parameters.Add("idPlayer", MySqlDbType.Int32);
                checkMatchCommand.Parameters.Add("dateMatch", MySqlDbType.Date);

                MySqlCommand checkQueryCommand = new MySqlCommand(Resources.MySqlQueries.CheckQueryById, connection);
                checkQueryCommand.Parameters.Add("id", MySqlDbType.Int32);

                connection.Open();
                foreach (PlayerViewModel playerViewModel in UserPlayers)
                {
                    playerViewModel.CheckMySqlDb(checkPlayerCommand, checkMatchCommand, checkQueryCommand);
                }

                connection.Close();
            }
        }

        #endregion

        private void Upload()
        {
            using (MySqlConnection connection = MySqlDataProvider.GetConnection())
            {
                MySqlCommand insertPlayerCommand = Player.GetInsertCommand();
                insertPlayerCommand.Parameters["idUser"].Value = CurrentUser.Id;

                MySqlCommand insertMatchCommand = Match.GetInsertCommand();
                insertMatchCommand.Parameters["idUser"].Value = CurrentUser.Id;

                MySqlCommand insertQueryCommand = Query.GetInsertCommand();
                insertQueryCommand.Parameters["idUser"].Value = CurrentUser.Id;

                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    insertPlayerCommand.Connection = connection;
                    insertPlayerCommand.Transaction = transaction;

                    insertMatchCommand.Connection = connection;
                    insertMatchCommand.Transaction = transaction;

                    insertQueryCommand.Connection = connection;
                    insertQueryCommand.Transaction = transaction;

                    foreach (PlayerViewModel playerViewModel in UserPlayers)
                    {
                        playerViewModel.InsertIntoRemDb(insertPlayerCommand, insertMatchCommand, insertQueryCommand);
                    }

                    transaction.Commit();
                    connection.Close();

                    ClearCanAddProperties();
                }
                catch (MySqlException exception)
                {
                    transaction.Rollback();
                    connection.Close();

                    Message = exception.Message;
                    MessageForeground = Brushes.Red;
                }
            }

            Message = "ГОТОВО";
        }

        private void ClearCanAddProperties()
        {
            foreach (PlayerViewModel playerViewModel in UserPlayers)
            {
                playerViewModel.ClearCanAddProperties();
            }
        }

        private void SelectAll()
        {
            foreach (var playerViewModel in UserPlayers)
            {
                playerViewModel.CanAdd = true;
            }
        }

        private void SetNames()
        {
            using (MySqlConnection connection = MySqlDataProvider.GetConnection())
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.UpdateName, connection, transaction);
                command.Parameters.Add("name", MySqlDbType.VarChar);
                command.Parameters.Add("id", MySqlDbType.Int32);

                try
                {
                    foreach (PlayerViewModel model in UserPlayers.Where(x => x.CanAdd))
                    {
                        command.Parameters["name"].Value = model.Name;
                        command.Parameters["id"].Value = model.Id;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    connection.Close();

                    ClearCanAddProperties();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    connection.Close();
                }
            }
        }

        #endregion

        #region Commands

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

        #region SelectAllCommand

        private RelayCommand _selectAllCommand;

        public RelayCommand SelectAllCommand
        {
            get
            {
                if (_selectAllCommand == null)
                    _selectAllCommand = new RelayCommand(param => SelectAll());

                return _selectAllCommand;
            }
        }

        #endregion

        #region UpdateNamesCommand

        private RelayCommand _updateNamesCommand;

        public RelayCommand UpdateNamesCommand
        {
            get
            {
                if (_updateNamesCommand == null)
                    _updateNamesCommand = new RelayCommand(x => SetNames());

                return _updateNamesCommand;
            }
        }

        #endregion

        #endregion
    }
}
