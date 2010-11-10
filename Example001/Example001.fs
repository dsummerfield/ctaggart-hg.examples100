module Example001.WinForm
open System.Collections.Generic
open System
open System.Windows.Forms
open System.ComponentModel
open System.Drawing
//creates a new form
let iqform=new Form(Text="Compute IQ", Size=new System.Drawing.Size(300, 200),StartPosition=FormStartPosition.CenterScreen,AutoScaleMode=AutoScaleMode.Font)
//creates a label
let n1label=new Label(Text="Mental age:",Top=20,Left=5,AutoSize=true)
let firsttextbox=new TextBox(Location=new System.Drawing.Point(80, 20))
//creates another label and change its text to “Second number:”
let n2label=new Label(Text="Chronological age:", Location=new System.Drawing.Point(0,50),AutoSize=true)
let secondtextbox=new TextBox(Location=new System.Drawing.Point(100,50))
//creates another label and change its text to sum
let n3label=new Label(Text="IQ:", Location=new System.Drawing.Point(0, 90),AutoSize=true)
//creates a label that will display the result of the computation
let anslabel=new Label(Location=new System.Drawing.Point(80, 90), BorderStyle=BorderStyle.FixedSingle)
//make our buttons
let addbutton=new Button(Text="Compute", Location=new System.Drawing.Point(100, 130))
let exitbutton=new Button(Text="Exit", Location=new System.Drawing.Point(200, 130))
//add the controls into the form
iqform.Controls.Add(n1label)
iqform.Controls.Add(firsttextbox)
iqform.Controls.Add(n2label)
iqform.Controls.Add(secondtextbox)
iqform.Controls.Add(n3label)
iqform.Controls.Add(anslabel)
iqform.Controls.Add(addbutton)
iqform.Controls.Add(exitbutton)

//when the compute button is clicked
addbutton.Click.Add(fun addfunction ->
let manum=Convert.ToDouble(firsttextbox.Text)
let canum=Convert.ToDouble(secondtextbox.Text)
let iq=Convert.ToDouble((manum/canum)*100.00)

//display the iq value in the anslabel
anslabel.Text<-Convert.ToString(iq))
 //when the exit button is clicked, close the form            
exitbutton.Click.Add(fun exit -> iqform.Close())  
Application.Run(iqform)