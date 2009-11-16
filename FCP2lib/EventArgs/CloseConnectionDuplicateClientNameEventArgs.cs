using System;

namespace Freenet.FCP2
{

    public class CloseConnectionDuplicateClientNameEventArgs : EventArgs
    {

        /// <summary>
        /// CloseConnectionDuplicateClientNameEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal CloseConnectionDuplicateClientNameEventArgs(MessageParser parsed)
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