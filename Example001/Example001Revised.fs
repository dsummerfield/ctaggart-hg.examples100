module Example001.WinFormRevised

open System
open System.Windows.Forms
open System.Drawing

//creates a new form
let iqform = new Form(Text="Compute IQ", Size=Size(300, 200), StartPosition=FormStartPosition.CenterScreen, AutoScaleMode=AutoScaleMode.Font)

//creates a label
let n1label = new Label(Text="Mental age:", Top=20, Left=5, AutoSize=true)
let firsttextbox = new TextBox(Location=Point(80, 20))

//creates another label and change its text to “Second number:”
let n2label = new Label(Text="Chronological age:", Location=Point(0,50), AutoSize=true)
let secondtextbox = new TextBox(Location=Point(100,50))

//creates another label and change its text to sum
let n3label = new Label(Text="IQ:", Location=Point(0, 90), AutoSize=true)

//creates a label that will display the result of the computation
let anslabel = new Label(Location=Point(80, 90), BorderStyle=BorderStyle.FixedSingle)

//make our buttons
let addbutton= new Button(Text="Compute", Location=Point(100, 130))
let exitbutton= new Button(Text="Exit", Location=Point(200, 130))

//add the controls into the form
iqform.Controls.Add n1label
iqform.Controls.Add firsttextbox
iqform.Controls.Add n2label
iqform.Controls.Add secondtextbox
iqform.Controls.Add n3label
iqform.Controls.Add anslabel
iqform.Controls.Add addbutton
iqform.Controls.Add exitbutton

//when the compute button is clicked
//display the iq value in the anslabel
addbutton.Click.Add(fun _ ->
  let manum = Convert.ToDouble firsttextbox.Text
  let canum = Convert.ToDouble secondtextbox.Text
  let iq = Convert.ToDouble((manum/canum)*100.00)
  anslabel.Text <- Convert.ToString(iq)
)

//when the exit button is clicked, close the form            
exitbutton.Click.Add(fun _ -> iqform.Close())

Application.Run iqform