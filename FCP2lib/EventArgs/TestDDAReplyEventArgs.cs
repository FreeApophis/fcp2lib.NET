using System;

namespace Freenet.FCP2
{
    public class TestDDAReplyEventArgs : EventArgs
    {
        private readonly string contentToWrite;
        private readonly string directory;
        private readonly string readFilename;
        private readonly string writeFilename;

        /// <summary>
        /// TestDDAReplyEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal TestDDAReplyEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            directory = parsed["Directory"];
            readFilename = parsed["ReadFilename"];
            writeFilename = parsed["WriteFilename"];
            contentToWrite = parsed["ContentToWrite"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Directory
        {
            get { return directory; }
        }

        public string ReadFilename
        {
            get { return readFilename; }
        }

        public string WriteFilename
        {
            get { return writeFilename; }
        }

        public string ContentToWrite
        {
            get { return contentToWrite; }
        }
    }
}