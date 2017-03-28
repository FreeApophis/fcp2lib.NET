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
        FCP2.EventArgs.NodeHelloEventArgs simpleResult;

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

            Assert.AreEqual("2.0", simpleResult?.FcpVersion);
            Assert.AreEqual("Fred", simpleResult?.Node);

            protocol.Disconnect();
        }

        private void Protocol_NodeHelloEvent(object sender, FCP2.EventArgs.NodeHelloEventArgs e)
        {
            simpleResult = e;
            _hello = true;
        }

        FCP2.EventArgs.DataFoundEventArgs dataFound;
        FCP2.EventArgs.AllDataEventArgs allData;
        System.Text.StringBuilder testData = new System.Text.StringBuilder();


        [TestMethod]
        public void Test2()
        {
            var protocol = new FCP2Protocol("test-client");
            protocol.ClientHello();

            protocol.DataFoundEvent += Protocol_DataFoundEvent;
            protocol.AllDataEvent += Protocol_AllDataEvent;

            const string TestIdentifier = "test-file";
            protocol.ClientGet("CHK@khy~PN5-P-Ze18SdPfb~eMVCcWv1RTDNaP3ketO3TwQ,-kcC5FBaNN7wZtcHgZGObkxk9221BL5r~wbbVTUhyHc,AAMC--8/test.txt", TestIdentifier);

            int retry = 1000;
            while (--retry > 0 && !Ready())
            {
                System.Threading.Thread.Sleep(100);
            }

            const int TestSize = 16;
            const string TestContentType = "text/plain";
            const string TestData = "this is a test\r\n";

            Assert.IsNotNull(allData);
            Assert.IsNotNull(dataFound);

            Assert.AreEqual(TestSize, dataFound.Datalength);
            Assert.AreEqual(TestIdentifier, dataFound.Identifier);
            Assert.IsFalse(dataFound.Global);
            Assert.AreEqual(TestContentType, dataFound.ContentType);

            Assert.IsTrue(allData.StartupTime <= allData.CompletionTime);

            Assert.AreEqual(TestSize, allData.Datalength);
            Assert.AreEqual(TestIdentifier, allData.Identifier);
            Assert.AreEqual(TestData, testData.ToString());

            protocol.Disconnect();
        }

        private bool Ready()
        {
            return allData != null && dataFound != null;
        }

        private void Protocol_AllDataEvent(object sender, FCP2.EventArgs.AllDataEventArgs e)
        {
            Trace.WriteLine("Protocol_AllDataEvent");
            var stream = e.GetStream();
            var buffer = new byte[1024];

            var bytesToRead = e.Datalength;
            while (bytesToRead > 0)
            {
                var bytesRead = stream.Read(buffer, 0, (int)Math.Min(bytesToRead, buffer.Length));
                bytesToRead -= bytesRead;
                testData.Append(System.Text.Encoding.UTF8.GetString(buffer).TrimEnd('\0'));
            }

            allData = e;
        }

        private void Protocol_DataFoundEvent(object sender, FCP2.EventArgs.DataFoundEventArgs e)
        {
            Trace.WriteLine("Protocol_DataFoundEvent");

            dataFound = e;
        }
    }
}
