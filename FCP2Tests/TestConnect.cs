using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FCP2.Protocol;
using System.Diagnostics;

namespace FCP2Tests
{
    [TestClass]
    public class TestConnect
    {
        bool _hello = false;
        FCP2.EventArgs.NodeHelloEventArgs result;

        [TestMethod]
        public void SimpleConnect()
        {
            var protocol = new FCP2Protocol("test-client");
            protocol.ClientHello();
            protocol.NodeHelloEvent += Protocol_NodeHelloEvent;

            int retry = 200;
            while (!_hello && --retry > 0)
            {
                System.Threading.Thread.Sleep(100);
            }

            Assert.IsTrue(_hello);

            Assert.AreEqual("2.0", result?.FcpVersion);
            Assert.AreEqual("Fred", result?.Node);

            protocol.Disconnect();
        }

        private void Protocol_NodeHelloEvent(object sender, FCP2.EventArgs.NodeHelloEventArgs e)
        {
            result = e;
            _hello = true;
        }

        [TestMethod]
        public void Test2()
        {
            var protocol = new FCP2Protocol("test-client");
            protocol.ClientHello();
            protocol.NodeDataEvent += Protocol_NodeDataEvent;
            protocol.ClientGet("USK@yGvITGZzrY1vUZK-4AaYLgcjZ7ysRqNTMfdcO8gS-LY,-ab5bJVD3Lp-LXEQqBAhJpMKrKJ19RnNaZMIkusU79s,AQACAAE/toad/56/activelink.png", "Toad-Active-Link");

            System.Threading.Thread.Sleep(5000);
        }

        private void Protocol_NodeDataEvent(object sender, FCP2.EventArgs.NodeDataEventArgs e)
        {
            Trace.WriteLine("HERE");
        }
    }
}
