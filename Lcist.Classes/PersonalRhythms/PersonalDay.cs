using System;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using Lcist.Classes.BaseClasses;
using MySql.Data.MySqlClient;

namespace Lcist.Classes.PersonalRhythms
{
    /// <summary>
    ///     Оценка дня пользователем
    /// </summary>
    public class PersonalDay : DataItem
    {
        public override bool HasIdentifier => false;

        /// <summary>
        ///     Дата оценки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Оценка 1
        /// </summary>
        public int Mark1 { get; set; }

        /// <summary>
        ///     Оценка 2
        /// </summary>
        public int Mark2 { get; set; }

        /// <summary>
        ///     Включен в расчет
        /// </summary>
        public bool IsCalculating { get; set; }

        /// <summary>
        ///     Признак "Может быть добавлен к расчету"
        /// </summary>
        public bool CanAdded { get; set; }

        public PersonalDay(MySqlDataReader reader) : base(reader)
        {
            IsCalculating = false;
            CanAdded = false;
        }

        public PersonalDay(FbDataReader reader) : base(reader)
        {
            IsCalculating = false;
            CanAdded = false;
        }

        protected override void ReadItemProperties(MySqlDataReader reader)
        {
            Date = (DateTime) reader["dateMark"];
            Mark1 = (int) reader["mark1"];
            Mark2 = (int) reader["mark2"];
        }

        protected override void ReadItemProperties(FbDataReader reader)
        {
            Date = (DateTime)reader["DateDay"];
            Mark1 = (int)reader["Mark1"];
            Mark2 = (int)reader["Mark2"];
        }

        public override string GetDescription() => Date.ToShortDateString();

        public override string ToString() => GetDescription();

        public bool CheckUploading(MySqlCommand command)
        {
            command.Parameters["date"].Value = Date;

            object id = command.ExecuteScalar();

            return (id == null || DBNull.Value.Equals(id));
        }
    }
}
