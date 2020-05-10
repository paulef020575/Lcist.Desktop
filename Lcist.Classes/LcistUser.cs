using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using Lcist.Classes.BaseClasses;

namespace Lcist.Classes
{
    /// <summary>
    ///     Класс "Пользователь системы"
    /// </summary>
    public class LcistUser : MySqlDataItem
    {
        #region Properties

        /// <summary>
        ///     Имя пользователя в системе
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Настроящее имя пользователя
        /// </summary>
        public string RealName { get; private set; }

        #endregion

        public LcistUser(DbDataReader reader) : base(reader)
        {
        }

        public LcistUser(FbDataReader reader)
        {
            Id = (int)reader["ID"];
            Name = (string)reader["Name"];
            RealName = (string) reader["RealName"];
        }

        /// <summary>
        ///     Заполняет свойства объекта
        /// </summary>
        /// <param name="reader">Считанные из БД данные</param>
        protected override void ReadItemProperties(DbDataReader reader)
        {
            Name = (string) reader["name"];
            RealName = (string) reader["realName"];
        }

        public override string GetDescription() => $"{Name} ({RealName})";

        public override string ToString() => GetDescription();
    }
}
