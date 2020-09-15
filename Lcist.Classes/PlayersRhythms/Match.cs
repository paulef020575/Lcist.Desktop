using System;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using Lcist.Classes.BaseClasses;
using MySql.Data.MySqlClient;

namespace Lcist.Classes.PlayersRhythms
{
    /// <summary>
    ///     Класс "Матч"
    /// </summary>
    public class Match : DataItem
    {
        #region Properties

        public override bool HasIdentifier => false;

        #region DateMatch

        /// <summary>
        ///     Дата матча
        /// </summary>
        public DateTime DateMatch { get; set; }

        #endregion

        #region Goal1

        /// <summary>
        ///     время первого гола
        /// </summary>
        public short Goal1 { get; set; }

        #endregion

        #region Goal2

        /// <summary>
        ///     время 2го гола
        /// </summary>
        public short Goal2 { get; set; }


        #endregion

        #region Goal3

        public short Goal3 { get; set; }

        #endregion

        #region Yellow

        public short Yellow { get; set; }

        #endregion

        #region YellowReason

        public short YellowReason { get; set; }

        #endregion

        #region Red

        
        public short Red { get; set; }

        #endregion

        #region RedReason

        public short RedReason { get; set; }

        #endregion

        #region PersonalMark

        public short PersonalMark { get; set; }

        #endregion

        #endregion

        public Match(DbDataReader reader) : base(reader)
        {
        }

        public Match(MySqlDataReader reader) : base(reader)
        {
        }

        public Match(FbDataReader reader) : base(reader)
        {
        }

        protected override void ReadItemProperties(FbDataReader reader)
        {
            DateMatch = (DateTime)reader["dateMatch"];
            Goal1 = (short)reader["goal1"];
            Goal2 = (short)reader["goal2"];
            Goal3 = (short)reader["goal3"];
            Yellow = (short)reader["yellow"];
            YellowReason = (short)reader["reason1"];
            Red = (short)reader["red"];
            RedReason = (short)reader["reason2"];
            PersonalMark = (short)reader["personal"];
        }

        public override string GetDescription() => DateMatch.ToShortDateString();

        /// <summary>
        ///     возвращает команду на вставку записи в удаленную БД
        /// </summary>
        /// <returns>команда</returns>
        public static MySqlCommand GetInsertCommand()
        {
            /*
             * INSERT INTO matches (user, player, matchDate, goal1, goal2, goal3, yellow1, red, reason1, reason3, personal, stage)
             * VALUES (@idUser, @idPlayer, @dateMatch, @goal1, @goal2, @goal3, @yellow, @red, @reason1, @reason3, @personal, 2)
             */
            MySqlCommand command = new MySqlCommand(Resources.MySqlQueries.InsertMatch);
            command.Parameters.Add("idUser", MySqlDbType.Int32);
            command.Parameters.Add("idPlayer", MySqlDbType.Int32);
            command.Parameters.Add("dateMatch", MySqlDbType.Date);
            command.Parameters.Add("goal1", MySqlDbType.Int16);
            command.Parameters.Add("goal2", MySqlDbType.Int16);
            command.Parameters.Add("goal3", MySqlDbType.Int16);
            command.Parameters.Add("yellow", MySqlDbType.Int16);
            command.Parameters.Add("red", MySqlDbType.Int16);
            command.Parameters.Add("reason1", MySqlDbType.Int16);
            command.Parameters.Add("reason3", MySqlDbType.Int16);
            command.Parameters.Add("personal", MySqlDbType.Int16);

            return command;
        }

        public void InsertIntoRemDb(MySqlCommand insertMatchCommand, Player player)
        {
            insertMatchCommand.Parameters["idPlayer"].Value = player.Id;
            insertMatchCommand.Parameters["dateMatch"].Value = DateMatch;
            insertMatchCommand.Parameters["goal1"].Value = Goal1;
            insertMatchCommand.Parameters["goal2"].Value = Goal2;
            insertMatchCommand.Parameters["goal3"].Value = Goal3;
            insertMatchCommand.Parameters["yellow"].Value = Yellow;
            insertMatchCommand.Parameters["red"].Value = Red;
            insertMatchCommand.Parameters["reason1"].Value = YellowReason;
            insertMatchCommand.Parameters["red"].Value = RedReason;
            insertMatchCommand.Parameters["personal"].Value = PersonalMark;

            insertMatchCommand.ExecuteNonQuery();


        }
    }
}
