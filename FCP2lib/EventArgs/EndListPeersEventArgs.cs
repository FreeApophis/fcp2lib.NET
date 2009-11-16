using System;

namespace Freenet.FCP2
{

    public class EndListPeersEventArgs : EventArgs
    {

        /// <summary>
        /// EndListPeersEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal EndListPeersEventArgs(MessageParser parsed)
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