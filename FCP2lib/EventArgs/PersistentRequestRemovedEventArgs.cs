using System;

namespace Freenet.FCP2
{

    public class PersistentRequestRemovedEventArgs : EventArgs
    {
        private readonly bool global;
        private readonly string identifier;

        /// <summary>
        /// PersistentRequestRemovedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentRequestRemovedEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identifier = parsed["Identifier"];
            global = bool.Parse(parsed["Global"]);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public bool Global
        {
            get { return global; }
        }
    }
}