using System.Diagnostics.Contracts;

namespace FCP2.Keys
{
    public class UpdateableSubspaceKey : FCP2Key
    {
        internal const string KeyPrefix = "USK@";

        internal UpdateableSubspaceKey(string key)
        {
            Contract.Requires(key.StartsWith(KeyPrefix));

            Valid = false;
        }

        public override bool Valid { get; }

        public override string ToString()
        {
            return KeyPrefix;
        }
    }
}