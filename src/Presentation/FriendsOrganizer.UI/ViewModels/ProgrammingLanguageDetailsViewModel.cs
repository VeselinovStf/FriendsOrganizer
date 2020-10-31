using FriendsOrganizer.UI.UIServices;
using FriendsOrganizer.UI.ViewModels.Abstraction;
using Prism.Events;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.ViewModels
{
    public class ProgrammingLanguageDetailsViewModel : DetailViewModelBase
    {
        public ProgrammingLanguageDetailsViewModel(
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService) 
            : base(eventAggregator, messageDialogService)
        {
        }

        public override Task LoadAddableAsync()
        {
            return Task.Delay(0);
        }

        public override Task LoadAsync(int id)
        {
            Id = id;
            return Task.Delay(0);
        }

        protected override void OnDeleteExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnSaveExecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
