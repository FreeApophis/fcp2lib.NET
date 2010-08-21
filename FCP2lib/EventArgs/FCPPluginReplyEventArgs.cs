/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2010 Thomas Bruderer <apophis@apophis.ch>
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

namespace FCP2.EventArgs
{

    public class FCPPluginReplyEventArgs : System.EventArgs
    {
        private readonly Stream data;
        private readonly long? dataLength;
        private readonly string identifier;
        private readonly string pluginName;
        private readonly MessageParser replies;

        /// <summary>
        /// FCPPluginReplyEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal FCPPluginReplyEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            pluginName = parsed.PluginName;
            
            dataLength = parsed.DataLength;
            if (parsed.DataLength.LastConversationSucessfull)
            {
                data = null; /* TODO: Similar to AllData*/
                throw new NotImplementedException("Unclear format");

                /* TODO: Data? EndMessage? */
            }
            identifier = parsed.Identifier;

            replies = parsed;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public Stream Data
        {
            get { return data; }
        }

        public string PluginName
        {
            get { return pluginName; }
        }

        public long? DataLength
        {
            get { return dataLength; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public MessageParser Replies
        {
            get { return replies; }
        }
    }
}