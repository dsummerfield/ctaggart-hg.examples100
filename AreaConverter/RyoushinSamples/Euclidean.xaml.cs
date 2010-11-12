using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RyoushinSamples
{
    public partial class Euclidean : UserControl
    {
        public Euclidean()
        {
            InitializeComponent();
        }

        private void LayoutRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PointCollection points = new PointCollection();
            foreach (Point p in poly.Points)
                points.Add(p);
            Point newPoint = e.GetPosition(poly);
            points.Add(newPoint);
            poly.Points = points;
            // placeholder
            Ellipse ph = new Ellipse
            {
                Fill = new SolidColorBrush(Colors.Black),
                Width = 4D,
                Height = 4D
            };
            TranslateTransform t = new TranslateTransform { X = newPoint.X - 2D, Y = newPoint.Y - 2D };
            ph.RenderTransform = t;
            cnv.Children.Add(ph);
            //
            TextBlock tb = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.Gray),
                FontSize = 8
            };
            tb.Text = newPoint.ToString();
            tb.RenderTransform = t;
            cnv.Children.Add(tb);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            poly.Points = new PointCollection();
            cnv.Children.Clear();
        }
    }
}
