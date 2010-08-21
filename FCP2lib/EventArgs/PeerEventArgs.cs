/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2010 Thomas Bruderer <apophis@apophis.ch>
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
using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class PeerEventArgs : System.EventArgs
    {
        private readonly ArkType ark;
        private readonly AuthType auth;
        private readonly DsaGroupType dsaGroup;
        private readonly DsaPubKeyType dsaPubKey;
        private readonly string identity;
        private readonly string lastGoodVersion;
        private readonly double location;
        private readonly MetadataType metadata;
        private readonly string myName;
        private readonly bool opennet;
        private readonly PhysicalType physical;
        private readonly bool testnet;
        private readonly string version;
        private readonly VolatileType @volatile;

        /// <summary>
        /// PeerEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PeerEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            lastGoodVersion = parsed.lastGoodVersion;
            opennet = parsed.opennet;
            myName = parsed.myName;
            identity = parsed.identity;
            location = parsed.location;
            testnet = parsed.testnet;
            version = parsed.version;
            physical = new PhysicalType(parsed);
            ark = new ArkType(parsed);
            dsaPubKey = new DsaPubKeyType(parsed);
            dsaGroup = new DsaGroupType(parsed);
            auth = new AuthType(parsed);
            @volatile = new VolatileType(parsed);
            metadata = new MetadataType(parsed);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string LastGoodVersion
        {
            get { return lastGoodVersion; }
        }

        public bool Opennet
        {
            get { return opennet; }
        }

        public string MyName
        {
            get { return myName; }
        }

        public string Identity
        {
            get { return identity; }
        }

        public double Location
        {
            get { return location; }
        }

        public bool Testnet
        {
            get { return testnet; }
        }

        public string Version
        {
            get { return version; }
        }

        public PhysicalType Physical
        {
            get { return physical; }
        }

        public ArkType Ark
        {
            get { return ark; }
        }

        public DsaPubKeyType DsaPubKey
        {
            get { return dsaPubKey; }
        }

        public DsaGroupType DsaGroup
        {
            get { return dsaGroup; }
        }

        public AuthType Auth
        {
            get { return auth; }
        }

        public VolatileType @Volatile
        {
            get { return @volatile; }
        }

        public MetadataType Metadata
        {
            get { return metadata; }
        }

        #region Nested type: ArkType

        public class ArkType
        {
            private readonly long number;
            private readonly string pubURI;

            internal ArkType(dynamic parsed)
            {
                pubURI = parsed.ark.pubURI;
                number = parsed.ark.number;
            }

            public string PubURI
            {
                get { return pubURI; }
            }

            public long Number
            {
                get { return number; }
            }
        }

        #endregion

        #region Nested type: AuthType

        public class AuthType
        {
            private readonly long negTypes;

            internal AuthType(dynamic parsed)
            {
                negTypes = parsed.auth.negTypes;
            }

            public long NegTypes
            {
                get { return negTypes; }
            }
        }

        #endregion

        #region Nested type: DsaGroupType

        public class DsaGroupType
        {
            private readonly string g;
            private readonly string p;
            private readonly string q;

            internal DsaGroupType(dynamic parsed)
            {
                p = parsed.dsaGroup.p;
                g = parsed.dsaGroup.g;
                q = parsed.dsaGroup.q;
            }

            public string P
            {
                get { return p; }
            }

            public string G
            {
                get { return g; }
            }

            public string Q
            {
                get { return q; }
            }
        }

        #endregion

        #region Nested type: DsaPubKeyType

        public class DsaPubKeyType
        {
            private readonly string y;

            internal DsaPubKeyType(dynamic parsed)
            {
                y = parsed.dsaPubKey.y;
            }

            public string Y
            {
                get { return y; }
            }
        }

        #endregion

        #region Nested type: MetadataType

        public class MetadataType
        {
            private readonly DetectedType detected;
            private readonly long hadRoutableConnectionCount;
            private readonly long routableConnectionCheckCount;
            private readonly DateTime timeLastConnected;
            private readonly DateTime timeLastReceivedPacket;
            private readonly DateTime timeLastRoutable;
            private readonly DateTime timeLastSuccess;

            internal MetadataType(dynamic parsed)
            {
                routableConnectionCheckCount = parsed.metadata.routableConnectionCheckCount;
                hadRoutableConnectionCount = parsed.metadata.hadRoutableConnectionCount;
                timeLastConnected = parsed.metadata.timeLastConnected;
                timeLastSuccess = parsed.metadata.timeLastSuccess;
                timeLastRoutable = parsed.metadata.timeLastRoutable;
                timeLastReceivedPacket = parsed.metadata.timeLastReceivedPacket;
                detected = new DetectedType(parsed);
            }

            public long RoutableConnectionCheckCount
            {
                get { return routableConnectionCheckCount; }
            }

            public long HadRoutableConnectionCount
            {
                get { return hadRoutableConnectionCount; }
            }

            public DateTime TimeLastConnected
            {
                get { return timeLastConnected; }
            }

            public DateTime TimeLastSuccess
            {
                get { return timeLastSuccess; }
            }

            public DateTime TimeLastRoutable
            {
                get { return timeLastRoutable; }
            }

            public DateTime TimeLastReceivedPacket
            {
                get { return timeLastReceivedPacket; }
            }

            public DetectedType Detected
            {
                get { return detected; }
            }

            #region Nested type: DetectedType

            public class DetectedType
            {
                private readonly IPEndPoint udp;

                internal DetectedType(dynamic parsed)
                {
                    string[] ip = parsed.metadata.detected.udp.Split(new[] { ':' });
                    var ipAddress = IPAddress.Parse(ip[0]);
                    if (ipAddress != null) udp = new IPEndPoint((ipAddress), int.Parse(ip[1]));
                }

                public IPEndPoint UDP
                {
                    get { return udp; }
                }
            }

            #endregion
        }

        #endregion

        #region Nested type: PhysicalType

        public class PhysicalType
        {
            private readonly IPEndPoint udp;

            internal PhysicalType(dynamic parsed)
            {
                string[] ip = parsed.physical.udp.Split(new[] { ':' });
                var ipAddress = IPAddress.Parse(ip[0]);
                if (ipAddress != null) udp = new IPEndPoint((ipAddress), int.Parse(ip[1]));
            }

            public IPEndPoint UDP
            {
                get { return udp; }
            }
        }

        #endregion

        #region Nested type: @VolatileType

        public class VolatileType
        {
            private readonly double averagePing;
            private readonly string lastRoutingBackoffReason;
            private readonly double overloadProbability;
            private readonly double percentTimeRoutableConnection;
            private readonly long routingBackoff;
            private readonly long routingBackoffLength;
            private readonly double routingBackoffPercent;
            private readonly string status;
            private readonly long totalBytesIn;
            private readonly long totalBytesOut;

            internal VolatileType(dynamic parsed)
            {
                averagePing = parsed.@volatile.averagePingTime;
                overloadProbability = parsed.@volatile.overloadProbability;
                percentTimeRoutableConnection = parsed.@volatile.percentTimeRoutableConnection;
                routingBackoffPercent = parsed.@volatile.routingBackoffPercent;
                status = parsed.@volatile.status;
                totalBytesIn = parsed.@volatile.totalBytesIn;
                routingBackoffLength = parsed.@volatile.routingBackoffLength;
                lastRoutingBackoffReason = parsed.@volatile.lastRoutingBackoffReason;
                routingBackoff = parsed.@volatile.routingBackoff;
                totalBytesOut = parsed.@volatile.totalBytesOut;
            }

            public double AveragePing
            {
                get { return averagePing; }
            }

            public double OverloadProbability
            {
                get { return overloadProbability; }
            }

            public double PercentTimeRoutableConnection
            {
                get { return percentTimeRoutableConnection; }
            }

            public double RoutingBackoffPercent
            {
                get { return routingBackoffPercent; }
            }

            public string Status
            {
                get { return status; }
            }

            public long TotalBytesIn
            {
                get { return totalBytesIn; }
            }

            public long RoutingBackoffLength
            {
                get { return routingBackoffLength; }
            }

            public string LastRoutingBackoffReason
            {
                get { return lastRoutingBackoffReason; }
            }

            public long RoutingBackoff
            {
                get { return routingBackoff; }
            }

            public long TotalBytesOut
            {
                get { return totalBytesOut; }
            }
        }

        #endregion
    }
}