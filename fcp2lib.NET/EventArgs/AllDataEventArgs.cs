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
using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class AllDataEventArgs : System.EventArgs
    {

        public string Identifier { get; }
        public long Datalength { get; }
        public DateTime StartupTime { get; }
        public DateTime CompletionTime { get; }

        Stream _data;

        /// <summary>
        /// AllDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        /// <param name="data">Data</param>
        internal AllDataEventArgs(dynamic parsed, Stream data)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            if (!parsed.DataAvailable)
                throw new NotSupportedException("AllDataEvent without Data");

            _data = data;
            Identifier = parsed.Identifier;
            Datalength = parsed.DataLength;
            StartupTime = parsed.StartupTime;
            CompletionTime = parsed.CompletionTime;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }


        /// <summary>
        /// This Method only gets the data stream once, the data stream is cleared
        /// after that and the method will get null back!
        /// To make sure only one consumer tries to read from the stream!
        ///
        /// Your Handler is NOT allowed to finish before you have completely read the Data!
        /// </summary>
        public Stream GetStream()
        {
            var temp = _data;
            _data = null;
            return temp;
        }
    }
}