/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2010 Thomas Bruderer <apophis@apophis.ch>
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

namespace FCP2.EventArgs
{

    public class NodeDataEventArgs : System.EventArgs
    {
        private readonly ArkType ark;
        private readonly AuthType auth;
        private readonly string clientNonce;
        private readonly DsaGroupType dsaGroup;
        private readonly DsaPrivKeyType dsaPrivKey;
        private readonly DsaPubKeyType dsaPubKey;
        private readonly string identity;
        private readonly string lastGoodVersion;
        private readonly double location;
        private readonly string myName;
        private readonly bool opennet;
        private readonly PhysicalType physical;
        private readonly string sig;
        private readonly string version;
        private readonly VolatileType @volatile;

        /// <summary>
        /// NodeDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal NodeDataEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            lastGoodVersion = parsed.lastGoodVersion;
            sig = parsed.sig;
            opennet = parsed.opennet;
            identity = parsed.identity;
            myName = parsed.myName;
            version = parsed.version;
            physical = new PhysicalType(parsed.physical);
            ark = new ArkType(parsed.ark);
            dsaPubKey = new DsaPubKeyType(parsed.dsaPubKey);
            dsaPrivKey = new DsaPrivKeyType(parsed.dsaPrivKey);
            dsaGroup = new DsaGroupType(parsed.dsaGroup);
            auth = new AuthType(parsed.auth);

            clientNonce = parsed.clientNonce;
            location = parsed.location;
            if (!parsed.location.LastConversionSucessfull) { location = -1.0; }

            if (parsed.@volatile.startedSwaps.Exists())
            {
                @volatile = new VolatileType(parsed.@volatile);
            }

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string LastGoodVersion
        {
            get { return lastGoodVersion; }
        }

        public string Sig
        {
            get { return sig; }
        }

        public bool Opennet
        {
            get { return opennet; }
        }

        public string Identity
        {
            get { return identity; }
        }

