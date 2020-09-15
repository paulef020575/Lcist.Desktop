using System.Collections.ObjectModel;
using Lcist.Classes;
using Lcist.Classes.BaseClasses;
using Lcist.Classes.PlayersRhythms;
using Lcist.Desktop.Properties;
using Lcist.Desktop.ViewModels.Base;
using Lcist.Desktop.ViewModels.PlayersRythms;

namespace Lcist.Desktop.ViewModels
{
    /// <summary>
    ///     Данные пользователя
    /// </summary>
    public class UserViewModel : DataItemViewModel<LcistUser>
    {
        #region Properties

        #region Id 

        public int Id => DataItem.Id;

        #endregion

        #region Name

        /// <summary>
        ///     Логин пользователя
        /// </summary>
        public string Name => DataItem.Name;

        #endregion

        #region PlayersCount

        public long PlayersCount => DataItem.PlayersCount;

        #endregion

        #region DataItems

        #region Players

        private ObservableCollection<PlayerViewModel> _userPlayers;
        /// <summary>
        ///     Игроки, созданные пользователем
        /// </summary>
        public ObservableCollection<PlayerViewModel> UserPlayers
        {
            get
            {
                if (_userPlayers == null || _userPlayers.Count == 0)
                    ReloadPlayers();

                return _userPlayers;
            }
        }

        #endregion

        #endregion
        #endregion

        public UserViewModel(LcistUser dataItem) : base(dataItem)
        {
        }

        #region Methods


        #region ReloadPlayers
        
        /// <summary>
        ///     Заполняет список игроков
        /// </summary>
        public void ReloadPlayers()
        {
            if (_userPlayers == null)
                _userPlayers = new ObservableCollection<PlayerViewModel>();
            else
                _userPlayers.Clear();

            foreach (Player player in FirebirdDataProvider.GetUserPlayers(Settings.Default.LocalDbFile, DataItem))
            {
                _userPlayers.Add(new PlayerViewModel(player));
            }
        }

        #endregion

        #endregion
    }
}
