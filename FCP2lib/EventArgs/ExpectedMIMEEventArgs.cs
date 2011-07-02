using FCP2.Protocol;

namespace FCP2.EventArgs
{
    public class ExpectedMimeEventArgs : System.EventArgs
    {
        private readonly string identifier;
        private readonly bool global;
        private readonly MetadataType metadata;

        /// <summary>
        /// ExpectedMIMEEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal ExpectedMimeEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.identifier;
            global = parsed.global;
            metadata = new MetadataType(parsed);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public MetadataType Metadata
        {
            get { return metadata; }
        }

        public bool Global
        {
            get { return global; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public class MetadataType
        {
            private readonly string contentType;

            internal MetadataType(dynamic parsed)
            {
                contentType = parsed.metadata.contentType;

            }

            public string ContentType
            {
                get { return contentType; }
            }

        }
    }
}
