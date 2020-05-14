using Lcist.Classes;
using Lcist.Desktop.ViewModels.Base;

namespace Lcist.Desktop.ViewModels.PlayersRythms
{
    public class UploadPlayersViewModel : ViewModelWithUsers 
    {
        public override string Title { get; }
        protected override bool UserForViewModel(LcistUser user)
        {
            throw new System.NotImplementedException();
        }

        protected override void RefreshUserData()
        {
            throw new System.NotImplementedException();
        }
    }
}
