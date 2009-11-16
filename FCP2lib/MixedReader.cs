using System.IO;
using System.Text;

namespace Freenet.FCP2
{
    /// <summary>
    /// Minimalistic StreamReader Implementation which makes it possible to read binary data aswell.
    /// 
    /// Its a shame that the StreamReader has no option to use it unbuffered!
    /// 
    /// Only the minimalistic Functions are implemented, and probably not very fast, but it supports fully UTF8
    /// 
    /// Its fixed as a UTF8 Reader, cannot read Lines longer than 1023 Bytes and Newline is only unix-style \n
    /// 
    /// Only Readline() is implemented
    /// </summary>
    public class MixedReader : TextReader
    {
        private byte[] buffer = new byte[1024];
        private Stream stream;

        public MixedReader(Stream stream)
        {
            this.stream = stream;
        }

        public override string ReadLine()
        {
            // max line length in byte 1024

            var enc = new UTF8Encoding();
            long cur; int i = 0;
            bool end = false;
            while(!end) {
                cur = stream.ReadByte();
                switch(cur) {
                    case -1:
                        //throw new EndOfStreamException();
                        break;
                    case 10:
                        end = true;
                        break;
                    default:
                        buffer[i] = (byte)cur;
                        break;
                }
                ++i;
            }
            return enc.GetString(buffer, 0, i-1);
        }
    }
}
