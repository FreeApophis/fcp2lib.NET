using System;

namespace Freenet.FCP2
{

    public class UnknownPeerNoteTypeEventArgs : EventArgs
    {

        private readonly string peerNoteType;

        /// <summary>
        /// UnknownPeerNoteTypeEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal UnknownPeerNoteTypeEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            peerNoteType = parsed["PeerNoteType"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string PeerNoteType
        {
            get { return peerNoteType; }
        }
    }
}