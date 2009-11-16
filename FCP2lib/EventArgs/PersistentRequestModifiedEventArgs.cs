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

namespace Freenet.FCP2
{

    public class PersistentRequestModifiedEventArgs : EventArgs
    {
        private readonly string clientToken;
        private readonly bool global;
        private readonly string identifier;
        private readonly PriorityClassEnum priorityClass;

        /// <summary>
        /// PersistentRequestModifiedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentRequestModifiedEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identifier = parsed["Identifier"];
            global = bool.Parse(parsed["Global"]);
            clientToken = parsed["ClientToken"];
            if (parsed["PriorityClass"] != null)
                priorityClass = (PriorityClassEnum)(int.Parse(parsed["PriorityClass"]));

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public bool Global
        {
            get { return global; }
        }

        public string ClientToken
        {
            get { return clientToken; }
        }

        public PriorityClassEnum PriorityClass
        {
            get { return priorityClass; }
        }
    }
}