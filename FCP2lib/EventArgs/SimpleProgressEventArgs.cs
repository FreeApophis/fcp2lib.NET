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

    public class SimpleProgressEventArgs : EventArgs {
        
        private int total;
        
        public int Total {
            get { return total; }
        }
        
        private int failed;
        
        public int Failed {
            get { return failed; }
        }
        
        private int fatallyFailed;
        
        public int FatallyFailed {
            get { return fatallyFailed; }
        }
        
        private int succeeded;
        
        public int Succeeded {
            get { return succeeded; }
        }
        
        private bool finalizedTotal;
        
        public bool FinalizedTotal {
            get { return finalizedTotal; }
        }
        
        private string identifier;
        
        public string Identifier {
            get { return identifier; }
        }
         
        /// <summary>
        /// SimpleProgressEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal SimpleProgressEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.total = int.Parse(parsed["Total"]);
            this.failed = int.Parse(parsed["Failed"]);
            this.fatallyFailed = int.Parse(parsed["FatallyFailed"]);
            this.succeeded = int.Parse(parsed["Succeeded"]);
            this.finalizedTotal = bool.Parse(parsed["FinalizedTotal"]);
            this.identifier = parsed["Identifier"];
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}