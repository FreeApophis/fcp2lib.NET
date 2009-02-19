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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Freenet.FCP2
{
    /// <summary>
    /// FCP 2.0 Protocol implementation according to following RFC:
    /// http://wiki.freenetproject.org/FreenetFCPSpec2Point0
    /// </summary>
    public class FCP2
    {
        #region Private declarations
        private IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9481);
        private TcpClient client = new TcpClient();
        private TextReader fnread;
        private TextWriter fnwrite;
        
        private string clientName;
        private const string fcpVersion = "2.0";
        #endregion
        
        public const string endMessage = "EndMessage";

        
        #region Event Handler
        public event EventHandler<NodeHelloEventArgs>                           NodeHelloEvent;
        public event EventHandler<CloseConnectionDuplicateClientNameEventArgs>  CloseConnectionDuplicateClientNameEvent;
        public event EventHandler<PeerEventArgs>                                PeerEvent;
        public event EventHandler<PeerNoteEventArgs>                            PeerNoteEvent;
        public event EventHandler<EndListPeersEventArgs>                        EndListPeersEvent;
        public event EventHandler<EndListPeerNotesEventArgs>                    EndListPeerNotesEvent;
        public event EventHandler<PeerRemovedEventArgs>                         PeerRemovedEvent;
        public event EventHandler<NodeDataEventArgs>                            NodeDataEvent;
        public event EventHandler<ConfigDataEventArgs>                          ConfigDataEvent;
        public event EventHandler<TestDDAReplyEventArgs>                        TestDDAReplyEvent;
        public event EventHandler<TestDDACompleteEventArgs>                     TestDDACompleteEvent;
        public event EventHandler<SSKKeypairEventArgs>                          SSKKeypairEvent;
        public event EventHandler<PersistentGetEventArgs>                       PersistentGetEvent;
        public event EventHandler<PersistentPutEventArgs>                       PersistentPutEvent;
        public event EventHandler<PersistentPutDirEventArgs>                    PersistentPutDirEvent;
        public event EventHandler<URIGeneratedEventArgs>                        URIGeneratedEvent;
        public event EventHandler<PutSuccessfulEventArgs>                       PutSuccessfulEvent;
        public event EventHandler<PutFetchableEventArgs>                        PutFetchableEvent;
        public event EventHandler<DataFoundEventArgs>                           DataFoundEvent;
        public event EventHandler<AllDataEventArgs>                             AllDataEvent;
        public event EventHandler<StartedCompressionEventArgs>                  StartedCompressionEvent;
        public event EventHandler<FinishedCompressionEventArgs>                 FinishedCompressionEvent;
        public event EventHandler<SimpleProgressEventArgs>                      SimpleProgressEvent;
        public event EventHandler<EndListPersistentRequestsEventArgs>           EndListPersistentRequestsEvent;
        public event EventHandler<PersistentRequestRemovedEventArgs>            PersistentRequestRemovedEvent;
        public event EventHandler<PersistentRequestModifiedEventArgs>           PersistentRequestModifiedEvent;
        public event EventHandler<PutFailedEventArgs>                           PutFailedEvent;
        public event EventHandler<GetFailedEventArgs>                           GetFailedEvent;
        public event EventHandler<ProtocolErrorEventArgs>                       ProtocolErrorEvent;
        public event EventHandler<IdentifierCollisionEventArgs>                 IdentifierCollisionEvent;
        public event EventHandler<UnknownNodeIdentifierEventArgs>               UnknownNodeIdentifierEvent;
        public event EventHandler<UnknownPeerNoteTypeEventArgs>                 UnknownPeerNoteTypeEvent;
        public event EventHandler<SubscribedUSKEventArgs>                       SubscribedUSKEvent;
        public event EventHandler<SubscribedUSKUpdateEventArgs>                 SubscribedUSKUpdateEvent;
        public event EventHandler<PluginInfoEventArgs>                          PluginInfoEvent;
        public event EventHandler<FCPPluginReplyEventArgs>                      FCPPluginReplyEvent;
        #endregion
        
        #region EventInvocation
        protected virtual void OnNodeHelloEvent(NodeHelloEventArgs e)
        {
            EventHandler<NodeHelloEventArgs> handler = NodeHelloEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnCloseConnectionDuplicateClientNameEvent(CloseConnectionDuplicateClientNameEventArgs e)
        {
            EventHandler<CloseConnectionDuplicateClientNameEventArgs> handler = CloseConnectionDuplicateClientNameEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPeerEvent(PeerEventArgs e)
        {
            EventHandler<PeerEventArgs> handler = PeerEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPeerNoteEvent(PeerNoteEventArgs e)
        {
            EventHandler<PeerNoteEventArgs> handler = PeerNoteEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnEndListPeersEvent(EndListPeersEventArgs e)
        {
            EventHandler<EndListPeersEventArgs> handler = EndListPeersEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnEndListPeerNotesEvent(EndListPeerNotesEventArgs e)
        {
            EventHandler<EndListPeerNotesEventArgs> handler = EndListPeerNotesEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPeerRemovedEvent(PeerRemovedEventArgs e)
        {
            EventHandler<PeerRemovedEventArgs> handler = PeerRemovedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        
        protected virtual void OnNodeDataEvent(NodeDataEventArgs e)
        {
            EventHandler<NodeDataEventArgs> handler = NodeDataEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnConfigDataEvent(ConfigDataEventArgs e)
        {
            EventHandler<ConfigDataEventArgs> handler = ConfigDataEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnTestDDAReplyEvent(TestDDAReplyEventArgs e)
        {
            EventHandler<TestDDAReplyEventArgs> handler = TestDDAReplyEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnTestDDACompleteEvent(TestDDACompleteEventArgs e)
        {
            EventHandler<TestDDACompleteEventArgs> handler = TestDDACompleteEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnSSKKeypairEvent(SSKKeypairEventArgs e)
        {
            EventHandler<SSKKeypairEventArgs> handler = SSKKeypairEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPersistentGetEvent(PersistentGetEventArgs e)
        {
            EventHandler<PersistentGetEventArgs> handler = PersistentGetEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPersistentPutEvent(PersistentPutEventArgs e)
        {
            EventHandler<PersistentPutEventArgs> handler = PersistentPutEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPersistentPutDirEvent(PersistentPutDirEventArgs e)
        {
            EventHandler<PersistentPutDirEventArgs> handler = PersistentPutDirEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnURIGeneratedEvent(URIGeneratedEventArgs e)
        {
            EventHandler<URIGeneratedEventArgs> handler = URIGeneratedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPutSuccessfulEvent(PutSuccessfulEventArgs e)
        {
            EventHandler<PutSuccessfulEventArgs> handler = PutSuccessfulEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPutFetchableEvent(PutFetchableEventArgs e)
        {
            EventHandler<PutFetchableEventArgs> handler = PutFetchableEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnDataFoundEvent(DataFoundEventArgs e)
        {
            EventHandler<DataFoundEventArgs> handler = DataFoundEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnAllDataEvent(AllDataEventArgs e)
        {
            EventHandler<AllDataEventArgs> handler = AllDataEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        
        protected virtual void OnStartedCompressionEvent(StartedCompressionEventArgs e)
        {
            EventHandler<StartedCompressionEventArgs> handler = StartedCompressionEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnFinishedCompressionEvent(FinishedCompressionEventArgs e)
        {
            EventHandler<FinishedCompressionEventArgs> handler = FinishedCompressionEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnSimpleProgressEvent(SimpleProgressEventArgs e)
        {
            EventHandler<SimpleProgressEventArgs> handler = SimpleProgressEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnEndListPersistentRequestsEvent(EndListPersistentRequestsEventArgs e)
        {
            EventHandler<EndListPersistentRequestsEventArgs> handler = EndListPersistentRequestsEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPersistentRequestRemovedEvent(PersistentRequestRemovedEventArgs e)
        {
            EventHandler<PersistentRequestRemovedEventArgs> handler = PersistentRequestRemovedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPersistentRequestModifiedEvent(PersistentRequestModifiedEventArgs e)
        {
            EventHandler<PersistentRequestModifiedEventArgs> handler = PersistentRequestModifiedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPutFailedEvent(PutFailedEventArgs e)
        {
            EventHandler<PutFailedEventArgs> handler = PutFailedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnGetFailedEvent(GetFailedEventArgs e)
        {
            EventHandler<GetFailedEventArgs> handler = GetFailedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnProtocolErrorEvent(ProtocolErrorEventArgs e)
        {
            EventHandler<ProtocolErrorEventArgs> handler = ProtocolErrorEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnIdentifierCollisionEvent(IdentifierCollisionEventArgs e)
        {
            EventHandler<IdentifierCollisionEventArgs> handler = IdentifierCollisionEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnUnknownNodeIdentifierEvent(UnknownNodeIdentifierEventArgs e)
        {
            EventHandler<UnknownNodeIdentifierEventArgs> handler = UnknownNodeIdentifierEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnUnknownPeerNoteTypeEvent(UnknownPeerNoteTypeEventArgs e)
        {
            EventHandler<UnknownPeerNoteTypeEventArgs> handler = UnknownPeerNoteTypeEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnSubscribedUSKEvent(SubscribedUSKEventArgs e)
        {
            EventHandler<SubscribedUSKEventArgs> handler = SubscribedUSKEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnSubscribedUSKUpdateEvent(SubscribedUSKUpdateEventArgs e)
        {
            EventHandler<SubscribedUSKUpdateEventArgs> handler = SubscribedUSKUpdateEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnPluginInfoEvent(PluginInfoEventArgs e)
        {
            EventHandler<PluginInfoEventArgs> handler = PluginInfoEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        
        protected virtual void OnFCPPluginReplyEvent(FCPPluginReplyEventArgs e)
        {
            EventHandler<FCPPluginReplyEventArgs> handler = FCPPluginReplyEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion
        
        public FCP2(IPEndPoint nodeAdress, string clientName) {
            this.ep = nodeAdress;
            this.clientName = clientName;
        }

        public FCP2(string clientName) {
            this.clientName = clientName;
        }
        
        /// <summary>
        /// Connects and sends a ClientHello
        /// </summary>
        /// <returns>returns false if we are already connected</returns>
        private bool ConnectIfNeeded()
        {
            if(client.Connected) {
                return false;
            }
            
            client.Connect(ep);

            fnread = new StreamReader(client.GetStream(), System.Text.ASCIIEncoding.UTF8);
            fnwrite = new StreamWriter(client.GetStream(), System.Text.ASCIIEncoding.UTF8);
            
            RealClientHello(clientName, fcpVersion);
            return true;

        }

        #region Parser
        private void MessageParser() {
            string line;
            while(client.Connected) {
                line = fnread.ReadLine();
                switch(line) {
                        case "NodeHello": parseNodeHello();
                        break;
                    case "CloseConnectionDuplicateClientName":
                        break;
                    case "Peer":
                        break;
                    case "PeerNote":
                        break;
                    case "EndListPeers":
                        break;
                    case "EndListPeerNotes":
                        break;
                    case "PeerRemoved":
                        break;
                    case "NodeData":
                        break;
                    case "ConfigData":
                        break;
                    case "TestDDAReply":
                        break;
                    case "TestDDAComplete":
                        break;
                    case "SSKKeypair":
                        break;
                    case "PersistentGet":
                        break;
                    case "PersistentPut":
                        break;
                    case "PersistentPutDir":
                        break;
                    case "URIGenerated":
                        break;
                    case "PutSuccessful":
                        break;
                    case "PutFetchable":
                        break;
                    case "DataFound":
                        break;
                    case "AllData":
                        break;
                    case "StartedCompression":
                        break;
                    case "FinishedCompression":
                        break;
                    case "SimpleProgress":
                        break;
                    case "EndListPersistentRequests":
                        break;
                    case "PersistentRequestRemoved":
                        break;
                    case "PersistentRequestModified":
                        break;
                    case "PutFailed":
                        break;
                    case "GetFailed":
                        break;
                    case "ProtocolError":
                        break;
                    case "IdentifierCollision":
                        break;
                    case "UnknownNodeIdentifier":
                        break;
                    case "UnknownPeerNoteType":
                        break;
                    case "SubscribedUSK":
                        break;
                    case "SubscribedUSKUpdate":
                        break;
                    case "PluginInfo":
                        break;
                    case "FCPPluginReply":
                        break;
                    default:
                        throw new ArgumentException("unknown message from freenet node");
                }
            }
        }
        
        public void parseNodeHello() {
            MessageParser parsed = new MessageParser(fnread);
            OnNodeHelloEvent(new NodeHelloEventArgs());
        }
        #endregion
        
        /// <summary>
        /// This must be the first message from the client on any given connection.
        /// The node will respond with a NodeHello Event.
        /// </summary>
        /// <param name="name">A name to uniquely identify the client to the node. This is
        /// used for persistence, so a client can see the same local queue if it disconnects
        /// and then reconnects. If a connection is attempted with the same name as an existing
        /// connection, you will get an error</param>
        /// <param name="expectedVersion">The version of FCP to expect. In this case it will always
        /// be 2.0, but in the future clients may want to check if the node supports later versions.
        /// We do not do anything with ExpectedVersion yet, but it may be verified in the future.</param>
        private void RealClientHello(string name, string expectedVersion) {
            /* this will be called automatically on every Connection start */
            
            fnwrite.WriteLine("ClientHello");
            fnwrite.WriteLine("Name=" + name);
            fnwrite.WriteLine("ExpectedVersion=" + expectedVersion);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        

        
        #region public interface
        /// <summary>
        /// This must be the first message from the client on any given connection.
        /// The node will respond with a NodeHello Event.
        /// </summary>
        /// <param name="name">A name to uniquely identify the client to the node. This is
        /// used for persistence, so a client can see the same local queue if it disconnects
        /// and then reconnects. If a connection is attempted with the same name as an existing
        /// connection, you will get an error</param>
        private void ClientHello(string name) {
            clientName = name;
            if(!ConnectIfNeeded()) {
                RealClientHello(name, fcpVersion);
            }
        }
        
        /// <summary>
        /// This must be the first message from the client on any given connection.
        /// The node will respond with a NodeHello Event.
        /// </summary>
        private void ClientHello() {
            ClientHello(clientName);
        }
        
        /// <summary>
        /// This message asks the Freenet node for the details of a given Freenet connected directly to it (peer).
        /// </summary>
        /// <param name="nodeIdentifier">The node name (except for opennet peers), identity or IP:port pair of the peer to be modified.</param>
        /// <param name="withMetadata">When true, additional metadata is added to the output of the Peer reply sent by the node.</param>
        /// <param name="withVolatile">When true, additional volatile data is added to the output of the Peer reply sent by the node.</param>
        public void ListPeer(string nodeIdentifier, bool? withMetadata, bool? withVolatile) {
            ConnectIfNeeded();
            fnwrite.WriteLine("ListPeer");
            fnwrite.WriteLine("NodeIdentifier=" + nodeIdentifier);
            if(withMetadata!=null)
                fnwrite.WriteLine("WithMetadata=true" + withMetadata.Value.ToString());
            if(withVolatile!=null)
                fnwrite.WriteLine("WithVolatile=" + withVolatile.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// This message asks the Freenet node for the details of a given Freenet connected directly to it (peer).
        /// </summary>
        /// <param name="nodeIdentifier">The node name (except for opennet peers), identity or IP:port pair of the peer to be modified.</param>
        public void ListPeer(string nodeIdentifier) {
            ListPeer(nodeIdentifier, null, null);
        }

        /// <summary>
        /// This message asks the Freenet node for a list of other Freenet nodes connected directly to you (peers).
        /// </summary>
        /// <param name="identifier">Identifier for the request; this lets you match the Peer reply to this request.</param>
        /// <param name="withMetadata">If set true, the metadata subtree be included in the returned fieldset for each peer</param>
        /// <param name="withVolatile">If set true, the volatile status information will be included in the returned fieldset for each peer</param>
        public void ListPeers(string identifier, bool? withMetadata, bool? withVolatile) {
            ConnectIfNeeded();
            fnwrite.WriteLine("ListPeers");
            if(!String.IsNullOrEmpty(identifier))
                fnwrite.WriteLine("Identifier=" + identifier);
            if(withMetadata!=null)
                fnwrite.WriteLine("WithMetadata=true" + withMetadata.Value.ToString());
            if(withVolatile!=null)
                fnwrite.WriteLine("WithVolatile=" + withVolatile.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// This message asks the Freenet node for a list of other Freenet nodes connected directly to you (peers).
        /// </summary>
        public void ListPeers() {
            ListPeers(null, null, null);
        }
        
        /// <summary>
        /// This message lists the peer notes for a given peer of your Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to list the peer notes of.</param>
        public void ListPeerNotes(string nodeIdentifier) {
            ConnectIfNeeded();
            fnwrite.WriteLine("ListPeerNotes");
            fnwrite.WriteLine("NodeIdentifier=" + nodeIdentifier);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        ///  It adds a peer to the Freenet node. The message should contain only one of a File,
        ///  URL or a raw Node reference. Your own node reference is found in a file called
        ///  node-port, where port is the UDP port number than Freenet is using. The node references
        ///  of your peers are found in the file called peers-port. The node will respond with
        ///  a Peer message when the peer was added successfully, or a ProtocolError message
        ///  if the peer could not be added.
        /// </summary>
        /// <param name="file">The peer noderef is read from the given local filename.</param>
        public void AddPeer(FileInfo file) {
            ConnectIfNeeded();
            fnwrite.WriteLine("AddPeer");
            fnwrite.WriteLine("File=" + file.FullName);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        ///  It adds a peer to the Freenet node. The message should contain only one of a File,
        ///  URL or a raw Node reference. Your own node reference is found in a file called
        ///  node-port, where port is the UDP port number than Freenet is using. The node references
        ///  of your peers are found in the file called peers-port. The node will respond with
        ///  a Peer message when the peer was added successfully, or a ProtocolError message
        ///  if the peer could not be added.
        /// </summary>
        /// <param name="uri">The peers noderef is read from the given URI.</param>
        public void AddPeer(Uri uri) {
            ConnectIfNeeded();
            fnwrite.WriteLine("AddPeer");
            fnwrite.WriteLine("URL=" + uri.AbsoluteUri);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        /// <summary>
        ///  It adds a peer to the Freenet node. The message should contain only one of a File,
        ///  URL or a raw Node reference. Your own node reference is found in a file called
        ///  node-port, where port is the UDP port number than Freenet is using. The node references
        ///  of your peers are found in the file called peers-port. The node will respond with
        ///  a Peer message when the peer was added successfully, or a ProtocolError message
        ///  if the peer could not be added.
        /// </summary>
        /// <param name="nodeRef">The peers plain noderef as a string</param>
        public void AddPeer(string nodeRef) {
            ConnectIfNeeded();
            fnwrite.WriteLine("AddPeer");
            fnwrite.WriteLine(nodeRef);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        
        /// <summary>
        /// This message modifies settings for a given peer of your Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to be modified.</param>
        /// <param name="allowLocalAdresses">If set, the peer identified by the given NodeIdentifier is set allowLocalAddresses or not accordingly.</param>
        /// <param name="isDisabled">If set, the peer identified by the given NodeIdentifier is enabled or disabled accordingly.</param>
        /// <param name="isListenOnly">If set, the peer identified by the given NodeIdentifier is set ListenOnly or not accordingly.</param>
        public void ModifyPeer(string nodeIdentifier, bool? allowLocalAdresses, bool? isDisabled, bool? isListenOnly) {
            ConnectIfNeeded();
            fnwrite.WriteLine("ModifyPeer");
            fnwrite.WriteLine("NodeIdentifier=" + nodeIdentifier);
            if (allowLocalAdresses!=null)
                fnwrite.WriteLine("AllowLocalAdresses=" + allowLocalAdresses.Value.ToString());
            if (isDisabled!=null)
                fnwrite.WriteLine("IsDisabled=" + isDisabled.Value.ToString());
            if (isListenOnly!=null)
                fnwrite.WriteLine("IsListenOnly=" + isListenOnly.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// This message modifies settings for a given peer of your Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to be modified.</param>
        public void ModifyPeer(string nodeIdentifier) {
            ModifyPeer(nodeIdentifier, null, null, null);
        }
        
        /// <summary>
        /// This message modifies a peer note for a given peer of your Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to be modified.</param>
        /// <param name="noteText">String of the note text to set the specified peer note to.</param>
        /// <param name="peerNoteType">Specify the type of the peer note, by code number (currently, may change in the future). Type codes are: 1-peer private note</param>
        public void ModifyPeerNote(string nodeIdentifier, string noteText, PeerNoteType peerNoteType) {
            ConnectIfNeeded();
            fnwrite.WriteLine("ModifyPeer");
            fnwrite.WriteLine("NodeIdentifier=" + nodeIdentifier);
            fnwrite.WriteLine("NoteText=" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(noteText)));
            fnwrite.WriteLine("PeerNoteType=" + ((int)peerNoteType).ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It removes a given peer from you Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to be removed from the node.</param>
        public void RemovePeer(string nodeIdentifier) {
            ConnectIfNeeded();
            fnwrite.WriteLine("RemovePeer");
            fnwrite.WriteLine("NodeIdentifier=" + nodeIdentifier);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        /// <summary>
        /// This message asks the Freenet node for it's node reference and possibly node statistics.
        /// </summary>
        /// <param name="giveOpennetRef">If set true, the opennet node reference is returned rather than the darknet node reference</param>
        /// <param name="withPrivate">If set true, the private-to-the-nodes fields be included with the returned node reference</param>
        /// <param name="withVolatile">If set true, the volatile statistical information will be included in the returned fieldset</param>
        public void GetNode(bool? giveOpennetRef, bool? withPrivate, bool? withVolatile) {
            ConnectIfNeeded();
            fnwrite.WriteLine("GetNode");
            if (giveOpennetRef!=null)
                fnwrite.WriteLine("GiveOpennetRef=" + giveOpennetRef.Value.ToString());
            if (withPrivate!=null)
                fnwrite.WriteLine("withPrivate=" + withPrivate.Value.ToString());
            if (withVolatile!=null)
                fnwrite.WriteLine("withVolatile=" + withVolatile.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// This message asks the Freenet node for it's node reference and possibly node statistics.
        /// </summary>
        public void GetNode() {
            GetNode(null, null, null);
        }

        /// <summary>
        /// This message asks the Freenet node for it's configuration information.
        /// </summary>
        /// <param name="withCurrent">If set true, the current configuration settings will be returned in the "current" tree of the ConfigData message fieldset</param>
        /// <param name="withDefaults">If set true, the default configuration settings will be returned in the "default" tree of the ConfigData message fieldset</param>
        /// <param name="withSortOrder">If set true, the configuration setting sort order numbers will be returned in the "sortOrder" tree of the ConfigData message fieldset</param>
        /// <param name="withExpertFlag">If set true, the configuration setting expertFlags (only show in expert/advanced mode flags) will be returned in the "expertFlag" tree of the ConfigData message fieldset</param>
        /// <param name="withForceWriteFlag">If set true, the configuration setting forcedWriteFlags (always written to disk flags) will be returned in the "forceWriteFlag" tree of the ConfigData message fieldset</param>
        /// <param name="withShortDescription">If set true, the configuration setting short descriptions will be returned in the "shortDescription" tree of the ConfigData message fieldset</param>
        /// <param name="withLongDescription"> 	If set true, the configuration setting long descriptions will be returned in the "longDescription" tree of the ConfigData message fieldset</param>
        /// <param name="withDataTypes">(since 1110) If set true, specify the type of data expected for each field in the "dataType" tree. Possible values are "number" (i18n standard allowed : 'K', 'M', etc can be used), "string", "boolean", "stringArray"</param>
        public void GetConfig(bool? withCurrent, bool? withDefaults, bool? withSortOrder, bool? withExpertFlag,
                              bool? withForceWriteFlag, bool? withShortDescription, bool? withLongDescription, bool? withDataTypes) {
            
            ConnectIfNeeded();
            fnwrite.WriteLine("GetConfig");
            if (withCurrent!=null)
                fnwrite.WriteLine("WithCurrent=" + withCurrent.Value.ToString());
            if (withDefaults!=null)
                fnwrite.WriteLine("WithDefaults=" + withDefaults.Value.ToString());
            if (withSortOrder!=null)
                fnwrite.WriteLine("WithSortOrder=" + withSortOrder.Value.ToString());
            if (withExpertFlag!=null)
                fnwrite.WriteLine("WithExpertFlag=" + withExpertFlag.Value.ToString());
            if (withForceWriteFlag!=null)
                fnwrite.WriteLine("WithForceWriteFlag=" + withForceWriteFlag.Value.ToString());
            if (withShortDescription!=null)
                fnwrite.WriteLine("WithShortDescription=" + withShortDescription.Value.ToString());
            if (withLongDescription!=null)
                fnwrite.WriteLine("WithLongDescription=" + withLongDescription.Value.ToString());
            if (withDataTypes!=null)
                fnwrite.WriteLine("WithDataTypes=" + withDataTypes.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        /// <summary>
        /// This message asks the Freenet node for it's configuration information.
        /// </summary>
        public void GetConfig() {
            GetConfig(null, null, null, null, null, null, null, null);
        }
        

        /// <summary>
        /// This message modifies configuration settings of your Freenet node.
        /// </summary>
        /// <param name="fieldset">The configuration fieldset path (KEYs) and value (VALUEs) to be modified.
        /// The Dictionary has the paths as keys and the values as values, dont include the '=' character.
        /// Example:
        ///</param>
        /// <example>
        /// Set: (logger.interval=30MINUTE)
        /// <code>
        /// Dictionary<string, string> dict = new Dictionary<string, string>();
        /// dict.Add("logger.interval", "30MINUTE");
        /// fcpclient.ModifyConfig(dict);
        ///</code></example>
        public void ModifyConfig(Dictionary<string, string> fieldset) {
            ConnectIfNeeded();
            
            /* nothing to modify */
            if (fieldset==null) {
                return;
            }

            fnwrite.WriteLine("ModifyConfig");
            foreach(KeyValuePair<string, string> field in fieldset) {
                fnwrite.WriteLine(field.Key + "=" + field.Value);
            }
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        /// <summary>
        /// This message must be sent before any operation involving DDA is requested. If you don't,
        /// the node will reply with a ProtocolError code 25.The node will respond with a TestDDAReply message.
        /// 
        /// You can send more than one TestDDARequest message for different directories if necessary :
        /// the node handles them per socket, meaning that if you close/reopen the socket you have to re-do it.
        /// Each TestDDARequest message will be obsoleted if a new one is received for the same directory.
        /// </summary>
        /// <param name="directory">The directory files you want to access reside in</param>
        /// <param name="wantReadDirectory">Do you plan to do any PUT operation ? Be aware that the node won't allow you to do it unless it has **write** access to the given directory. See the note above</param>
        /// <param name="wantWriteDirectory">Do you plan to do any GET operation ?</param>
        public void TestDDARequest(string directory, bool? wantReadDirectory, bool?  wantWriteDirectory) {
            ConnectIfNeeded();
            fnwrite.WriteLine("TestDDARequest");
            fnwrite.WriteLine("Directory=" + directory);
            if (wantReadDirectory!=null)
                fnwrite.WriteLine("WantReadDirectory=" + wantReadDirectory.Value.ToString());
            if (wantWriteDirectory!=null)
                fnwrite.WriteLine("WantWriteDirectory=" + wantWriteDirectory.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// This message must be sent before any operation involving DDA is requested. If you don't,
        /// the node will reply with a ProtocolError code 25.The node will respond with a TestDDAReply message.
        /// 
        /// You can send more than one TestDDARequest message for different directories if necessary :
        /// the node handles them per socket, meaning that if you close/reopen the socket you have to re-do it.
        /// Each TestDDARequest message will be obsoleted if a new one is received for the same directory.
        /// </summary>
        /// <param name="directory">The directory files you want to access reside in</param>
        public void TestDDARequest(string directory) {
            TestDDARequest(directory, null, null);
        }

        
        /// <summary>
        /// It should be sent *AFTER* a TestDDAReply message is received. The node will respond with a TestDDAComplete message.
        /// </summary>
        /// <param name="Directory">The directory files you want to access reside in. The node will send a protocol error error code if it doesn't know about it.</param>
        /// <param name="ReadContent">The content of the file your client has read from the ReadFilename</param>
        /// <remarks>The FCP client *HAS TO* clean up the files it creates; The node will cleanup files it has created.
        /// You can retry to enable DDA requests more than once for either the same or a different directory.
        /// Only last test result will be taken into account. If you enabled WantWriteAccess your FCP client should have
        ///  created the file *before* you send this message.</remarks>
        public void TestDDAResponse(string directory, string readContent) {
            ConnectIfNeeded();
            fnwrite.WriteLine("TestDDAResponse");
            fnwrite.WriteLine("Directory=" + directory);
            fnwrite.WriteLine("ReadContent=" + readContent);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        /// <summary>
        /// It asks the node to generate us an SSK keypair. The response will come back in a SSKKeypair message.
        /// </summary>
        /// <param name="identifier">the identifier does not have to be unique. That is, no IdentifierCollision
        /// is send if the identifier collides with the identifier of a get / put request or the identifier of
        /// another request to generate a SSK.</param>
        public void GenerateSSK(string identifier) {
            ConnectIfNeeded();
            fnwrite.WriteLine("GenerateSSK");
            fnwrite.WriteLine("Identifier=" + identifier);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        


        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file including the data directly, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="Metadata_ContentType">The MIME type of the data being inserted. For text, if charset is not specified, node should auto-detect it and force the auto-detected version</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxRetries">Number of times to retry if the first time doesn't work. -1 means retry forever.</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        /// <param name="getCHKOnly">If set to true, it won't actually insert the data, just return the key it would generate.If the key is USK, you may want to transform it into a SSK, to prevent the node spending time searching for an unused index.</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="dontCompress">Hint to node: don't try to compress the data, it's already compressed</param>
        /// <param name="clientToken">Sent back to client on the PersistentPut if this is a persistent request</param>
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: connection)</param>
        /// <param name="targetFilename">Filename to be appended to a CHK insert. Technically it creates a one-file manifest with this filename pointing to the file being uploaded. Ignored for all types other than CHK, since other types have human-readable filenames anyway. Empty means no filename.</param>
        /// <param name="earlyEncode">It signals to the node that it should attempt to encode the whole file immediately and generate the key, and insert the top blocks, as soon as possible, rather than waiting until each layer has been inserted before inserting the layer above it.</param>
        /// <param name="filehash">This field will allow you to override any TestDDA restriction if you provide a hash of the content which should be inserted. It should be computed like that : Base64Encode(SHA256( NodeHello.identifier + '-' + Identifier + '-' + content)). That setting has been introduced in 1027.</param>
        /// <param name="binaryBlob">If true, insert a binary blob (a collection of keys used in the downloading of a specific key or site). Implies no metadata, no URI.</param>
        /// <param name="data">A stream to insert directly to freenet</param>
        public void ClientPutDirect(string uri, string Metadata_ContentType, string identifier, Verbosity? verbosity,
                                    int? maxRetries, PriorityClass? priorityClass, bool? getCHKOnly, bool? global,
                                    bool? dontCompress, string clientToken, Persistence? persistence, string targetFilename,
                                    bool? earlyEncode, string fileHash, bool? binaryBlob, Stream data) {

            ConnectIfNeeded();
            fnwrite.WriteLine("ClientPut");
            fnwrite.WriteLine("URI=" + uri);
            if (!String.IsNullOrEmpty(Metadata_ContentType))
                fnwrite.WriteLine("Metadata.ContentType=" + Metadata_ContentType);
            fnwrite.WriteLine("Identifier=" + identifier);
            if(verbosity!=null)
                fnwrite.WriteLine("Verbosity=" + ((int)verbosity.Value).ToString());
            if(maxRetries!=null)
                fnwrite.WriteLine("maxRetries=" + maxRetries.Value.ToString());
            if(priorityClass!=null)
                fnwrite.WriteLine("PriorityClass=" + ((int)priorityClass.Value).ToString());
            if(getCHKOnly!=null)
                fnwrite.WriteLine("GetCHKOnly=" + getCHKOnly.Value.ToString());
            if(global!=null)
                fnwrite.WriteLine("Global=" + global.Value.ToString());
            if(dontCompress!=null)
                fnwrite.WriteLine("DontCompress=" + dontCompress.Value.ToString());
            if(!String.IsNullOrEmpty(clientToken))
                fnwrite.WriteLine("ClientToken=" + clientToken);
            if(persistence!=null)
                fnwrite.WriteLine("Persistence=" + persistence.Value.ToString());
            if(!String.IsNullOrEmpty(targetFilename))
                fnwrite.WriteLine("TargetFilename=" + targetFilename);
            if(earlyEncode!=null)
                fnwrite.WriteLine("EarlyEncode=" + earlyEncode.Value.ToString());
            if(!String.IsNullOrEmpty(fileHash))
                fnwrite.WriteLine("Filehash=" + fileHash);
            if(binaryBlob!=null)
                fnwrite.WriteLine("binaryBlob=" + binaryBlob.Value.ToString());
            fnwrite.WriteLine("UploadFrom=direct");
            fnwrite.WriteLine("DataLength=" + data.Length.ToString());
            fnwrite.WriteLine(endMessage);
            
            // *** stream file ***
            byte[] buffer = new byte[1024];
            int readbytes;
            while((readbytes = data.Read(buffer, 0, buffer.Length)) != 0) {
                client.GetStream().Write(buffer, 0, readbytes);
            }
            client.GetStream().Flush();
        }
        
        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file including the data directly, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="data">A stream to insert directly to freenet</param>
        public void ClientPutDirect(string uri, string identifier, Stream data) {
            ClientPutDirect(uri, null, identifier, null, null, null, null, null, null, null, null, null, null, null, null, data);
        }
        
        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file on disk, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="Metadata_ContentType">The MIME type of the data being inserted. For text, if charset is not specified, node should auto-detect it and force the auto-detected version</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxRetries">Number of times to retry if the first time doesn't work. -1 means retry forever.</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        /// <param name="getCHKOnly">If set to true, it won't actually insert the data, just return the key it would generate.If the key is USK, you may want to transform it into a SSK, to prevent the node spending time searching for an unused index.</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="dontCompress">Hint to node: don't try to compress the data, it's already compressed</param>
        /// <param name="clientToken">Sent back to client on the PersistentPut if this is a persistent request</param>
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: connection)</param>
        /// <param name="targetFilename">Filename to be appended to a CHK insert. Technically it creates a one-file manifest with this filename pointing to the file being uploaded. Ignored for all types other than CHK, since other types have human-readable filenames anyway. Empty means no filename.</param>
        /// <param name="earlyEncode">It signals to the node that it should attempt to encode the whole file immediately and generate the key, and insert the top blocks, as soon as possible, rather than waiting until each layer has been inserted before inserting the layer above it.</param>
        /// <param name="filehash">This field will allow you to override any TestDDA restriction if you provide a hash of the content which should be inserted. It should be computed like that : Base64Encode(SHA256( NodeHello.identifier + '-' + Identifier + '-' + content)). That setting has been introduced in 1027.</param>
        /// <param name="binaryBlob">If true, insert a binary blob (a collection of keys used in the downloading of a specific key or site). Implies no metadata, no URI.</param>
        /// <param name="file">The FileInfo for the File to be inserted</param>
        public void ClientPutDisk(string uri, string Metadata_ContentType, string identifier, Verbosity? verbosity,
                                  int? maxRetries, PriorityClass? priorityClass, bool? getCHKOnly, bool? global,
                                  bool? dontCompress, string clientToken, Persistence? persistence, string targetFilename,
                                  bool? earlyEncode, string fileHash, bool? binaryBlob, FileInfo file) {

            ConnectIfNeeded();
            fnwrite.WriteLine("ClientPut");
            fnwrite.WriteLine("URI=" + uri);
            if (!String.IsNullOrEmpty(Metadata_ContentType))
                fnwrite.WriteLine("Metadata.ContentType=" + Metadata_ContentType);
            fnwrite.WriteLine("Identifier=" + identifier);
            if(verbosity!=null)
                fnwrite.WriteLine("Verbosity=" + ((int)verbosity.Value).ToString());
            if(maxRetries!=null)
                fnwrite.WriteLine("maxRetries=" + maxRetries.Value.ToString());
            if(priorityClass!=null)
                fnwrite.WriteLine("PriorityClass=" + ((int)priorityClass.Value).ToString());
            if(getCHKOnly!=null)
                fnwrite.WriteLine("GetCHKOnly=" + getCHKOnly.Value.ToString());
            if(global!=null)
                fnwrite.WriteLine("Global=" + global.Value.ToString());
            if(dontCompress!=null)
                fnwrite.WriteLine("DontCompress=" + dontCompress.Value.ToString());
            if(!String.IsNullOrEmpty(clientToken))
                fnwrite.WriteLine("ClientToken=" + clientToken);
            if(persistence!=null)
                fnwrite.WriteLine("Persistence=" + persistence.Value.ToString());
            if(!String.IsNullOrEmpty(targetFilename))
                fnwrite.WriteLine("TargetFilename=" + targetFilename);
            if(earlyEncode!=null)
                fnwrite.WriteLine("EarlyEncode=" + earlyEncode.Value.ToString());
            if(!String.IsNullOrEmpty(fileHash))
                fnwrite.WriteLine("Filehash=" + fileHash);
            if(binaryBlob!=null)
                fnwrite.WriteLine("binaryBlob=" + binaryBlob.Value.ToString());
            fnwrite.WriteLine("UploadFrom=disk");
            fnwrite.WriteLine("Filename=" + file.FullName);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file on disk, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="file">The FileInfo for the File to be inserted</param>
        public void ClientPutDisk(string uri, string identifier, FileInfo file) {
            ClientPutDisk(uri, null, identifier, null, null, null, null, null, null, null, null, null, null, null, null, file);
        }
        

        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file redirecting to another key, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="Metadata_ContentType">The MIME type of the data being inserted. For text, if charset is not specified, node should auto-detect it and force the auto-detected version</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxRetries">Number of times to retry if the first time doesn't work. -1 means retry forever.</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        /// <param name="getCHKOnly">If set to true, it won't actually insert the data, just return the key it would generate.If the key is USK, you may want to transform it into a SSK, to prevent the node spending time searching for an unused index.</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="dontCompress">Hint to node: don't try to compress the data, it's already compressed</param>
        /// <param name="clientToken">Sent back to client on the PersistentPut if this is a persistent request</param>
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: connection)</param>
        /// <param name="targetFilename">Filename to be appended to a CHK insert. Technically it creates a one-file manifest with this filename pointing to the file being uploaded. Ignored for all types other than CHK, since other types have human-readable filenames anyway. Empty means no filename.</param>
        /// <param name="earlyEncode">It signals to the node that it should attempt to encode the whole file immediately and generate the key, and insert the top blocks, as soon as possible, rather than waiting until each layer has been inserted before inserting the layer above it.</param>
        /// <param name="filehash">This field will allow you to override any TestDDA restriction if you provide a hash of the content which should be inserted. It should be computed like that : Base64Encode(SHA256( NodeHello.identifier + '-' + Identifier + '-' + content)). That setting has been introduced in 1027.</param>
        /// <param name="binaryBlob">If true, insert a binary blob (a collection of keys used in the downloading of a specific key or site). Implies no metadata, no URI.</param>
        /// <param name="targetURI">This is an existing freenet URI such as KSK@sample.txt. The idea is that you insert a new key that redirects to this one</param>
        public void ClientPutRedirect(string uri, string Metadata_ContentType, string identifier, Verbosity? verbosity,
                                      int? maxRetries, PriorityClass? priorityClass, bool? getCHKOnly, bool? global,
                                      bool? dontCompress, string clientToken, Persistence? persistence, string targetFilename,
                                      bool? earlyEncode, string fileHash, bool? binaryBlob, string targetURI) {

            ConnectIfNeeded();
            fnwrite.WriteLine("ClientPut");
            fnwrite.WriteLine("URI=" + uri);
            if (!String.IsNullOrEmpty(Metadata_ContentType))
                fnwrite.WriteLine("Metadata.ContentType=" + Metadata_ContentType);
            fnwrite.WriteLine("Identifier=" + identifier);
            if(verbosity!=null)
                fnwrite.WriteLine("Verbosity=" + ((int)verbosity.Value).ToString());
            if(maxRetries!=null)
                fnwrite.WriteLine("MaxRetries=" + maxRetries.Value.ToString());
            if(priorityClass!=null)
                fnwrite.WriteLine("PriorityClass=" + ((int)priorityClass.Value).ToString());
            if(getCHKOnly!=null)
                fnwrite.WriteLine("GetCHKOnly=" + getCHKOnly.Value.ToString());
            if(global!=null)
                fnwrite.WriteLine("Global=" + global.Value.ToString());
            if(dontCompress!=null)
                fnwrite.WriteLine("DontCompress=" + dontCompress.Value.ToString());
            if(!String.IsNullOrEmpty(clientToken))
                fnwrite.WriteLine("ClientToken=" + clientToken);
            if(persistence!=null)
                fnwrite.WriteLine("Persistence=" + persistence.Value.ToString());
            if(!String.IsNullOrEmpty(targetFilename))
                fnwrite.WriteLine("TargetFilename=" + targetFilename);
            if(earlyEncode!=null)
                fnwrite.WriteLine("EarlyEncode=" + earlyEncode.Value.ToString());
            if(!String.IsNullOrEmpty(fileHash))
                fnwrite.WriteLine("Filehash=" + fileHash);
            if(binaryBlob!=null)
                fnwrite.WriteLine("binaryBlob=" + binaryBlob.Value.ToString());
            fnwrite.WriteLine("UploadFrom=redirect");
            fnwrite.WriteLine("TargetURI=" + targetURI);
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file redirecting to another key, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="targetURI">This is an existing freenet URI such as KSK@sample.txt. The idea is that you insert a new key that redirects to this one</param>
        public void ClientPutRedirect(string uri, string identifier, string targetURI) {
            ClientPutRedirect(uri, null, identifier, null, null, null, null, null, null, null, null, null, null, null, null, targetURI);
        }
        
        
        /// <summary>
        /// It inserts an entire on-disk directory (including subdirectories) under a single key (technically, as a manifest file), so that each of the inserted files is located using the same key
        /// </summary>
        /// <param name="identifier"> This is just for client to be able to identify the directory that is been inserted.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxRetries">Number of times to retry if the first time doesn't work. -1 means retry forever.</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="getCHKOnly">If set to true, it won't actually insert the data, just return the key it would generate.If the key is USK, you may want to transform it into a SSK, to prevent the node spending time searching for an unused index.</param>
        /// <param name="dontCompress">Hint to node: don't try to compress the data, it's already compressed</param>
        /// <param name="clientToken">Sent back to client on the PersistentPut if this is a persistent request</param>
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: connection)</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="defaultName">The item to display when someone requests the Uri only (without the item name part)</param>
        /// <param name="filename">The filename for the File to be inserted</param>
        /// <param name="allowUnreadableFiles">unless true, any unreadable files cause the whole request to fail; optional</param>
        public void ClientPutDiskDir(string identifier, Verbosity? verbosity, int? maxRetries, PriorityClass? priorityClass,
                                     string uri, bool? getCHKOnly, bool? dontCompress, string clientToken,
                                     Persistence? persistence, bool? global, string defaultName, string filename,
                                     bool allowUnreadableFiles) {
            
            ConnectIfNeeded();
            fnwrite.WriteLine("ClientPutDiskDir");
            fnwrite.WriteLine("Identifier=" + identifier);
            if(verbosity!=null)
                fnwrite.WriteLine("Verbosity=" + ((int)verbosity.Value).ToString());
            if(maxRetries!=null)
                fnwrite.WriteLine("MaxRetries=" + maxRetries.Value.ToString());
            if(priorityClass!=null)
                fnwrite.WriteLine("PriorityClass=" + ((int)priorityClass.Value).ToString());
            fnwrite.WriteLine("URI=" + uri);
            if(getCHKOnly!=null)
                fnwrite.WriteLine("GetCHKOnly=" + getCHKOnly.Value.ToString());
            if(dontCompress!=null)
                fnwrite.WriteLine("DontCompress=" + dontCompress.Value.ToString());
            if(!String.IsNullOrEmpty(clientToken))
                fnwrite.WriteLine("ClientToken=" + clientToken);
            if(persistence!=null)
                fnwrite.WriteLine("Persistence=" + persistence.Value.ToString());
            if(global!=null)
                fnwrite.WriteLine("Global=" + global.Value.ToString());
            if(!String.IsNullOrEmpty(defaultName))
                fnwrite.WriteLine("DefaultName=" + defaultName);
            fnwrite.WriteLine("Filename=" + filename);
            fnwrite.WriteLine("AllowUnreadableFiles=" + allowUnreadableFiles.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It inserts an entire on-disk directory (including subdirectories) under a single key (technically, as a manifest file), so that each of the inserted files is located using the same key
        /// </summary>
        /// <param name="identifier"> This is just for client to be able to identify the directory that is been inserted.</param>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="defaultName">The item to display when someone requests the Uri only (without the item name part)</param>
        /// <param name="filename">The filename for the File to be inserted</param>
        /// <param name="allowUnreadableFiles">unless true, any unreadable files cause the whole request to fail; optional</param>
        public void ClientPutDiskDir(string identifier, string uri, string defaultName, string filename,
                                     bool allowUnreadableFiles) {
            ClientPutDiskDir(identifier, null, null, null, uri, null, null, null, null, null, defaultName, filename, allowUnreadableFiles);
            
            
        }

        public void ClientPutComplexDir(string uri, string identifier, Verbosity? verbosity, int? maxRetries,
                                        PriorityClass? priorityClass, bool? getCHKOnly, bool? global, bool? dontCompress,
                                        string clientToken, Persistence? persistence, string targetFilename, bool? earlyEncode,
                                        string defaultName, List<InsertItem> filelist) {
            ConnectIfNeeded();
            fnwrite.WriteLine("ClientPutComplexDir");
            fnwrite.WriteLine("URI=" + uri);
            fnwrite.WriteLine("Identifier=" + identifier);
            if(verbosity!=null)
                fnwrite.WriteLine("Verbosity=" + ((int)verbosity.Value).ToString());
            if(maxRetries!=null)
                fnwrite.WriteLine("MaxRetries=" + maxRetries.Value.ToString());
            if(priorityClass!=null)
                fnwrite.WriteLine("PriorityClass=" + ((int)priorityClass.Value).ToString());
            if(getCHKOnly!=null)
                fnwrite.WriteLine("GetCHKOnly=" + getCHKOnly.Value.ToString());
            if(global!=null)
                fnwrite.WriteLine("Global=" + global.Value.ToString());
            if(dontCompress!=null)
                fnwrite.WriteLine("DontCompress=" + dontCompress.Value.ToString());
            if(!String.IsNullOrEmpty(clientToken))
                fnwrite.WriteLine("ClientToken=" + clientToken);
            if(persistence!=null)
                fnwrite.WriteLine("Persistence=" + persistence.Value.ToString());
            if(!String.IsNullOrEmpty(targetFilename))
                fnwrite.WriteLine("TargetFilename=" + targetFilename);
            if(earlyEncode!=null)
                fnwrite.WriteLine("EarlyEncode=" + earlyEncode.Value.ToString());
            if(!String.IsNullOrEmpty(defaultName))
                fnwrite.WriteLine("DefaultName=" + defaultName);
            int counter = 0;
            foreach(InsertItem file in filelist) {
                fnwrite.WriteLine("Files." + counter + ".Name=" + file.Name);
                if(file is DataItem) {
                    DataItem item = (DataItem)file;
                    fnwrite.WriteLine("Files." + counter + ".UploadFrom=direct");
                    fnwrite.WriteLine("Files." + counter + ".DataLength=" + item.Data.Length);
                    if(!String.IsNullOrEmpty(item.ContentType))
                        fnwrite.WriteLine("Files." + counter + ".Metadata.ContentType=" + item.ContentType);
                }
                if(file is FileItem) {
                    FileItem item = (FileItem)file;
                    fnwrite.WriteLine("Files." + counter + ".UploadFrom=disk");
                    fnwrite.WriteLine("Files." + counter + ".Filename=" + item.Filename);
                    if(!String.IsNullOrEmpty(item.ContentType))
                        fnwrite.WriteLine("Files." + counter + ".Metadata.ContentType=" + item.ContentType);
                    
                }
                if(file is RedirectItem) {
                    RedirectItem item = (RedirectItem)file;
                    fnwrite.WriteLine("Files." + counter + ".UploadFrom=redirect");
                    fnwrite.WriteLine("Files." + counter + ".TargetURI=" + item.TargetURI);
                    
                }
                counter++;
            }
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
            
            /* this appending is very experimental since there is no example in the documentation
             * I think all the data from direct files are concatenated directly in the same order
             * as in the list above. (TODO: Verfiy expected behaviour)
             * We flush all DataItems, and ignore obviously everything else. 
             * I hope the FN node is parsing multiple files correctly.
             * * * * * * * */
            
            byte[] buffer = new byte[1024];
            int readbytes;
            foreach(InsertItem file in filelist) {
                if(file is DataItem) {
                    while((readbytes = ((DataItem)file).Data.Read(buffer, 0, buffer.Length)) != 0) {
                        client.GetStream().Write(buffer, 0, readbytes);
                    }
                }
            }
            client.GetStream().Flush();
        }
        
        public void ClientPutComplexDir(string uri, string identifier, List<InsertItem> filelist) {
            ClientPutComplexDir(uri, identifier, null, null, null, null, null, null, null, null, null, null, null, filelist);
        }
        
        /// <summary>
        /// It is used to specify a file to download from Freenet.
        /// </summary>
        /// <param name="ignoreDS">Do we ignore the datastore? In the old FCP this was called RemoveLocalKey.</param>
        /// <param name="dsonly">Check only in our local datastore for the file i.e. don't ask other nodes if they have the file. (~= htl 0)</param>
        /// <param name="uri">The URI of the freenet file you want to download e.g. KSK@sample.txt, CHK@zfwLW...Dvs,AAEC--8/</param>
        /// <param name="identifier">A string to uniquely identify to the client the file you are receiving.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxSize">Maximum size of returned data in bytes.</param>
        /// <param name="maxTempSize">Maximum size of intermediary data in bytes.</param>
        /// <param name="maxRetries">Number of times the node will automatically retry to get the data. -1 means retry forever, and will use ULPRs to maintain the request efficiently.</param>
        /// <param name="priorityClass">How to prioritise the download. Default: 4 (Bulk offline splitfile fetches, usually to disk)</param>
        /// <param name="persistence">Whether the download stays on the queue across new client connections, Freenet restarts, or forever</param>
        /// <param name="clientToken">Returned in PersistentGet, a hint to the client, so the client doesn't need to maintain its own state. [FIXME: how does this differ from Identifier?]</param>
        /// <param name="global">Whether the download is visible on the global queue or not.</param>
        /// <param name="binaryBlob">If true, return the data blocks required to fetch this site as a "binary blob" (.fblob) file.</param>
        /// <param name="allowedMIMETypes">If set, only allow certain MIME types in the returned data. If the data is of a MIME type which isn't listed, the request will fail with a WRONG_MIME_TYPE error (code 29) as soon as it realises this.</param>
        /// <param name="returnType">direct: return the data directly to the client via an AllData message, once we
        /// have all of it. (For persistent requests, the client will get a DataFound message but must send a
        /// GetRequestStatus to ask for the AllData). none = don't return the data at all, just fetch it to the node
        /// and tell the client when we have finished. disk = write the data to disk. In future, chunked may also be
        /// supported (return it in segments as they are ready), but this is not yet implemented. If you download to
        /// disk, you have to do a  TestDDARequest.</param>
        /// <param name="filename">Name and path of the file where the download is to be stored.</param>
        /// <param name="tempFilename">Name and path of a temporary file where the partial download is to be stored.</param>
        public void ClientGet(bool? ignoreDS, bool? dsonly, string uri, string identifier,
                              Verbosity? verbosity, int? maxSize, int? maxTempSize, int? maxRetries, PriorityClass? priorityClass,
                              Persistence? persistence, string clientToken, bool? global, bool? binaryBlob,
                              string allowedMIMETypes, ReturnType? returnType, string filename, string tempFilename) {

            ConnectIfNeeded();
            fnwrite.WriteLine("ClientGet");
            if(ignoreDS!=null)
                fnwrite.WriteLine("IgnoreDS=" + ignoreDS.Value.ToString());
            if(dsonly!=null)
                fnwrite.WriteLine("DSonly=" + dsonly.Value.ToString());
            fnwrite.WriteLine("URI=" + uri);
            fnwrite.WriteLine("Identifier=" + identifier);
            if(verbosity!=null)
                fnwrite.WriteLine("Verbosity=" + ((int)verbosity.Value).ToString());
            if(maxSize!=null)
                fnwrite.WriteLine("MaxSize=" + maxSize.Value.ToString());
            if(maxTempSize!=null)
                fnwrite.WriteLine("MaxTempSize=" + maxTempSize.Value.ToString());
            if(maxRetries!=null)
                fnwrite.WriteLine("MaxRetries=" + maxRetries.Value.ToString());
            if(priorityClass!=null)
                fnwrite.WriteLine("PriorityClass=" + ((int)priorityClass.Value).ToString());
            if(persistence!=null)
                fnwrite.WriteLine("Persistence=" + persistence.Value.ToString());
            if(!String.IsNullOrEmpty(clientToken))
                fnwrite.WriteLine("ClientToken=" + clientToken);
            if(global!=null)
                fnwrite.WriteLine("Global=" + global.Value.ToString());
            if(binaryBlob!=null)
                fnwrite.WriteLine("BinaryBlob=" + binaryBlob.Value.ToString());
            if(!String.IsNullOrEmpty(allowedMIMETypes))
                fnwrite.WriteLine("AllowedMIMETypes=" + allowedMIMETypes);
            
            if(returnType!=null) {
                fnwrite.WriteLine("ReturnType=" + returnType.Value.ToString());
                if(returnType==ReturnType.disk) {
                    fnwrite.WriteLine("Filename=" + filename);
                    if(!String.IsNullOrEmpty(tempFilename))
                        fnwrite.WriteLine("TempFileName=" + tempFilename);
                }
            }
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It is used to specify a file to download from Freenet.
        /// </summary>
        /// <param name="uri">The URI of the freenet file you want to download e.g. KSK@sample.txt, CHK@zfwLW...Dvs,AAEC--8/</param>
        /// <param name="identifier">A string to uniquely identify to the client the file you are receiving.</param>
        public void ClientGet(string uri, string identifier) {
            ClientGet(null, null, uri, identifier, null, null, null,null, null,null, null,null, null,null, null,null, null);
        }

        /// <summary>
        /// This message asks the Freenet node for the presence of the given pluginname The node will respond with a PluginInfo event.
        /// </summary>
        /// <param name="pluginName">A name to identify the plugin. Must be the same as class name shown on plugins page</param>
        /// <param name="identifier">This is for client to be able to identify responses</param>
        /// <param name="detailed">If true, detailed information is returned (requires full access)</param>
        public void GetPluginInfo(string pluginName, string identifier, bool? detailed) {
            ConnectIfNeeded();
            fnwrite.WriteLine("GetPluginInfo");
            fnwrite.WriteLine("PluginName=" + pluginName);
            if(!String.IsNullOrEmpty(identifier))
                fnwrite.WriteLine("Identifier=" + identifier);
            if(detailed!=null)
                fnwrite.WriteLine("Detailed=" + detailed.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginName">A name to identify the plugin. Must be the same as class name shown on plugins page</param>
        public void GetPluginInfo(string pluginName) {
            GetPluginInfo(pluginName, null, null);
        }

        
        /// <summary>
        /// This message is used to send a messege to a plugin. The plugin may or may not respond with a FCPPluginReply message.
        /// </summary>
        /// <param name="pluginName">A name to identify the plugin. Must be the same as class name shown on plugins page</param>
        /// <param name="identifier">Not mandatory yet, but you should use it. May the plugin responds in random order or delayed to many calls at once. Depends on plugin implementation.</param>
        /// <param name="dataLength">Length of data if data is passed along with the message</param>
        /// <param name="pluginParams">An amount of param.item=value pairs. Added as a Dictionary, see example</param>
        /// <example>
        /// Set: (Param.Hello=world)
        /// <code>
        /// Dictionary<string, string> dict = new Dictionary<string, string>();
        /// dict.Add("Param.Hello", "world");
        /// fcpclient.FCPPluginMessage("plugins.HelloFCP.HelloFCP", "id1", null, dict);
        ///</code></example>
        public void FCPPluginMessage(string pluginName, string identifier, Stream data, Dictionary<string, string> pluginParams) {
            ConnectIfNeeded();
            if (pluginParams==null) {
                pluginParams = new Dictionary<string, string>();
            }
            
            fnwrite.WriteLine("FCPPluginMessage");
            fnwrite.WriteLine("PluginName=" + pluginName);
            if(!String.IsNullOrEmpty(identifier))
                fnwrite.WriteLine("Identifier=" + identifier);
            if (data!=null)
                fnwrite.WriteLine("DataLength=" + data.Length.ToString());
            foreach(KeyValuePair<string, string> param in pluginParams) {
                fnwrite.WriteLine(param.Key + "=" + param.Value);
            }
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
            
            // *** stream binary data if any ***
            if (data!=null) {
                byte[] buffer = new byte[1024];
                int readbytes;
                while((readbytes = data.Read(buffer, 0, buffer.Length)) != 0) {
                    client.GetStream().Write(buffer, 0, readbytes);
                }
            }
            client.GetStream().Flush();
        }

        public void FCPPluginMessage(string pluginName, Dictionary<string, string> pluginParams) {
            FCPPluginMessage(pluginName, null, null, pluginParams);
        }
        
        
        public void SubscribeUSK(string uri, string identifier, bool? dontPoll) {
            ConnectIfNeeded();
            fnwrite.WriteLine("SubscribeUSK");
            fnwrite.WriteLine("URI=" + uri);
            fnwrite.WriteLine("Identifier=" + identifier);
            if (dontPoll!=null)
                fnwrite.WriteLine("DontPoll=" + dontPoll.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        public void SubscribeUSK(string uri, string identifier) {
            SubscribeUSK(uri, identifier, null);
        }
        
        /// <summary>
        /// It makes the global queue of inserts and retrievals visible or invisible to the client.
        /// </summary>
        /// <param name="enabled">enable Global Watch</param>
        /// <param name="verbosityMask">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        public void WatchGlobal(bool enabled, Verbosity verbosityMask) {
            ConnectIfNeeded();
            fnwrite.WriteLine("WatchGlobal");
            fnwrite.WriteLine("Enabled=" + enabled.ToString());
            fnwrite.WriteLine("VerbosityMask=" + ((int)verbosityMask).ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It asks the node to provide all it knows about the current progress of the request.
        /// Normally for a persistent, direct, completed ClientGet this will be a PersistentGet
        /// message followed by a DataFound message (maybe a SimpleProgress message first), and
        /// then an AllData message with the actual data.
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">Whether to look on the global queue for inserts and downloads</param>
        /// <param name="onlyData">Whether to just include the AllData message (for direct ClientGet's). If false, all status messages for that request will be included.</param>
        public void GetRequestStatus(string identifier, bool? global, bool? onlyData) {
            ConnectIfNeeded();
            fnwrite.WriteLine("GetRequestStatus");
            fnwrite.WriteLine("Identifier=" + identifier);
            if (global!=null)
                fnwrite.WriteLine("Global=" + global.Value.ToString());
            if (onlyData!=null)
                fnwrite.WriteLine("OnlyData=" + onlyData.Value.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }

        /// <summary>
        /// It asks the node to provide all it knows about the current progress of the request.
        /// Normally for a persistent, direct, completed ClientGet this will be a PersistentGet
        /// message followed by a DataFound message (maybe a SimpleProgress message first), and
        /// then an AllData message with the actual data.
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        public void GetRequestStatus(string identifier) {
            GetRequestStatus(identifier, null, null);
        }

        
        /// <summary>
        /// Ask the node to list all persistent requests for this client on the node (and for the global
        /// queue too if watch global queue is enabled). Node will respond with a series of PersistentGet
        /// or PersistentPut messages, possibly some DataFound, GetFailed, PutFailed or PutSuccessful
        /// messages, and possibly some status messages (SimpleProgress, StartedCompression or
        /// FinishedCompression), and then an EndListPersistentRequests message.
        /// </summary>
        public void ListPersistentRequests() {
            ConnectIfNeeded();
            fnwrite.WriteLine("ListPersistentRequests");
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It asks the Freenet node to remove the request (persistent or not) that is identified by the Identifier value. Note that you must specify Global=true if it is a global request - even if WatchGlobal is enabled.
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">The Global field specifies whether the request is on the global queue or not (Identifier namespaces are separate for the global and local queue).</param>
        public void RemoveRequest(string identifier, bool global) {
            ConnectIfNeeded();
            fnwrite.WriteLine("RemoveRequest");
            fnwrite.WriteLine("Identifier=" + identifier);
            fnwrite.WriteLine("Global=" + global.ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It modifies the ClientToken and PriorityClass fields of the persistent request that is identified by
        /// the Identifier value. Since 1016: The node will confirm
        /// the modify of the request by sending a PersistentRequestModified to the client that sent the
        /// ModifyPersistentRequest message, and to each client that listens to the global queue
        /// (if the request is on the global queue).
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">The Global field specifies whether the request is on the global queue or not (Identifier namespaces are separate for the global and local queue).</param>
        /// <param name="clientToken">New Client Token</param>
        /// <param name="priorityClass">Priority of the insert</param>
        public void ModifyPersistentRequest(string identifier, bool global, string clientToken, PriorityClass? priorityClass) {
            ConnectIfNeeded();
            fnwrite.WriteLine("ModifyPersistentRequest");
            fnwrite.WriteLine("Identifier=" + identifier);
            fnwrite.WriteLine("Global=" + global.ToString());
            if(!String.IsNullOrEmpty(clientToken))
                fnwrite.WriteLine("ClientToken=" + clientToken);
            if(priorityClass!=null)
                fnwrite.WriteLine("PriorityClass=" + ((int)priorityClass).ToString());
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        
        /// <summary>
        /// It modifies the ClientToken and PriorityClass fields of the persistent request that is identified by
        /// the Identifier value. Since 1016: The node will confirm
        /// the modify of the request by sending a PersistentRequestModified to the client that sent the
        /// ModifyPersistentRequest message, and to each client that listens to the global queue
        /// (if the request is on the global queue).
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">The Global field specifies whether the request is on the global queue or not (Identifier namespaces are separate for the global and local queue).</param>
        /// <param name="clientToken">New Client Token</param>
        public void ModifyPersistentRequest(string identifier, bool global, string clientToken) {
            ModifyPersistentRequest(identifier, global, clientToken, null);
        }

        /// <summary>
        /// It modifies the ClientToken and PriorityClass fields of the persistent request that is identified by
        /// the Identifier value. Since 1016: The node will confirm
        /// the modify of the request by sending a PersistentRequestModified to the client that sent the
        /// ModifyPersistentRequest message, and to each client that listens to the global queue
        /// (if the request is on the global queue).
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">The Global field specifies whether the request is on the global queue or not (Identifier namespaces are separate for the global and local queue).</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        public void ModifyPersistentRequest(string identifier, bool global, PriorityClass? priorityClass) {
            ModifyPersistentRequest(identifier, global, null, priorityClass);
        }
        
        /// <summary>
        /// This command allows an client program to remotely shut down a Freenet node. A confirmation message will be sent (ProtocolError code 18: Shutting down)
        /// </summary>
        public void Shutdown() {
            ConnectIfNeeded();
            fnwrite.WriteLine("Shutdown");
            fnwrite.WriteLine(endMessage);
            fnwrite.Flush();
        }
        #endregion
    }
}