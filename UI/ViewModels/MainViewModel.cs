using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Navigation;

namespace UI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand PendingFlightsBtn { get; set; }
        public RelayCommand ProcessingFlightsBtn { get; set; }
        public RelayCommand CompletedFlightsBtn { get; set; }

        public RelayCommand LegsBtn { get; set; }

        public Services.NavigationService NavigationService { get; set; }

        public MainViewModel(Services.NavigationService navigationService)
        {
            NavigationService = navigationService;

            PendingFlightsBtn = new RelayCommand(OpenPendingFlightsView);
            ProcessingFlightsBtn = new RelayCommand(OpenProcessingFlightsView);
            CompletedFlightsBtn = new RelayCommand(OpenCompletedFlightsView);
            LegsBtn = new RelayCommand(OpenLegsView);
        }

        private async void OpenLegsView()
        {
            await NavigationService.ShowAsync(Constant.Windows.Legs);
        }

        private async void OpenPendingFlightsView()
        {
            await NavigationService.ShowAsync(Constant.Windows.PendingFlights);
        }

        private async void OpenProcessingFlightsView()
        {
            await NavigationService.ShowAsync(Constant.Windows.ProcessingFlights);
        }

        private async void OpenCompletedFlightsView()
        {
            await NavigationService.ShowAsync(Constant.Windows.CompletedFlights);
        }
    }
}
