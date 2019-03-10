/*
 *  The FCP2.0 Library, complete access to freenet's FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2016 Thomas Bruderer <apophis@apophis.ch>
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
        public string LastGoodVersion { get; }
        public bool Opennet { get; }
        public string MyName { get; }
        public string Identity { get; }
        public double Location { get; }
        public bool Testnet { get; }
        public string Version { get; }
        public PhysicalType Physical { get; }
        public ArkType Ark { get; }
        public DsaPubKeyType DsaPubKey { get; }
        public DsaGroupType DsaGroup { get; }
        public AuthType Auth { get; }
        public VolatileType @Volatile { get; }
        public MetadataType Metadata { get; }

        /// <summary>
        /// PeerEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PeerEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            LastGoodVersion = parsed.lastGoodVersion;
            Opennet = parsed.opennet;
            MyName = parsed.myName;
            Identity = parsed.identity;
            Location = parsed.location;
            Testnet = parsed.testnet;
            Version = parsed.version;
            Physical = new PhysicalType(parsed);
            Ark = new ArkType(parsed);
            DsaPubKey = new DsaPubKeyType(parsed);
            DsaGroup = new DsaGroupType(parsed);
            Auth = new AuthType(parsed);
            Volatile = new VolatileType(parsed);
            Metadata = new MetadataType(parsed);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        #region Nested type: ArkType

        public class ArkType
        {
            public string PubURI { get; }
            public long Number { get; }

            internal ArkType(dynamic parsed)
            {
                PubURI = parsed.ark.pubURI;
                Number = parsed.ark.number;
            }
        }

        #endregion

        #region Nested type: AuthType

        public class AuthType
        {
            public long NegTypes { get; }

            internal AuthType(dynamic parsed)
            {
                NegTypes = parsed.auth.negTypes;
            }
        }

        #endregion

        #region Nested type: DsaGroupType

        public class DsaGroupType
        {
            public string P { get; }
            public string G { get; }
            public string Q { get; }

            internal DsaGroupType(dynamic parsed)
            {
                P = parsed.dsaGroup.p;
                G = parsed.dsaGroup.g;
                Q = parsed.dsaGroup.q;
            }
        }

        #endregion

        #region Nested type: DsaPubKeyType

        public class DsaPubKeyType
        {
            public string Y { get; }

            internal DsaPubKeyType(dynamic parsed)
            {
                Y = parsed.dsaPubKey.y;
            }
        }

        #endregion

        #region Nested type: MetadataType

        public class MetadataType
        {
            public long RoutableConnectionCheckCount { get; }
            public long HadRoutableConnectionCount { get; }
            public DateTime TimeLastConnected { get; }
            public DateTime TimeLastSuccess { get; }
            public DateTime TimeLastRoutable { get; }
            public DateTime TimeLastReceivedPacket { get; }
            public DetectedType Detected { get; }

            internal MetadataType(dynamic parsed)
            {
                RoutableConnectionCheckCount = parsed.metadata.routableConnectionCheckCount;
                HadRoutableConnectionCount = parsed.metadata.hadRoutableConnectionCount;
                TimeLastConnected = parsed.metadata.timeLastConnected;
                TimeLastSuccess = parsed.metadata.timeLastSuccess;
                TimeLastRoutable = parsed.metadata.timeLastRoutable;
                TimeLastReceivedPacket = parsed.metadata.timeLastReceivedPacket;
                Detected = new DetectedType(parsed);
            }

            #region Nested type: DetectedType

            public class DetectedType
            {
                public IPEndPoint UDP { get; }

                internal DetectedType(dynamic parsed)
                {
                    string[] ip = parsed.metadata.detected.udp.Split(new[] { ':' });
                    var ipAddress = IPAddress.Parse(ip[0]);
                    UDP = new IPEndPoint((ipAddress), int.Parse(ip[1]));
                }
            }

            #endregion
        }

        #endregion

        #region Nested type: PhysicalType

        public class PhysicalType
        {
            public IPEndPoint UDP { get; }

            internal PhysicalType(dynamic parsed)
            {
                string[] ip = parsed.physical.udp.Split(new[] { ':' });
                var ipAddress = IPAddress.Parse(ip[0]);
                UDP = new IPEndPoint((ipAddress), int.Parse(ip[1]));
            }

        }

        #endregion

        #region Nested type: @VolatileType

        public class VolatileType
        {
            public double AveragePing { get; }
            public double OverloadProbability { get; }
            public double PercentTimeRoutableConnection { get; }
            public double RoutingBackoffPercent { get; }
            public string Status { get; }
            public long TotalBytesIn { get; }
            public long RoutingBackoffLength { get; }
            public string LastRoutingBackoffReason { get; }
            public long RoutingBackoff { get; }
            public long TotalBytesOut { get; }

            internal VolatileType(dynamic parsed)
            {
                AveragePing = parsed.@volatile.averagePingTime;
                OverloadProbability = parsed.@volatile.overloadProbability;
                PercentTimeRoutableConnection = parsed.@volatile.percentTimeRoutableConnection;
                RoutingBackoffPercent = parsed.@volatile.routingBackoffPercent;
                Status = parsed.@volatile.status;
                TotalBytesIn = parsed.@volatile.totalBytesIn;
                RoutingBackoffLength = parsed.@volatile.routingBackoffLength;
                LastRoutingBackoffReason = parsed.@volatile.lastRoutingBackoffReason;
                RoutingBackoff = parsed.@volatile.routingBackoff;
                TotalBytesOut = parsed.@volatile.totalBytesOut;
            }
        }

        #endregion
    }
}