using System;
using System.Collections.Generic;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using Lcist.Classes.BaseClasses;
using MySql.Data.MySqlClient;

namespace Lcist.Classes.PersonalRhythms
{
    public class PersonalResult : DataItem
    {
        #region Properties

        #region DateFrom

        /// <summary>
        ///     Дата отсчета
        /// </summary>
        public DateTime DateFrom { get; set; }

        #endregion

        #region Length

        /// <summary>
        ///     Продолжительность периода
        /// </summary>
        public short Length { get; set; }

        #endregion

        #region Stage

        /// <summary>
        ///     Стадия исполнения
        /// </summary>
        public QueryStage Stage { get; set; }

        #endregion

        #region Date1

        /// <summary>
        ///     Начало физического биоритма
        /// </summary>
        public DateTime Date1 { get; set; }

        #endregion

        #region Date2

        /// <summary>
        ///     Начало эмоционального биоритма
        /// </summary>
        public DateTime Date2 { get; set; }

        #endregion

        #region Date3

        /// <summary>
        ///     Начало интеллектуального биоритма
        /// </summary>
        public DateTime Date3 { get; set; }

        #endregion

        #region User

        /// <summary>
        ///     Пользователь, сгенерировавший запрос
        /// </summary>
        public LcistUser User { get; private set; }

        #endregion

        #endregion

        public PersonalResult(FbDataReader reader) : base(reader) { }

        public PersonalResult(MySqlDataReader reader) : base(reader) { }

        public PersonalResult(MySqlDataReader reader, Dictionary<int, LcistUser> userList) : this(reader)
        {
            User = userList[(int) reader["user"]];
        }

        protected override void ReadItemProperties(MySqlDataReader reader)
        {
            DateFrom = (DateTime)reader["dateFrom"];
            Length = (short)(sbyte) reader["length"];
            Stage = (QueryStage)(sbyte)reader["stage1"];
            Date1 = (DateTime) reader["date1"];
            Date2 = (DateTime)reader["date2"];
            Date3 = (DateTime)reader["date3"];
        }

        protected override void ReadItemProperties(FbDataReader reader)
        {
            DateFrom = (DateTime)reader["dateFrom"];
            Length = (short)reader["days"];
            Stage = (QueryStage)reader["stage"];
            Date1 = (DateTime)reader["date1"];
            Date2 = (DateTime)reader["date2"];
            Date3 = (DateTime)reader["date3"];
        }

        public override string GetDescription()
        {
            return $"{DateFrom.ToShortDateString()} ({Length})";
        }

        public void Upload(MySqlCommand command)
        {
            command.Parameters["id"].Value = Id;
            command.Parameters["dateFrom"].Value = DateFrom;
            command.Parameters["length"].Value = Length;
            command.Parameters["date1"].Value = Date1;
            command.Parameters["stage"].Value = Stage;
            command.Parameters["date2"].Value = Date2;
            command.Parameters["date3"].Value = Date3;

            command.ExecuteNonQuery();
        }
    }
}
