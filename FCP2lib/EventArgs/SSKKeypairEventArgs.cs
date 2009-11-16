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

namespace Freenet.FCP2
{

    public class SSKKeypairEventArgs : EventArgs
    {
        private readonly string identifier;
        private readonly string insertURI;
        private readonly string requestURI;

        /// <summary>
        /// SSKKeypairEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal SSKKeypairEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            insertURI = parsed["InsertURI"];
            requestURI = parsed["RequestURI"];
            identifier = parsed["Identifier"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string InsertURI
        {
            get { return insertURI; }
        }

        public string RequestURI
        {
            get { return requestURI; }
        }

        public string Identifier
        {
            get { return identifier; }
        }
    }
}