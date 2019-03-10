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

using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class SSKKeypairEventArgs : System.EventArgs
    {
        public string InsertURI { get; }
        public string RequestURI { get; }
        public string Identifier { get; }

        /// <summary>
        /// SSKKeypairEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal SSKKeypairEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            InsertURI = parsed.InsertURI;
            RequestURI = parsed.RequestURI;
            Identifier = parsed.Identifier;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }
    }
}