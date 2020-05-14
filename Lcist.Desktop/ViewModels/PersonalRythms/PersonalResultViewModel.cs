using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Lcist.Classes.PersonalRhythms;
using Lcist.Desktop.ViewModels.Base;
using MySql.Data.MySqlClient;

namespace Lcist.Desktop.ViewModels.PersonalRythms
{
    public class PersonalResultViewModel : DataItemViewModel<PersonalResult>
    {
        public PersonalResultViewModel(PersonalResult dataItem) : base(dataItem)
        {
        }

        #region Properties

        #region Id 

        /// <summary>
        ///     идентификатор записи
        /// </summary>
        public int Id => DataItem.Id;

        #endregion

        #region DateFrom

        /// <summary>
        ///     Дата отсчета
        /// </summary>
        public DateTime DateFrom => DataItem.DateFrom;

        #endregion

        #region Length

        /// <summary>
        ///      Продолжительность периода
        /// </summary>
        public short Length => DataItem.Length;

        #endregion

        #region CanAdded

        private bool _canAdded;
        /// <summary>
        ///     Признак "Добавить в удаленную БД"
        /// </summary>
        public bool CanAdded
        {
            get { return _canAdded; }
            set
            {
                if (_canAdded != value)
                {
                    _canAdded = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #endregion

        public void Upload(MySqlCommand command)
        {
            if (CanAdded) DataItem.Upload(command);
        }
    }
}
