using System;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using Lcist.Classes.BaseClasses;
using MySql.Data.MySqlClient;

namespace Lcist.Classes.PlayersRhythms
{
    /// <summary>
    ///     Класс "Запрос на расчет готовности игрока"
    /// </summary>
    public class Query : DataItem
    {
        #region Properties

        #region DateQuery

        /// <summary>
        ///     Дата создания запроса
        /// </summary>
        public DateTime DateQuery { get; set; }

        #endregion

        #region DateFor

        /// <summary>
        ///     дата рассчитываемого матча
        /// </summary>
        public DateTime DateFor { get; set; }

        #endregion

        #region Stage

        
        public short Stage { get; set; }

        #endregion

        #region Phisical

        /// <summary>
        ///     рассчитанное значение физического биоритма
        /// </summary>
        public int? Phisical { get; set; }

        #endregion

        #region Emotional

        /// <summary>
        ///     рассчитанное значение эмоционального биоритма
        /// </summary>  
        public int? Emotional { get; set; }

        #endregion

        #region Shift1

        /// <summary>
        ///     Сдвиг фазы для физического биоритма
        /// </summary>
        public short? Shift1 { get; set; }

        #endregion

        #region Shift2

        /// <summary>
        ///     Сдвиг по фазе для эмоционального биоритма
        /// </summary>
        public short? Shift2 { get; set; }

        #endregion

        #region Shift3

        /// <summary>
        ///     Сдвиг по фазе для интеллектуального биоритма
        /// </summary>
        public short? Shift3 { get; set; }

        #endregion

        #endregion

        public Query(DbDataReader reader) : base(reader)
        {
        }

        public Query(MySqlDataReader reader) : base(reader)
        {
        }

        public Query(FbDataReader reader) : base(reader)
        {
        }

        public override string GetDescription()
        {
            return DateFor.ToShortDateString();
        }

        #region Methods

        #region ReadProperties

        protected override void ReadItemProperties(DbDataReader reader)
        {
            DateFor = (DateTime) reader["dateFor"];
            Stage = (short) reader["stage"];
            Shift1 = (short?) (DBNull.Value.Equals(reader["shift1"]) ? null : reader["shift1"]);
            Shift2 = (short?) (DBNull.Value.Equals(reader["shift2"]) ? null : reader["shift2"]);
            Shift3 = (short?) (DBNull.Value.Equals(reader["shift3"]) ? null : reader["shift3"]);
        }

        protected override void ReadItemProperties(FbDataReader reader)
        {
            base.ReadItemProperties(reader);
            Phisical = (int?) (DBNull.Value.Equals(reader["phisical"]) ? null : (short?) reader["phisical"]);
            Emotional = (int?)(DBNull.Value.Equals(reader["emotional"]) ? null : (short?) reader["emotional"]);
            DateQuery = DateFor.AddDays(-1);
        }

        #endregion

        #endregion

        public static MySqlCommand GetInsertCommand()
        {
            /*
             * INSERT INTO queries (id, user, dateQuery, dateFor, stage, player, position, phisical, emotional, shift1, shift2, shift3)
             * VALUES (@id, @idUser, @dateQuery, @dateFor, 2, @idPlayer, 0, @phisical, @emotional, @shift1, @shift2, @shift3)
             */

            MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.InsertQuery);
            command.Parameters.Add("id", MySqlDbType.Int32);
            command.Parameters.Add("idUser", MySqlDbType.Int32);
            command.Parameters.Add("dateQuery", MySqlDbType.Date);
            command.Parameters.Add("dateFor", MySqlDbType.Date);
            command.Parameters.Add("idPlayer", MySqlDbType.Int32);
            command.Parameters.Add("phisical", MySqlDbType.Int32);
            command.Parameters.Add("emotional", MySqlDbType.Int32);
            command.Parameters.Add("shift1", MySqlDbType.Int16);
            command.Parameters.Add("shift2", MySqlDbType.Int16);
            command.Parameters.Add("shift3", MySqlDbType.Int16);

            return command;
        }

        public void InsertIntoRemDb(MySqlCommand insertQueryCommand, Player player)
        {
            insertQueryCommand.Parameters["id"].Value = Id;
            insertQueryCommand.Parameters["dateQuery"].Value = DateQuery;
            insertQueryCommand.Parameters["dateFor"].Value = DateFor;
            insertQueryCommand.Parameters["idPlayer"].Value = player.Id;
            insertQueryCommand.Parameters["phisical"].Value = Phisical;
            insertQueryCommand.Parameters["emotional"].Value = Emotional;
            insertQueryCommand.Parameters["shift1"].Value = Shift1 ?? 0;
            insertQueryCommand.Parameters["shift2"].Value = Shift2 ?? 0;
            insertQueryCommand.Parameters["shift3"].Value = Shift3 ?? 0;

            insertQueryCommand.ExecuteNonQuery();
        }
    }
}
