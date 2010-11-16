namespace CompleteGraph

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Shapes
open System.Windows.Data

type CompleteGraph() as cg =
  inherit UserControl()

  let shape = Polyline()
  do
    let vb = Viewbox()
    cg.Content <- vb;
    vb.Child <- shape
    shape.Stroke <- SolidColorBrush Colors.Black
    shape.StrokeThickness <- 0.001

  let drawEdges vertices =
    shape.Points.Clear()
    let n = vertices
    let p i =
      let t = float(i % n) / float n * 2.0 * Math.PI
      Point(1.0 + sin t, 1.0 + cos t)
    for i=0 to n-1 do
      for j=i+1 to n do
        Seq.iter shape.Points.Add [p i; p j]

  member cg.Vertices
    with set v = drawEdges v

type CompleteGraphWithSlider() as cgws =
  inherit UserControl()
  do
    let grid = Grid()
    cgws.Content <- grid
    grid.RowDefinitions.Add(RowDefinition(Height=GridLength.Auto))
    grid.RowDefinitions.Add(RowDefinition())
    grid.ColumnDefinitions.Add(ColumnDefinition())

    let addToGrid uiElement row =
      grid.Children.Add uiElement
      uiElement.SetValue(Grid.RowProperty, row)
      uiElement.SetValue(Grid.ColumnProperty, 0)

    let slider = Slider()
    addToGrid slider 0
    slider.Minimum <- 3.
    slider.Maximum <- 48.

    let graph = CompleteGraph()
    addToGrid graph 1

    slider.ValueChanged
    |> Observable.filter(fun args ->
      let a = args.NewValue |> int
      let b = args.OldValue |> int
      a <> b
    )
    |> Observable.add(fun args ->
      let vertices = args.NewValue |> int
      graph.Vertices <- vertices
      grid.SetValue(ToolTipService.ToolTipProperty, sprintf "%d vertices" vertices)
    )

    slider.Value <- 24.

type App() as app =
  inherit Application()
  do
    app.Startup.Add(fun _ ->
      app.RootVisual <- CompleteGraphWithSlider()
    )