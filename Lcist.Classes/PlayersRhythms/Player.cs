using System;
using System.Data.Common;
using Lcist.Classes.BaseClasses;

namespace Lcist.Classes.PlayersRhythms
{
    /// <summary>
    ///     
    /// </summary>
    public class Player : MySqlDataItem
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

        protected override void ReadItemProperties(DbDataReader reader)
        {
            Name = (string) reader["name"];
            Birthday = (DateTime) reader["birthday"];
            State = (int) reader["state"];
            Cost = (decimal) reader["cost"];
        }

        public override string GetDescription() => Name;
    }
}
