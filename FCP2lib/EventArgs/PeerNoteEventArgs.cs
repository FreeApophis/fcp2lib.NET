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
using System.Text;

namespace FCP2.EventArgs
{

    public class PeerNoteEventArgs : System.EventArgs
    {

        private readonly string nodeIdentifier;
        private readonly string noteText;
        private readonly PeerNoteTypeEnum peerNoteType;

        /// <summary>
        /// PeerNoteEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PeerNoteEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            nodeIdentifier = parsed.NodeIdentifier;
            var enc = new UTF8Encoding();
            noteText = enc.GetString(Convert.FromBase64String(parsed.NoteText));
            peerNoteType = parsed.PeerNoteType;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string NodeIdentifier
        {
            get { return nodeIdentifier; }
        }

        public string NoteText
        {
            get { return noteText; }
        }

        public PeerNoteTypeEnum PeerNoteType
        {
            get { return peerNoteType; }
        }
    }
}

