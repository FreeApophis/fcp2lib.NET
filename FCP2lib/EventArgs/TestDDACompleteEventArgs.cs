/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2010 Thomas Bruderer <apophis@apophis.ch>
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

using FCP2.Protocol;

namespace FCP2.EventArgs
{


    public class TestDDACompleteEventArgs : System.EventArgs
    {

        private readonly string directory;
        private readonly bool readDirectoryAllowed;
        private readonly bool writeDirectoryAllowed;

        /// <summary>
        /// TestDDACompleteEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal TestDDACompleteEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            directory = parsed.Directory;
            readDirectoryAllowed = parsed.ReadDirectoryAllowed;
            writeDirectoryAllowed = parsed.WriteDirectoryAllowed;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Directory
        {
            get { return directory; }
        }

        public bool ReadDirectoryAllowed
        {
            get { return readDirectoryAllowed; }
        }

        public bool WriteDirectoryAllowed
        {
            get { return writeDirectoryAllowed; }
        }
    }
}