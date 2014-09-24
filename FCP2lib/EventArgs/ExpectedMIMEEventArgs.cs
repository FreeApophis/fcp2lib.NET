/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2014 Thomas Bruderer <apophis@apophis.ch>
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

namespace FCP2
{

    public class ExpectedMimeEventArgs : System.EventArgs
    {
        readonly string identifier;
        readonly bool global;
        readonly MetadataType metadata;

        /// <summary>
        /// ExpectedMIMEEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal ExpectedMimeEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.identifier;
            global = parsed.global;
            metadata = new MetadataType(parsed);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public MetadataType Metadata
        {
            get { return metadata; }
        }

        public bool Global
        {
            get { return global; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public class MetadataType
        {
            readonly string contentType;

            internal MetadataType(dynamic parsed)
            {
                contentType = parsed.metadata.contentType;

            }

            public string ContentType
            {
                get { return contentType; }
            }

        }
    }
}
