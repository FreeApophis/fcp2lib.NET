/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009 Thomas Bruderer <apophis@apophis.ch>
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
 
using System;

namespace Freenet.FCP2 {
    
    public class NodeHelloEventArgs : EventArgs {
        private string connectionIdentifier;
        
        public string ConnectionIdentifier {
            get { return connectionIdentifier; }
        }
        private string fcpVersion;
        
        public string FcpVersion {
            get { return fcpVersion; }
        }
        private string version;
        
        public string Version {
            get { return version; }
        }
        private string node;
        
        public string Node {
            get { return node; }
        }
        private string nodeLanguage;
        
        public string NodeLanguage {
            get { return nodeLanguage; }
        }
        private long extBuild;
        
        public long ExtBuild {
            get { return extBuild; }
        }
        private long extRevision;
        
        public long ExtRevision {
            get { return extRevision; }
        }
        private long build;
        
        public long Build {
            get { return build; }
        }
        private string revision;
        
        public string Revision {
            get { return revision; }
        }
        private bool testnet;
        
        public bool Testnet {
            get { return testnet; }
        }
        private string compressionCodecs;
        
        public string CompressionCodecs {
            get { return compressionCodecs; }
        }
        
        /// <summary>
        /// NodeHelloEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal NodeHelloEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.connectionIdentifier = parsed["ConnectionIdentifier"];
            this.fcpVersion = parsed["FCPVersion"];
            this.version =  parsed["Version"];
            this.node = parsed["Node"];
            this.nodeLanguage = parsed["NodeLanguage"];
            this.extBuild = long.Parse(parsed["ExtBuild"]);
            this.extRevision = long.Parse(parsed["ExtRevision"]);
            this.build = long.Parse(parsed["Build"]);
            this.revision = parsed["Revision"];
            this.testnet = bool.Parse(parsed["Testnet"]);
            this.compressionCodecs = parsed["CompressionCodecs"];
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}