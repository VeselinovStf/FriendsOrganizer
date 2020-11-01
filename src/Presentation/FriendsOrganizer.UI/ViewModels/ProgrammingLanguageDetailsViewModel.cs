using FriendsOrganizer.ProgrammingLanguages.Service.Abstraction;
using FriendsOrganizer.UI.ModelsWrappers;
using FriendsOrganizer.UI.UIServices;
using FriendsOrganizer.UI.ViewModels.Abstraction;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsOrganizer.UI.ViewModels
{
    public class ProgrammingLanguageDetailsViewModel : DetailViewModelBase
    {
        private readonly IProgrammingLanguagesService _programmingLanguagesService;

        public ProgrammingLanguageDetailsViewModel(
            IProgrammingLanguagesService programmingLanguagesService,
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService) 
            : base(eventAggregator, messageDialogService)
        {
            Title = "Programming Languages";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageModelWrapper>();
            this._programmingLanguagesService = programmingLanguagesService;
        }

        public ObservableCollection<ProgrammingLanguageModelWrapper> ProgrammingLanguages { get; }

        public override Task LoadAddableAsync()
        {
            return Task.Delay(0);
        }

        public override async Task LoadAsync(int id)
        {
            Id = id;

            foreach (var pr in ProgrammingLanguages)
            {
                pr.PropertyChanged -= Wrapper_PropertyChanged;
            }

            ProgrammingLanguages.Clear();

            var languages = await this._programmingLanguagesService
                .GetAllAsync();

            foreach (var pr in languages)
            {
                var wrapper = new ProgrammingLanguageModelWrapper(pr);

                wrapper.PropertyChanged += Wrapper_PropertyChanged;

                ProgrammingLanguages.Add(wrapper);

            }

        }

        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChange)
            {
                HasChange = this._programmingLanguagesService.HasChanges();
            }

            if (e.PropertyName == nameof(ProgrammingLanguageModelWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new System.NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChange && ProgrammingLanguages.All(p => !p.HasErrors);
        }

        protected override async void OnSaveExecute()
        {
            await this._programmingLanguagesService.SaveAsync();
            HasChange = this._programmingLanguagesService.HasChanges();
        }
    }
}
