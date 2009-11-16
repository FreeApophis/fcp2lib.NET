using System;

namespace Freenet.FCP2
{

    public class SSKKeypairEventArgs : EventArgs
    {
        private readonly string identifier;
        private readonly string insertURI;
        private readonly string requestURI;

        /// <summary>
        /// SSKKeypairEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal SSKKeypairEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            insertURI = parsed["InsertURI"];
            requestURI = parsed["RequestURI"];
            identifier = parsed["Identifier"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string InsertURI
        {
            get { return insertURI; }
        }

        public string RequestURI
        {
            get { return requestURI; }
        }

        public string Identifier
        {
            get { return identifier; }
        }
    }
}