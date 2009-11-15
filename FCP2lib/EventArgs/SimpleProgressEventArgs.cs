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
        
        private long total;
        
        public long Total {
            get { return total; }
        }
        
        private long required;
        
        public long Required {
            get { return required; }
        }
        
        private long failed;
        
        public long Failed {
            get { return failed; }
        }
        
        private long fatallyFailed;
        
        public long FatallyFailed {
            get { return fatallyFailed; }
        }
        
        private long succeeded;
        
        public long Succeeded {
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
            
            this.total = long.Parse(parsed["Total"]);
            this.required = long.Parse(parsed["Required"]);
            this.failed = long.Parse(parsed["Failed"]);
            this.fatallyFailed = long.Parse(parsed["FatallyFailed"]);
            this.succeeded = long.Parse(parsed["Succeeded"]);
            this.finalizedTotal = bool.Parse(parsed["FinalizedTotal"]);
            this.identifier = parsed["Identifier"];
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}