namespace Example001Silverlight

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media

module UI =
  let createForm() =
    let form = Canvas()
  
    form.Width <- 300.
    form.Height <- 200.
    form.Background <- SolidColorBrush Colors.LightGray

    // create the controls
    let title = TextBlock(Text="Compute IQ")
    let n1label = TextBlock(Text="Mental age:")
    let firsttextbox = TextBox(Width=30.)
    let n2label = TextBlock(Text="Chronological age:")
    let secondtextbox = TextBox(Width=30.)
    let n3label = TextBlock(Text="IQ:")
    let anslabel = TextBlock()
    let addbutton = Button(Content="Compute")

    // add the controls to the form
    let addToForm uiElement (top:double) (left:double) =
      form.Children.Add uiElement
      uiElement.SetValue(Canvas.TopProperty, top)
      uiElement.SetValue(Canvas.LeftProperty, left)

    addToForm title 5. 5.
    addToForm n1label 40. 5.
    addToForm firsttextbox 40. 120.
    addToForm n2label 70. 5.
    addToForm secondtextbox 70. 120.
    addToForm n3label 100. 5.
    addToForm anslabel 100. 120.
    addToForm addbutton 130. 120.

    //when the compute button is clicked
    //display the iq value in the anslabel
    addbutton.Click.Add(fun _ ->
      try
        let manum = Convert.ToDouble firsttextbox.Text
        let canum = Convert.ToDouble secondtextbox.Text
        let iq = Convert.ToDouble((manum/canum)*100.00)
        anslabel.Text <- Convert.ToString(iq)
      with :? FormatException ->
        anslabel.Text <- String.Empty
    )

    form

type App() as app =
  inherit Application()
  do
    app.Startup.Add(fun _ ->
      app.RootVisual <- UI.createForm()
    )