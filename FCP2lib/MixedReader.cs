/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2010 Thomas Bruderer <apophis@apophis.ch>
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.IO;
using System.Text;

namespace FCP2
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
        private readonly byte[] buffer = new byte[1024];
        private readonly Stream stream;

        public MixedReader(Stream stream)
        {
            this.stream = stream;
        }

        public override string ReadLine()
        {
            // max line length in byte 1024

            var enc = new UTF8Encoding();
            int i = 0;
            bool end = false;
            while (!end)
            {
                long cur = stream.ReadByte();
                switch (cur)
                {
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
            return enc.GetString(buffer, 0, i - 1);
        }
    }
}
