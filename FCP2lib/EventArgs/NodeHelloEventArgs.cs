/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2014 Thomas Bruderer <apophis@apophis.ch>
 *  Copyright (c) 2009 Felipe Barriga Richards
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace FCP2
{

    public class NodeHelloEventArgs : System.EventArgs
    {
        readonly long build;
        readonly string compressionCodecs;
        readonly string connectionIdentifier;
        readonly long extBuild;
        readonly long extRevision;
        readonly string fcpVersion;
        readonly string node;
        readonly string nodeLanguage;
        readonly string revision;
        readonly bool testnet;
        readonly string version;

        /// <summary>
        /// NodeHelloEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal NodeHelloEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            connectionIdentifier = parsed.ConnectionIdentifier;
            fcpVersion = parsed.FCPVersion;
            version = parsed.Version;
            node = parsed.Node;
            nodeLanguage = parsed.NodeLanguage;
            extBuild = parsed.ExtBuild;
            extRevision = parsed.ExtRevision;
            build = parsed.Build;
            revision = parsed.Revision;
            testnet = parsed.Testnet;
            compressionCodecs = parsed.CompressionCodecs;

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