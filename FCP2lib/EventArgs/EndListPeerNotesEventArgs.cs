using System;

namespace Freenet.FCP2
{

    public class EndListPeerNotesEventArgs : EventArgs
    {

        /// <summary>
        /// EndListPeerNotesEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal EndListPeerNotesEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }
    }
}