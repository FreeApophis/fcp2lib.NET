using System;

namespace Freenet.FCP2
{

    public class EndListPersistentRequestsEventArgs : EventArgs
    {

        /// <summary>
        /// EndListPersistentRequestsEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal EndListPersistentRequestsEventArgs(MessageParser parsed)
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