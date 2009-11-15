/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009 Thomas Bruderer <apophis@apophis.ch>
 *  Copyright (c) 2009 Felipe Barriga Richards
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

    public class ProtocolErrorEventArgs : EventArgs {
        
        private bool global;
        
        public bool Global {
            get { return global; }
        }
        
        private long code;
        
        public long Code {
            get { return code; }
        }
        
        private string codeDescription;
        
        public string CodeDescription {
            get { return codeDescription; }
        }
        
        private string extraDescription;
        
        public string ExtraDescription {
            get { return extraDescription; }
        }
        
        private bool fatal;
        
        public bool Fatal {
            get { return fatal; }
        }
        
        private string identifier;
        
        public string Identifier {
            get { return identifier; }
        }
        
        /// <summary>
        /// ProtocolErrorEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal ProtocolErrorEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.global = (parsed["Global"]!=null) ? bool.Parse(parsed["Global"]) : false;
            this.code = long.Parse(parsed["Code"]);
            this.codeDescription = parsed["CodeDescription"];
            this.extraDescription = parsed["ExtraDescription"];
            this.fatal = bool.Parse(parsed["Fatal"]);
            this.identifier = parsed["Identifier"];

            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}