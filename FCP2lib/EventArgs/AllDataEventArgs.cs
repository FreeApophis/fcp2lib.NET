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
using System.Collections.Generic;

namespace Freenet.FCP2 {

    public class AllDataEventArgs : EventArgs {
        
        
        private Stream data;
        
        /// <summary>
        /// This Method only gets the Datastream once, the Datastream is cleared 
        /// after that and the method will get null back!
        /// To make sure only one consumer tries to read from the stream!
        /// 
        /// Your Handler is NOT allowed to finish before you have completly read the Data!
        /// </summary>
        public Stream GetStream() {
            Stream temp = data;
            data = null;
            return temp;
        }
        
        private string identifier;
        
        public string Identifier {
            get { return identifier; }
        }
        
        private long datalength;
        
        public long Datalength {
            get { return datalength; }
        }
        
        private DateTime startupTime;
        
        public DateTime StartupTime {
            get { return startupTime; }
        }
        
        private DateTime completionTime;
        
        public DateTime CompletionTime {
            get { return completionTime; }
        }
        
        
        /// <summary>
        /// AllDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public AllDataEventArgs(MessageParser parsed, Stream data) {
            FCP2.ArgsDebug(this, parsed);

            if (!parsed.DataAvailable)
                throw new NotSupportedException("AllDataEvent without Data");
            
            this.data = data;
            this.identifier = parsed["Identifier"];
            this.datalength = long.Parse(parsed["DataLength"]);
            this.startupTime = FCP2.FromUnix(parsed["StartupTime"]);
            this.completionTime = FCP2.FromUnix(parsed["CompletionTime"]);
            
        }
    }
}