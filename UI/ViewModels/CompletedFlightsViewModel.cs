using Common.Models;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using UI.Services;

namespace UI.ViewModels
{
    public class CompletedFlightsViewModel:ObservableObject
    {
        private readonly IFlightsApiService _flightsApiService;
        public ObservableCollection<Flight> CompletedFlights { get; set; }

        private readonly Timer _timer;
        private readonly ILogger<CompletedFlightsViewModel> _logger;

        public CompletedFlightsViewModel(IFlightsApiService flightsApiService, ILogger<CompletedFlightsViewModel> logger)
        {
            _flightsApiService = flightsApiService;
            _logger = logger;

            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(async (s, e) => { await OnGetFlights(s, e); });
            _timer.Interval = 5000;
            _timer.Enabled = true;
            GetFlights();
        }

        private async Task OnGetFlights(object sender, ElapsedEventArgs e)
        {
            await GetFlights();
        }

        private async Task GetFlights()
        {
            try
            {
                var res = await _flightsApiService.GetFlightsByStatus(3);
                CompletedFlights = new ObservableCollection<Flight>(res.Items);
                RaisePropertyChanged("CompletedFlights");
            }
            catch(Exception ex)
            {
                _logger.LogError("Get Compeleted flights - Error on server");
            }
        }
    }
}
