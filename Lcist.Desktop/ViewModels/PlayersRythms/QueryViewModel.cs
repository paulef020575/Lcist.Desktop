using System;
using Lcist.Classes.PlayersRhythms;
using Lcist.Desktop.ViewModels.Base;
using MySql.Data.MySqlClient;

namespace Lcist.Desktop.ViewModels.PlayersRythms
{
    /// <summary>
    ///     Класс для отображения запросов на расчет
    /// </summary>
    public class QueryViewModel : DataItemViewModel<Query>
    {
        public QueryViewModel(Query dataItem) : base(dataItem)
        {
        }

        #region Properties

        #region DateFor

        /// <summary>
        ///     Дата для расчета
        /// </summary>
        public DateTime DateFor => DataItem.DateFor;

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

        public void CheckMySqlDb(MySqlCommand checkQueryCommand)
        {
            try
            {
                checkQueryCommand.Parameters["id"].Value = DataItem.Id;
                long result = (long)checkQueryCommand.ExecuteScalar();

                CanAdd = (result == 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void InsertIntoRemDb(MySqlCommand insertQueryCommand, Player player)
        {
            if (CanAdd)
                DataItem.InsertIntoRemDb(insertQueryCommand, player);
        }
    }
}
