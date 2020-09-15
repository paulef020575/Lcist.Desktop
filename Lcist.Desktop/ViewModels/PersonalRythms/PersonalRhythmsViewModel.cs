using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
    public class PersonalRhythmsViewModel : ViewModel
    {
        #region Properties

        #region Title

        /// <summary>
        ///     Заголовок VM
        /// </summary>
        public override string Title => $"{Strings.RhythmTitle}";

        #endregion

        #region QueriesQueue

        private ObservableCollection<PersonalResultViewModel> _queriesQueue;

        public ObservableCollection<PersonalResultViewModel> QueriesQueue
        {
            get
            {
                if (_queriesQueue == null)
                    FillQueue();

                return _queriesQueue;
            }
        }

        #endregion

        #endregion

        public PersonalRhythmsViewModel()
        {
        }

        #region FillQueue

        private async void FillQueue()
        {
            Message = "Загружаем очередь заказов...";

            _queriesQueue = new ObservableCollection<PersonalResultViewModel>();
            IEnumerable<PersonalResultViewModel> loadResult = await LoadQueue();
            foreach (PersonalResultViewModel item in loadResult)
                _queriesQueue.Add(item);

            Message = "Загрузка завершена";
        }

        private async Task<IEnumerable<PersonalResultViewModel>> LoadQueue()
        {
            List<PersonalResultViewModel> result = new List<PersonalResultViewModel>();

            foreach (PersonalResult dataItem in MySqlDataProvider.GetRhythmQueue())
            {
                result.Add(new PersonalResultViewModel(dataItem));
            }

            return result;
        }

        #endregion
    }
}
