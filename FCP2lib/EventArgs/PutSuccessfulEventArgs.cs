using System;

namespace Freenet.FCP2
{

    public class PutSuccessfulEventArgs : EventArgs
    {
        private readonly DateTime completionTime;
        private readonly bool global;
        private readonly string identifier;
        private readonly DateTime startupTime;
        private readonly string uri;

        /// <summary>
        /// PutSuccessfulEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PutSuccessfulEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            global = bool.Parse(parsed["Global"]);
            identifier = parsed["Identifier"];
            startupTime = FCP2.FromUnix(parsed["StartupTime"]);
            completionTime = FCP2.FromUnix(parsed["CompletionTime"]);
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

        public DateTime StartupTime
        {
            get { return startupTime; }
        }

        public DateTime CompletionTime
        {
            get { return completionTime; }
        }

        public string URI
        {
            get { return uri; }
        }
    }
}