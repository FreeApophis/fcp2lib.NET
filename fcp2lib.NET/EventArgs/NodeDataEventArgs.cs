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
using System.Net;
using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class NodeDataEventArgs : System.EventArgs
    {
        public string LastGoodVersion { get; }
        public string Sig { get; }
        public bool Opennet { get; }
        public string Identity { get; }
        public string Version { get; }
        public PhysicalType Physical { get; }
        public ArkType Ark { get; }
        public DsaPubKeyType DsaPubKey { get; }
        public DsaPrivKeyType DsaPrivKey { get; }
        public DsaGroupType DsaGroup { get; }
        public AuthType Auth { get; }
        public string ClientNonce { get; }
        public double Location { get; }
        public VolatileType Volatile { get; }

        /// <summary>
        /// NodeDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal NodeDataEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            LastGoodVersion = parsed.lastGoodVersion;
            Sig = parsed.sig;
            Opennet = parsed.opennet;
            Identity = parsed.identity;
            Version = parsed.version;
            Physical = new PhysicalType(parsed.physical);
            Ark = new ArkType(parsed.ark);
            DsaPubKey = new DsaPubKeyType(parsed.dsaPubKey);
            DsaPrivKey = new DsaPrivKeyType(parsed.dsaPrivKey);
            DsaGroup = new DsaGroupType(parsed.dsaGroup);
            Auth = new AuthType(parsed.auth);

            ClientNonce = parsed.clientNonce;
            Location = parsed.location;
            if (!parsed.location.LastConversionSucessfull) { Location = -1.0; }

            if (parsed.@volatile.startedSwaps.Exists())
            {
                Volatile = new VolatileType(parsed.@volatile);
            }

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        #region Nested type: ArkType

        public class ArkType
        {
            public string PubURI { get; }
            public string PrivURI { get; }
            public long Number { get; }

            internal ArkType(dynamic ark)
            {
                PubURI = ark.pubURI;
                PrivURI = ark.privURI;
                Number = ark.number;
            }
        }

        #endregion

        #region Nested type: AuthType

        public class AuthType
        {
            readonly List<long> negTypes = new List<long>();

            public List<long> NegTypes => negTypes;

            internal AuthType(dynamic auth)
            {
                if (auth.negTypes == null) return;
                foreach (string an in ((string)auth.negTypes).Split(';'))
                {
                    negTypes.Add(long.Parse(an));
                }
            }

        }

        #endregion

        #region Nested type: DsaGroupType

        public class DsaGroupType
        {
            public string P { get; }
            public string G { get; }
            public string Q { get; }

            internal DsaGroupType(dynamic dsaGroup)
            {
                P = dsaGroup.p;
                G = dsaGroup.g;
                Q = dsaGroup.q;
            }
        }

        #endregion

        #region Nested type: DsaPrivKeyType

        public class DsaPrivKeyType
        {
            public string X { get; }

            internal DsaPrivKeyType(dynamic dsaPrivKey)
            {
                X = dsaPrivKey.x;
            }
        }

        #endregion

        #region Nested type: DsaPubKeyType

        public class DsaPubKeyType
        {
            public string Y { get; }

            internal DsaPubKeyType(dynamic dsaPubKey)
            {
                Y = dsaPubKey.y;
            }
        }

        #endregion

        #region Nested type: PhysicalType

        public class PhysicalType
        {
            public List<IPEndPoint> UDP { get; } = new List<IPEndPoint>();

            internal PhysicalType(dynamic physical)
            {
                foreach (string pu in ((string)physical.udp).Split(';'))
                {
                    var ip = pu.Split(':');
                    if (ip.Length > 2)
                    {
                        /* we have an ipv6 adress */
                        var ipAddress = IPAddress.Parse(pu.Substring(0, pu.Length - 1 - ip[ip.Length - 1].Length));
                        UDP.Add(new IPEndPoint(ipAddress, int.Parse(ip[ip.Length - 1])));
                    }
                    else
                    {
                        var ipAddress = IPAddress.Parse(ip[0]);
                        UDP.Add(new IPEndPoint(ipAddress, int.Parse(ip[1])));
                    }
                }
            }
        }

        #endregion

        #region Nested type: VolatileType

        public class VolatileType
        {
            public long StartedSwaps { get; }
            public long CacheAccesses { get; }
            public long TotalInputBytes { get; }
            public long NetworkSizeEstimateSession { get; }
            public long StoreKeys { get; }
            public long CachedKeys { get; }
            public double LocationChangePerSwap { get; }
            public long SwapsRejectedNowhereToGo { get; }
            public long NumberOfNotConnected { get; }
            public long NumberOfListenOnly { get; }
            public long TotalOutputBytes { get; }
            public double SwapsPerNoSwaps { get; }
            public long AllocatedJavaMemory { get; }
            public double PercentStoreHitsOfAccesses { get; }
            public long NetworkSizeEstimate24HourRecent { get; }
            public long OverallAccesses { get; }
            public double PercentOverallKeysOfMax { get; }
            public double LocationChangePerMinute { get; }
            public double NoSwaps { get; }
            public long CachedSize { get; }
            public long UptimeSeconds { get; }
            public long NumberOfArkFetchers { get; }
            public long NetworkSizeEstimate48HourRecent { get; }
            public long MaxOverallKeys { get; }
            public long NumberOfDisconnected { get; }
            public double Swaps { get; }
            public long MaximumJavaMemory { get; }
            public double AvgStoreAccessRate { get; }
            public long TotalInputRate { get; }
            public double RecentInputRate { get; }
            public long OverallKeys { get; }
            public double BackedOffPercent { get; }
            public long RunningThreadCount { get; }
            public long StoreAccesses { get; }
            public long NumberOfDisabled { get; }
            public long CachedStoreMisses { get; }
            public double RoutingMissDistance { get; }
            public long SwapsRejectedRateLimit { get; }
            public long TotalOutputRate { get; }
            public double AveragePingTime { get; }
            public long NumberOfBursting { get; }
            public long UsedJavaMemory { get; }
            public DateTime StartupTime { get; }
            public double LocationChangePerSession { get; }
            public long NumberOfNeverConnected { get; }
            public long FreeJavaMemory { get; }
            public long TotalPayloadOutputRate { get; }
            public bool IsUsingWrapper { get; }
            public long StoreMisses { get; }
            public long StoreHits { get; }
            public long TotalPayloadOutputPercent { get; }
            public long StoreSize { get; }
            public long NumberOfTooOld { get; }
            public double AvgConnectedPeersPerNode { get; }
            public long AvailableCPUs { get; }
            public double SwapsPerMinute { get; }
            public double NoSwapsPerMinute { get; }
            public long NumberOfListening { get; }
            public long SwapsRejectedAlreadyLocked { get; }
            public long MaxOverallSize { get; }
            public long NumberOfSimpleConnected { get; }
            public long OverallSize { get; }
            public long NumberOfTransferringRequestSenders { get; }
            public double PercentCachedStoreHitsOfAccesses { get; }
            public double BwlimitDelayTime { get; }
            public double NumberOfRemotePeerLocationsSeenInSwaps { get; }
            public double PInstantReject { get; }
            public long TotalPayloadOutputBytes { get; }
            public long NumberOfRoutingBackedOff { get; }
            public long NumberOfConnected { get; }
            public long CachedStoreHits { get; }
            public double RecentOutputRate { get; }
            public long SwapsRejectedRecognizedID { get; }
            public long NumberOfTooNew { get; }
            public long NumberOfSeedClients { get; }
            public long NumberOfSeedServers { get; }
            public long OpennetSizeEstimateSession { get; }
            public NumberWithRoutingBackoffReasonsType NumberWithRoutingBackoffReasons { get; }

            internal VolatileType(dynamic @volatile)
            {
                StartedSwaps = @volatile.startedSwaps;
                CacheAccesses = @volatile.cacheAccesses;
                TotalInputBytes = @volatile.totalInputBytes;
                NetworkSizeEstimateSession = @volatile.networkSizeEstimateSession;
                StoreKeys = @volatile.storeKeys;
                CachedKeys = @volatile.cachedKeys;
                LocationChangePerSwap = @volatile.locationChangePerSwap;
                SwapsRejectedNowhereToGo = @volatile.swapsRejectedNowhereToGo;
                NumberOfNotConnected = @volatile.numberOfNotConnected;
                NumberOfListenOnly = @volatile.numberOfListenOnly;
                TotalOutputBytes = @volatile.totalOutputBytes;
                SwapsPerNoSwaps = @volatile.swapsPerNoSwaps;
                AllocatedJavaMemory = @volatile.allocatedJavaMemory;
                PercentStoreHitsOfAccesses = @volatile.percentStoreHitsOfAccesses;
                NetworkSizeEstimate24HourRecent = @volatile.networkSizeEstimate24HourRecent;
                OverallAccesses = @volatile.overallAccesses;
                PercentOverallKeysOfMax = @volatile.percentOverallKeysOfMax;
                LocationChangePerMinute = @volatile.locationChangePerMinute;
                NoSwaps = @volatile.noSwaps;
                CachedSize = @volatile.cachedSize;
                UptimeSeconds = @volatile.uptimeSeconds;
                NumberOfArkFetchers = @volatile.numberOfARKFetchers;
                NetworkSizeEstimate48HourRecent = @volatile.networkSizeEstimate48HourRecent;
                MaxOverallKeys = @volatile.maxOverallKeys;
                NumberOfDisconnected = @volatile.numberOfDisconnected;
                Swaps = @volatile.swaps;
                MaximumJavaMemory = @volatile.maximumJavaMemory;
                AvgStoreAccessRate = @volatile.avgStoreAccessRate;
                TotalInputRate = @volatile.totalInputRate;
                RecentInputRate = @volatile.recentInputRate;
                OverallKeys = @volatile.overallKeys;
                BackedOffPercent = @volatile.backedOffPercent;
                RunningThreadCount = @volatile.runningThreadCount;
                StoreAccesses = @volatile.storeAccesses;
                NumberOfDisabled = @volatile.numberOfDisabled;
                CachedStoreMisses = @volatile.cachedStoreMisses;
                RoutingMissDistance = @volatile.routingMissDistance;
                SwapsRejectedRateLimit = @volatile.swapsRejectedRateLimit;
                TotalOutputRate = @volatile.totalOutputRate;
                AveragePingTime = @volatile.averagePingTime;
                NumberOfBursting = @volatile.numberOfBursting;
                UsedJavaMemory = @volatile.usedJavaMemory;
                StartupTime = @volatile.startupTime;
                LocationChangePerSession = @volatile.locationChangePerSession;
                NumberOfNeverConnected = @volatile.numberOfNeverConnected;
                FreeJavaMemory = @volatile.freeJavaMemory;
                TotalPayloadOutputRate = @volatile.totalPayloadOutputRate;
                IsUsingWrapper = @volatile.isUsingWrapper;
                StoreMisses = @volatile.storeMisses;
                StoreHits = @volatile.storeHits;
                TotalPayloadOutputPercent = @volatile.totalPayloadOutputPercent;
                StoreSize = @volatile.storeSize;
                NumberOfTooOld = @volatile.numberOfTooOld;
                AvgConnectedPeersPerNode = @volatile.avgConnectedPeersPerNode;
                AvailableCPUs = @volatile.availableCPUs;
                SwapsPerMinute = @volatile.swapsPerMinute;
                NoSwapsPerMinute = @volatile.noSwapsPerMinute;
                NumberOfListening = @volatile.numberOfListening;
                SwapsRejectedAlreadyLocked = @volatile.swapsRejectedAlreadyLocked;
                MaxOverallSize = @volatile.maxOverallSize;
                NumberOfSimpleConnected = @volatile.numberOfSimpleConnected;
                OverallSize = @volatile.overallSize;
                NumberOfTransferringRequestSenders = @volatile.numberOfTransferringRequestSenders;
                PercentCachedStoreHitsOfAccesses = @volatile.percentCachedStoreHitsOfAccesses;
                BwlimitDelayTime = @volatile.bwlimitDelayTime;
                NumberOfRemotePeerLocationsSeenInSwaps = @volatile.numberOfRemotePeerLocationsSeenInSwaps;
                PInstantReject = @volatile.pInstantReject;
                TotalPayloadOutputBytes = @volatile.totalPayloadOutputBytes;
                NumberOfRoutingBackedOff = @volatile.numberOfRoutingBackedOff;
                NumberOfConnected = @volatile.numberOfConnected;
                CachedStoreHits = @volatile.cachedStoreHits;
                RecentOutputRate = @volatile.recentOutputRate;
                SwapsRejectedRecognizedID = @volatile.swapsRejectedRecognizedID;
                NumberOfTooNew = @volatile.numberOfTooNew;
                NumberOfSeedClients = @volatile.numberOfSeedClients;
                NumberOfSeedServers = @volatile.numberOfSeedServers;
                OpennetSizeEstimateSession = @volatile.opennetSizeEstimateSession;

                NumberWithRoutingBackoffReasons = new NumberWithRoutingBackoffReasonsType(@volatile.numberWithRoutingBackoffReasons);
            }


            #region Nested type: NumberWithRoutingBackoffReasonsType

            public class NumberWithRoutingBackoffReasonsType
            {
                public int InsertTimeoutNoFinalAck { get; }
                public int TransferFailedInsert { get; }
                public int TurtledTransfer { get; }
                public int ForwardRejectedOverload { get; }
                public int TransferFailedRequest { get; }

                internal NumberWithRoutingBackoffReasonsType(dynamic numberWithRoutingBackoffReasons)
                {
                    InsertTimeoutNoFinalAck = numberWithRoutingBackoffReasons.InsertTimeoutNoFinalAck;
                    TransferFailedInsert = numberWithRoutingBackoffReasons.TransferFailedInsert;
                    TurtledTransfer = numberWithRoutingBackoffReasons.TurtledTransfer;
                    ForwardRejectedOverload = numberWithRoutingBackoffReasons.ForwardRejectedOverload;
                    TransferFailedRequest = numberWithRoutingBackoffReasons.TransferFailedRequest;
                }
            }

            #endregion
        }

        #endregion
    }
}