using WorldApp.WPF.Database;
using WorldApp.WPF.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace WorldApp.WPF.ViewModels
{
    internal class CustomDialogViewModel : ObservableObject
    {
        public CustomDialogViewModel(TViewModel viewModel)
        {
            if (viewModel is null)
                throw new ArgumentNullException(string.Format(Resources.Resources.CANT_BE_NULL, nameof(viewModel)));
            
            Title = viewModel.Name;
            CurrentViewModel = viewModel;
            AddCommand = new RelayCommand(AddAsync);
        }

        public ICommand AddCommand { get; }

        public TViewModel CurrentViewModel { get; set; }

        public string Title { get; set; }

        private async void AddAsync() => await CurrentViewModel.AddAsync();
    }
}