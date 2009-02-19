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
        
    }
    
    public class CloseConnectionDuplicateClientNameEventArgs : EventArgs {
        
    }
    
    public class PeerEventArgs : EventArgs {
        
    }
    
    public class PeerNoteEventArgs : EventArgs {
        
    }
    
    public class EndListPeersEventArgs : EventArgs {
        
    }
    
    public class EndListPeerNotesEventArgs : EventArgs {
        
    }
    
    public class PeerRemovedEventArgs : EventArgs {
        
    }
    
    public class NodeDataEventArgs : EventArgs {
        
    }
    
    public class ConfigDataEventArgs : EventArgs {
        
    }
    
    public class TestDDAReplyEventArgs : EventArgs {
        
    }
    
    public class TestDDACompleteEventArgs : EventArgs {
        
    }
    
    public class SSKKeypairEventArgs : EventArgs {
        
    }
    
    public class PersistentGetEventArgs : EventArgs {
        
    }
    
    public class PersistentPutEventArgs : EventArgs {
        
    }
    
    public class PersistentPutDirEventArgs : EventArgs {
        
    }
    
    public class URIGeneratedEventArgs : EventArgs {
        
    }

    public class PutSuccessfulEventArgs : EventArgs {
        
    }

    public class PutFetchableEventArgs : EventArgs {
        
    }

    public class DataFoundEventArgs : EventArgs {
        
    }

    public class AllDataEventArgs : EventArgs {
        
    }

    public class StartedCompressionEventArgs : EventArgs {
        
    }

    public class FinishedCompressionEventArgs : EventArgs {
        
    }

    public class SimpleProgressEventArgs : EventArgs {
        
    }

    public class EndListPersistentRequestsEventArgs : EventArgs {
        
    }
    
    public class PersistentRequestRemovedEventArgs : EventArgs {
        
    }
    
    public class PersistentRequestModifiedEventArgs : EventArgs {
        
    }

    public class PutFailedEventArgs : EventArgs {
        
    }

    public class GetFailedEventArgs : EventArgs {
        
    }

    public class ProtocolErrorEventArgs : EventArgs {
        
    }

    public class IdentifierCollisionEventArgs : EventArgs {
        
    }

    public class UnknownNodeIdentifierEventArgs : EventArgs {
        
    }

    public class UnknownPeerNoteTypeEventArgs : EventArgs {
        
    }

    public class SubscribedUSKEventArgs : EventArgs {
        
    }

    public class SubscribedUSKUpdateEventArgs : EventArgs {
        
    }

    public class PluginInfoEventArgs : EventArgs {
        
    }

    public class FCPPluginReplyEventArgs : EventArgs {
        
    }
}