namespace UI
{
    public static class Constant
    {
        public static class FlightStatuses
        {
            public const int Waiting = 1;
            public const int Proccessing = 2;
            public const int Completed = 3;
        }

        public static class Windows
        {
            public const string MainWindow = nameof(MainWindow);
            public const string PendingFlights = nameof(PendingFlights);
            public const string ProcessingFlights = nameof(ProcessingFlights);
            public const string CompletedFlights = nameof(CompletedFlights);
            public const string Legs = nameof(Legs);
        }
    }
}
