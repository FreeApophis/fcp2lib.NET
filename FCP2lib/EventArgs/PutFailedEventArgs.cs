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
    
    public class PutFailedEventArgs : EventArgs {

        private int code;
        private string identifier;
        private bool global = false;
        private string expectedURI;
        private string codeDescription;
        private string extraDescription;
        private bool fatal  = false;
        private string shortCodeDescription;
        
        /// <summary>
        /// PutFailedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PutFailedEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.code = int.Parse(parsed["Code"]);
            this.identifier = parsed["identifier"];
            this.global = bool.Parse(parsed["Global"]);
            this.expectedURI = parsed["ExpectedURI"];
            this.codeDescription = parsed["CodeDescription"];
            this.extraDescription = parsed["ExtraDescription"];
            this.fatal = bool.Parse(parsed["Fatal"]);
            this.shortCodeDescription = parsed["ShortCodeDescription"];
            
            /* TODO: Complex Put Failed */
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}