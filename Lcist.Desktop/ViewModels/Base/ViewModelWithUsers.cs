using System.Collections.ObjectModel;
using Lcist.Classes;
using Lcist.Classes.BaseClasses;
using Lcist.Desktop.Properties;

namespace Lcist.Desktop.ViewModels.Base
{
    public abstract class ViewModelWithUsers : ViewModel
    {
        #region Properties

        #region UserList

        private ObservableCollection<LcistUser> _userList;

        public ObservableCollection<LcistUser> UserList
        {
            get
            {
                if (_userList == null)
                    _userList = LoadUserList();

                return _userList;

            }
        }

        #endregion

        #region CurrentUser

        private LcistUser _currentUser;

        public LcistUser CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (!value.Equals(_currentUser))
                {
                    _currentUser = value;
                    RefreshUserData();
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        #region LoadUserList

        protected virtual ObservableCollection<LcistUser> LoadUserList()
        {
            ObservableCollection<LcistUser> result = new ObservableCollection<LcistUser>();

            foreach (LcistUser user in FirebirdDataProvider.GetLcistUsers(Settings.Default.LocalDbFile))
            {
                if (UserForViewModel(user))
                {
                    result.Add(user);

                    if (user.Id == 23) CurrentUser = user;
                }
            }

            return result;
        }

        #endregion

        #region UserForViewModel

        protected abstract bool UserForViewModel(LcistUser user);

        #endregion

        #region RefreshUserData

        protected abstract void RefreshUserData();

        #endregion

        #endregion
    }
}
