using System;
using System.IO;

namespace Freenet.FCP2
{

    public class AllDataEventArgs : EventArgs
    {
        private readonly DateTime completionTime;
        private Stream data;
        private readonly long datalength;

        private readonly string identifier;
        private readonly DateTime startupTime;

        /// <summary>
        /// AllDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        /// <param name="data">Data</param>
        internal AllDataEventArgs(MessageParser parsed, Stream data)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            if (!parsed.DataAvailable)
                throw new NotSupportedException("AllDataEvent without Data");

            this.data = data;
            identifier = parsed["Identifier"];
            datalength = long.Parse(parsed["DataLength"]);
            startupTime = FCP2.FromUnix(parsed["StartupTime"]);
            completionTime = FCP2.FromUnix(parsed["CompletionTime"]);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public long Datalength
        {
            get { return datalength; }
        }

        public DateTime StartupTime
        {
            get { return startupTime; }
        }

        public DateTime CompletionTime
        {
            get { return completionTime; }
        }

        /// <summary>
        /// This Method only gets the Datastream once, the Datastream is cleared 
        /// after that and the method will get null back!
        /// To make sure only one consumer tries to read from the stream!
        /// 
        /// Your Handler is NOT allowed to finish before you have completly read the Data!
        /// </summary>
        public Stream GetStream()
        {
            Stream temp = data;
            data = null;
            return temp;
        }
    }
}