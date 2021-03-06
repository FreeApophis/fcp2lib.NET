/*
 *  The FCP2.0 Library, complete access to freenet's FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2016 Thomas Bruderer <apophis@apophis.ch>
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
using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class PutSuccessfulEventArgs : System.EventArgs
    {
        public bool Global { get; }
        public string Identifier { get; }
        public DateTime StartupTime { get; }
        public DateTime CompletionTime { get; }
        public string URI { get; }

        /// <summary>
        /// PutSuccessfulEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PutSuccessfulEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            Global = parsed.Global;
            Identifier = parsed.Identifier;
            StartupTime = parsed.StartupTime;
            CompletionTime = parsed.CompletionTime;
            URI = parsed.URI;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }
    }
}