using System;
using System.Collections.Generic;
using System.Linq;
using Lcist.Classes;
using Lcist.Classes.BaseClasses;
using Lcist.Classes.PersonalRhythms;
using Lcist.Desktop.Properties;
using Lcist.Desktop.ViewModels.Base;
using Lcist.Resources;

namespace Lcist.Desktop.ViewModels.PersonalRythms
{
    /// <summary>
    ///     VM для расчета персональных биоритмов
    /// </summary>
    public class PersonalRhythmsVM : ViewModel
    {
        #region Properties

        #region Title

        /// <summary>
        ///     Заголовок VM
        /// </summary>
        public override string Title => $"{Strings.RhythmTitle} - {User?.GetDescription() ?? Strings.EmptyUser}";

        #endregion

        #region User

        private LcistUser _user;
        /// <summary>
        ///     Рассчитываемый пользователь
        /// </summary>
        public LcistUser User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    SetUser(value);
                }
            }
        }

        #endregion

        #region UserList

        public IEnumerable<LcistUser> UserList { get; }

        #endregion

        #region FullDayList

        /// <summary>
        ///     Список дней, заполненных пользователем
        /// </summary>
        public List<PersonalDay> FullDayList { get; private set; }

        #endregion

        #endregion

        #region Constructor

        public PersonalRhythmsVM()
        {
            UserList = MySqlDataProvider.GetLcistUsers();

            FullDayList = new List<PersonalDay>();
        }

        #endregion

        #region Methods

        #region SetUser

        /// <summary>
        ///     устанавливает обрабатываемого пользователя и обновляет связанные с ним данные
        /// </summary>
        /// <param name="user"></param>
        private void SetUser(LcistUser user)
        {
            _user = user;
            OnPropertyChanged(nameof(User));
            OnPropertyChanged(nameof(Title));


            FullDayList = (List<PersonalDay>)MySqlDataProvider.GetPersonalDays(user);
            MarkStartPeriod(Settings.Default.PeriodLength, Settings.Default.PeriodCount);
            OnPropertyChanged(nameof(FullDayList));
        }

        #endregion

        #region MarkStartPeriod

        /// <summary>
        ///     Помечает для расчета первые дни в рамках настроек
        /// </summary>
        private void MarkStartPeriod(int periodLength, int periodCount)
        {
            Dictionary<DateTime, int> dateFromLength = new Dictionary<DateTime, int>();
            foreach (PersonalDay personalDay in FullDayList)
            {
                DateTime dateFrom = personalDay.Date, dateTo = dateFrom.AddMonths(periodLength);
                List<PersonalDay> dayList = FullDayList.Where(x => x.Date >= dateFrom && x.Date <= dateTo).ToList();
                if (dayList.Count >= periodCount)
                {
                    dayList.ForEach(x => x.IsCalculating = true);
                    MarkCanAddedDays();
                    return;
                }
            }
        }

        #endregion

        #region MarkCanAddedDays

        /// <summary>
        ///     помечает доступные для добавления дни
        /// </summary>
        private void MarkCanAddedDays()
        {
            FullDayList.ForEach(x => x.CanAdded = false);

            PersonalDay firstMarkedDay = FullDayList.First(x => x.IsCalculating == true);
            PersonalDay canAddDay = FullDayList.LastOrDefault(x => x.Date < firstMarkedDay.Date);
            if (canAddDay != null) canAddDay.CanAdded = true;

            PersonalDay lastMarkedDay = FullDayList.Last(x => x.IsCalculating == true);
            canAddDay = FullDayList.FirstOrDefault(x => x.Date > lastMarkedDay.Date);
            if (canAddDay != null) canAddDay.CanAdded = true;
        }

        #endregion

        #endregion

    }
}
