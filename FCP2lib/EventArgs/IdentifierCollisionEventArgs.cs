using System;

namespace Freenet.FCP2
{

    public class IdentifierCollisionEventArgs : EventArgs
    {
        private readonly bool global;
        private readonly string identifier;

        /// <summary>
        /// IdentifierCollisionEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal IdentifierCollisionEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identifier = parsed["Identifier"];
            if (parsed["Global"] != null)
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