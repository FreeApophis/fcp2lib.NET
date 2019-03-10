using System;
using System.Diagnostics;
using FCP2.Protocol;
using Xunit;

namespace FCP2.Test
{
    public class TestConnect
    {
        bool _hello;
        EventArgs.NodeHelloEventArgs _simpleResult;

        [Fact]
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

            Assert.True(_hello);

            Assert.Equal("2.0", _simpleResult?.FcpVersion);
            Assert.Equal("Fred", _simpleResult?.Node);

            protocol.Disconnect();
        }

        private void Protocol_NodeHelloEvent(object sender, EventArgs.NodeHelloEventArgs e)
        {
            _simpleResult = e;
            _hello = true;
        }

        EventArgs.DataFoundEventArgs _dataFound;
        EventArgs.AllDataEventArgs _allData;
        readonly System.Text.StringBuilder _testData = new System.Text.StringBuilder();


        [Fact]
        public void Test2()
        {
            var protocol = new FCP2Protocol("test-client");
            protocol.ClientHello();

            protocol.DataFoundEvent += Protocol_DataFoundEvent;
            protocol.AllDataEvent += Protocol_AllDataEvent;

            const string testIdentifier = "test-file";
            protocol.ClientGet("CHK@khy~PN5-P-Ze18SdPfb~eMVCcWv1RTDNaP3ketO3TwQ,-kcC5FBaNN7wZtcHgZGObkxk9221BL5r~wbbVTUhyHc,AAMC--8/test.txt", testIdentifier);

            int retry = 1000;
            while (--retry > 0 && !Ready())
            {
                System.Threading.Thread.Sleep(100);
            }

            const int testSize = 16;
            const string testContentType = "text/plain";
            const string testData = "this is a test\r\n";

            Assert.NotNull(_allData);
            Assert.NotNull(_dataFound);

            Assert.Equal(testSize, _dataFound.DataLength);
            Assert.Equal(testIdentifier, _dataFound.Identifier);
            Assert.False(_dataFound.Global);
            Assert.Equal(testContentType, _dataFound.ContentType);

            Assert.True(_allData.StartupTime <= _allData.CompletionTime);

            Assert.Equal(testSize, _allData.DataLength);
            Assert.Equal(testIdentifier, _allData.Identifier);
            Assert.Equal(testData, _testData.ToString());

            protocol.Disconnect();
        }

        private bool Ready()
        {
            return _allData != null && _dataFound != null;
        }

        private void Protocol_AllDataEvent(object sender, EventArgs.AllDataEventArgs e)
        {
            Trace.WriteLine("Protocol_AllDataEvent");
            var stream = e.GetStream();
            var buffer = new byte[1024];

            var bytesToRead = e.DataLength;
            while (bytesToRead > 0)
            {
                var bytesRead = stream.Read(buffer, 0, (int)Math.Min(bytesToRead, buffer.Length));
                bytesToRead -= bytesRead;
                _testData.Append(System.Text.Encoding.UTF8.GetString(buffer).TrimEnd('\0'));
            }

            _allData = e;
        }

        private void Protocol_DataFoundEvent(object sender, EventArgs.DataFoundEventArgs e)
        {
            Trace.WriteLine("Protocol_DataFoundEvent");

            _dataFound = e;
        }
    }
}
