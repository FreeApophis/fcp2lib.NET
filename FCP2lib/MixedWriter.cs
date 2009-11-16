using System.IO;
using System.Text;

namespace Freenet.FCP2
{
    /// <summary>
    /// Minimalistic StreamWriter Implementation which makes it possible to write binary data aswell.
    /// 
    /// Its a shame that the StreamWriter has no option to use it unbuffered!
    /// 
    /// Only the minimalistic Functions are implemented, and probably not very fast, but it supports fully UTF8
    /// 
    /// Its fixed as a UTF8 Writer, cannot write Lines longer than 1023 Bytes and Newline is only unix-style \n
    /// 
    /// Only WriteLine(string) and Flush() are implemented
    /// </summary>
    public class MixedWriter : TextWriter
    {
        private readonly byte[] buffer = new byte[1024];
        private readonly Stream stream;

        public MixedWriter(Stream stream)
        {
            this.stream = stream;
        }

        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        public override void WriteLine(string value)
        {
#if DEBUG
            //            Console.ForegroundColor = ConsoleColor.Blue;
            //            Console.WriteLine(value);
            //            Console.ForegroundColor = ConsoleColor.Gray;
#endif
            value = value + "\n";
            var enc = new UTF8Encoding();
            int numbytes = enc.GetBytes(value, 0, value.Length, buffer, 0);
            stream.Write(buffer, 0, numbytes);
        }

        public override void Flush()
        {
            stream.Flush();
        }
    }
}
