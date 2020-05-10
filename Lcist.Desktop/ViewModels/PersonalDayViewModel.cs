using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lcist.Classes.PersonalRhythms;
using Lcist.Desktop.ViewModels.Base;

namespace Lcist.Desktop.ViewModels
{
    public class PersonalDayViewModel : DataItemViewModel<PersonalDay>
    {
        #region Properties

        #region Date

        public DateTime Date
        {
            get => DataItem.Date;
            set => SetValue(value);
        }

        #endregion

        #region CanAdded

        public bool CanAdded
        {
            get => DataItem.CanAdded;
            set => SetValue(value);
        }


        #endregion

        #endregion

        public PersonalDayViewModel(PersonalDay dataItem) : base(dataItem)
        {
        }
    }
}
