using System;
using System.Net;

namespace Freenet.FCP2
{

    public class PeerEventArgs : EventArgs
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
        internal PeerEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif

            lastGoodVersion = parsed["lastGoodVersion"];
            opennet = bool.Parse(parsed["opennet"]);
            myName = parsed["myName"];
            identity = parsed["identity"];
            location = double.Parse(parsed["location"]);
            testnet = bool.Parse(parsed["testnet"]);
            version = parsed["version"];
            physical = new PhysicalType(parsed);
            ark = new ArkType(parsed);
            dsaPubKey = new DsaPubKeyType(parsed);
            dsaGroup = new DsaGroupType(parsed);
            auth = new AuthType(parsed);
            if (parsed["volatile.averagePingTime"] != null)
                @volatile = new VolatileType(parsed);
            if (parsed["metadata.routableConnectionCheckCount"] != null)
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

        public VolatileType Volatile
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

            internal ArkType(MessageParser parsed)
            {
                pubURI = parsed["ark.pubURI"];
                number = long.Parse(parsed["ark.number"]);
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

            internal AuthType(MessageParser parsed)
            {
                negTypes = long.Parse(parsed["auth.negTypes"]);
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

            internal DsaGroupType(MessageParser parsed)
            {
                p = parsed["dsaGroup.p"];
                g = parsed["dsaGroup.g"];
                q = parsed["dsaGroup.q"];
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

            internal DsaPubKeyType(MessageParser parsed)
            {
                y = parsed["dsaPubKey.y"];
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

            internal MetadataType(MessageParser parsed)
            {
                routableConnectionCheckCount = long.Parse(parsed[" metadata.routableConnectionCheckCount"]);
                hadRoutableConnectionCount = long.Parse(parsed[" metadata.hadRoutableConnectionCount"]);
                timeLastConnected = FCP2.FromUnix(parsed[" metadata.timeLastConnected"]);
                timeLastSuccess = FCP2.FromUnix(parsed[" metadata.timeLastSuccess"]);
                timeLastRoutable = FCP2.FromUnix(parsed[" metadata.timeLastRoutable"]);
                timeLastReceivedPacket = FCP2.FromUnix(parsed[" metadata.timeLastReceivedPacket"]);
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

                internal DetectedType(MessageParser parsed)
                {
                    string[] ip = parsed["metadata.detected.udp"].Split(new[] { ':' });
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

            internal PhysicalType(MessageParser parsed)
            {
                string[] ip = parsed["physical.udp"].Split(new[] { ':' });
                var ipAddress = IPAddress.Parse(ip[0]);
                if (ipAddress != null) udp = new IPEndPoint((ipAddress), int.Parse(ip[1]));
            }

            public IPEndPoint UDP
            {
                get { return udp; }
            }
        }

        #endregion

        #region Nested type: VolatileType

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

            internal VolatileType(MessageParser parsed)
            {
                averagePing = double.Parse(parsed["volatile.averagePingTime"]);
                overloadProbability = double.Parse(parsed["volatile.overloadProbability"]);
                percentTimeRoutableConnection = double.Parse(parsed["volatile.percentTimeRoutableConnection"]);
                routingBackoffPercent = double.Parse(parsed["volatile.routingBackoffPercent"]);
                status = parsed["volatile.status"];
                totalBytesIn = long.Parse(parsed["volatile.totalBytesIn"]);
                routingBackoffLength = long.Parse(parsed["volatile.routingBackoffLength"]);
                lastRoutingBackoffReason = parsed["volatile.lastRoutingBackoffReason"];
                routingBackoff = long.Parse(parsed["volatile.routingBackoff"]);
                totalBytesOut = long.Parse(parsed["volatile.totalBytesOut"]);
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