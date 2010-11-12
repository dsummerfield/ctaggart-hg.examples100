using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace RyoushinSamples.Converters
{
public class AreaConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is System.Windows.Media.PointCollection)
        {
            double area = Pacem.Science.Geometry.EuclideanGeometry.Area2D(
                (System.Windows.Media.PointCollection)value
                );
            if (targetType == typeof(double))
                return area;
            if (targetType == typeof(string))
            {
                if (parameter is string)
                    return area.ToString((string)parameter, culture);
                else
                    return area.ToString(culture);
            }
        }
        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}
