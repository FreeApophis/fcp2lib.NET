using System;
using System.IO;
using System.Net;
using FCP2.Keys;

namespace FCP2.Protocol
{
    public class FCP2Client : IDisposable
    {
        public FCP2Protocol FCP2Protocol { get; }

        public bool IsConnected => false;

        public FCP2Client(IPEndPoint nodeAdress, string clientName)
        {
            FCP2Protocol = new FCP2Protocol(nodeAdress, clientName);

            FCP2Protocol.NodeHelloEvent += FCP2Protocol_NodeHelloEvent;
        }

        private void FCP2Protocol_NodeHelloEvent(object sender, EventArgs.NodeHelloEventArgs e)
        {
            
        }

        public FCP2Client(string clientName)
        {
            FCP2Protocol = new FCP2Protocol(FCP2Protocol.StandardFCP2Endpoint, clientName);
        }

        public FCP2Download Download(FCP2Key key)
        {
            return new FCP2Download(key, null);
        }

        public FCP2Upload Upload(FCP2Key key, FileInfo file)
        {
            return  new FCP2Upload();
        }

        public void Dispose()
        {
            FCP2Protocol.Dispose();
        }
    }
}
