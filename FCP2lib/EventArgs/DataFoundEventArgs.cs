using System;

namespace Freenet.FCP2
{

    public class DataFoundEventArgs : EventArgs
    {
        private readonly string contentType;
        private readonly long datalength;
        private readonly bool global;

        private readonly string identifier;

        /// <summary>
        /// DataFoundEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal DataFoundEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            contentType = parsed["Metadata.ContentType"];
            datalength = long.Parse(parsed["DataLength"]);
            global = (parsed["Global"] != null);
            identifier = parsed["Identifier"];

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

        public long Datalength
        {
            get { return datalength; }
        }

        public string ContentType
        {
            get { return contentType; }
        }
    }
}