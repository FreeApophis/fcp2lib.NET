using System;

namespace Freenet.FCP2
{

    public class FinishedCompressionEventArgs : EventArgs
    {
        private readonly long codec;
        private readonly long compressedSize;
        private readonly string identifier;
        private readonly long originalSize;

        /// <summary>
        /// FinishedCompressionEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal FinishedCompressionEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            identifier = parsed["Identifier"];
            codec = long.Parse(parsed["Codec"]);
            originalSize = long.Parse(parsed["OriginalSize"]);
            compressedSize = long.Parse(parsed["CompressedSize"]);

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

        public long OriginalSize
        {
            get { return originalSize; }
        }

        public long CompressedSize
        {
            get { return compressedSize; }
        }
    }
}