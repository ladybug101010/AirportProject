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
    public class ProcessingFlightsViewModel: ObservableObject
    {
        private readonly IFlightsApiService _flightsApiService;
        public ObservableCollection<Flight> ProcessingFlights { get; set; }

        private readonly Timer _timer;
        private ILogger<ProcessingFlightsViewModel> _logger;

        public ProcessingFlightsViewModel(IFlightsApiService flightsApiService, ILogger<ProcessingFlightsViewModel> logger)
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
                var res = await _flightsApiService.GetFlightsByStatus(2);
                ProcessingFlights = new ObservableCollection<Flight>(res.Items);
                RaisePropertyChanged("ProcessingFlights");
            }
            catch (Exception ex)
            {
                _logger.LogError("Get processing flights - Error on server");
            }
        }
    }
}
