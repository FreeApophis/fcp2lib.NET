using System;

namespace Freenet.FCP2
{

    public class ConfigDataEventArgs : EventArgs
    {
        readonly MessageParser config;

        /// <summary>
        /// ConfigDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal ConfigDataEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            config = parsed;

            /* Too much to parse by hand -
             * we would duplicate most of the nodes 
             * datastructures for configuration 
             * Therfore we expose the MessageParser directly...
             * 
             * C# 4.0 - dynamic type could be the solution!
             * * * * * */

#if DEBUG
            parsed.PrintAccessCount();
#endif

        }

        public MessageParser Config
        {
            get { return config; }
        }
    }
}