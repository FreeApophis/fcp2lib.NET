using System;

namespace Freenet.FCP2
{

    public class ProtocolErrorEventArgs : EventArgs
    {
        private readonly long code;
        private readonly string codeDescription;
        private readonly string extraDescription;
        private readonly bool fatal;
        private readonly bool global;
        private readonly string identifier;

        /// <summary>
        /// ProtocolErrorEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal ProtocolErrorEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            global = (parsed["Global"] != null) ? bool.Parse(parsed["Global"]) : false;
            code = long.Parse(parsed["Code"]);
            codeDescription = parsed["CodeDescription"];
            extraDescription = parsed["ExtraDescription"];
            fatal = bool.Parse(parsed["Fatal"]);
            identifier = parsed["Identifier"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public bool Global
        {
            get { return global; }
        }

        public long Code
        {
            get { return code; }
        }

        public string CodeDescription
        {
            get { return codeDescription; }
        }

        public string ExtraDescription
        {
            get { return extraDescription; }
        }

        public bool Fatal
        {
            get { return fatal; }
        }

        public string Identifier
        {
            get { return identifier; }
        }
    }
}