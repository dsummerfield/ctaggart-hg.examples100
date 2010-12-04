module AsyncSerialPort

open System.IO.Ports
open System.Text

type SerialPort with

  member this.AsyncWriteLine(s:string) =
    this.BaseStream.AsyncWrite(this.Encoding.GetBytes(s+"\n"))
  
  // expects a terminating line feed '\n'
  member this.AsyncReadLine() =
    async {
      let sb = StringBuilder()
      let bufferRef = ref (Array.zeroCreate<byte> this.ReadBufferSize)
      let buffer = !bufferRef
      let lastChr = ref 0uy
      while !lastChr <> byte '\n' do
        let! readCount = this.BaseStream.AsyncRead buffer
        lastChr := buffer.[readCount-1]
        sb.Append (this.Encoding.GetString(buffer.[0 .. readCount-1])) |> ignore
      sb.Length <- sb.Length-1 // get rid of '\n'
      return sb.ToString()
    }
