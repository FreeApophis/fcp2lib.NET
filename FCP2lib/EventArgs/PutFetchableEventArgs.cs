using System;

namespace Freenet.FCP2
{

    public class PutFetchableEventArgs : EventArgs
    {
        private readonly bool global;
        private readonly string identifier;
        private readonly string uri;

        /// <summary>
        /// PutFetchableEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PutFetchableEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            global = bool.Parse(parsed["Global"]);
            identifier = parsed["Identifier"];
            uri = parsed["URI"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public bool Global
        {
            get { return global; }
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