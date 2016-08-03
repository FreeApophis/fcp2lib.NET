using System.Diagnostics.Contracts;

namespace FCP2.Keys
{
    public class SignedSubspaceKey : FCP2Key
    {
        internal const string KeyPrefix = "SSK@";

        private readonly string _privateKey;
        private readonly string _publicKey;
        private readonly string _containerItem;

        internal SignedSubspaceKey(string key)
        {
            Contract.Requires(key.StartsWith(KeyPrefix));

            bool valid = true;

            key = key.Substring(4);
            var parts = key.Split(new[] { '/' }, 2);

            if (parts.Length < 2)
            {
                valid = false;
            }
            else
            {
                _containerItem = parts[1];
                
                // TODO
            }

            IsInsertUri = false;
            Valid = valid;
        }

        public override bool Valid { get; }

        public bool IsInsertUri { get; }
        public bool IsRequestUri => !IsInsertUri;

        public override string ToString()
        {
            Contract.Requires(Valid);
            if (IsInsertUri)
            {
                return $"{KeyPrefix}";
            }
            else
            {
                return $"{KeyPrefix}";

            }
        }
    }
}