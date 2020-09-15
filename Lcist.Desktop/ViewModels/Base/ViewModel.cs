using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

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

        #region Message

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MessageVisibility));
                }
            }
        }

        #endregion

        #region MessageForeground

        private Brush _messageForeground = SystemColors.InfoTextBrush;
        /// <summary>
        ///     цвет текста сообщения
        /// </summary>
        public Brush MessageForeground
        {
            get { return _messageForeground; }
            set
            {
                if (!_messageForeground.Equals(value))
                {
                    _messageForeground = value;
                    OnPropertyChanged();

                }
            }
        }
        #endregion

        #region MessageBackground
         
        private Brush _messageBackground = SystemColors.InfoBrush;
        /// <summary>
        ///     цвет сообщения
        /// </summary>
        public Brush MessageBackground
        {
            get { return _messageBackground; }
            set
            {
                if (_messageBackground.Equals(value))
                {
                    _messageBackground = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region MessageVisibility

        public Visibility MessageVisibility => (Message.Length > 0 ? Visibility.Visible : Visibility.Collapsed);

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
