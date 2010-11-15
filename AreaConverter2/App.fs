namespace AreaConverter2

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Shapes
open System.Windows.Data

open MediaExt // defines SolidColorBrush.FromArgb

type public AreaConverter() =

  /// calculates the area of a polygon
  let area2D (polygon:PointCollection) = 
    let n = polygon.Count
    let divideBy2 f = f * 0.5
    let step i = polygon.[i].X * polygon.[(i+1)%n].Y - polygon.[i].Y * polygon.[(i+1)%n].X
    {0..n-1}
    |> Seq.fold(fun acc i -> acc + step i ) 0.0
    |> divideBy2
  
  interface IValueConverter with

    member x.Convert(value, targetType, parameter, culture) =
      match value with
      | :? PointCollection as polygon ->
        let area = area2D polygon
        if targetType = typeof<double> then
          area |> box
        else if targetType = typeof<string> then
          match parameter with
          | :? string as p -> area.ToString(p, culture) |> box
          | _ -> area.ToString culture |> box
        else
          raise (NotImplementedException())
      | _ -> raise (NotImplementedException())

    member x.ConvertBack(value, targetType, parameter, culture) =
      raise (NotImplementedException())

type Euclidean() as this =
  inherit UserControl()
  do
    let layoutRoot = Grid()
    this.Content <- layoutRoot
    layoutRoot.Background <- SolidColorBrush.FromArgb "#efefef"
    layoutRoot.SetValue(ToolTipService.ToolTipProperty, "click to add point")

    let border = Border()
    layoutRoot.Children.Add border
    border.CornerRadius <- CornerRadius 3.
    border.Padding <- Thickness 4.
    border.Height <- 32.
    border.VerticalAlignment <- VerticalAlignment.Top
    border.Background <- SolidColorBrush.FromArgb "#FFC0C0C0" // Silver

    let stackPanel = StackPanel()
    border.Child <- stackPanel
    stackPanel.Orientation <- Orientation.Horizontal
    stackPanel.Children.Add(TextBlock(Text="area (units):", VerticalAlignment=VerticalAlignment.Center))
    let txt = TextBlock()
    stackPanel.Children.Add txt
    txt.FontWeight <- FontWeights.Bold
    txt.Foreground <- SolidColorBrush Colors.Black
    txt.VerticalAlignment <- VerticalAlignment.Center
    txt.Margin <- Thickness(5., 0., 5., 0.)
    let btnClear = Button(Content="clear shape")
    stackPanel.Children.Add btnClear
    
    let cnv2 = Canvas(HorizontalAlignment=HorizontalAlignment.Center, VerticalAlignment=VerticalAlignment.Center)
    layoutRoot.Children.Add cnv2

    let poly = Polygon()
    cnv2.Children.Add poly
    poly.Fill <- SolidColorBrush Colors.Red
    poly.Stroke <- SolidColorBrush Colors.Black
    poly.StrokeThickness <- 1.

    // start off with a square
    let points = PointCollection()
    points.Add(Point(0.,0.))
    points.Add(Point(100.,0.))
    points.Add(Point(100.,100.))
    points.Add(Point(0.,100.))
    poly.Points <- points
    
    let cnv = Canvas(HorizontalAlignment=HorizontalAlignment.Center, VerticalAlignment=VerticalAlignment.Center)
    layoutRoot.Children.Add cnv

    let binding = Binding()
    binding.Mode <- BindingMode.OneWay
    binding.Source <- poly
    binding.Path <- PropertyPath "Points"
    binding.Converter <- AreaConverter()
    txt.SetBinding(TextBlock.TextProperty, binding) |> ignore

    layoutRoot.MouseLeftButtonUp.Add(fun args ->
      let points = new PointCollection()
      poly.Points |> Seq.iter points.Add
      let newPoint = args.GetPosition poly
      points.Add newPoint
      poly.Points <- points

      let placeholder = Ellipse()
      placeholder.Fill <- SolidColorBrush Colors.Black
      placeholder.Width <- 4.
      placeholder.Height <- 4.
      let t = TranslateTransform()
      t.X <- newPoint.X - 2.
      t.Y <- newPoint.Y - 2.
      placeholder.RenderTransform <- t
      cnv.Children.Add placeholder

      let tb = TextBlock()
      tb.Foreground <- SolidColorBrush Colors.Gray
      tb.FontSize <- 8.
      tb.Text <- newPoint.ToString()
      tb.RenderTransform <- t
      cnv.Children.Add tb
    )

    btnClear.Click.Add(fun _ ->
      poly.Points <- PointCollection()
      cnv.Children.Clear()
    )
    ()

type App() as app =
  inherit Application()
  do
    app.Startup.Add(fun _ ->
      app.RootVisual <- Euclidean()
    )