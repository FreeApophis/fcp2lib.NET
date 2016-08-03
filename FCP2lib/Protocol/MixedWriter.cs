/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2016 Thomas Bruderer <apophis@apophis.ch>
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

using System;
using System.IO;
using System.Text;

namespace FCP2.Protocol
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
        readonly byte[] _buffer = new byte[1024];
        readonly Stream _stream;

        public MixedWriter(Stream stream)
        {
            _stream = stream;
        }

        public override Encoding Encoding => Encoding.UTF8;
        public override void Flush() => _stream.Flush();

        public override void WriteLine(string value)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.Gray;
#endif
            value = value + "\n";
            var enc = new UTF8Encoding();
            int numbytes = enc.GetBytes(value, 0, value.Length, _buffer, 0);
            _stream.Write(_buffer, 0, numbytes);
        }

    }
}
