/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009 Thomas Bruderer <apophis@apophis.ch>
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
        private int extBuild;
        
        public int ExtBuild {
            get { return extBuild; }
        }
        private int extRevision;
        
        public int ExtRevision {
            get { return extRevision; }
        }
        private int build;
        
        public int Build {
            get { return build; }
        }
        private int revision;
        
        public int Revision {
            get { return revision; }
        }
        private bool testnet;
        
        public bool Testnet {
            get { return testnet; }
        }
        private int compressionCodecs;
        
        public int CompressionCodecs {
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
            this.extBuild = int.Parse(parsed["ExtBuild"]);
            this.extRevision = int.Parse(parsed["ExtRevision"]);
            this.build = int.Parse(parsed["Build"]);
            this.revision = int.Parse(parsed["Revision"]);
            this.testnet = bool.Parse(parsed["Testnet"]);
            this.compressionCodecs = int.Parse(parsed["CompressionCodecs"]);
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
    }
}