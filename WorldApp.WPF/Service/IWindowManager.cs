namespace WorldApp.WPF.Service
{
    public interface IWindowManager
    {
        void CreateMainWindow();

        Task ShowDialogAsync<TViewModelType>() where TViewModelType : TViewModel;
        Task ShowNotificationAsync(string cITY_ADDED, string sUCCESS);
    }
}