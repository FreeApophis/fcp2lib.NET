using System;

namespace Freenet.FCP2
{


    public class TestDDACompleteEventArgs : EventArgs
    {

        private readonly string directory;
        private readonly bool readDirectoryAllowed;
        private readonly bool writeDirectoryAllowed;

        /// <summary>
        /// TestDDACompleteEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal TestDDACompleteEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            directory = parsed["Directory"];
            readDirectoryAllowed = (parsed["ReadDirectoryAllowed"] != null) ? bool.Parse(parsed["ReadDirectoryAllowed"]) : false;
            writeDirectoryAllowed = (parsed["WriteDirectoryAllowed"] != null) ? bool.Parse(parsed["WriteDirectoryAllowed"]) : false;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Directory
        {
            get { return directory; }
        }

        public bool ReadDirectoryAllowed
        {
            get { return readDirectoryAllowed; }
        }

        public bool WriteDirectoryAllowed
        {
            get { return writeDirectoryAllowed; }
        }
    }
}