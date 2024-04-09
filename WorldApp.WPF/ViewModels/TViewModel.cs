using CommunityToolkit.Mvvm.ComponentModel;

namespace WorldApp.WPF
{
    public abstract class TViewModel : ObservableObject
    {
        public abstract string Name { get; }

        public abstract Task AddAsync();
    }
}