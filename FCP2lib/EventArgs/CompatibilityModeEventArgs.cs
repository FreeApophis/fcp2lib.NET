using FCP2.Protocol;

namespace FCP2.EventArgs
{
    public class CompatibilityModeEventArgs : System.EventArgs
    {
        private readonly string identifier;
        private readonly string global;
        private readonly string min;
        private readonly string minNumber;
        private readonly string max;
        private readonly string maxNumber;
        private readonly string splitfileCryptoKey;
        private readonly string dontCompress;
        private readonly string definitive;

        internal CompatibilityModeEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.Identifier;
            global = parsed.Global;
            min = parsed.Min;
            minNumber = parsed.MinNumber;
            max = parsed.Max;
            maxNumber = parsed.MaxNumber;
            splitfileCryptoKey = parsed.SplitfileCryptoKey;
            dontCompress = parsed.DontCompress;
            definitive = parsed.Definitive;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get
            {
                return identifier;
            }
        }

        public string Global
        {
            get
            {
                return global;
            }
        }
        public string Min
        {
            get
            {
                return min;
            }
        }
        public string MinNumber
        {
            get
            {
                return minNumber;
            }
        }
        public string Max
        {
            get
            {
                return max;
            }
        }
        public string MaxNumber
        {
            get
            {
                return maxNumber;
            }
        }
        public string SplitfileCryptoKey
        {
            get
            {
                return splitfileCryptoKey;
            }
        }
        public string DontCompress
        {
            get
            {
                return dontCompress;
            }
        }
        public string Definitive
        {
            get
            {
                return definitive;
            }
        }
    }
}
