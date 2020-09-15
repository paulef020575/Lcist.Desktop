using System;
using System.Data;
using Lcist.Classes.PlayersRhythms;
using Lcist.Desktop.ViewModels.Base;
using MySql.Data.MySqlClient;

namespace Lcist.Desktop.ViewModels.PlayersRythms
{
    public class MatchViewModel : DataItemViewModel<Match>
    {
        #region Properties

        #region DateMatch

        public DateTime DateMatch => DataItem.DateMatch;

        #endregion

        #region CanAdd

        private bool _canAdd;

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

        #endregion

        public MatchViewModel(Match dataItem) : base(dataItem)
        {
        }

        public void CheckMySqlDb(MySqlCommand checkMatchCommand)
        {
            checkMatchCommand.Parameters["dateMatch"].Value = DateMatch;
            long result = (long)checkMatchCommand.ExecuteScalar();

            CanAdd = (result == 0);
        }

        public void InsertIntoRemDb(MySqlCommand insertMatchCommand, Player player)
        {
            if (CanAdd)
                DataItem.InsertIntoRemDb(insertMatchCommand, player);
        }
    }
}
