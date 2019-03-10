using System;
using System.IO;
using System.Net;
using FCP2.Keys;

namespace FCP2.Protocol
{
    public class FCP2Client : IDisposable
    {
        private readonly FCP2Protocol _fcp2Protocol;

        public FCP2Protocol FCP2Protocol => _fcp2Protocol;

        public bool IsConnected => false;

        public FCP2Client(IPEndPoint nodeAddress, string clientName)
        {
            _fcp2Protocol = new FCP2Protocol(nodeAddress, clientName);

            _fcp2Protocol.NodeHelloEvent += FCP2Protocol_NodeHelloEvent;
        }

        private void FCP2Protocol_NodeHelloEvent(object sender, EventArgs.NodeHelloEventArgs e)
        {

        }

        public FCP2Client(string clientName)
        {
            _fcp2Protocol = new FCP2Protocol(FCP2Protocol.StandardFCP2Endpoint, clientName);
        }

        public FCP2Download Download(FCP2Key key)
        {
            return new FCP2Download(key, null);
        }

        public FCP2Upload Upload(FCP2Key key, FileInfo file)
        {
            return new FCP2Upload();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fcp2Protocol.Dispose();
                }
            }
        }
    }
}
