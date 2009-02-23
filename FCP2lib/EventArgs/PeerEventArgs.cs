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
using System.Net;

namespace Freenet.FCP2 {
    
    public class PeerEventArgs : EventArgs {

        private string lastGoodVersion;
        
        public string LastGoodVersion {
            get { return lastGoodVersion; }
        }
        
        private bool opennet;
        
        public bool Opennet {
            get { return opennet; }
        }
        
        private string myName;
        
        public string MyName {
            get { return myName; }
        }
        
        private string identity;
        
        public string Identity {
            get { return identity; }
        }
        
        private double location;
        
        public double Location {
            get { return location; }
        }
        
        private bool testnet;
        
        public bool Testnet {
            get { return testnet; }
        }
        
        private string version;
        
        public string Version {
            get { return version; }
        }
        
        private PhysicalType physical;
        
        public PhysicalType Physical {
            get { return physical; }
        }
        
        private ArkType ark;
        
        public ArkType Ark {
            get { return ark; }
        }
        
        private DsaPubKeyType dsaPubKey;
        
        public DsaPubKeyType DsaPubKey {
            get { return dsaPubKey; }
        }
        
        private DsaGroupType dsaGroup;
        
        public DsaGroupType DsaGroup {
            get { return dsaGroup; }
        }
        
        private AuthType auth;
        
        public AuthType Auth {
            get { return auth; }
        }
        
        private VolatileType @volatile = null;
        
        public VolatileType Volatile {
            get { return @volatile; }
        }
        
        private MetadataType metadata = null;
        
        public MetadataType Metadata {
            get { return metadata; }
        }
        
        /// <summary>
        /// PeerEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PeerEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.lastGoodVersion = parsed["lastGoodVersion"];
            this.opennet = bool.Parse(parsed["opennet"]);
            this.myName = parsed["myName"];
            this.identity = parsed["identity"];
            this.location = double.Parse(parsed["location"]);
            this.testnet = bool.Parse(parsed["testnet"]);
            this.version = parsed["version"];
            physical = new PhysicalType(parsed);
            ark = new ArkType(parsed);
            dsaPubKey = new DsaPubKeyType(parsed);
            dsaGroup = new DsaGroupType(parsed);
            auth = new AuthType(parsed);
            if (parsed["volatile.averagePingTime"]!=null)
                @volatile = new VolatileType(parsed);
            if (parsed["metadata.routableConnectionCheckCount"]!=null)
                metadata = new MetadataType(parsed);
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
        
        public class PhysicalType {
            IPEndPoint udp;
            
            public IPEndPoint UDP {
                get { return udp; }
            }
            
            internal PhysicalType(MessageParser parsed) {
                string[] ip = parsed["physical.udp"].Split(new char[] {':'});
                this.udp = new IPEndPoint(IPAddress.Parse(ip[0]), int.Parse(ip[1]));
            }
        }

        public class ArkType {
            private string pubURI;
            
            public string PubURI {
                get { return pubURI; }
            }
            
            private int number;
            
            public int Number {
                get { return number; }
            }
            
            internal ArkType(MessageParser parsed) {
                this.pubURI = parsed["ark.pubURI"];
                this.number = int.Parse(parsed["ark.number"]);
            }
            
        }
        
        public class DsaPubKeyType {
            private string y;
            
            public string Y {
                get { return y; }
            }
            
            internal DsaPubKeyType(MessageParser parsed) {
                this.y = parsed["dsaPubKey.y"];
            }
        }

        public class DsaGroupType {
            private string p;
            
            public string P {
                get { return p; }
            }
            
            private string g;
            
            public string G {
                get { return g; }
            }
            
            private string q;
            
            public string Q {
                get { return q; }
            }
            
            internal DsaGroupType(MessageParser parsed) {
                this.p = parsed["dsaGroup.p"];
                this.g = parsed["dsaGroup.g"];
                this.q = parsed["dsaGroup.q"];
            }
        }
        
        public class AuthType {
            private int negTypes;
            
            public int NegTypes {
                get { return negTypes; }
            }
            
            internal AuthType(MessageParser parsed) {
                this.negTypes = int.Parse(parsed["auth.negTypes"]);
            }
        }
        
        public class VolatileType {
            private double averagePing;
            
            public double AveragePing {
                get { return averagePing; }
            }

