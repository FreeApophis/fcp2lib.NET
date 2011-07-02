using FCP2.Protocol;

namespace FCP2.EventArgs
{
    public class ExpectedDataLengthEventArgs : System.EventArgs
    {
        private readonly string identifier;
        private readonly bool global;
        private readonly long dataLength;

        /// <summary>
        /// ExpectedDataLengthEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal ExpectedDataLengthEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.identifier;
            global = parsed.global;
            dataLength = parsed.dataLength;
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

        public long DataLength
        {
            get { return dataLength; }
        }
    }
}
