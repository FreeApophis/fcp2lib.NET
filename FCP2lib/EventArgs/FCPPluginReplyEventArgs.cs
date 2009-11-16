using System;
using System.IO;

namespace Freenet.FCP2
{

    public class FCPPluginReplyEventArgs : EventArgs
    {
        private readonly Stream data;
        private readonly long? dataLength;
        private readonly string identifier;
        private readonly string pluginName;
        private readonly MessageParser replies;

        /// <summary>
        /// FCPPluginReplyEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal FCPPluginReplyEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            pluginName = parsed["PluginName"];
            if (parsed["DataLength"] != null)
            {
                dataLength = long.Parse(parsed["DataLength"]);

                data = null; /* TODO: Similar to AllData*/
                throw new NotImplementedException("Unclear format");

                /* TODO: Data? EndMessage? */
            }
            identifier = parsed["Identifier"];

            replies = parsed;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public Stream Data
        {
            get { return data; }
        }

        public string PluginName
        {
            get { return pluginName; }
        }

        public long? DataLength
        {
            get { return dataLength; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public MessageParser Replies
        {
            get { return replies; }
        }
    }
}