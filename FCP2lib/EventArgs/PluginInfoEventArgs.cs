using System;

namespace Freenet.FCP2
{

    public class PluginInfoEventArgs : EventArgs
    {
        private readonly string identifier;
        private readonly string originalUri;
        private readonly string pluginName;
        private readonly bool started;

        /// <summary>
        /// PluginInfoEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PluginInfoEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            pluginName = parsed["PluginName"];
            identifier = parsed["Identifier"];
            originalUri = parsed["OriginalUri"];
            if (parsed["Started"] != null)
                started = bool.Parse(parsed["Started"]);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string PluginName
        {
            get { return pluginName; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public string OriginalUri
        {
            get { return originalUri; }
        }

        public bool Started
        {
            get { return started; }
        }
    }
}