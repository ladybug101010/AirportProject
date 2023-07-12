using Microsoft.Extensions.DependencyInjection;

namespace UI.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();

        public PendingFlightsViewModel PendingFlightsViewModel => App.ServiceProvider.GetRequiredService<PendingFlightsViewModel>();

        public ProcessingFlightsViewModel ProcessingFlightsViewModel => App.ServiceProvider.GetRequiredService<ProcessingFlightsViewModel>();

        public CompletedFlightsViewModel CompletedFlightsViewModel => App.ServiceProvider.GetRequiredService<CompletedFlightsViewModel>();

        public LegsViewModel LegsViewModel => App.ServiceProvider.GetRequiredService<LegsViewModel>();
    }
}
