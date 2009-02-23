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
       
    public class PersistentGetEventArgs : EventArgs {

        private string uri;
        
        public string URI {
            get { return uri; }
        }
        
        private VerbosityEnum verbosity;
        
        public VerbosityEnum Verbosity {
            get { return verbosity; }
        }
        
        private ReturnTypeEnum returnType;
        
        public ReturnTypeEnum ReturnType {
            get { return returnType; }
        }
        
        private string filename;
        
        public string Filename {
            get { return filename; }
        }
        
        private string tempFilename;
        
        public string TempFilename {
            get { return tempFilename; }
        }
        
        private string clientToken;
        
        public string ClientToken {
            get { return clientToken; }
        }
        
        private PriorityClassEnum priorityClass;
        
        public PriorityClassEnum PriorityClass {
            get { return priorityClass; }
        }
        
        private PersistenceEnum persistenceType;
        
        public PersistenceEnum PersistenceType {
            get { return persistenceType; }
        }
        
        private bool global;
        
        public bool Global {
            get { return global; }
        }
        
        private int maxRetries;
        
        public int MaxRetries {
            get { return maxRetries; }
        }
        
        /// <summary>
        /// PersistentGetEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentGetEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.uri = parsed["URI"];
            this.verbosity = (VerbosityEnum)(int.Parse(parsed["Verbosity"]));
            this.returnType = (ReturnTypeEnum)Enum.Parse(typeof(ReturnTypeEnum), parsed["ReturnType"]);
            this.filename = parsed["Filename"];
            this.tempFilename = parsed["TempFilename"];
            this.clientToken = parsed["ClientToken"];
            this.priorityClass = (PriorityClassEnum)int.Parse(parsed["PriorityClass"]);
            this.persistenceType = (PersistenceEnum)Enum.Parse(typeof(PersistenceEnum), parsed["PersistenceType"]);
            this.global = bool.Parse(parsed["Global"]);
            this.maxRetries = int.Parse(parsed["MaxRetries"]);  
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}