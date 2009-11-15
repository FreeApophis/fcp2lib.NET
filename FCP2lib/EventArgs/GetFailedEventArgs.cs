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
    
    public class GetFailedEventArgs : EventArgs {
        
        private bool global = false;
        
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
        
        private bool fatal  = false;
        
        public bool Fatal {
            get { return fatal; }
        }
        
        private string shortCodeDescription;
        
        public string ShortCodeDescription {
            get { return shortCodeDescription; }
        }
        
        private string identifier;
        
        public string Identifier {
            get { return identifier; }
        }
        
        private long expectedDataLength;
        
        public long ExpectedDataLength {
            get { return expectedDataLength; }
        }
        
        private expectedMetadataType expectedMetadata;

        public expectedMetadataType ExpectedMetadata {
            get { return expectedMetadata; }
        }
        
        private bool finalizedExpected  = false;
        
        public bool FinalizedExpected {
            get { return finalizedExpected; }
        }
        
        private string redirectURI;
        
        public string RedirectURI {
            get { return redirectURI; }
        }
                
        /// <summary>
        /// GetFailedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal GetFailedEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            if (parsed["Global"] != null)
                this.global = bool.Parse(parsed["Global"]);
            this.code = long.Parse(parsed["Code"]);
            this.codeDescription = parsed["CodeDescription"];
            this.extraDescription = parsed["ExtraDescription"];
            if (parsed["Fatal"] != null)
                this.fatal = bool.Parse(parsed["Fatal"]);
            this.shortCodeDescription = parsed["ShortCodeDescription"];
            this.identifier = parsed["Identifier"];
            if (parsed["ExpectedDataLength"] != null)
                this.expectedDataLength = long.Parse(parsed["ExpectedDataLength"]);
            this.expectedMetadata = new expectedMetadataType(parsed);
            if (parsed["FinalizedExpected"] != null)
                this.finalizedExpected= bool.Parse(parsed["FinalizedExpected"]);
            this.redirectURI = parsed["RedirectURI"];  
            
            /* TODO: Complex Get Failed */
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
        
        public class expectedMetadataType{
            
            private string contentType;
            
            public string ContentType {
                get { return contentType; }
            }

            internal expectedMetadataType(MessageParser parsed) {
                this.contentType = parsed["ExpectedMetadata.ContentType"];                
            }
        }
    }
}