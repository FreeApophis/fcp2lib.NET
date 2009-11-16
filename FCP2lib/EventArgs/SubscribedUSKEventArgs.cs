using System;

namespace Freenet.FCP2
{

    public class SubscribedUSKEventArgs : EventArgs
    {
        private readonly bool dontPoll;
        private readonly string identifier;
        private readonly string uri;

        /// <summary>
        /// SubscribedUSKEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal SubscribedUSKEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identifier = parsed["Identifier"];
            uri = parsed["URI"];
            dontPoll = bool.Parse(parsed["DontPoll"]);

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

        public bool DontPoll
        {
            get { return dontPoll; }
        }
    }
}