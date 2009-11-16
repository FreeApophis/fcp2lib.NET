using System;

namespace Freenet.FCP2
{

    public class PersistentRequestModifiedEventArgs : EventArgs
    {
        private readonly string clientToken;
        private readonly bool global;
        private readonly string identifier;
        private readonly PriorityClassEnum priorityClass;

        /// <summary>
        /// PersistentRequestModifiedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentRequestModifiedEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identifier = parsed["Identifier"];
            global = bool.Parse(parsed["Global"]);
            clientToken = parsed["ClientToken"];
            if (parsed["PriorityClass"] != null)
                priorityClass = (PriorityClassEnum)(int.Parse(parsed["PriorityClass"]));

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

        public string ClientToken
        {
            get { return clientToken; }
        }

        public PriorityClassEnum PriorityClass
        {
            get { return priorityClass; }
        }
    }
}