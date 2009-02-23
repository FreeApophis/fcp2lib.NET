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

namespace Freenet.FCP2 {
    
    
    public class TestDDAReplyEventArgs : EventArgs {

        private string directory;
        
        public string Directory {
            get { return directory; }
        }
        
        private string readFilename;
        
        public string ReadFilename {
            get { return readFilename; }
        }
        
        private string writeFilename;
        
        public string WriteFilename {
            get { return writeFilename; }
        }
        
        private string contentToWrite;
        
        public string ContentToWrite {
            get { return contentToWrite; }
        }
        
        /// <summary>
        /// TestDDAReplyEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal TestDDAReplyEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.directory = parsed["Directory"];
            this.readFilename = parsed["ReadFilename"];
            this.writeFilename = parsed["WriteFilename"];
            this.contentToWrite = parsed["ContentToWrite"];

            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}