using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lcist.Desktop.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        #region Properties

        #region Title

        public abstract string Title { get; }

        #endregion

        #region IsModified

        protected bool IsModified { get; set; }

        #endregion


        #region PreviousViewModel

        public virtual ViewModel PreviousViewModel { get; set; }

        #endregion

        #endregion

        #region INotifyPropertyChanged implementation

        private PropertyChangedEventHandler _onPropertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _onPropertyChanged += value; }
            remove { _onPropertyChanged -= value; }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (_onPropertyChanged != null)
                _onPropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName != nameof(IsModified))
            {
                MarkAsModified();
                OnPropertyChanged(nameof(IsModified));
            }

        }

        #endregion

        #region ChangeViewModel

        private EventHandler<ViewModel> changeViewModelEventHandler;

        public event EventHandler<ViewModel> ChangeViewModelEvent
        {
            add { changeViewModelEventHandler += value; }
            remove { changeViewModelEventHandler -= value; }
        }

        #endregion

        #region MarkAsModified

        protected void MarkAsModified()
        {
            if (!IsModified)
            {
                IsModified = true;
                OnPropertyChanged(nameof(IsModified));
            }
        }

        #endregion

        #region ClearModified

        protected void ClearModified()
        {
            IsModified = false;
            OnPropertyChanged(nameof(IsModified));
        }


        #endregion

    }
}
