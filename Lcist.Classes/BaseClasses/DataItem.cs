using System;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;


namespace Lcist.Classes.BaseClasses
{
    /// <summary>
    ///     объект "Строка БД"
    /// </summary>
    public abstract class DataItem
    {
        #region Properties

        #region Id

        /// <summary>
        ///     Идентификатор записи
        /// </summary>
        public int Id { get; set; }

        #endregion

        #region HasIdentifier

        /// <summary>
        ///     Определяет наличие у класс идентификатора
        /// </summary>
        public virtual bool HasIdentifier => true;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        ///     запрещает создание объекта не из БД
        /// </summary>
        private DataItem() { }
        
        /// <summary>
        ///     восстанавливает объект из абстрактной БД
        /// </summary>
        /// <param name="reader">считанная из таблицы строка</param>
        public DataItem(DbDataReader reader)
        {
            Id = (HasIdentifier ? (int) reader["Id"] : -1);
            ReadItemProperties(reader);
        }


        /// <summary>
        ///     восстанавливает объект из удаленной БД 
        /// </summary>
        /// <param name="reader">считанная из таблицы строка</param>
        public DataItem(MySqlDataReader reader)
        {
            Id = (HasIdentifier ? (int)reader["Id"] : -1);
            ReadItemProperties(reader);
        }


        /// <summary>
        ///     восстанавливает объект из локальной БД
        /// </summary>
        /// <param name="reader">считанная из таблицы строка</param>
        public DataItem(FbDataReader reader)
        {
            Id = (HasIdentifier ? (int)reader["Id"] : -1);
            ReadItemProperties(reader);
        }
        
        #endregion

        #region Methods

        #region ReadItemProperties

        /// <summary>
        ///     восстанавливает свойства на основе объекта из абстрактной БД
        /// </summary>
        /// <param name="reader">считанная из таблицы строка</param>
        protected virtual void ReadItemProperties(DbDataReader reader)
        {
            throw new NotImplementedException("ReadItemProperties with DbDataReader parameter");
        }

        /// <summary>
        ///     восстанавливает свойства на основе объекта из БД MySql (удаленная БД)
        /// </summary>
        /// <param name="reader">считанная из таблицы строка</param>
        protected virtual void ReadItemProperties(MySqlDataReader reader)
        {
            ReadItemProperties((DbDataReader)reader);
        }

        /// <summary>
        ///     восстанавливает свойства на основе объекта из БД Firebird (локальная БД)
        /// </summary>
        /// <param name="reader">считанная из таблицы строка</param>
        protected virtual void ReadItemProperties(FbDataReader reader)
        {
            ReadItemProperties((DbDataReader)reader);
        }


        #endregion

        #region GetDescription

        /// <summary>
        ///     возвращает описание объекта
        /// </summary>
        /// <returns>строковая идентификация объекта</returns>
        public abstract string GetDescription();

        #endregion

        #region Equals

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            DataItem item = obj as DataItem;
            if (item == null) return false;

            if (HasIdentifier) return Id == item.Id;

            throw new NotImplementedException("Equals method");
        }

        #endregion

        #endregion
    }
}
