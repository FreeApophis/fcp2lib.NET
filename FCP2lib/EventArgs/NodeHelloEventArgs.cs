/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2016 Thomas Bruderer <apophis@apophis.ch>
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

using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class NodeHelloEventArgs : System.EventArgs
    {
        public string ConnectionIdentifier { get; }
        public string FcpVersion { get; }
        public string Version { get; }
        public string Node { get; }
        public string NodeLanguage { get; }
        public long ExtBuild { get; }
        public long ExtRevision { get; }
        public long Build { get; }
        public string Revision { get; }
        public bool Testnet { get; }
        public string CompressionCodecs { get; }

        /// <summary>
        /// NodeHelloEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal NodeHelloEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            ConnectionIdentifier = parsed.ConnectionIdentifier;
            FcpVersion = parsed.FCPVersion;
            Version = parsed.Version;
            Node = parsed.Node;
            NodeLanguage = parsed.NodeLanguage;
            ExtBuild = parsed.ExtBuild;
            ExtRevision = parsed.ExtRevision;
            Build = parsed.Build;
            Revision = parsed.Revision;
            Testnet = parsed.Testnet;
            CompressionCodecs = parsed.CompressionCodecs;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }
    }
}