using System;

namespace Freenet.FCP2 {

    public class SubscribedUSKUpdateEventArgs : EventArgs {

        private readonly long edition;
        private readonly string identifier;
        private readonly string uri;

        /// <summary>
        /// SubscribedUSKUpdateEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal SubscribedUSKUpdateEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif

            edition = long.Parse(parsed["Edition"]);
            identifier = parsed["Identifier"];
            uri = parsed["URI"];

            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }

        public long Edition {
            get { return edition; }
        }

        public string Identifier {
            get { return identifier; }
        }

        public string URI {
            get { return uri; }
        }
    }
}