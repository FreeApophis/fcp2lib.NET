using System;

namespace Freenet.FCP2
{

    public class PersistentPutEventArgs : EventArgs
    {
        private readonly string clientToken;
        private readonly long dataLength;
        private readonly string filename;
        private readonly bool global;
        private readonly long maxRetries;
        private readonly MetadataType metadata;
        private readonly PriorityClassEnum priorityClass;
        private readonly string targetFilename;
        private readonly UploadFromEnum uploadFrom;
        private readonly string uri;
        private readonly VerbosityEnum verbosity;

        /// <summary>
        /// PersistentPutEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentPutEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            uri = parsed["URI"];
            verbosity = (VerbosityEnum)(long.Parse(parsed["Verbosity"]));
            priorityClass = (PriorityClassEnum)long.Parse(parsed["PriorityClass"]);
            uploadFrom = (UploadFromEnum)Enum.Parse(typeof(UploadFromEnum), parsed["UploadFrom"]);
            filename = parsed["Filename"];
            targetFilename = parsed["TargetFilename"];
            metadata = new MetadataType(parsed);
            clientToken = parsed["ClientToken"];
            global = bool.Parse(parsed["Global"]);
            dataLength = long.Parse(parsed["DataLength"]);
            maxRetries = long.Parse(parsed["MaxRetries"]);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string URI
        {
            get { return uri; }
        }

        public VerbosityEnum Verbosity
        {
            get { return verbosity; }
        }

        public PriorityClassEnum PriorityClass
        {
            get { return priorityClass; }
        }

        public UploadFromEnum UploadFrom
        {
            get { return uploadFrom; }
        }

        public string Filename
        {
            get { return filename; }
        }

        public string TargetFilename
        {
            get { return targetFilename; }
        }

        public MetadataType Metadata
        {
            get { return metadata; }
        }

        public string ClientToken
        {
            get { return clientToken; }
        }

        public bool Global
        {
            get { return global; }
        }

        public long DataLength
        {
            get { return dataLength; }
        }

        public long MaxRetries
        {
            get { return maxRetries; }
        }

        #region Nested type: MetadataType

        public class MetadataType
        {
            private readonly string contentType;

            internal MetadataType(MessageParser parsed)
            {
                contentType = parsed["Metadata.ContentType"];
            }

            public string ContentType
            {
                get { return contentType; }
            }
        }

        #endregion
    }
}