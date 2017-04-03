using FCP2.Protocol;
using System;

namespace ImageLoader
{
    class Client
    {
        private static Client instance;
        private static object syncRoot = new Object();

        private FCP2Protocol _protocol;
        private long _requestID = 0;

        private Client()
        {
            _protocol = new FCP2Protocol("ImageLoader");
            _protocol.ClientHello();
        }

        public long RequestID => ++_requestID;
        public FCP2Protocol Protocol => _protocol;

        public static Client Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Client();
                        }
                    }
                }

                return instance;
            }
        }
    }
}
