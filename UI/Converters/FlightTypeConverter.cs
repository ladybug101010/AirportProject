using Common.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace UI.Converters
{
    public class FlightTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url;
            var flight = (Flight)value;
            if (flight == null)
            {
                url = string.Empty;
            }
            else
            {
                if (flight.Type == Types.Landing)
                {
                    url = "https://img-s-msn-com.akamaized.net/tenant/amp/entityid/AA12jHsO.img?h=630&w=1200&m=6&q=60&o=t&l=f&f=jpg";
                }
                else
                {
                    url = "https://thumbs.dreamstime.com/b/takeoff-plane-airport-36265238.jpg";
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
