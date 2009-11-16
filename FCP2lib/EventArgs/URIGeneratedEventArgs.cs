using System;

namespace Freenet.FCP2
{

    public class URIGeneratedEventArgs : EventArgs
    {

        private readonly string identifier;

        private readonly string uri;

        /// <summary>
        /// URIGeneratedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal URIGeneratedEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif
            identifier = parsed["Identifier"];
            uri = parsed["URI"];
#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public string URI
        {
            get { return uri; }
        }
    }
}