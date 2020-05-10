using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lcist.Classes.BaseClasses
{
    /// <summary>
    ///     Базовый класс для данных из MySql
    /// </summary>
    public abstract class MySqlDataItem : DataItem
    {
        /// <summary>
        ///     Идентификатор строки
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        ///     Запрещаем создание без данных
        /// </summary>
        protected MySqlDataItem() { }

        /// <summary>
        ///     Создает объект на основе прочитанной строки
        /// </summary>
        /// <param name="reader">считанные из БД данные</param>
        public MySqlDataItem(DbDataReader reader)
        {
            Id = (int) reader["id"];
            ReadItemProperties(reader);
        }

        public override bool Equals(object obj)
        {
            return (obj is MySqlDataItem && ((MySqlDataItem) obj).Id == Id);
        }
    }
}
