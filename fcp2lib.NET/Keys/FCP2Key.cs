namespace FCP2.Keys
{
    public abstract class FCP2Key
    {
        public static FCP2Key Create(string key)
        {
            FCP2Key resultKey = null;

            switch (key.Substring(0, 4))
            {
                case ContentHashKey.KeyPrefix:
                    resultKey = new ContentHashKey(key);
                    break;
                case UpdateableSubspaceKey.KeyPrefix:
                    resultKey = new UpdateableSubspaceKey(key);
                    break;
                case KeywordSignedKey.KeyPrefix:
                    resultKey = new KeywordSignedKey(key);
                    break;
                case SignedSubspaceKey.KeyPrefix:
                    resultKey = new SignedSubspaceKey(key);
                    break;
            }

            return resultKey != null && resultKey.Valid ? resultKey : null;
        }

        protected FCP2Key()
        {

        }

        public abstract bool Valid { get; }
        public abstract override string ToString();
    }
}