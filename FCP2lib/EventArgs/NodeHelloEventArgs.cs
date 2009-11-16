using System;

namespace Freenet.FCP2
{

    public class NodeHelloEventArgs : EventArgs
    {
        private readonly long build;
        private readonly string compressionCodecs;
        private readonly string connectionIdentifier;
        private readonly long extBuild;
        private readonly long extRevision;
        private readonly string fcpVersion;
        private readonly string node;
        private readonly string nodeLanguage;
        private readonly string revision;
        private readonly bool testnet;
        private readonly string version;

        /// <summary>
        /// NodeHelloEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal NodeHelloEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            connectionIdentifier = parsed["ConnectionIdentifier"];
            fcpVersion = parsed["FCPVersion"];
            version = parsed["Version"];
            node = parsed["Node"];
            nodeLanguage = parsed["NodeLanguage"];
            extBuild = long.Parse(parsed["ExtBuild"]);
            extRevision = long.Parse(parsed["ExtRevision"]);
            build = long.Parse(parsed["Build"]);
            revision = parsed["Revision"];
            testnet = bool.Parse(parsed["Testnet"]);
            compressionCodecs = parsed["CompressionCodecs"];

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string ConnectionIdentifier
        {
            get { return connectionIdentifier; }
        }

        public string FcpVersion
        {
            get { return fcpVersion; }
        }

        public string Version
        {
            get { return version; }
        }

        public string Node
        {
            get { return node; }
        }

        public string NodeLanguage
        {
            get { return nodeLanguage; }
        }

        public long ExtBuild
        {
            get { return extBuild; }
        }

        public long ExtRevision
        {
            get { return extRevision; }
        }

        public long Build
        {
            get { return build; }
        }

        public string Revision
        {
            get { return revision; }
        }

        public bool Testnet
        {
            get { return testnet; }
        }

        public string CompressionCodecs
        {
            get { return compressionCodecs; }
        }
    }
}