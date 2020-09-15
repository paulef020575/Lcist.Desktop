using System;
using System.Collections.ObjectModel;
using System.Linq;
using Lcist.Classes.BaseClasses;
using Lcist.Classes.PlayersRhythms;
using Lcist.Desktop.Properties;
using Lcist.Desktop.ViewModels.Base;
using MySql.Data.MySqlClient;

namespace Lcist.Desktop.ViewModels.PlayersRythms
{
    /// <summary>
    ///     Класс для отображения игроков
    /// </summary>
    public class PlayerViewModel : DataItemViewModel<Player>
    {

        #region Properties

        #region Name

        /// <summary>
        ///     Имя игрока
        /// </summary>
        public string Name => DataItem.Name;

        #endregion

        #region Birthday

        public DateTime Birthday => DataItem.Birthday;

        #endregion

        #region Id

        /// <summary>
        ///     Идентификатор игрока
        /// </summary>
        public int Id => DataItem.Id;

        #endregion

        #region Matches

        private ObservableCollection<MatchViewModel> _matches;

        public ObservableCollection<MatchViewModel> Matches
        {
            get
            {
                if (_matches == null)
                    FillMatches();

                return _matches;
            }
        }

        #endregion

        #region Queries

        private ObservableCollection<QueryViewModel> _queries;

        public ObservableCollection<QueryViewModel> Queries
        {
            get
            {
                if (_queries == null)
                    FillQueries();

                return _queries;
            }
        }

        #endregion

        #region CanAdd


        private bool _canAdd;
        /// <summary>
        ///     Признак "Не найден в интернете"
        /// </summary>
        public bool CanAdd
        {
            get { return _canAdd; }
            set
            {
                if (_canAdd != value)
                {
                    _canAdd = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region CanAddMatches

        public bool CanAddMatches => Matches.Any(x => x.CanAdd);

        #endregion

        #region CanAddQueries

        public bool CanAddQueries => Queries.Any(x => x.CanAdd);


        #endregion

        #endregion

        public PlayerViewModel(Player dataItem) : base(dataItem)
        {
        }

        #region Methods

        #region FillMatches

        private void FillMatches()
        {
            _matches = new ObservableCollection<MatchViewModel>();

            foreach (Match match in FirebirdDataProvider.GetPlayerMatches(Settings.Default.LocalDbFile, DataItem))
                _matches.Add(new MatchViewModel(match));
        }

        #endregion

        #region FillQueries

        private void FillQueries()
        {
            _queries = new ObservableCollection<QueryViewModel>();

            foreach (Query query in FirebirdDataProvider.GetPlayerQueries(Settings.Default.LocalDbFile, DataItem))
                _queries.Add(new QueryViewModel(query));
        }

        #endregion

        #endregion

        public void CheckMySqlDb(MySqlCommand checkPlayerCommand, MySqlCommand checkMatchCommand, MySqlCommand checkQueryCommand)
        {
            checkPlayerCommand.Parameters["id"].Value = DataItem.Id;
            long result = (long)checkPlayerCommand.ExecuteScalar();
            CanAdd = (result == 0);


            checkMatchCommand.Parameters["idPlayer"].Value = DataItem.Id;
            foreach (MatchViewModel matchViewModel in Matches)
                matchViewModel.CheckMySqlDb(checkMatchCommand);
            OnPropertyChanged(nameof(CanAddMatches));

            foreach (QueryViewModel queryViewModel in Queries)
                queryViewModel.CheckMySqlDb(checkQueryCommand);
            OnPropertyChanged(nameof(CanAddQueries));

        }

        public void InsertIntoRemDb(MySqlCommand insertPlayerCommand, MySqlCommand insertMatchCommand, MySqlCommand insertQueryCommand)
        {
            if (CanAdd)
                DataItem.InsertIntoRemDb(insertPlayerCommand);

            if (CanAddMatches)
                foreach (MatchViewModel matchViewModel in Matches)
                    matchViewModel.InsertIntoRemDb(insertMatchCommand, DataItem);

            if (CanAddQueries)
                foreach (QueryViewModel queryViewModel in Queries)
                    queryViewModel.InsertIntoRemDb(insertQueryCommand, DataItem);
        }

        public void ClearCanAddProperties()
        {
            CanAdd = false;
            Matches.ToList().ForEach(x => x.CanAdd = false);
            Queries.ToList().ForEach(x => x.CanAdd = false);

            OnPropertyChanged(nameof(CanAddMatches));
            OnPropertyChanged(nameof(CanAddQueries));
        }
    }
}
