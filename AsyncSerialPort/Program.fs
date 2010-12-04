module Program

open System.Windows
open System.Windows.Controls
open System.IO.Ports
open AsyncSerialPort

type Form() as this =
  inherit Window()

  do
    let sp = StackPanel()
    this.Content <- sp
    let tbRequest = TextBox();
    tbRequest.Text <- "*IDN?"
    let tbResponse = TextBlock()
    let btn = Button(Content="Query Device")

    sp.Children.Add tbRequest |> ignore
    sp.Children.Add tbResponse |> ignore
    sp.Children.Add btn |> ignore

    let sp = new SerialPort(PortName="COM1", BaudRate=9600, DataBits=8, Parity=Parity.None, StopBits=StopBits.One);

    // synchronous, blocks UI
//    btn.Click.Add(fun _ ->
//      sp.WriteLine tbRequest.Text
//      let rsp = sp.ReadLine();
//      tbResponse.Text <- rsp;
//    )

    // asynchronous, does not block UI
    btn.Click.Add(fun _ ->
      async {
        do! sp.AsyncWriteLine tbRequest.Text
        let! rsp = sp.AsyncReadLine()
        tbResponse.Text <- rsp;
      }
      |> Async.StartImmediate
    )

    sp.Open()
    ()


[<System.STAThread>]
do
  Application().Run(Form()) |> ignore