using System;
using System.Data.Common;
using Lcist.Classes.BaseClasses;
using MySql.Data.MySqlClient;

namespace Lcist.Classes.PlayersRhythms
{
    /// <summary>
    ///     
    /// </summary>
    public class Player : DataItem
    {
        #region Properties

        #region Name

        /// <summary>
        ///     ФИО игрока
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Birthday

        /// <summary>
        ///     Дата рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        #endregion

        #region State

        /// <summary>
        ///     Идентификатор гражданства
        /// </summary>
        public int State { get; set; }

        #endregion

        #region Cost

        /// <summary>
        ///     рассчитанная стоимость
        /// </summary>
        public decimal Cost { get; set; }

        #endregion

        #region Stage

        /// <summary>
        ///     стадия расчета
        /// </summary>
        public int Stage => (Cost > 0 ? 2 : 0);

        #endregion

        #endregion

        public Player(DbDataReader reader) : base(reader) { }

        protected override void ReadItemProperties(DbDataReader reader)
        {
            Name = (string) reader["name"];
            Birthday = (DateTime) reader["birthday"];
            State = (int) reader["state"];
            Cost = (decimal) reader["cost"];
        }

        public override string GetDescription() => Name;

        public static MySqlCommand GetInsertCommand()
        {
            /*
             * INSERT INTO players (Id, Name, Birthday, user, state, cost, stage)
             * VALUES (@id, @name, @birthday, @idUser, @state, @cost, 2)
             */
            MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.InsertPlayer);
            command.Parameters.Add("id", MySqlDbType.Int32);
            command.Parameters.Add("name", MySqlDbType.VarChar);
            command.Parameters.Add("birthday", MySqlDbType.Date);
            command.Parameters.Add("idUser", MySqlDbType.Int32);
            command.Parameters.Add("state", MySqlDbType.Int32);
            command.Parameters.Add("cost", MySqlDbType.Decimal);

            return command;
        }

        public void InsertIntoRemDb(MySqlCommand command)
        {
            command.Parameters["id"].Value = Id;
            command.Parameters["name"].Value = Name;
            command.Parameters["birthday"].Value = Birthday;
            command.Parameters["state"].Value = State;
            command.Parameters["cost"].Value = Cost;

            command.ExecuteNonQuery();
        }
    }
}
