/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009 Thomas Bruderer <apophis@apophis.ch>
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

namespace Freenet.FCP2
{

    public class AllDataEventArgs : EventArgs
    {
        private readonly DateTime completionTime;
        private Stream data;
        private readonly long datalength;

        private readonly string identifier;
        private readonly DateTime startupTime;

        /// <summary>
        /// AllDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        /// <param name="data">Data</param>
        internal AllDataEventArgs(MessageParser parsed, Stream data)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            if (!parsed.DataAvailable)
                throw new NotSupportedException("AllDataEvent without Data");

            this.data = data;
            identifier = parsed["Identifier"];
            datalength = long.Parse(parsed["DataLength"]);
            startupTime = FCP2.FromUnix(parsed["StartupTime"]);
            completionTime = FCP2.FromUnix(parsed["CompletionTime"]);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public long Datalength
        {
            get { return datalength; }
        }

        public DateTime StartupTime
        {
            get { return startupTime; }
        }

        public DateTime CompletionTime
        {
            get { return completionTime; }
        }

        /// <summary>
        /// This Method only gets the Datastream once, the Datastream is cleared 
        /// after that and the method will get null back!
        /// To make sure only one consumer tries to read from the stream!
        /// 
        /// Your Handler is NOT allowed to finish before you have completly read the Data!
        /// </summary>
        public Stream GetStream()
        {
            Stream temp = data;
            data = null;
            return temp;
        }
    }
}