using System;
using System.Text;

namespace Freenet.FCP2
{

    public class PeerNoteEventArgs : EventArgs
    {

        private readonly string nodeIdentifier;
        private readonly string noteText;
        private readonly PeerNoteTypeEnum peerNoteType;

        /// <summary>
        /// PeerNoteEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PeerNoteEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            nodeIdentifier = parsed["NodeIdentifier"];
            var enc = new UTF8Encoding();
            noteText = enc.GetString(Convert.FromBase64String(parsed["NoteText"]));
            peerNoteType = (PeerNoteTypeEnum)long.Parse(parsed["PeerNoteType"]);

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

