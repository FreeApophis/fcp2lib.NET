using System;

namespace Freenet.FCP2
{

    public class PersistentGetEventArgs : EventArgs
    {
        private readonly string clientToken;
        private readonly string filename;
        private readonly bool global;
        private readonly long maxRetries;
        private readonly PersistenceEnum persistenceType;
        private readonly PriorityClassEnum priorityClass;
        private readonly ReturnTypeEnum returnType;
        private readonly string tempFilename;
        private readonly string uri;
        private readonly VerbosityEnum verbosity;

        /// <summary>
        /// PersistentGetEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentGetEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            uri = parsed["URI"];
            verbosity = (VerbosityEnum)(long.Parse(parsed["Verbosity"]));
            returnType = (ReturnTypeEnum)Enum.Parse(typeof(ReturnTypeEnum), parsed["ReturnType"]);
            filename = parsed["Filename"];
            tempFilename = parsed["TempFilename"];
            clientToken = parsed["ClientToken"];
            priorityClass = (PriorityClassEnum)long.Parse(parsed["PriorityClass"]);
            persistenceType = (PersistenceEnum)Enum.Parse(typeof(PersistenceEnum), parsed["PersistenceType"]);
            global = bool.Parse(parsed["Global"]);
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

        public ReturnTypeEnum ReturnType
        {
            get { return returnType; }
        }

        public string Filename
        {
            get { return filename; }
        }

        public string TempFilename
        {
            get { return tempFilename; }
        }

        public string ClientToken
        {
            get { return clientToken; }
        }

        public PriorityClassEnum PriorityClass
        {
            get { return priorityClass; }
        }

        public PersistenceEnum PersistenceType
        {
            get { return persistenceType; }
        }

        public bool Global
        {
            get { return global; }
        }

        public long MaxRetries
        {
            get { return maxRetries; }
        }
    }
}