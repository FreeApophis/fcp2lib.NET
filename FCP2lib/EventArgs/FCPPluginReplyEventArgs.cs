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

namespace Freenet.FCP2 {

    public class FCPPluginReplyEventArgs : EventArgs {
        
        private string pluginName;
        
        public string PluginName {
            get { return pluginName; }
        }
        
        private long? dataLength;
        
        public long? DataLength {
            get { return dataLength; }
        }
        
        private string identifier;
        
        public string Identifier {
            get { return identifier; }
        }
        
        private MessageParser replies;
        
        public MessageParser Replies {
            get { return replies; }
        }
        
        private Stream data;
        
        /// <summary>
        /// FCPPluginReplyEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal FCPPluginReplyEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.pluginName = parsed["PluginName"];
            if(parsed["DataLength"] != null) {
                this.dataLength = long.Parse(parsed["DataLength"]);
                
                data = null; /* TODO: Similar to AllData*/
                throw new NotImplementedException("Unclear format");
                
                /* TODO: Data? EndMessage? */
            }
            this.identifier = parsed["Identifier"];
            
            this.replies = parsed;
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}