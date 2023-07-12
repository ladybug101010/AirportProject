using Common.Models;
using GalaSoft.MvvmLight.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converters
{
    public class FlightBrandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url=string.Empty;

            var flight = (Flight)value;

            if (flight == null)
            {
                return url;
            }
            else
            {
                if (flight.Brand.Equals("ElAl"))
                {
                    url = "https://serviced.co.il/wp-content/uploads/2021/07/%D7%90%D7%9C-%D7%A2%D7%9C-%D7%A9%D7%99%D7%A8%D7%95%D7%AA-%D7%9C%D7%A7%D7%95%D7%97%D7%95%D7%AA.jpg";
                }
                else if(flight.Brand.Equals("Israir"))
                {
                    url = "https://serviced.co.il/wp-content/uploads/2021/06/%D7%99%D7%A9%D7%A8%D7%90%D7%99%D7%99%D7%A8-%D7%9C%D7%95%D7%92%D7%95.jpg";
                }
            }

            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
