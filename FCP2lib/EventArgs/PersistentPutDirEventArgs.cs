using System;

namespace Freenet.FCP2
{

    public class PersistentPutDirEventArgs : EventArgs
    {

        /// <summary>
        /// PersistentPutDirEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentPutDirEventArgs(MessageParser parsed)
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