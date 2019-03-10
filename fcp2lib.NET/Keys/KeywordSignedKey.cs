using System.Diagnostics;

namespace FCP2.Keys
{
    public class KeywordSignedKey : FCP2Key
    {
        internal const string KeyPrefix = "KSK@";

        private readonly string _keyword;

        internal KeywordSignedKey(string key)
        {
            Debug.Assert(key.StartsWith(KeyPrefix));

            _keyword = key.Substring(4);

            Valid = true;
        }


        public override bool Valid { get; }

        public override string ToString()
        {
            Debug.Assert(Valid);

            return $"{KeyPrefix}{_keyword}";
        }
    }
}