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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using FCP2.EventArgs;

namespace FCP2.Protocol
{
    /// <summary>
    /// FCP 2.0 Protocol implementation according to following RFC:
    /// http://wiki.freenetproject.org/FreenetFCPSpec2Point0
    /// </summary>
    public class FCP2Protocol : IDisposable
    {
        #region Private declarations

        internal static readonly IPEndPoint StandardFCP2Endpoint = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 9481);

        private readonly IPEndPoint _ep;
        private readonly TcpClient _client = new TcpClient();
        private TextReader _fnread;
        private TextWriter _fnwrite;
        readonly bool isMultiThreaded = false;

        public string ClientName { get; }
        private const string FCPVersion = "2.0";
        #endregion

        #region Private Methods
        /// <summary>
        /// Connects and sends a ClientHello
        /// </summary>
        /// <returns>returns false if we are already connected</returns>
        private bool ConnectIfNeeded()
        {
            lock (_client)
            {
                if (_client.Connected)
                {
                    return false;
                }

                _client.Connect(_ep);

                _fnread = new MixedReader(_client.GetStream());
                _fnwrite = new MixedWriter(_client.GetStream());

                _parserThread = new Thread(MessageParser);
                _parserThread.Start();

                RealClientHello(ClientName, FCPVersion);
            }

            return true;

        }


        private void Write(string value)
        {
            _fnwrite.WriteLine(value);
        }

        private void Write(string key, bool? value)
        {
            if (value.HasValue)
            {
                _fnwrite.WriteLine($"{key}={value}");
            }
        }

        private void Write(string key, long? value)
        {
            if (value.HasValue)
            {
                _fnwrite.WriteLine($"{key}={value}");
            }
        }

        private void Write(string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _fnwrite.WriteLine($"{key}={value}");
            }
        }

        private void Write<T>(string key, T? value) where T : struct
        {
            if (value.HasValue)
            {
                _fnwrite.WriteLine($"{key}={value.Value}");
            }
        }

        internal void EndMessage()
        {
            _fnwrite.WriteLine(nameof(EndMessage));
            _fnwrite.Flush();
        }


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
        private void RealClientHello(string name, string expectedVersion)
        {
            /* this will be called automatically on every Connection start */
            Write("ClientHello");
            Write("Name", name);
            Write("ExpectedVersion", expectedVersion);

            EndMessage();
        }
        #endregion

