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
using System.Windows.Input;

namespace FriendsOrganizer.UI.ViewModels
{
    public class ProgrammingLanguageDetailsViewModel : DetailViewModelBase, IProgrammingLanguageViewModel
    {
        private readonly IProgrammingLanguagesService _programmingLanguagesService;

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public ProgrammingLanguageDetailsViewModel(
            IProgrammingLanguagesService programmingLanguagesService,
            IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService) 
            : base(eventAggregator, messageDialogService)
        {
            Title = "Programming Languages";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageModelWrapper>();
            this._programmingLanguagesService = programmingLanguagesService;

            AddCommand = new DelegateCommand(OnAddProgramminLanguageExecute);
            RemoveCommand = new DelegateCommand(OnRemoveProgramminLanguageExecute, OnRemoveProgramminLanguageCanExecute);
        }

        private bool OnRemoveProgramminLanguageCanExecute()
        {
            return SelectedProgrammingLanguage != null;
        }

        private async void OnRemoveProgramminLanguageExecute()
        {
            var isReferenced = await this._programmingLanguagesService
                .IsReferencedAsync(SelectedProgrammingLanguage.Id);

            if (isReferenced)
            {
                await this._messageDialogService.ShowInfoDialogAsync("This language is referencet by at least one friend, can't be removed");

                return;
            }


            SelectedProgrammingLanguage.PropertyChanged -= Wrapper_PropertyChanged;
            this._programmingLanguagesService.Remove(SelectedProgrammingLanguage.Model);
            ProgrammingLanguages.Remove(SelectedProgrammingLanguage);
            SelectedProgrammingLanguage = null;
            HasChange = this._programmingLanguagesService.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private async void OnAddProgramminLanguageExecute()
        {
            var wrapper = new ProgrammingLanguageModelWrapper(new Data.Models.ProgrammingLanguage());
            wrapper.PropertyChanged  += Wrapper_PropertyChanged;
            await this._programmingLanguagesService.AddAsync(wrapper.Model);
            ProgrammingLanguages.Add(wrapper);

            wrapper.Name = "";
        }

        public ObservableCollection<ProgrammingLanguageModelWrapper> ProgrammingLanguages { get; }

        private ProgrammingLanguageModelWrapper _selectedProgrammingLanguage;

        public ProgrammingLanguageModelWrapper SelectedProgrammingLanguage
        {
            get { return _selectedProgrammingLanguage; }
            set 
            { 
                _selectedProgrammingLanguage = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }


        public override async Task LoadAddableAsync()
        {
            await LoadAsync(Id);
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
            RaiseCollectionSaveEvent();
        }
    }
}
