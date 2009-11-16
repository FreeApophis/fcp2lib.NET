using System;

namespace Freenet.FCP2
{

    public class PeerRemovedEventArgs : EventArgs
    {

        private readonly string identity;
        private readonly string nodeIdentifier;

        /// <summary>
        /// PeerRemovedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PeerRemovedEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identity = parsed["Identity"];
            nodeIdentifier = parsed["NodeIdentifier"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identity
        {
            get { return identity; }
        }

        public string NodeIdentifier
        {
            get { return nodeIdentifier; }
        }
    }
}