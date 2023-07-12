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
    public class LegsViewModel: ObservableObject
    {
        private readonly ILegsApiService _legsApiService;
        public ObservableCollection<Leg> Legs { get; set; }

        private readonly System.Timers.Timer _timer;
        private readonly ILogger<LegsViewModel> _logger;

        public LegsViewModel(ILegsApiService legsApiService, ILogger<LegsViewModel> logger)
        {
            _legsApiService = legsApiService;
            _logger = logger;

            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(async (s, e) => { await OnGetLegs(s, e); });
            _timer.Interval = 3000;
            _timer.Enabled = true;

            GetLegs();
        }

        private async Task OnGetLegs(object sender, ElapsedEventArgs e)
        {
            await GetLegs();
        }

        private async Task GetLegs()
        {
            try
            {
                var res = await _legsApiService.GetLegs();
                Legs = new ObservableCollection<Leg>(res);
                RaisePropertyChanged("Legs");
            }
            catch (Exception ex)
            {
                _logger.LogError("Get legs - Error on server");
            }
        }
    }
}
