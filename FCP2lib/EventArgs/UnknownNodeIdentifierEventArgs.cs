using System;

namespace Freenet.FCP2
{

    public class UnknownNodeIdentifierEventArgs : EventArgs
    {

        private readonly string nodeIdentifier;

        /// <summary>
        /// UnknownNodeIdentifierEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal UnknownNodeIdentifierEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            nodeIdentifier = parsed["NodeIdentifier"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string NodeIdentifier
        {
            get { return nodeIdentifier; }
        }
    }
}