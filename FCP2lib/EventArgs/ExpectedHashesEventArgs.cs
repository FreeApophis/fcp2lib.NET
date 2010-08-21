using FCP2.Protocol;

namespace FCP2.EventArgs
{
    public class ExpectedHashesEventArgs : System.EventArgs
    {
        private readonly string identifier;
        private readonly bool global;
        private readonly HashesType hashes;


        internal ExpectedHashesEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.Identifier;
            global = parsed.Global;
            hashes = new HashesType(parsed.Hashes);

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

        public bool Global
        {
            get
            {
                return global;
            }
        }


        public HashesType Hashes
        {
            get
            {
                return hashes;
            }
        }

        #region Nested type: HashesType
        public class HashesType
        {
            private readonly string sha512;
            private readonly string sha256;
            private readonly string md5;
            private readonly string sha1;
            private readonly string tth;
            private readonly string ed2k;

            public HashesType(dynamic hashes)
            {
                sha512 = hashes.SHA512;
                sha256 = hashes.SHA256;
                md5 = hashes.MD5;
                sha1 = hashes.SHA1;
                tth = hashes.TTH;
                ed2k = hashes.ED2K;
            }

            public string SHA512
            {
                get
                {
                    return sha512;
                }
            }
            public string SHA256
            {
                get
                {
                    return sha256;
                }
            }
            public string MD5
            {
                get
                {
                    return md5;
                }
            }
            public string SHA1
            {
                get
                {
                    return sha1;
                }
            }
            public string TTH
            {
                get
                {
                    return tth;
                }
            }
            public string ED2K
            {
                get
                {
                    return ed2k;
                }
            }
        }


        #endregion
    }
}
