using System;

namespace Freenet.FCP2
{

    public class StartedCompressionEventArgs : EventArgs
    {
        private readonly long codec;
        private readonly string identifier;

        /// <summary>
        /// StartedCompressionEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal StartedCompressionEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identifier = parsed["Identifier"];
            codec = long.Parse(parsed["Codec"]);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public long Codec
        {
            get { return codec; }
        }
    }
}