        public string MyName
        {
            get { return myName; }
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

        public DsaPrivKeyType DsaPrivKey
        {
            get { return dsaPrivKey; }
        }

        public DsaGroupType DsaGroup
        {
            get { return dsaGroup; }
        }

        public AuthType Auth
        {
            get { return auth; }
        }

        public string ClientNonce
        {
            get { return clientNonce; }
        }

        public double Location
        {
            get { return location; }
        }

        public VolatileType Volatile
        {
            get { return @volatile; }
        }

        #region Nested type: ArkType

        public class ArkType
        {
            private readonly long number;
            private readonly string privURI;
            private readonly string pubURI;

            internal ArkType(dynamic ark)
            {
                pubURI = ark.pubURI;
                privURI = ark.privURI;
                number = ark.number;
            }

            public string PubURI
            {
                get { return pubURI; }
            }


            public string PrivURI
            {
                get { return privURI; }
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
            private readonly List<long> negTypes = new List<long>();

            internal AuthType(dynamic auth)
            {
                if (auth.negTypes != null)
                {
                    foreach (string an in ((string)auth.negTypes).Split(new[] { ';' }))
                    {
                        negTypes.Add(long.Parse(an));
                    }
                }
            }

            public List<long> NegTypes
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

            internal DsaGroupType(dynamic dsaGroup)
            {
                p = dsaGroup.p;
                g = dsaGroup.g;
                q = dsaGroup.q;
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

        #region Nested type: DsaPrivKeyType

        public class DsaPrivKeyType
        {
            private readonly string x;

            internal DsaPrivKeyType(dynamic dsaPrivKey)
            {
                x = dsaPrivKey.x;
            }

            public string X
            {
                get { return x; }
            }
        }

        #endregion

        #region Nested type: DsaPubKeyType

        public class DsaPubKeyType
        {
            private readonly string y;

            internal DsaPubKeyType(dynamic dsaPubKey)
            {
                y = dsaPubKey.y;
            }

            public string Y
            {
                get { return y; }
            }
        }

        #endregion

        #region Nested type: PhysicalType

        public class PhysicalType
        {
            private readonly List<IPEndPoint> udp = new List<IPEndPoint>();

            internal PhysicalType(dynamic physical)
            {
                foreach (string pu in ((string)physical.udp).Split(new[] { ';' }))
                {
                    var ip = pu.Split(new[] { ':' });
                    if (ip.Length > 2)
                    {
                        /* we have an ipv6 adress */
                        var ipAddress = IPAddress.Parse(pu.Substring(0, pu.Length - 1 - ip[ip.Length - 1].Length));
                        if (ipAddress != null) udp.Add(new IPEndPoint(ipAddress, int.Parse(ip[ip.Length - 1])));
                    }
                    else
                    {
                        var ipAddress = IPAddress.Parse(ip[0]);
                        if (ipAddress != null) udp.Add(new IPEndPoint(ipAddress, int.Parse(ip[1])));
                    }
                }
            }

            public List<IPEndPoint> UDP
            {
                get { return udp; }
            }
        }

        #endregion

        #region Nested type: VolatileType

        public class VolatileType
        {
            private readonly long allocatedJavaMemory;
            private readonly long availableCPUs;
            private readonly double averagePingTime;
            private readonly double avgConnectedPeersPerNode;
            private readonly double avgStoreAccessRate;
            private readonly double backedOffPercent;
            private readonly double bwlimitDelayTime;
            private readonly long cacheAccesses;
            private readonly long cachedKeys;
            private readonly long cachedSize;
            private readonly long cachedStoreHits;
            private readonly long cachedStoreMisses;
            private readonly long freeJavaMemory;
            private readonly bool isUsingWrapper;
            private readonly double locationChangePerMinute;
            private readonly double locationChangePerSession;
            private readonly double locationChangePerSwap;
            private readonly long maximumJavaMemory;
            private readonly long maxOverallKeys;
            private readonly long maxOverallSize;
            private readonly long networkSizeEstimate24HourRecent;
            private readonly long networkSizeEstimate48HourRecent;
            private readonly long networkSizeEstimateSession;
            private readonly double noSwapsPerMinute;
            private readonly long numberOfArkFetchers;
            private readonly long numberOfBursting;
            private readonly long numberOfConnected;
            private readonly long numberOfDisabled;
            private readonly long numberOfDisconnected;
            private readonly long numberOfInsertSenders;
            private readonly long numberOfListening;
            private readonly long numberOfListenOnly;
            private readonly long numberOfNeverConnected;
            private readonly long numberOfNotConnected;
            private readonly double numberOfRemotePeerLocationsSeenInSwaps;
            private readonly long numberOfRequestSenders;
            private readonly long numberOfRoutingBackedOff;
            private readonly long numberOfSeedClients;
            private readonly long numberOfSeedServers;
            private readonly long numberOfSimpleConnected;
            private readonly long numberOfTooNew;
            private readonly long numberOfTooOld;
            private readonly long numberOfTransferringRequestSenders;
            private readonly NumberWithRoutingBackoffReasonsType numberWithRoutingBackoffReasons;
            private readonly long opennetSizeEstimate24HourRecent;
            private readonly long opennetSizeEstimate48HourRecent;
            private readonly long opennetSizeEstimateSession;
            private readonly long overallAccesses;
            private readonly long overallKeys;
            private readonly long overallSize;
            private readonly double percentCachedStoreHitsOfAccesses;
            private readonly double percentOverallKeysOfMax;
            private readonly double percentStoreHitsOfAccesses;
            private readonly double pInstantReject;
            private readonly double recentInputRate;
            private readonly double recentOutputRate;
            private readonly double routingMissDistance;
            private readonly long runningThreadCount;
            private readonly long startedSwaps;
            private readonly DateTime startupTime;
            private readonly long storeAccesses;
            private readonly long storeHits;
            private readonly long storeKeys;
            private readonly long storeMisses;
            private readonly long storeSize;
            private readonly double swaps;
            private readonly double swapsPerMinute;
            private readonly double swapsPerNoSwaps;
            private readonly long swapsRejectedAlreadyLocked;
            private readonly long swapsRejectedLoop;
            private readonly long swapsRejectedNowhereToGo;
            private readonly long swapsRejectedRateLimit;
            private readonly long swapsRejectedRecognizedID;
            private readonly long totalInputBytes;
            private readonly long totalInputRate;
            private readonly long totalOutputBytes;
            private readonly long totalOutputRate;
            private readonly long totalPayloadOutputBytes;
            private readonly long totalPayloadOutputPercent;
            private readonly long totalPayloadOutputRate;
            private readonly long unclaimedFifoSize;
            private readonly long uptimeSeconds;
            private readonly long usedJavaMemory;

            internal VolatileType(dynamic @volatile)
            {
                startedSwaps = @volatile.startedSwaps;
                cacheAccesses = @volatile.cacheAccesses;
                totalInputBytes = @volatile.totalInputBytes;
                networkSizeEstimateSession = @volatile.networkSizeEstimateSession;
                storeKeys = @volatile.storeKeys;
                cachedKeys = @volatile.cachedKeys;
                locationChangePerSwap = @volatile.locationChangePerSwap;
                swapsRejectedNowhereToGo = @volatile.swapsRejectedNowhereToGo;
                numberOfNotConnected = @volatile.numberOfNotConnected;
                numberOfListenOnly = @volatile.numberOfListenOnly;
                totalOutputBytes = @volatile.totalOutputBytes;
                swapsPerNoSwaps = @volatile.swapsPerNoSwaps;
                allocatedJavaMemory = @volatile.allocatedJavaMemory;
                percentStoreHitsOfAccesses = @volatile.percentStoreHitsOfAccesses;
                networkSizeEstimate24HourRecent = @volatile.networkSizeEstimate24HourRecent;
                overallAccesses = @volatile.overallAccesses;
                percentOverallKeysOfMax = @volatile.percentOverallKeysOfMax;
                locationChangePerMinute = @volatile.locationChangePerMinute;
                NoSwaps = @volatile.noSwaps;
                cachedSize = @volatile.cachedSize;
                uptimeSeconds = @volatile.uptimeSeconds;
                numberOfArkFetchers = @volatile.numberOfARKFetchers;
                networkSizeEstimate48HourRecent = @volatile.networkSizeEstimate48HourRecent;
                maxOverallKeys = @volatile.maxOverallKeys;
                numberOfDisconnected = @volatile.numberOfDisconnected;
                swaps = @volatile.swaps;
                maximumJavaMemory = @volatile.maximumJavaMemory;
                avgStoreAccessRate = @volatile.avgStoreAccessRate;
                totalInputRate = @volatile.totalInputRate;
                recentInputRate = @volatile.recentInputRate;
                overallKeys = @volatile.overallKeys;
                backedOffPercent = @volatile.backedOffPercent;
                runningThreadCount = @volatile.runningThreadCount;
                storeAccesses = @volatile.storeAccesses;
                numberOfDisabled = @volatile.numberOfDisabled;
                cachedStoreMisses = @volatile.cachedStoreMisses;
                routingMissDistance = @volatile.routingMissDistance;
                swapsRejectedRateLimit = @volatile.swapsRejectedRateLimit;
                totalOutputRate = @volatile.totalOutputRate;
                averagePingTime = @volatile.averagePingTime;
                numberOfBursting = @volatile.numberOfBursting;
                numberOfInsertSenders = @volatile.numberOfInsertSenders;
                usedJavaMemory = @volatile.usedJavaMemory;
                startupTime = @volatile.startupTime;
                locationChangePerSession = @volatile.locationChangePerSession;
                numberOfNeverConnected = @volatile.numberOfNeverConnected;
                freeJavaMemory = @volatile.freeJavaMemory;
                totalPayloadOutputRate = @volatile.totalPayloadOutputRate;
                isUsingWrapper = @volatile.isUsingWrapper;
                storeMisses = @volatile.storeMisses;
                storeHits = @volatile.storeHits;
                totalPayloadOutputPercent = @volatile.totalPayloadOutputPercent;
                storeSize = @volatile.storeSize;
                numberOfTooOld = @volatile.numberOfTooOld;
                avgConnectedPeersPerNode = @volatile.avgConnectedPeersPerNode;
                availableCPUs = @volatile.availableCPUs;
                swapsPerMinute = @volatile.swapsPerMinute;
                noSwapsPerMinute = @volatile.noSwapsPerMinute;
                numberOfListening = @volatile.numberOfListening;
                swapsRejectedAlreadyLocked = @volatile.swapsRejectedAlreadyLocked;
                maxOverallSize = @volatile.maxOverallSize;
                numberOfSimpleConnected = @volatile.numberOfSimpleConnected;
                numberOfRequestSenders = @volatile.numberOfRequestSenders;
                overallSize = @volatile.overallSize;
                numberOfTransferringRequestSenders = @volatile.numberOfTransferringRequestSenders;
                percentCachedStoreHitsOfAccesses = @volatile.percentCachedStoreHitsOfAccesses;
                swapsRejectedLoop = @volatile.swapsRejectedLoop;
                bwlimitDelayTime = @volatile.bwlimitDelayTime;
                numberOfRemotePeerLocationsSeenInSwaps = @volatile.numberOfRemotePeerLocationsSeenInSwaps;
                pInstantReject = @volatile.pInstantReject;
                totalPayloadOutputBytes = @volatile.totalPayloadOutputBytes;
                numberOfRoutingBackedOff = @volatile.numberOfRoutingBackedOff;
                unclaimedFifoSize = @volatile.unclaimedFifoSize;
                numberOfConnected = @volatile.numberOfConnected;
                cachedStoreHits = @volatile.cachedStoreHits;
                recentOutputRate = @volatile.recentOutputRate;
                swapsRejectedRecognizedID = @volatile.swapsRejectedRecognizedID;
                numberOfTooNew = @volatile.numberOfTooNew;
                numberOfSeedClients = @volatile.numberOfSeedClients;
                opennetSizeEstimate48HourRecent = @volatile.opennetSizeEstimate48HourRecent;
                numberOfSeedServers = @volatile.numberOfSeedServers;
                opennetSizeEstimateSession = @volatile.opennetSizeEstimateSession;
                opennetSizeEstimate24HourRecent = @volatile.opennetSizeEstimate24HourRecent;

                numberWithRoutingBackoffReasons = new NumberWithRoutingBackoffReasonsType(@volatile.numberWithRoutingBackoffReasons);
            }

            public long StartedSwaps
            {
                get { return startedSwaps; }
            }

            public long CacheAccesses
            {
                get { return cacheAccesses; }
            }

            public long TotalInputBytes
            {
                get { return totalInputBytes; }
            }

            public long NetworkSizeEstimateSession
            {
                get { return networkSizeEstimateSession; }
            }

            public long StoreKeys
            {
                get { return storeKeys; }
            }

            public long CachedKeys
            {
                get { return cachedKeys; }
            }

            public double LocationChangePerSwap
            {
                get { return locationChangePerSwap; }
            }

            public long SwapsRejectedNowhereToGo
            {
                get { return swapsRejectedNowhereToGo; }
            }

            public long NumberOfNotConnected
            {
                get { return numberOfNotConnected; }
            }

            public long NumberOfListenOnly
            {
                get { return numberOfListenOnly; }
            }

            public long TotalOutputBytes
            {
                get { return totalOutputBytes; }
            }

            public double SwapsPerNoSwaps
            {
                get { return swapsPerNoSwaps; }
            }

            public long AllocatedJavaMemory
            {
                get { return allocatedJavaMemory; }
            }

            public double PercentStoreHitsOfAccesses
            {
                get { return percentStoreHitsOfAccesses; }
            }

            public long NetworkSizeEstimate24HourRecent
            {
                get { return networkSizeEstimate24HourRecent; }
            }

            public long OverallAccesses
            {
                get { return overallAccesses; }
            }

            public double PercentOverallKeysOfMax
            {
                get { return percentOverallKeysOfMax; }
            }

            public double LocationChangePerMinute
            {
                get { return locationChangePerMinute; }
            }

            public double NoSwaps { get; set; }

            public long CachedSize
            {
                get { return cachedSize; }
            }

            public long UptimeSeconds
            {
                get { return uptimeSeconds; }
            }

            public long NumberOfArkFetchers
            {
                get { return numberOfArkFetchers; }
            }

            public long NetworkSizeEstimate48HourRecent
            {
                get { return networkSizeEstimate48HourRecent; }
            }

            public long MaxOverallKeys
            {
                get { return maxOverallKeys; }
            }

            public long NumberOfDisconnected
            {
                get { return numberOfDisconnected; }
            }

            public double Swaps
            {
                get { return swaps; }
            }

            public long MaximumJavaMemory
            {
                get { return maximumJavaMemory; }
            }

            public double AvgStoreAccessRate
            {
                get { return avgStoreAccessRate; }
            }

            public long TotalInputRate
            {
                get { return totalInputRate; }
            }

            public double RecentInputRate
            {
                get { return recentInputRate; }
            }

            public long OverallKeys
            {
                get { return overallKeys; }
            }

            public double BackedOffPercent
            {
                get { return backedOffPercent; }
            }

            public long RunningThreadCount
            {
                get { return runningThreadCount; }
            }

            public long StoreAccesses
            {
                get { return storeAccesses; }
            }

            public long NumberOfDisabled
            {
                get { return numberOfDisabled; }
            }

            public long CachedStoreMisses
            {
                get { return cachedStoreMisses; }
            }

            public double RoutingMissDistance
            {
                get { return routingMissDistance; }
            }

            public long SwapsRejectedRateLimit
            {
                get { return swapsRejectedRateLimit; }
            }

            public long TotalOutputRate
            {
                get { return totalOutputRate; }
            }

            public double AveragePingTime
            {
                get { return averagePingTime; }
            }

            public long NumberOfBursting
            {
                get { return numberOfBursting; }
            }

            public long NumberOfInsertSenders
            {
                get { return numberOfInsertSenders; }
            }

            public long UsedJavaMemory
            {
                get { return usedJavaMemory; }
            }

            public DateTime StartupTime
            {
                get { return startupTime; }
            }

            public double LocationChangePerSession
            {
                get { return locationChangePerSession; }
            }

            public long NumberOfNeverConnected
            {
                get { return numberOfNeverConnected; }
            }

            public long FreeJavaMemory
            {
                get { return freeJavaMemory; }
            }

            public long TotalPayloadOutputRate
            {
                get { return totalPayloadOutputRate; }
            }

            public bool IsUsingWrapper
            {
                get { return isUsingWrapper; }
            }

            public long StoreMisses
            {
                get { return storeMisses; }
            }

            public long StoreHits
            {
                get { return storeHits; }
            }

            public long TotalPayloadOutputPercent
            {
                get { return totalPayloadOutputPercent; }
            }

            public long StoreSize
            {
                get { return storeSize; }
            }

            public long NumberOfTooOld
            {
                get { return numberOfTooOld; }
            }

            public double AvgConnectedPeersPerNode
            {
                get { return avgConnectedPeersPerNode; }
            }

            public long AvailableCPUs
            {
                get { return availableCPUs; }
            }

            public double SwapsPerMinute
            {
                get { return swapsPerMinute; }
            }

            public double NoSwapsPerMinute
            {
                get { return noSwapsPerMinute; }
            }

            public long NumberOfListening
            {
                get { return numberOfListening; }
            }

            public long SwapsRejectedAlreadyLocked
            {
                get { return swapsRejectedAlreadyLocked; }
            }

            public long MaxOverallSize
            {
                get { return maxOverallSize; }
            }

            public long NumberOfSimpleConnected
            {
                get { return numberOfSimpleConnected; }
            }

            public long NumberOfRequestSenders
            {
                get { return numberOfRequestSenders; }
            }

            public long OverallSize
            {
                get { return overallSize; }
            }

            public long NumberOfTransferringRequestSenders
            {
                get { return numberOfTransferringRequestSenders; }
            }

            public double PercentCachedStoreHitsOfAccesses
            {
                get { return percentCachedStoreHitsOfAccesses; }
            }

            public long SwapsRejectedLoop
            {
                get { return swapsRejectedLoop; }
            }

            public double BwlimitDelayTime
            {
                get { return bwlimitDelayTime; }
            }

            public double NumberOfRemotePeerLocationsSeenInSwaps
            {
                get { return numberOfRemotePeerLocationsSeenInSwaps; }
            }

            public double PInstantReject
            {
                get { return pInstantReject; }
            }

            public long TotalPayloadOutputBytes
            {
                get { return totalPayloadOutputBytes; }
            }

            public long NumberOfRoutingBackedOff
            {
                get { return numberOfRoutingBackedOff; }
            }

            public long UnclaimedFifoSize
            {
                get { return unclaimedFifoSize; }
            }

            public long NumberOfConnected
            {
                get { return numberOfConnected; }
            }

            public long CachedStoreHits
            {
                get { return cachedStoreHits; }
            }

            public double RecentOutputRate
            {
                get { return recentOutputRate; }
            }

            public long SwapsRejectedRecognizedID
            {
                get { return swapsRejectedRecognizedID; }
            }

            public long NumberOfTooNew
            {
                get { return numberOfTooNew; }
            }

            public long NumberOfSeedClients
            {
                get { return numberOfSeedClients; }
            }

            public long OpennetSizeEstimate48HourRecent
            {
                get { return opennetSizeEstimate48HourRecent; }
            }

            public long NumberOfSeedServers
            {
                get { return numberOfSeedServers; }
            }

            public long OpennetSizeEstimateSession
            {
                get { return opennetSizeEstimateSession; }
            }

            public long OpennetSizeEstimate24HourRecent
            {
                get { return opennetSizeEstimate24HourRecent; }
            }

            public NumberWithRoutingBackoffReasonsType NumberWithRoutingBackoffReasons
            {
                get { return numberWithRoutingBackoffReasons; }
            }


            #region Nested type: NumberWithRoutingBackoffReasonsType

            public class NumberWithRoutingBackoffReasonsType
            {
                internal NumberWithRoutingBackoffReasonsType(dynamic numberWithRoutingBackoffReasons)
                {
                    /* TODO: implementation */
                }
            }

            #endregion
        }

        #endregion
    }
}