            private double overloadProbability;
            
            public double OverloadProbability {
                get { return overloadProbability; }
            }
            
            private double percentTimeRoutableConnection;
            
            public double PercentTimeRoutableConnection {
                get { return percentTimeRoutableConnection; }
            }
            
            private double routingBackoffPercent;
            
            public double RoutingBackoffPercent {
                get { return routingBackoffPercent; }
            }
            
            private string status;
            
            public string Status {
                get { return status; }
            }
            
            private int totalBytesIn;
            
            public int TotalBytesIn {
                get { return totalBytesIn; }
            }
            
            private int routingBackoffLength;
            
            public int RoutingBackoffLength {
                get { return routingBackoffLength; }
            }
            
            private string lastRoutingBackoffReason;
            
            public string LastRoutingBackoffReason {
                get { return lastRoutingBackoffReason; }
            }
            
            private int routingBackoff;
            
            public int RoutingBackoff {
                get { return routingBackoff; }
            }
            
            private int totalBytesOut;
            
            public int TotalBytesOut {
                get { return totalBytesOut; }
            }
            
            internal VolatileType(MessageParser parsed) {
                this.averagePing = double.Parse(parsed["volatile.averagePingTime"]);
                this.overloadProbability = double.Parse(parsed["volatile.overloadProbability"]);
                this.percentTimeRoutableConnection = double.Parse(parsed["volatile.percentTimeRoutableConnection"]);
                this.routingBackoffPercent = double.Parse(parsed["volatile.routingBackoffPercent"]);
                this.status = parsed["volatile.status"];
                this.totalBytesIn = int.Parse(parsed["volatile.totalBytesIn"]);
                this.routingBackoffLength = int.Parse(parsed["volatile.routingBackoffLength"]);
                this.lastRoutingBackoffReason = parsed["volatile.lastRoutingBackoffReason"];
                this.routingBackoff = int.Parse(parsed["volatile.routingBackoff"]);
                this.totalBytesOut = int.Parse(parsed["volatile.totalBytesOut"]);
            }
        }
        
        public class MetadataType {
            
            private int routableConnectionCheckCount;
            
            public int RoutableConnectionCheckCount {
                get { return routableConnectionCheckCount; }
            }
            
            private int hadRoutableConnectionCount;
            
            public int HadRoutableConnectionCount {
                get { return hadRoutableConnectionCount; }
            }
            
            private DateTime timeLastConnected;
            
            public DateTime TimeLastConnected {
                get { return timeLastConnected; }
            }
            
            private DateTime timeLastSuccess;
            
            public DateTime TimeLastSuccess {
                get { return timeLastSuccess; }
            }
            
            private DateTime timeLastRoutable;
            
            public DateTime TimeLastRoutable {
                get { return timeLastRoutable; }
            }
            
            private DateTime timeLastReceivedPacket;
            
            public DateTime TimeLastReceivedPacket {
                get { return timeLastReceivedPacket; }
            }
            
            private DetectedType detected;
            
            public DetectedType Detected {
                get { return detected; }
            }
            
            internal MetadataType(MessageParser parsed) {
                this.routableConnectionCheckCount = int.Parse(parsed[" metadata.routableConnectionCheckCount"]);
                this.hadRoutableConnectionCount = int.Parse(parsed[" metadata.hadRoutableConnectionCount"]);
                this.timeLastConnected = FCP2.FromUnix(parsed[" metadata.timeLastConnected"]);
                this.timeLastSuccess = FCP2.FromUnix(parsed[" metadata.timeLastSuccess"]);
                this.timeLastRoutable = FCP2.FromUnix(parsed[" metadata.timeLastRoutable"]);
                this.timeLastReceivedPacket = FCP2.FromUnix(parsed[" metadata.timeLastReceivedPacket"]);
                this.detected =  new DetectedType(parsed);
            }
            
            public class DetectedType {
                IPEndPoint udp;
                
                public IPEndPoint UDP {
                    get { return udp; }
                }
                
                internal DetectedType(MessageParser parsed) {
                    string[] ip = parsed["metadata.detected.udp"].Split(new char[] {':'});
                    this.udp = new IPEndPoint(IPAddress.Parse(ip[0]), int.Parse(ip[1]));
                }
            }
        }
    }
}