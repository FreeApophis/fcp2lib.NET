using System;

namespace Freenet.FCP2
{

    public class PutFailedEventArgs : EventArgs
    {

        private readonly long code;
        private readonly string codeDescription;
        private readonly string expectedURI;
        private readonly string extraDescription;
        private readonly bool fatal;
        private readonly bool global;
        private readonly string identifier;
        private readonly string shortCodeDescription;

        /// <summary>
        /// PutFailedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PutFailedEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            code = long.Parse(parsed["Code"]);
            identifier = parsed["identifier"];
            global = bool.Parse(parsed["Global"]);
            expectedURI = parsed["ExpectedURI"];
            codeDescription = parsed["CodeDescription"];
            extraDescription = parsed["ExtraDescription"];
            fatal = bool.Parse(parsed["Fatal"]);
            shortCodeDescription = parsed["ShortCodeDescription"];

            /* TODO: Complex Put Failed */

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string ShortCodeDescription
        {
            get { return shortCodeDescription; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public bool Global
        {
            get { return global; }
        }

        public bool Fatal
        {
            get { return fatal; }
        }

        public string ExtraDescription
        {
            get { return extraDescription; }
        }

        public string ExpectedURI
        {
            get { return expectedURI; }
        }

        public string CodeDescription
        {
            get { return codeDescription; }
        }

        public long Code
        {
            get { return code; }
        }
    }
}