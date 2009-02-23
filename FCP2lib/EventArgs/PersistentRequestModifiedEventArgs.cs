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
    
    public class PersistentRequestModifiedEventArgs : EventArgs {
        
        private string identifier;
        
        public string Identifier {
            get { return identifier; }
        }
        
        private bool global;
        
        public bool Global {
            get { return global; }
        }
        
        private string clientToken;
        
        public string ClientToken {
            get { return clientToken; }
        }
        
        private PriorityClassEnum priorityClass;
        
        public PriorityClassEnum PriorityClass {
            get { return priorityClass; }
        }
        
        /// <summary>
        /// PersistentRequestModifiedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentRequestModifiedEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.identifier = parsed["Identifier"];
            this.global = bool.Parse(parsed["Global"]);
            this.clientToken = parsed["ClientToken"];
            this.priorityClass = (PriorityClassEnum)(int.Parse(parsed["PriorityClass"]));
           
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}