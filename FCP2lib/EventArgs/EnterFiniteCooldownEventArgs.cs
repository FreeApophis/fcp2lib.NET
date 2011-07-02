using FCP2.Protocol;

namespace FCP2.EventArgs
{
    public class EnterFiniteCooldownEventArgs : System.EventArgs
    {
        private readonly string identifier;
        private readonly bool global;
        private readonly long wakeupTime;


        /// <summary>
        /// EnterFiniteCooldownEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal EnterFiniteCooldownEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.identifier;
            global = parsed.global;
            wakeupTime = parsed.wakeupTime;
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

        public long WakeupTime
        {
            get { return wakeupTime; }
        }
    }
}
