using Lcist.Classes.BaseClasses;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace Lcist.Desktop.ViewModels.Base
{
    public class DataItemViewModel<TDataItem> : ViewModel
        where TDataItem : DataItem
    {
        #region Properties

        #region DataItem

        public TDataItem DataItem { get; set; }

        #endregion

        #region Title

        public override string Title => DataItem.GetDescription();

        #endregion

        #endregion

        #region Methods

        #region SetValue

        public void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            PropertyInfo propertyInfo = typeof(TDataItem).GetProperty(propertyName);
            T currentValue = (T)propertyInfo.GetValue(DataItem);
            
            propertyInfo.SetValue(DataItem, value);

            OnPropertyChanged(propertyName);
            MarkAsModified();
        }

        #endregion

        #endregion

        #region Constructor

        public DataItemViewModel(TDataItem dataItem)
        {
            DataItem = dataItem;
        }

        #endregion
    }
}
