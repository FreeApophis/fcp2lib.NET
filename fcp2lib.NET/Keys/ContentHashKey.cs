using System.Diagnostics.Contracts;

namespace FCP2.Keys
{
    public class ContentHashKey : FCP2Key
    {
        internal const string KeyPrefix = "CHK@";

        private readonly string _dataHash;
        private readonly string _decryptionKey;
        private readonly string _flags;
        private readonly string _containerItem;

        internal ContentHashKey(string key)
        {
            bool valid = true;

            key = key.Substring(4);
            var parts = key.Split(new[] {'/'}, 2);

            if (parts.Length < 2)
            {
                valid = false;
            }
            else
            {
                _containerItem = parts[1];
                var subparts = parts[0].Split(',');
                if (parts.Length != 3)
                {
                    valid = false;
                }
                else
                {
                    _dataHash = subparts[0];
                    _decryptionKey = subparts[0];
                    _flags = subparts[0];
                }
            }

            Valid = valid;
        }

        public override bool Valid { get; }

        public override string ToString()
        {
            Contract.Requires(Valid);

            return $"{KeyPrefix}{_dataHash},{_decryptionKey},{_flags}/{_containerItem}";
        }
    }
}