#if DEBUG
        public static void ArgsDebug(System.EventArgs args, MessageParser parsed)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(args.GetType() + "()");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var kvp in parsed)
            {
                Console.WriteLine(kvp.Key + "", kvp.Value);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
#endif

        public static DateTime FromUnix(string unix)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            double seconds = long.Parse(unix) / 1000.0;
            return epoch.AddSeconds(seconds);
        }

        #region Event Handler
        public event EventHandler<NodeHelloEventArgs> NodeHelloEvent;
        public event EventHandler<CloseConnectionDuplicateClientNameEventArgs> CloseConnectionDuplicateClientNameEvent;
        public event EventHandler<PeerEventArgs> PeerEvent;
        public event EventHandler<PeerNoteEventArgs> PeerNoteEvent;
        public event EventHandler<EndListPeersEventArgs> EndListPeersEvent;
        public event EventHandler<EndListPeerNotesEventArgs> EndListPeerNotesEvent;
        public event EventHandler<PeerRemovedEventArgs> PeerRemovedEvent;
        public event EventHandler<NodeDataEventArgs> NodeDataEvent;
        public event EventHandler<ConfigDataEventArgs> ConfigDataEvent;
        public event EventHandler<TestDDAReplyEventArgs> TestDDAReplyEvent;
        public event EventHandler<TestDDACompleteEventArgs> TestDDACompleteEvent;
        public event EventHandler<SSKKeypairEventArgs> SSKKeypairEvent;
        public event EventHandler<PersistentGetEventArgs> PersistentGetEvent;
        public event EventHandler<PersistentPutEventArgs> PersistentPutEvent;
        public event EventHandler<PersistentPutDirEventArgs> PersistentPutDirEvent;
        public event EventHandler<URIGeneratedEventArgs> URIGeneratedEvent;
        public event EventHandler<PutSuccessfulEventArgs> PutSuccessfulEvent;
        public event EventHandler<PutFetchableEventArgs> PutFetchableEvent;
        public event EventHandler<DataFoundEventArgs> DataFoundEvent;
        public event EventHandler<AllDataEventArgs> AllDataEvent;
        public event EventHandler<StartedCompressionEventArgs> StartedCompressionEvent;
        public event EventHandler<FinishedCompressionEventArgs> FinishedCompressionEvent;
        public event EventHandler<SimpleProgressEventArgs> SimpleProgressEvent;
        public event EventHandler<ExpectedHashesEventArgs> ExpectedHashesEvent;
        public event EventHandler<ExpectedMimeEventArgs> ExpectedMimeEvent;
        public event EventHandler<ExpectedDataLengthEventArgs> ExpectedDataLengthEvent;
        public event EventHandler<CompatibilityModeEventArgs> CompatibilityModeEvent;
        public event EventHandler<EndListPersistentRequestsEventArgs> EndListPersistentRequestsEvent;
        public event EventHandler<PersistentRequestRemovedEventArgs> PersistentRequestRemovedEvent;
        public event EventHandler<PersistentRequestModifiedEventArgs> PersistentRequestModifiedEvent;
        public event EventHandler<SendingToNetworkEventArgs> SendingToNetworkEvent;
        public event EventHandler<EnterFiniteCooldownEventArgs> EnterFiniteCooldownEvent;
        public event EventHandler<PutFailedEventArgs> PutFailedEvent;
        public event EventHandler<GetFailedEventArgs> GetFailedEvent;
        public event EventHandler<ProtocolErrorEventArgs> ProtocolErrorEvent;
        public event EventHandler<IdentifierCollisionEventArgs> IdentifierCollisionEvent;
        public event EventHandler<UnknownNodeIdentifierEventArgs> UnknownNodeIdentifierEvent;
        public event EventHandler<UnknownPeerNoteTypeEventArgs> UnknownPeerNoteTypeEvent;
        public event EventHandler<SubscribedUSKEventArgs> SubscribedUSKEvent;
        public event EventHandler<SubscribedUSKUpdateEventArgs> SubscribedUSKUpdateEvent;
        public event EventHandler<PluginInfoEventArgs> PluginInfoEvent;
        public event EventHandler<PluginRemovedEventArgs> PluginRemovedEvent;
        public event EventHandler<FCPPluginReplyEventArgs> FCPPluginReplyEvent;
        public event EventHandler<GeneratedMetadataEventArgs> GeneratedMetadataEvent;
        #endregion

        #region EventInvocation
        protected void DispatchEvent<T>(object sender, EventHandler<T> handler, T eventArgs) where T : System.EventArgs
        {
            if (handler == null) return;

            if (isMultiThreaded)
            {
                ThreadPool.QueueUserWorkItem(state => handler.Invoke(sender, eventArgs));
            }
            else
            {
                handler(sender, eventArgs);
            }
        }

        /// <summary>
        /// Warning, Only One consumer can read the data!!!
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAllDataEvent(AllDataEventArgs e)
        {
            AllDataEvent?.Invoke(this, e);

            Stream data;

            // we read the Data into oblivion if no consumer read it! (!Reason for MixedReader)
            if ((data = e.GetStream()) == null) return;

            var buffer = new byte[1024];
            var bytesToRead = e.Datalength;
            while (bytesToRead > 0)
            {
                bytesToRead -= data.Read(buffer, 0, (int)Math.Min(bytesToRead, buffer.Length));
            }
        }
        #endregion

        private Thread _parserThread;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeAdress"></param>
        /// <param name="clientName">A name to uniquely identify the client to the node. This is
        /// used for persistence, so a client can see the same local queue if it disconnects
        /// and then reconnects. If a connection is attempted with the same name as an existing
        /// connection, you will get an error</param>
        public FCP2Protocol(IPEndPoint nodeAdress, string clientName)
        {
            _ep = nodeAdress ?? StandardFCP2Endpoint;
            ClientName = clientName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientName">A name to uniquely identify the client to the node. This is
        /// used for persistence, so a client can see the same local queue if it disconnects
        /// and then reconnects. If a connection is attempted with the same name as an existing
        /// connection, you will get an error</param>
        public FCP2Protocol(string clientName)
        {
            ClientName = clientName;
            _ep = StandardFCP2Endpoint;
        }

        /// <summary>
        /// Cleanly Disconnects from the Freenet node
        /// </summary>
        public void Disconnect()
        {
            if (!_client.Connected) return;
            RealDisconnect();
            _client.GetStream().Close();
            _client.Close();
        }


        #region Parser

        private void MessageParser()
        {
            string line;
            while ((line = _fnread.ReadLine()) != null)
            {
                CreateEvent(line);
            }
        }

        private void CreateEvent(string line)
        {
            switch (line)
            {
                case "NodeHello":
                    DispatchEvent(this, NodeHelloEvent, new NodeHelloEventArgs(new MessageParser(_fnread)));
                    break;
                case "CloseConnectionDuplicateClientName":
                    DispatchEvent(this, CloseConnectionDuplicateClientNameEvent,
                        new CloseConnectionDuplicateClientNameEventArgs(new MessageParser(_fnread)));
                    break;
                case "Peer":
                    DispatchEvent(this, PeerEvent, new PeerEventArgs(new MessageParser(_fnread)));
                    break;
                case "PeerNote":
                    DispatchEvent(this, PeerNoteEvent, new PeerNoteEventArgs(new MessageParser(_fnread)));
                    break;
                case "EndListPeers":
                    DispatchEvent(this, EndListPeersEvent, new EndListPeersEventArgs(new MessageParser(_fnread)));
                    break;
                case "EndListPeerNotes":
                    DispatchEvent(this, EndListPeerNotesEvent, new EndListPeerNotesEventArgs(new MessageParser(_fnread)));
                    break;
                case "PeerRemoved":
                    DispatchEvent(this, PeerRemovedEvent, new PeerRemovedEventArgs(new MessageParser(_fnread)));
                    break;
                case "NodeData":
                    DispatchEvent(this, NodeDataEvent, new NodeDataEventArgs(new MessageParser(_fnread)));
                    break;
                case "ConfigData":
                    DispatchEvent(this, ConfigDataEvent, new ConfigDataEventArgs(new MessageParser(_fnread)));
                    break;
                case "TestDDAReply":
                    DispatchEvent(this, TestDDAReplyEvent, new TestDDAReplyEventArgs(new MessageParser(_fnread)));
                    break;
                case "TestDDAComplete":
                    DispatchEvent(this, TestDDACompleteEvent, new TestDDACompleteEventArgs(new MessageParser(_fnread)));
                    break;
                case "SSKKeypair":
                    DispatchEvent(this, SSKKeypairEvent, new SSKKeypairEventArgs(new MessageParser(_fnread)));
                    break;
                case "PersistentGet":
                    DispatchEvent(this, PersistentGetEvent, new PersistentGetEventArgs(new MessageParser(_fnread)));
                    break;
                case "PersistentPut":
                    DispatchEvent(this, PersistentPutEvent, new PersistentPutEventArgs(new MessageParser(_fnread)));
                    break;
                case "PersistentPutDir":
                    DispatchEvent(this, PersistentPutDirEvent, new PersistentPutDirEventArgs(new MessageParser(_fnread)));
                    break;
                case "URIGenerated":
                    DispatchEvent(this, URIGeneratedEvent, new URIGeneratedEventArgs(new MessageParser(_fnread)));
                    break;
                case "PutSuccessful":
                    DispatchEvent(this, PutSuccessfulEvent, new PutSuccessfulEventArgs(new MessageParser(_fnread)));
                    break;
                case "PutFetchable":
                    DispatchEvent(this, PutFetchableEvent, new PutFetchableEventArgs(new MessageParser(_fnread)));
                    break;
                case "DataFound":
                    DispatchEvent(this, DataFoundEvent, new DataFoundEventArgs(new MessageParser(_fnread)));
                    break;
                case "AllData":
                    OnAllDataEvent(new AllDataEventArgs(new MessageParser(_fnread), _client.GetStream()));
                    break;
                case "StartedCompression":
                    DispatchEvent(this, StartedCompressionEvent, new StartedCompressionEventArgs(new MessageParser(_fnread)));
                    break;
                case "FinishedCompression":
                    DispatchEvent(this, FinishedCompressionEvent, new FinishedCompressionEventArgs(new MessageParser(_fnread)));
                    break;
                case "SimpleProgress":
                    DispatchEvent(this, SimpleProgressEvent, new SimpleProgressEventArgs(new MessageParser(_fnread)));
                    break;
                case "ExpectedHashes":
                    DispatchEvent(this, ExpectedHashesEvent, new ExpectedHashesEventArgs(new MessageParser(_fnread)));
                    break;
                case "ExpectedMIME":
                    DispatchEvent(this, ExpectedMimeEvent, new ExpectedMimeEventArgs(new MessageParser(_fnread)));
                    break;
                case "ExpectedDataLength":
                    DispatchEvent(this, ExpectedDataLengthEvent, new ExpectedDataLengthEventArgs(new MessageParser(_fnread)));
                    break;
                case "CompatibilityMode":
                    DispatchEvent(this, CompatibilityModeEvent, new CompatibilityModeEventArgs(new MessageParser(_fnread)));
                    break;
                case "EndListPersistentRequests":
                    DispatchEvent(this, EndListPersistentRequestsEvent, new EndListPersistentRequestsEventArgs(new MessageParser(_fnread)));
                    break;
                case "PersistentRequestRemoved":
                    DispatchEvent(this, PersistentRequestRemovedEvent, new PersistentRequestRemovedEventArgs(new MessageParser(_fnread)));
                    break;
                case "PersistentRequestModified":
                    DispatchEvent(this, PersistentRequestModifiedEvent, new PersistentRequestModifiedEventArgs(new MessageParser(_fnread)));
                    break;
                case "SendingToNetwork":
                    DispatchEvent(this, SendingToNetworkEvent, new SendingToNetworkEventArgs(new MessageParser(_fnread)));
                    break;
                case "EnterFiniteCooldown":
                    DispatchEvent(this, EnterFiniteCooldownEvent, new EnterFiniteCooldownEventArgs(new MessageParser(_fnread)));
                    break;
                case "PutFailed":
                    DispatchEvent(this, PutFailedEvent, new PutFailedEventArgs(new MessageParser(_fnread)));
                    break;
                case "GetFailed":
                    DispatchEvent(this, GetFailedEvent, new GetFailedEventArgs(new MessageParser(_fnread)));
                    break;
                case "ProtocolError":
                    DispatchEvent(this, ProtocolErrorEvent, new ProtocolErrorEventArgs(new MessageParser(_fnread)));
                    break;
                case "IdentifierCollision":
                    DispatchEvent(this, IdentifierCollisionEvent, new IdentifierCollisionEventArgs(new MessageParser(_fnread)));
                    break;
                case "UnknownNodeIdentifier":
                    DispatchEvent(this, UnknownNodeIdentifierEvent, new UnknownNodeIdentifierEventArgs(new MessageParser(_fnread)));
                    break;
                case "UnknownPeerNoteType":
                    DispatchEvent(this, UnknownPeerNoteTypeEvent, new UnknownPeerNoteTypeEventArgs(new MessageParser(_fnread)));
                    break;
                case "SubscribedUSK":
                    DispatchEvent(this, SubscribedUSKEvent, new SubscribedUSKEventArgs(new MessageParser(_fnread)));
                    break;
                case "SubscribedUSKUpdate":
                    DispatchEvent(this, SubscribedUSKUpdateEvent, new SubscribedUSKUpdateEventArgs(new MessageParser(_fnread)));
                    break;
                case "PluginInfo":
                    DispatchEvent(this, PluginInfoEvent, new PluginInfoEventArgs(new MessageParser(_fnread)));
                    break;
                case "PluginRemoved":
                    DispatchEvent(this, PluginRemovedEvent, new PluginRemovedEventArgs(new MessageParser(_fnread)));
                    break;
                case "FCPPluginReply":
                    DispatchEvent(this, FCPPluginReplyEvent, new FCPPluginReplyEventArgs(new MessageParser(_fnread)));
                    break;
                case "GeneratedMetadata":
                    DispatchEvent(this, GeneratedMetadataEvent, new GeneratedMetadataEventArgs(new MessageParser(_fnread)));
                    break;
                case "":
                    /* ignore empty line */
                    break;
                default:
                    throw new ArgumentException("unknown message from freenet node");
            }
        }

        #endregion

        #region public interface
        /// <summary>
        /// This must be the first message from the client on any given connection.
        /// The node will respond with a NodeHello Event.
        /// </summary>

        public void ClientHello()
        {
            if (!ConnectIfNeeded())
            {
                RealClientHello(ClientName, FCPVersion);
            }
        }

        /// <summary>
        /// This message asks the Freenet node for the details of a given Freenet connected directly to it (peer).
        /// </summary>
        /// <param name="nodeIdentifier">The node name (except for opennet peers), identity or IP:port pair of the peer to be modified.</param>
        /// <param name="withMetadata">When true, additional metadata is added to the output of the Peer reply sent by the node.</param>
        /// <param name="withVolatile">When true, additional volatile data is added to the output of the Peer reply sent by the node.</param>
        public void ListPeer(string nodeIdentifier, bool? withMetadata = null, bool? withVolatile = null)
        {
            ConnectIfNeeded();

            Write("ListPeer");
            Write("NodeIdentifier", nodeIdentifier);
            Write("WithMetadata", withMetadata);
            Write("WithVolatile", withVolatile);

            EndMessage();
        }

        /// <summary>
        /// This message asks the Freenet node for a list of other Freenet nodes connected directly to you (peers).
        /// </summary>
        /// <param name="identifier">Identifier for the request; this lets you match the Peer reply to this request.</param>
        /// <param name="withMetadata">If set true, the metadata subtree be included in the returned fieldset for each peer</param>
        /// <param name="withVolatile">If set true, the volatile status information will be included in the returned fieldset for each peer</param>
        public void ListPeers(string identifier = null, bool? withMetadata = null, bool? withVolatile = null)
        {
            ConnectIfNeeded();

            Write("ListPeers");
            Write("Identifier", identifier);
            Write("WithMetadata", withMetadata);
            Write("WithVolatile", withVolatile);

            EndMessage();
        }

        /// <summary>
        /// This message lists the peer notes for a given peer of your Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to list the peer notes of.</param>
        public void ListPeerNotes(string nodeIdentifier)
        {
            ConnectIfNeeded();

            Write("ListPeerNotes");
            Write("NodeIdentifier", nodeIdentifier);

            EndMessage();
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
        public void AddPeer(FileInfo file)
        {
            ConnectIfNeeded();

            Write("AddPeer");
            Write("File", file.FullName);

            EndMessage();
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
        public void AddPeer(Uri uri)
        {
            ConnectIfNeeded();

            Write("AddPeer");
            Write("URL", uri.AbsoluteUri);

            EndMessage();
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
        public void AddPeer(string nodeRef)
        {
            ConnectIfNeeded();

            Write("AddPeer");
            Write(nodeRef);

            EndMessage();
        }


        /// <summary>
        /// This message modifies settings for a given peer of your Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to be modified.</param>
        /// <param name="allowLocalAdresses">If set, the peer identified by the given NodeIdentifier is set allowLocalAddresses or not accordingly.</param>
        /// <param name="isDisabled">If set, the peer identified by the given NodeIdentifier is enabled or disabled accordingly.</param>
        /// <param name="isListenOnly">If set, the peer identified by the given NodeIdentifier is set ListenOnly or not accordingly.</param>
        public void ModifyPeer(string nodeIdentifier, bool? allowLocalAdresses = null, bool? isDisabled = null, bool? isListenOnly = null)
        {
            ConnectIfNeeded();

            Write("ModifyPeer");
            Write("NodeIdentifier", nodeIdentifier);
            Write("AllowLocalAdresses", allowLocalAdresses);
            Write("IsDisabled", isDisabled);
            Write("IsListenOnly", isListenOnly);

            EndMessage();
        }

        /// <summary>
        /// This message modifies a peer note for a given peer of your Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to be modified.</param>
        /// <param name="noteText">String of the note text to set the specified peer note to.</param>
        /// <param name="peerNoteType">Specify the type of the peer note, by code number (currently, may change in the future). Type codes are: 1-peer private note</param>
        public void ModifyPeerNote(string nodeIdentifier, string noteText, PeerNoteTypeEnum peerNoteType)
        {
            ConnectIfNeeded();

            Write("ModifyPeer");
            Write("NodeIdentifier", nodeIdentifier);
            Write("NoteText", Convert.ToBase64String(Encoding.UTF8.GetBytes(noteText)));
            Write("PeerNoteType", (long)peerNoteType);

            EndMessage();
        }

        /// <summary>
        /// It removes a given peer from you Freenet node.
        /// </summary>
        /// <param name="nodeIdentifier">The node name, identity or IP:port pair of the peer to be removed from the node.</param>
        public void RemovePeer(string nodeIdentifier)
        {
            ConnectIfNeeded();

            Write("RemovePeer");
            Write("NodeIdentifier", nodeIdentifier);

            EndMessage();
        }

        /// <summary>
        /// This message asks the Freenet node for it's node reference and possibly node statistics.
        /// </summary>
        /// <param name="giveOpennetRef">If set true, the opennet node reference is returned rather than the darknet node reference</param>
        /// <param name="withPrivate">If set true, the private-to-the-nodes fields be included with the returned node reference</param>
        /// <param name="withVolatile">If set true, the volatile statistical information will be included in the returned fieldset</param>
        public void GetNode(bool? giveOpennetRef = null, bool? withPrivate = null, bool? withVolatile = null)
        {
            ConnectIfNeeded();

            Write("GetNode");
            Write("GiveOpennetRef", giveOpennetRef);
            Write("WithPrivate", withPrivate);
            Write("WithVolatile", withVolatile);

            EndMessage();
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
        public void GetConfig(bool? withCurrent = null, bool? withDefaults = null, bool? withSortOrder = null, bool? withExpertFlag = null,
                              bool? withForceWriteFlag = null, bool? withShortDescription = null, bool? withLongDescription = null, bool? withDataTypes = null)
        {
            ConnectIfNeeded();

            Write("GetConfig");
            Write("WithCurrent", withCurrent);
            Write("WithDefaults", withDefaults);
            Write("WithSortOrder", withSortOrder);
            Write("WithExpertFlag", withExpertFlag);
            Write("WithForceWriteFlag", withForceWriteFlag);
            Write("WithShortDescription", withShortDescription);
            Write("WithLongDescription", withLongDescription);
            Write("WithDataTypes", withDataTypes);

            EndMessage();
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
        /// Dictionary&lt;string, string&gt; dict = new Dictionary&lt;string, string&gt;();
        /// dict.Add("logger.interval", "30MINUTE");
        /// fcpclient.ModifyConfig(dict);
        ///</code></example>
        public void ModifyConfig(Dictionary<string, string> fieldset)
        {
            ConnectIfNeeded();

            /* nothing to modify */
            if (fieldset == null)
            {
                return;
            }

            Write("ModifyConfig");
            foreach (var field in fieldset)
            {
                Write(field.Key, field.Value);
            }
            EndMessage();
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
        public void TestDDARequest(string directory, bool? wantReadDirectory = null, bool? wantWriteDirectory = null)
        {
            ConnectIfNeeded();

            Write("TestDDARequest");
            Write("Directory", directory);
            Write("WantReadDirectory", wantReadDirectory);
            Write("WantWriteDirectory", wantWriteDirectory);

            EndMessage();
        }

        /// <summary>
        /// It should be sent *AFTER* a TestDDAReply message is received. The node will respond with a TestDDAComplete message.
        /// </summary>
        /// <param name="directory">The directory files you want to access reside in. The node will send a protocol error error code if it doesn't know about it.</param>
        /// <param name="readContent">The content of the file your client has read from the ReadFilename</param>
        /// <remarks>The FCP client *HAS TO* clean up the files it creates; The node will cleanup files it has created.
        /// You can retry to enable DDA requests more than once for either the same or a different directory.
        /// Only last test result will be taken into account. If you enabled WantWriteAccess your FCP client should have
        ///  created the file *before* you send this message.</remarks>
        public void TestDDAResponse(string directory, string readContent)
        {
            ConnectIfNeeded();

            Write("TestDDAResponse");
            Write("Directory", directory);
            Write("ReadContent", readContent);

            EndMessage();
        }

        /// <summary>
        /// It asks the node to generate us an SSK keypair. The response will come back in a SSKKeypair message.
        /// </summary>
        /// <param name="identifier">the identifier does not have to be unique. That is, no IdentifierCollision
        /// is send if the identifier collides with the identifier of a get / put request or the identifier of
        /// another request to generate a SSK.</param>
        public void GenerateSSK(string identifier)
        {
            ConnectIfNeeded();

            Write("GenerateSSK");
            Write("Identifier", identifier);

            EndMessage();
        }



        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file including the data directly, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="metadataContentType">The MIME type of the data being inserted. For text, if charset is not specified, node should auto-detect it and force the auto-detected version</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxRetries">Number of times to retry if the first time doesn't work. -1 means retry forever.</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        /// <param name="getCHKOnly">If set to true, it won't actually insert the data, just return the key it would generate.If the key is USK, you may want to transform it into a SSK, to prevent the node spending time searching for an unused index.</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="dontCompress">Hint to node: don't try to compress the data, it's already compressed</param>
        /// <param name="clientToken">Sent back to client on the PersistentPut if this is a persistent request</param>
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: Connection)</param>
        /// <param name="targetFilename">Filename to be appended to a CHK insert. Technically it creates a one-file manifest with this filename pointing to the file being uploaded. Ignored for all types other than CHK, since other types have human-readable filenames anyway. Empty means no filename.</param>
        /// <param name="earlyEncode">It signals to the node that it should attempt to encode the whole file immediately and generate the key, and insert the top blocks, as soon as possible, rather than waiting until each layer has been inserted before inserting the layer above it.</param>
        /// <param name="fileHash">This field will allow you to override any TestDDA restriction if you provide a hash of the content which should be inserted. It should be computed like that : Base64Encode(SHA256( NodeHello.identifier + '-' + Identifier + '-' + content)). That setting has been introduced in 1027.</param>
        /// <param name="binaryBlob">If true, insert a binary blob (a collection of keys used in the downloading of a specific key or site). Implies no metadata, no URI.</param>
        /// <param name="data">A stream to insert directly to freenet</param>
        public void ClientPutDirect(string uri, string identifier, Stream data, string metadataContentType = null, VerbosityEnum? verbosity = null,
                                    long? maxRetries = null, PriorityClassEnum? priorityClass = null, bool? getCHKOnly = null, bool? global = null,
                                    bool? dontCompress = null, string clientToken = null, PersistenceEnum? persistence = null, string targetFilename = null,
                                    bool? earlyEncode = null, string fileHash = null, bool? binaryBlob = null)
        {
            ConnectIfNeeded();

            Write("ClientPut");
            Write("URI", uri);
            Write("Metadata.ContentType", metadataContentType);
            Write("Identifier", identifier);
            Write("Verbosity", (long)verbosity.Value);
            Write("maxRetries", maxRetries.Value);
            Write("PriorityClass", (long)priorityClass.Value);
            Write("GetCHKOnly", getCHKOnly);
            Write("Global", global);
            Write("DontCompress", dontCompress);
            Write("ClientToken", clientToken);
            Write("Persistence", persistence);
            Write("TargetFilename", targetFilename);
            Write("EarlyEncode", earlyEncode);
            Write("Filehash", fileHash);
            Write("binaryBlob", binaryBlob);
            Write("UploadFrom", "Direct");
            Write("DataLength", data.Length);

            EndMessage();

            // *** stream file ***
            var buffer = new byte[1024];
            int readbytes;
            while ((readbytes = data.Read(buffer, 0, buffer.Length)) != 0)
            {
                _client.GetStream().Write(buffer, 0, readbytes);
            }
            _client.GetStream().Flush();
        }

        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file on disk, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="metadataContentType">The MIME type of the data being inserted. For text, if charset is not specified, node should auto-detect it and force the auto-detected version</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxRetries">Number of times to retry if the first time doesn't work. -1 means retry forever.</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        /// <param name="getCHKOnly">If set to true, it won't actually insert the data, just return the key it would generate.If the key is USK, you may want to transform it into a SSK, to prevent the node spending time searching for an unused index.</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="dontCompress">Hint to node: don't try to compress the data, it's already compressed</param>
        /// <param name="clientToken">Sent back to client on the PersistentPut if this is a persistent request</param>
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: Connection)</param>
        /// <param name="targetFilename">Filename to be appended to a CHK insert. Technically it creates a one-file manifest with this filename pointing to the file being uploaded. Ignored for all types other than CHK, since other types have human-readable filenames anyway. Empty means no filename.</param>
        /// <param name="earlyEncode">It signals to the node that it should attempt to encode the whole file immediately and generate the key, and insert the top blocks, as soon as possible, rather than waiting until each layer has been inserted before inserting the layer above it.</param>
        /// <param name="fileHash">This field will allow you to override any TestDDA restriction if you provide a hash of the content which should be inserted. It should be computed like that : Base64Encode(SHA256( NodeHello.identifier + '-' + Identifier + '-' + content)). That setting has been introduced in 1027.</param>
        /// <param name="binaryBlob">If true, insert a binary blob (a collection of keys used in the downloading of a specific key or site). Implies no metadata, no URI.</param>
        /// <param name="file">The FileInfo for the File to be inserted</param>
        public void ClientPutDisk(string uri, string identifier, FileInfo file, string metadataContentType = null, VerbosityEnum? verbosity = null,
                                  long? maxRetries = null, PriorityClassEnum? priorityClass = null, bool? getCHKOnly = null, bool? global = null,
                                  bool? dontCompress = null, string clientToken = null, PersistenceEnum? persistence = null, string targetFilename = null,
                                  bool? earlyEncode = null, string fileHash = null, bool? binaryBlob = null)
        {
            ConnectIfNeeded();

            Write("ClientPut");
            Write("URI", uri);
            Write("Metadata.ContentType", metadataContentType);
            Write("Identifier", identifier);
            Write("Verbosity", (long)verbosity.Value);
            Write("maxRetries", maxRetries);
            Write("PriorityClass", (long)priorityClass.Value);
            Write("GetCHKOnly", getCHKOnly);
            Write("Global", global);
            Write("DontCompress", dontCompress);
            Write("ClientToken", clientToken);
            Write("Persistence", persistence);
            Write("TargetFilename", targetFilename);
            Write("EarlyEncode", earlyEncode);
            Write("Filehash", fileHash);
            Write("binaryBlob", binaryBlob);
            Write("UploadFrom", "disk");
            Write("Filename", file.FullName);

            EndMessage();
        }

        /// <summary>
        /// It is used to specify an insert into Freenet of a single file.
        /// This inserts a file redirecting to another key, A filename may be specified using the TargetFilename option.
        /// This is mostly useful with CHKs. The effect is to create a single file manifest which contains
        /// only the filename given, and points to the data just inserted. Thus the provided filename
        /// becomes the last part of the URI, and must be provided when fetching the data. See here for details.
        /// </summary>
        /// <param name="uri">The type of key to insert. When inserting an SSK key, you explicitly specifiy the version number. For a USK key, use a zero and it should automatically use the correct version number. (CHK@, KSK@name, SSK@privateKey/docname-1, USK@privateKey/docname/0/)</param>
        /// <param name="metadataContentType">The MIME type of the data being inserted. For text, if charset is not specified, node should auto-detect it and force the auto-detected version</param>
        /// <param name="identifier"> This is just for client to be able to identify files that have been inserted.</param>
        /// <param name="verbosity">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        /// <param name="maxRetries">Number of times to retry if the first time doesn't work. -1 means retry forever.</param>
        /// <param name="priorityClass">Priority of the insert (default 2: semi interactive)</param>
        /// <param name="getCHKOnly">If set to true, it won't actually insert the data, just return the key it would generate.If the key is USK, you may want to transform it into a SSK, to prevent the node spending time searching for an unused index.</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="dontCompress">Hint to node: don't try to compress the data, it's already compressed</param>
        /// <param name="clientToken">Sent back to client on the PersistentPut if this is a persistent request</param>
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: Connection)</param>
        /// <param name="targetFilename">Filename to be appended to a CHK insert. Technically it creates a one-file manifest with this filename pointing to the file being uploaded. Ignored for all types other than CHK, since other types have human-readable filenames anyway. Empty means no filename.</param>
        /// <param name="earlyEncode">It signals to the node that it should attempt to encode the whole file immediately and generate the key, and insert the top blocks, as soon as possible, rather than waiting until each layer has been inserted before inserting the layer above it.</param>
        /// <param name="fileHash">This field will allow you to override any TestDDA restriction if you provide a hash of the content which should be inserted. It should be computed like that : Base64Encode(SHA256( NodeHello.identifier + '-' + Identifier + '-' + content)). That setting has been introduced in 1027.</param>
        /// <param name="binaryBlob">If true, insert a binary blob (a collection of keys used in the downloading of a specific key or site). Implies no metadata, no URI.</param>
        /// <param name="targetURI">This is an existing freenet URI such as KSK@sample.txt. The idea is that you insert a new key that redirects to this one</param>
        public void ClientPutRedirect(string uri, string identifier, string targetURI, string metadataContentType = null,
                                      VerbosityEnum? verbosity = null, long? maxRetries = null, PriorityClassEnum? priorityClass = null,
                                      bool? getCHKOnly = null, bool? global = null, bool? dontCompress = null, string clientToken = null,
                                      PersistenceEnum? persistence = null, string targetFilename = null, bool? earlyEncode = null,
                                      string fileHash = null, bool? binaryBlob = null)
        {
            ConnectIfNeeded();

            Write("ClientPut");
            Write("URI", uri);
            Write("Metadata.ContentType", metadataContentType);
            Write("Identifier", identifier);
            Write("Verbosity", ((long)verbosity.Value));
            Write("MaxRetries", maxRetries);
            Write("PriorityClass", ((long)priorityClass.Value));
            Write("GetCHKOnly", getCHKOnly);
            Write("Global", global);
            Write("DontCompress", dontCompress);
            Write("ClientToken", clientToken);
            Write("Persistence", persistence);
            Write("TargetFilename", targetFilename);
            Write("EarlyEncode", earlyEncode);
            Write("Filehash", fileHash);
            Write("binaryBlob", binaryBlob);
            Write("UploadFrom", "redirect");
            Write("TargetURI", targetURI);

            EndMessage();
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
        /// <param name="persistence">Whether the insert stays on the queue across new client connections, freenet restarts, or forever (Default: Connection)</param>
        /// <param name="global">Whether the insert is visible on the global queue or not. If you set this as true, you should probably do a WatchGlobal, or you won't get any PutSuccessful / PutFailure message</param>
        /// <param name="defaultName">The item to display when someone requests the Uri only (without the item name part)</param>
        /// <param name="filename">The filename for the File to be inserted</param>
        /// <param name="allowUnreadableFiles">unless true, any unreadable files cause the whole request to fail; optional</param>
        public void ClientPutDiskDir(string identifier, string uri, string defaultName, string filename,
                                     bool allowUnreadableFiles, VerbosityEnum? verbosity = null, long? maxRetries = null,
                                     PriorityClassEnum? priorityClass = null, bool? getCHKOnly = null, bool? dontCompress = null,
                                        string clientToken = null, PersistenceEnum? persistence = null, bool? global = null)
        {
            ConnectIfNeeded();

            Write("ClientPutDiskDir");
            Write("Identifier", identifier);
            Write("Verbosity", (long?)verbosity);
            Write("MaxRetries", maxRetries);
            Write("PriorityClass", (long?)priorityClass);
            Write("URI", uri);
            Write("GetCHKOnly", getCHKOnly);
            Write("DontCompress", dontCompress);
            Write("ClientToken", clientToken);
            Write("Persistence", persistence);
            Write("Global", global);
            Write("DefaultName", defaultName);
            Write("Filename", filename);
            Write("AllowUnreadableFiles", allowUnreadableFiles);

            EndMessage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="identifier"></param>
        /// <param name="verbosity"></param>
        /// <param name="maxRetries"></param>
        /// <param name="priorityClass"></param>
        /// <param name="getCHKOnly"></param>
        /// <param name="global"></param>
        /// <param name="dontCompress"></param>
        /// <param name="clientToken"></param>
        /// <param name="persistence"></param>
        /// <param name="targetFilename"></param>
        /// <param name="earlyEncode"></param>
        /// <param name="defaultName"></param>
        /// <param name="filelist"></param>
        public void ClientPutComplexDir(string uri, string identifier, List<InsertItem> filelist, VerbosityEnum? verbosity = null, long? maxRetries = null,
                                        PriorityClassEnum? priorityClass = null, bool? getCHKOnly = null, bool? global = null, bool? dontCompress = null,
                                        string clientToken = null, PersistenceEnum? persistence = null, string targetFilename = null, bool? earlyEncode = null,
                                        string defaultName = null)
        {
            ConnectIfNeeded();

            Write("ClientPutComplexDir");
            Write("URI", uri);
            Write("Identifier", identifier);

            Write("Verbosity", ((long?)verbosity));
            Write("MaxRetries", maxRetries);
            Write("PriorityClass", ((long?)priorityClass));
            Write("GetCHKOnly", getCHKOnly);
            Write("Global", global);
            Write("DontCompress", dontCompress);
            Write("ClientToken", clientToken);
            Write("Persistence", persistence);
            Write("TargetFilename", targetFilename);
            Write("EarlyEncode", earlyEncode);
            Write("DefaultName", defaultName);

            long counter = 0;
            foreach (var file in filelist)
            {
                Write("Files." + counter + ".Name", file.Name);
                var dataItem = file as DataItem;
                if (dataItem != null)
                {
                    Write("Files." + counter + ".UploadFrom", "Direct");
                    Write("Files." + counter + ".DataLength", dataItem.Data.Length);
                    Write("Files." + counter + ".Metadata.ContentType", dataItem.ContentType);
                }
                var fileItem = file as FileItem;
                if (fileItem != null)
                {
                    Write("Files." + counter + ".UploadFrom=disk");
                    Write("Files." + counter + ".Filename", fileItem.Filename);
                    Write("Files." + counter + ".Metadata.ContentType", fileItem.ContentType);

                }
                var redirectItem = file as RedirectItem;
                if (redirectItem != null)
                {
                    Write("Files." + counter + ".UploadFrom", "redirect");
                    Write("Files." + counter + ".TargetURI", redirectItem.TargetURI);

                }
                counter++;
            }

            EndMessage();

            /* this appending is very experimental since there is no example in the documentation
             * I think all the data from Direct files are concatenated directly in the same order
             * as in the list above. (TODO: Verfiy expected behaviour)
             * We flush all DataItems, and ignore obviously everything else.
             * I hope the FN node is parsing multiple files correctly.
             * * * * * * * */

            var buffer = new byte[1024];

            foreach (var file in filelist.OfType<DataItem>())
            {
                int readbytes;
                while ((readbytes = (file).Data.Read(buffer, 0, buffer.Length)) != 0)
                {
                    _client.GetStream().Write(buffer, 0, readbytes);
                }
            }
            _client.GetStream().Flush();
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
        /// <param name="allowedMimeTypes">If set, only allow certain MIME types in the returned data. If the data is of a MIME type which isn't listed, the request will fail with a WRONG_MIME_TYPE error (code 29) as soon as it realises this.</param>
        /// <param name="returnType">Direct: return the data directly to the client via an AllData message, once we
        /// have all of it. (For persistent requests, the client will get a DataFound message but must send a
        /// GetRequestStatus to ask for the AllData). none = don't return the data at all, just fetch it to the node
        /// and tell the client when we have finished. disk = write the data to disk. In future, chunked may also be
        /// supported (return it in segments as they are ready), but this is not yet implemented. If you download to
        /// disk, you have to do a  TestDDARequest.</param>
        /// <param name="filename">Name and path of the file where the download is to be stored.</param>
        /// <param name="tempFilename">Name and path of a temporary file where the partial download is to be stored.</param>
        public void ClientGet(string uri, string identifier, bool? ignoreDS = null, bool? dsonly = null,
                              VerbosityEnum? verbosity = null, long? maxSize = null, long? maxTempSize = null, long? maxRetries = null, PriorityClassEnum? priorityClass = null,
                              PersistenceEnum? persistence = null, string clientToken = null, bool? global = null, bool? binaryBlob = null,
                              string allowedMimeTypes = null, ReturnTypeEnum? returnType = null, string filename = null, string tempFilename = null)
        {
            ConnectIfNeeded();

            Write("ClientGet");
            Write("IgnoreDS", ignoreDS);
            Write("DSonly", dsonly);
            Write("URI", uri);
            Write("Identifier", identifier);
            Write("Verbosity", ((long?)verbosity));
            Write("MaxSize", maxSize);
            Write("MaxTempSize", maxTempSize);
            Write("MaxRetries", maxRetries);
            Write("PriorityClass", ((long?)priorityClass));
            if (global.HasValue && global.Value && persistence.HasValue && persistence.Value == PersistenceEnum.Connection)
            {
                throw new FormatException("Error, global request must be persistent");
            }
            Write("Persistence", persistence);
            Write("ClientToken", clientToken);
            Write("Global", global);
            Write("BinaryBlob", binaryBlob);
            Write("AllowedMIMETypes", allowedMimeTypes);

            if (returnType != null)
            {
                Write("ReturnType", returnType);
                if (returnType == ReturnTypeEnum.Disk)
                {
                    Write("Filename", filename);
                    Write("TempFileName", tempFilename);
                }
            }

            EndMessage();
        }

        /// <summary>
        /// This command asks the Freenet node to load the plugin from the given location. The node will respond with a PluginInfo Event.
        /// </summary>
        /// <param name="identifier">This is for client to be able to identify responses</param>
        /// <param name="pluginUrl">An URI that point to the plugin location. official:the name of the official plugin file:a local file path freenet: a Freenet URL url:any url that your java vm can deal with</param>
        /// <param name="urlType">Type of plugin source. (currently autodection does not work if the local file does not exist or type is 'url')</param>
        /// <param name="store">If true, the plugin url is written to config.</param>
        /// <param name="officialSource">Means of obtaining an official plugin: freenet or https. "freenet" is strongly recommended and is the default unless the network security level is LOW. "https" may be faster and/or more reliable, but is traceable.</param>
        public void LoadPlugin(string identifier, string pluginUrl, UrlTypeEnum? urlType = null, bool? store = null, OfficialSourceTypeEnum? officialSource = null)
        {
            ConnectIfNeeded();

            Write("LoadPlugin");
            Write("Identifier", identifier);
            Write("PluginURL", pluginUrl);
            if (urlType.HasValue)
                Write("URLType", urlType.Value.ToProtocolString());
            if (store.HasValue)
                Write("Store", store.Value.ToProtocolString());
            if (officialSource.HasValue)
                Write("Source", officialSource.Value.ToProtocolString());

            EndMessage();
        }

        /// <summary>
        /// This command asks the Freenet node to reload the given plugin The node will respond with a PluginInfo Event.
        /// </summary>
        /// <param name="identifier">This is for client to be able to identify responses</param>
        /// <param name="pluginName">A name to identify the plugin. Must be the same as class name shown on plugins page</param>
        /// <param name="purge">If true, the cached copy of the plugin is removed.</param>
        public void ReloadPlugin(string identifier, string pluginName, bool? purge = null)
        {
            ConnectIfNeeded();

            Write("ReloadPlugin");
            Write("Identifier", identifier);
            Write("PluginName", pluginName);
            if (purge.HasValue)
                Write("Purge", purge.Value.ToProtocolString());

            EndMessage();
        }

        /// <summary>
        /// This command asks the Freenet node to remove the given plugin. The node will respond with a PluginRemoved event.
        /// </summary>
        /// <param name="identifier">This is for client to be able to identify responses</param>
        /// <param name="pluginName">A name to identify the plugin. Must be the same as class name shown on plugins page</param>
        /// <param name="purge"> 	If true, the cached copy of the plugin is removed.</param>
        public void RemovePlugin(string identifier, string pluginName, bool? purge = null)
        {
            ConnectIfNeeded();

            Write("RemovePlugin");
            Write("Identifier", identifier);
            Write("PluginName", pluginName);
            if (purge.HasValue)
                Write("Purge", purge.Value.ToProtocolString());

            EndMessage();
        }

        /// <summary>
        /// This message asks the Freenet node for the presence of the given pluginname The node will respond with a PluginInfo event.
        /// </summary>
        /// <param name="pluginName">A name to identify the plugin. Must be the same as class name shown on plugins page</param>
        /// <param name="identifier">This is for client to be able to identify responses</param>
        /// <param name="detailed">If true, detailed information is returned (requires full access)</param>
        public void GetPluginInfo(string pluginName, string identifier = null, bool? detailed = null)
        {
            ConnectIfNeeded();

            Write("GetPluginInfo");
            Write("PluginName", pluginName);
            Write("Identifier", identifier);
            Write("Detailed", detailed);

            EndMessage();
        }

        /// <summary>
        /// This message is used to send a messege to a plugin. The plugin may or may not respond with a FCPPluginReply message.
        /// </summary>
        /// <param name="pluginName">A name to identify the plugin. Must be the same as class name shown on plugins page</param>
        /// <param name="identifier">Not mandatory yet, but you should use it. May the plugin responds in random order or delayed to many calls at once. Depends on plugin implementation.</param>
        /// <param name="data">Data if data is passed along with the message</param>
        /// <param name="pluginParams">An amount of param.item=value pairs. Added as a Dictionary, see example</param>
        /// <example>
        /// Set: (Param.Hello=world)
        /// <code>
        /// Dictionary&lt;tring, string&gt; dict = new Dictionary&lt;string, string&gt;();
        /// dict.Add("Param.Hello", "world");
        /// fcpclient.FCPPluginMessage("plugins.HelloFCP.HelloFCP", "id1", null, dict);
        ///</code></example>
        public void FCPPluginMessage(string pluginName, Dictionary<string, string> pluginParams, string identifier = null, Stream data = null)
        {
            ConnectIfNeeded();

            if (pluginParams == null)
            {
                pluginParams = new Dictionary<string, string>();
            }

            Write("FCPPluginMessage");
            Write("PluginName", pluginName);
            Write("Identifier", identifier);
            if (data != null)
                Write("DataLength", data.Length);
            foreach (var param in pluginParams)
            {
                Write(param.Key + "", param.Value);
            }

            EndMessage();

            // *** stream binary data if any ***
            if (data != null)
            {
                var buffer = new byte[1024];
                int readbytes;
                while ((readbytes = data.Read(buffer, 0, buffer.Length)) != 0)
                {
                    _client.GetStream().Write(buffer, 0, readbytes);
                }
            }
            _client.GetStream().Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="identifier"></param>
        /// <param name="dontPoll"></param>
        public void SubscribeUSK(string uri, string identifier, bool? dontPoll = null)
        {
            ConnectIfNeeded();

            Write("SubscribeUSK");
            Write("URI", uri);
            Write("Identifier", identifier);
            Write("DontPoll", dontPoll);

            EndMessage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identifier"></param>
        public void UnsubscribeUSK(string identifier)
        {
            ConnectIfNeeded();

            Write("SubscribeUSK");
            Write("Identifier", identifier);

            EndMessage();
        }

        /// <summary>
        /// It makes the global queue of inserts and retrievals visible or invisible to the client.
        /// </summary>
        /// <param name="enabled">enable Global Watch</param>
        /// <param name="verbosityMask">report when complete, SimpleProgress messages, send StartedCompression and FinishedCompression messages (usable as Flag)</param>
        public void WatchGlobal(bool enabled, VerbosityEnum verbosityMask)
        {
            ConnectIfNeeded();

            Write("WatchGlobal");
            Write("Enabled", enabled);
            Write("VerbosityMask", ((long)verbosityMask));

            EndMessage();
        }

        /// <summary>
        /// It asks the node to provide all it knows about the current progress of the request.
        /// Normally for a persistent, Direct, completed ClientGet this will be a PersistentGet
        /// message followed by a DataFound message (maybe a SimpleProgress message first), and
        /// then an AllData message with the actual data.
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">Whether to look on the global queue for inserts and downloads</param>
        /// <param name="onlyData">Whether to just include the AllData message (for Direct ClientGet's). If false, all status messages for that request will be included.</param>
        public void GetRequestStatus(string identifier, bool? global = null, bool? onlyData = null)
        {
            ConnectIfNeeded();

            Write("GetRequestStatus");
            Write("Identifier", identifier);
            Write("Global", global);
            Write("OnlyData", onlyData);

            EndMessage();
        }

        /// <summary>
        /// Ask the node to list all persistent requests for this client on the node (and for the global
        /// queue too if watch global queue is enabled). Node will respond with a series of PersistentGet
        /// or PersistentPut messages, possibly some DataFound, GetFailed, PutFailed or PutSuccessful
        /// messages, and possibly some status messages (SimpleProgress, StartedCompression or
        /// FinishedCompression), and then an EndListPersistentRequests message.
        /// </summary>
        public void ListPersistentRequests()
        {
            ConnectIfNeeded();

            Write("ListPersistentRequests");

            EndMessage();
        }

        /// <summary>
        /// It asks the Freenet node to remove the request (persistent or not) that is identified by the Identifier value. Note that you must specify Global=true if it is a global request - even if WatchGlobal is enabled.
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">The Global field specifies whether the request is on the global queue or not (Identifier namespaces are separate for the global and local queue).</param>
        public void RemoveRequest(string identifier, bool global)
        {
            ConnectIfNeeded();

            Write("RemoveRequest");
            Write("Identifier", identifier);
            Write("Global", global);

            EndMessage();
        }

        /// <summary>
        /// It modifies the ClientToken and PriorityClass fields of the persistent request that is identified by
        /// the Identifier value. Since 1016: The node will confirm
        /// the modify of the request by sending a PersistentRequestModified to the client that sent the
        /// ModifyPersistentRequest message, and to each client that listens to the global queue
        /// (if the request is on the global queue). 
        /// ClientToken and PriorityClass are optional, but at least one of them should be set.
        /// </summary>
        /// <param name="identifier">The unique identifier of the queued insert or download.</param>
        /// <param name="global">The Global field specifies whether the request is on the global queue or not (Identifier namespaces are separate for the global and local queue).</param>
        /// <param name="clientToken">New Client Token</param>
        /// <param name="priorityClass">Priority of the insert</param>
        public void ModifyPersistentRequest(string identifier, bool global, string clientToken = null, PriorityClassEnum? priorityClass = null)
        {
            ConnectIfNeeded();

            Write("ModifyPersistentRequest");
            Write("Identifier", identifier);
            Write("Global", global);
            Write("ClientToken", clientToken);
            Write("PriorityClass", (long?)priorityClass);

            EndMessage();
        }

        /// <summary>
        /// This command allows an client program to remotely shut down a Freenet node. A confirmation message will be sent (ProtocolError code 18: Shutting down)
        /// </summary>
        public void Shutdown()
        {
            ConnectIfNeeded();

            Write("Shutdown");

            EndMessage();
        }

        /// <summary>
        /// Disconnect will immediately disconnect the FCP client from the server. 
        /// No confirmation message is sent by the node. Disconnect contains no data. 
        /// </summary>
        private void RealDisconnect()
        {
            Write("Disconnect");

            EndMessage();
        }

        /// <summary>
        /// This command is ignored by the Freenet node and can be used to prevent connection timeouts. The node does not respond with any event to it. Literally, a void command.
        /// </summary>
        public void Void()
        {
            ConnectIfNeeded();

            Write("Void");

            EndMessage();
        }
        #endregion


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _fnread.Dispose();
                    _fnwrite.Dispose();
                    _client.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
