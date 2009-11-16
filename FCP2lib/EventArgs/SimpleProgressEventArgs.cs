using System;

namespace Freenet.FCP2
{

    public class SimpleProgressEventArgs : EventArgs
    {
        private readonly long failed;
        private readonly long fatallyFailed;
        private readonly bool finalizedTotal;
        private readonly string identifier;
        private readonly long required;
        private readonly long succeeded;
        private readonly long total;

        /// <summary>
        /// SimpleProgressEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal SimpleProgressEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            total = long.Parse(parsed["Total"]);
            required = long.Parse(parsed["Required"]);
            failed = long.Parse(parsed["Failed"]);
            fatallyFailed = long.Parse(parsed["FatallyFailed"]);
            succeeded = long.Parse(parsed["Succeeded"]);
            finalizedTotal = bool.Parse(parsed["FinalizedTotal"]);
            identifier = parsed["Identifier"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public long Total
        {
            get { return total; }
        }

        public long Required
        {
            get { return required; }
        }

        public long Failed
        {
            get { return failed; }
        }

        public long FatallyFailed
        {
            get { return fatallyFailed; }
        }

        public long Succeeded
        {
            get { return succeeded; }
        }

        public bool FinalizedTotal
        {
            get { return finalizedTotal; }
        }

        public string Identifier
        {
            get { return identifier; }
        }
    }
}