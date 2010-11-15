namespace AreaConverter2

open System
open System.Windows.Media

/// Extensions for System.Windows.Media
module MediaExt =

  type Color with
    static member FromArgb(hex:string) =
      let toByte startIndex = Convert.ToByte(hex.Substring(startIndex,2),16)
      try
        match hex.Length with
        | 6 -> Color.FromArgb(255uy, toByte 0, toByte 2, toByte 4)
        | 7 -> Color.FromArgb(255uy, toByte 1, toByte 3, toByte 5)
        | 8 -> Color.FromArgb(toByte 0, toByte 2, toByte 4, toByte 6)
        | 9 -> Color.FromArgb(toByte 1, toByte 3, toByte 5, toByte 7)
        | _ -> Colors.White
      with :? FormatException ->
        Colors.White

  type SolidColorBrush with
    static member FromArgb(hex:string) =
      SolidColorBrush (Color.FromArgb hex)