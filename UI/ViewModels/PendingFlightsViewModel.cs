using Common.Models;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using UI.Services;

namespace UI.ViewModels
{
    public class PendingFlightsViewModel : ObservableObject
    {
        private readonly IFlightsApiService _flightsApiService;
        public ObservableCollection<Flight> PendingFlights { get; set; }

        private readonly System.Timers.Timer _timer;
        private readonly ILogger<PendingFlightsViewModel> _logger;

        public PendingFlightsViewModel(IFlightsApiService flightsApiService, ILogger<PendingFlightsViewModel> logger)
        {
            _flightsApiService = flightsApiService;
            _logger = logger;

            _timer = new System.Timers.Timer();
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
                var res = await _flightsApiService.GetFlightsByStatus(1);
                PendingFlights = new ObservableCollection<Flight>(res.Items);
                RaisePropertyChanged("PendingFlights");
            }
            catch (Exception ex)
            {
                _logger.LogError("Get Pending flights - Error on server");
            }
        }
    }
}
