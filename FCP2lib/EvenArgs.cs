﻿/*
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
        public NodeHelloEventArgs(MessageParser parsed) {
            this.connectionIdentifier = parsed["ConnectionIdentifier"];
            this.fcpVersion = parsed["ConnectionIdentifier"];
            this.version =  parsed["Version"];
            this.node = parsed["Node"];
            this.nodeLanguage = parsed["NodeLanguage"];
            this.extBuild = int.Parse(parsed["ExtBuild"]);
            this.extRevision = int.Parse(parsed["ExtRevision"]);
            this.build = int.Parse(parsed["Build"]);
            this.revision = int.Parse(parsed["Revision"]);
            this.testnet = bool.Parse(parsed["Testnet"]);
            this.compressionCodecs = int.Parse(parsed["CompressionCodecs"]);
        }
    }
    
    public class CloseConnectionDuplicateClientNameEventArgs : EventArgs {

        /// <summary>
        /// CloseConnectionDuplicateClientNameEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public CloseConnectionDuplicateClientNameEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PeerEventArgs : EventArgs {
        
        /// <summary>
        /// PeerEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PeerEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PeerNoteEventArgs : EventArgs {
        
        /// <summary>
        /// PeerNoteEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PeerNoteEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class EndListPeersEventArgs : EventArgs {
        
        /// <summary>
        /// EndListPeersEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public EndListPeersEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class EndListPeerNotesEventArgs : EventArgs {
        
        /// <summary>
        /// EndListPeerNotesEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public EndListPeerNotesEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PeerRemovedEventArgs : EventArgs {
        
        /// <summary>
        /// PeerRemovedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PeerRemovedEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class NodeDataEventArgs : EventArgs {
        
        /// <summary>
        /// NodeDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public NodeDataEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class ConfigDataEventArgs : EventArgs {
        
        /// <summary>
        /// ConfigDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public ConfigDataEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class TestDDAReplyEventArgs : EventArgs {
        
        /// <summary>
        /// TestDDAReplyEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public TestDDAReplyEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class TestDDACompleteEventArgs : EventArgs {
        
        /// <summary>
        /// TestDDACompleteEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public TestDDACompleteEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class SSKKeypairEventArgs : EventArgs {
        
        /// <summary>
        /// SSKKeypairEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public SSKKeypairEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PersistentGetEventArgs : EventArgs {
        
        /// <summary>
        /// PersistentGetEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PersistentGetEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PersistentPutEventArgs : EventArgs {
        
        /// <summary>
        /// PersistentPutEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PersistentPutEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PersistentPutDirEventArgs : EventArgs {
        
        /// <summary>
        /// PersistentPutDirEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PersistentPutDirEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class URIGeneratedEventArgs : EventArgs {
        
        /// <summary>
        /// URIGeneratedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public URIGeneratedEventArgs(MessageParser parsed) {
            
        }
    }

    public class PutSuccessfulEventArgs : EventArgs {
        
        /// <summary>
        /// PutSuccessfulEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PutSuccessfulEventArgs(MessageParser parsed) {
            
        }
    }

    public class PutFetchableEventArgs : EventArgs {
        
        /// <summary>
        /// PutFetchableEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PutFetchableEventArgs(MessageParser parsed) {
            
        }
    }

    public class DataFoundEventArgs : EventArgs {
        
         /// <summary>
        /// DataFoundEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public DataFoundEventArgs(MessageParser parsed) {
            
        }
   }

    public class AllDataEventArgs : EventArgs {
        
        /// <summary>
        /// AllDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public AllDataEventArgs(MessageParser parsed) {
            
        }
    }

    public class StartedCompressionEventArgs : EventArgs {
        
        /// <summary>
        /// StartedCompressionEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public StartedCompressionEventArgs(MessageParser parsed) {
            
        }
    }

    public class FinishedCompressionEventArgs : EventArgs {
        
        /// <summary>
        /// FinishedCompressionEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public FinishedCompressionEventArgs(MessageParser parsed) {
            
        }
    }

    public class SimpleProgressEventArgs : EventArgs {
        
        /// <summary>
        /// SimpleProgressEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public SimpleProgressEventArgs(MessageParser parsed) {
            
        }
    }

    public class EndListPersistentRequestsEventArgs : EventArgs {
        
        /// <summary>
        /// EndListPersistentRequestsEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public EndListPersistentRequestsEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PersistentRequestRemovedEventArgs : EventArgs {
        
        /// <summary>
        /// PersistentRequestRemovedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PersistentRequestRemovedEventArgs(MessageParser parsed) {
            
        }
    }
    
    public class PersistentRequestModifiedEventArgs : EventArgs {
        
        /// <summary>
        /// PersistentRequestModifiedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PersistentRequestModifiedEventArgs(MessageParser parsed) {
            
        }
    }

    public class PutFailedEventArgs : EventArgs {
        
        /// <summary>
        /// PutFailedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PutFailedEventArgs(MessageParser parsed) {
            
        }
    }

    public class GetFailedEventArgs : EventArgs {
        
        /// <summary>
        /// GetFailedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public GetFailedEventArgs(MessageParser parsed) {
            
        }
    }

    public class ProtocolErrorEventArgs : EventArgs {
        
        /// <summary>
        /// ProtocolErrorEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public ProtocolErrorEventArgs(MessageParser parsed) {
            
        }
    }

    public class IdentifierCollisionEventArgs : EventArgs {
        
        /// <summary>
        /// IdentifierCollisionEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public IdentifierCollisionEventArgs(MessageParser parsed) {
            
        }
    }

    public class UnknownNodeIdentifierEventArgs : EventArgs {
        
        /// <summary>
        /// UnknownNodeIdentifierEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public UnknownNodeIdentifierEventArgs(MessageParser parsed) {
            
        }
    }

    public class UnknownPeerNoteTypeEventArgs : EventArgs {
        
        /// <summary>
        /// UnknownPeerNoteTypeEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public UnknownPeerNoteTypeEventArgs(MessageParser parsed) {
            
        }
    }

    public class SubscribedUSKEventArgs : EventArgs {
        
        /// <summary>
        /// SubscribedUSKEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public SubscribedUSKEventArgs(MessageParser parsed) {
            
        }
    }

    public class SubscribedUSKUpdateEventArgs : EventArgs {
        
        /// <summary>
        /// SubscribedUSKUpdateEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public SubscribedUSKUpdateEventArgs(MessageParser parsed) {
            
        }
    }

    public class PluginInfoEventArgs : EventArgs {
        
        /// <summary>
        /// PluginInfoEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public PluginInfoEventArgs(MessageParser parsed) {
            
        }
    }

    public class FCPPluginReplyEventArgs : EventArgs {
        
        /// <summary>
        /// FCPPluginReplyEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        public FCPPluginReplyEventArgs(MessageParser parsed) {
            
        }
    }
}