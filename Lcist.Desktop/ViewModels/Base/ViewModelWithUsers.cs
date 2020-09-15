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

        private ObservableCollection<UserViewModel> _userList;

        public ObservableCollection<UserViewModel> UserList
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

        private UserViewModel _currentUser;

        public UserViewModel CurrentUser
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

        protected virtual ObservableCollection<UserViewModel> LoadUserList()
        {
            ObservableCollection<UserViewModel> result = new ObservableCollection<UserViewModel>();

            foreach (LcistUser user in FirebirdDataProvider.GetLcistUsers(Settings.Default.LocalDbFile))
            {
                if (UserForViewModel(user))
                {
                    UserViewModel viewModel = new UserViewModel(user);
                    result.Add(viewModel);

                    if (user.Id == 23) CurrentUser = viewModel;
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
