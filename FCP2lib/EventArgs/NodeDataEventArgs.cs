/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009 Thomas Bruderer <apophis@apophis.ch>
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
using System.Net;
using System.Collections.Generic;

namespace Freenet.FCP2 {
    
    public class NodeDataEventArgs : EventArgs {
        
        private string lastGoodVersion;
        
        public string LastGoodVersion {
            get { return lastGoodVersion; }
        }
        
        private string sig;
        
        public string Sig {
            get { return sig; }
        }
        
        private bool opennet;
        
        public bool Opennet {
            get { return opennet; }
        }
        
        private string identity;
        
        public string Identity {
            get { return identity; }
        }
        
        private string myName;
        
        public string MyName {
            get { return myName; }
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
        private DsaPrivKeyType dsaPrivKey;
        
        public DsaPrivKeyType DsaPrivKey {
            get { return dsaPrivKey; }
        }

        private DsaGroupType dsaGroup;
        
        public DsaGroupType DsaGroup {
            get { return dsaGroup; }
        }
        
        private AuthType auth;
        
        public AuthType Auth {
            get { return auth; }
        }
        
        private string clientNonce;
        
        public string ClientNonce {
            get { return clientNonce; }
        }
        
        private double location;
        
        public double Location {
            get { return location; }
        }
        
        private VolatileType @volatile = null;
        
        public VolatileType Volatile {
            get { return @volatile; }
        }
        
        /// <summary>
        /// NodeDataEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal NodeDataEventArgs(MessageParser parsed) {
            #if DEBUG
            FCP2.ArgsDebug(this, parsed);
            #endif
            
            this.lastGoodVersion = parsed["lastGoodVersion"];
            this.sig = parsed["sig"];
            this.opennet = bool.Parse(parsed["opennet"]);
            this.identity = parsed["identity"];
            this.myName = parsed["myName"];
            this.version = parsed["version"];
            this.physical = new PhysicalType(parsed);
            this.ark = new ArkType(parsed);
            this.dsaPubKey = new DsaPubKeyType(parsed);
            this.dsaPrivKey = new DsaPrivKeyType(parsed);
            this.dsaGroup = new DsaGroupType(parsed);
            this.auth = new AuthType(parsed);
            
            this.clientNonce = parsed["clientNonce"];
            this.location = (parsed["location"]!=null) ? double.Parse(parsed["location"]) : -1.0;
            
            if (parsed["volatile.startedSwaps"]!=null)
                @volatile = new VolatileType(parsed);
            
            #if DEBUG
            parsed.PrintAccessCount();
            #endif
        }
        
        public class PhysicalType {
            List<IPEndPoint> udp = new List<IPEndPoint>();
            
            public List<IPEndPoint> UDP {
                get { return udp; }
            }
            
            internal PhysicalType(MessageParser parsed) {
                foreach(string pu in parsed["physical.udp"].Split(new char[] {';'})) {
                    string[] ip = pu.Split(new char[] {':'});
                    if(ip.Length > 2) {
                        /* we have an ipv6 adress */
                        this.udp.Add(new IPEndPoint(IPAddress.Parse(pu.Substring(0,pu.Length-1-ip[ip.Length-1].Length)), int.Parse(ip[ip.Length-1])));
                    } else {
                        this.udp.Add(new IPEndPoint(IPAddress.Parse(ip[0]), int.Parse(ip[1])));
                    }
                }
            }
        }

        public class ArkType {
            private string pubURI;
            
            public string PubURI {
                get { return pubURI; }
            }
            
            
            private string privURI;
            
            public string PrivURI {
                get { return privURI; }
            }
            
            private long number;
            
            public long Number {
                get { return number; }
            }
            
            internal ArkType(MessageParser parsed) {
                this.pubURI = parsed["ark.pubURI"];
                this.privURI = parsed["ark.privURI"];
                this.number = long.Parse(parsed["ark.number"]);
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
        
        public class DsaPrivKeyType {
            private string x;
            
            public string X {
                get { return x; }
            }
            
            internal DsaPrivKeyType(MessageParser parsed) {
                this.x = parsed["dsaPrivKey.x"];
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
            private List<long> negTypes = new List<long>();
            
            public List<long> NegTypes {
                get { return negTypes; }
            }
            
            internal AuthType(MessageParser parsed) {
                if (parsed["auth.negTypes"]!=null) {
                    foreach(string an in parsed["auth.negTypes"].Split(new char[] {';'})) {
                        this.negTypes.Add(long.Parse(an));
                    }
                }
            }
        }
        
        public class VolatileType {
            
            private long startedSwaps;
            
            public long StartedSwaps {
                get { return startedSwaps; }
            }
            
            private long cacheAccesses;
            
            public long CacheAccesses {
                get { return cacheAccesses; }
            }
            
            private long totalInputBytes;
            
            public long TotalInputBytes {
                get { return totalInputBytes; }
            }
            
            private long networkSizeEstimateSession;
            
            public long NetworkSizeEstimateSession {
                get { return networkSizeEstimateSession; }
            }
            
            private long storeKeys;
            
            public long StoreKeys {
                get { return storeKeys; }
            }
            
            private long cachedKeys;
            
            public long CachedKeys {
                get { return cachedKeys; }
            }
            
            private double locationChangePerSwap;
            
            public double LocationChangePerSwap {
                get { return locationChangePerSwap; }
            }
            
            private long swapsRejectedNowhereToGo;
            
            public long SwapsRejectedNowhereToGo {
                get { return swapsRejectedNowhereToGo; }
            }
            
            private long numberOfNotConnected;
            
            public long NumberOfNotConnected {
                get { return numberOfNotConnected; }
            }
            
            private long numberOfListenOnly;
            
            public long NumberOfListenOnly {
                get { return numberOfListenOnly; }
            }
            
            private long totalOutputBytes;
            
            public long TotalOutputBytes {
                get { return totalOutputBytes; }
            }
            
            private double swapsPerNoSwaps;
            
            public double SwapsPerNoSwaps {
                get { return swapsPerNoSwaps; }
            }
            
            private long allocatedJavaMemory;
            
            public long AllocatedJavaMemory {
                get { return allocatedJavaMemory; }
            }
            
            private double percentStoreHitsOfAccesses;
            
            public double PercentStoreHitsOfAccesses {
                get { return percentStoreHitsOfAccesses; }
            }
            
            private long networkSizeEstimate24hourRecent;
            
            public long NetworkSizeEstimate24hourRecent {
                get { return networkSizeEstimate24hourRecent; }
            }
            
            private long overallAccesses;
            
            public long OverallAccesses {
                get { return overallAccesses; }
            }
            
            private double percentOverallKeysOfMax;
            
            public double PercentOverallKeysOfMax {
                get { return percentOverallKeysOfMax; }
            }
            
            private double locationChangePerMinute;
            
            public double LocationChangePerMinute {
                get { return locationChangePerMinute; }
            }
            
            private double noSwaps;
            
            public double NoSwaps {
                get { return noSwaps; }
                set { noSwaps = value; }
            }
            
            private long cachedSize;
            
            public long CachedSize {
                get { return cachedSize; }
            }
            
            private long uptimeSeconds;
            
            public long UptimeSeconds {
                get { return uptimeSeconds; }
            }
            
            private long numberOfARKFetchers;
            
            public long NumberOfARKFetchers {
                get { return numberOfARKFetchers; }
            }
            
            private long networkSizeEstimate48hourRecent;
            
            public long NetworkSizeEstimate48hourRecent {
                get { return networkSizeEstimate48hourRecent; }
            }
            
            private long maxOverallKeys;
            
            public long MaxOverallKeys {
                get { return maxOverallKeys; }
            }
            
            private long numberOfDisconnected;
            
            public long NumberOfDisconnected {
                get { return numberOfDisconnected; }
            }
            
            private double swaps;
            
            public double Swaps {
                get { return swaps; }
            }
            
            private long maximumJavaMemory;
            
            public long MaximumJavaMemory {
                get { return maximumJavaMemory; }
            }
            
            private double avgStoreAccessRate;
            
            public double AvgStoreAccessRate {
                get { return avgStoreAccessRate; }
            }
            
            private long totalInputRate;
            
            public long TotalInputRate {
                get { return totalInputRate; }
            }
            
            private double recentInputRate;
            
            public double RecentInputRate {
                get { return recentInputRate; }
            }
            
            private long overallKeys;
            
            public long OverallKeys {
                get { return overallKeys; }
            }
            
            private double backedOffPercent;
            
            public double BackedOffPercent {
                get { return backedOffPercent; }
            }
            
            private long runningThreadCount;
            
            public long RunningThreadCount {
                get { return runningThreadCount; }
            }
            
            private long storeAccesses;
            
            public long StoreAccesses {
                get { return storeAccesses; }
            }
            
            private long numberOfDisabled;
            
            public long NumberOfDisabled {
                get { return numberOfDisabled; }
            }
            
            private long cachedStoreMisses;
            
            public long CachedStoreMisses {
                get { return cachedStoreMisses; }
            }
            
            private double routingMissDistance;
            
            public double RoutingMissDistance {
                get { return routingMissDistance; }
            }
            
            private long swapsRejectedRateLimit;
            
            public long SwapsRejectedRateLimit {
                get { return swapsRejectedRateLimit; }
            }
            
            private long totalOutputRate;
            
            public long TotalOutputRate {
                get { return totalOutputRate; }
            }
            
            private double averagePingTime;
            
            public double AveragePingTime {
                get { return averagePingTime; }
            }
            
            private long numberOfBursting;
            
            public long NumberOfBursting {
                get { return numberOfBursting; }
            }
            
            private long numberOfInsertSenders;
            
            public long NumberOfInsertSenders {
                get { return numberOfInsertSenders; }
            }
            
            private long usedJavaMemory;
            
            public long UsedJavaMemory {
                get { return usedJavaMemory; }
            }
            
            private DateTime startupTime;
            
            public DateTime StartupTime {
                get { return startupTime; }
            }
            
            private double locationChangePerSession;
            
            public double LocationChangePerSession {
                get { return locationChangePerSession; }
            }
            
            private long numberOfNeverConnected;
            
            public long NumberOfNeverConnected {
                get { return numberOfNeverConnected; }
            }
            
            private long freeJavaMemory;
            
            public long FreeJavaMemory {
                get { return freeJavaMemory; }
            }
            
            private long totalPayloadOutputRate;
            
            public long TotalPayloadOutputRate {
                get { return totalPayloadOutputRate; }
            }
            
            private bool isUsingWrapper;
            
            public bool IsUsingWrapper {
                get { return isUsingWrapper; }
            }
            
            private long storeMisses;
            
            public long StoreMisses {
                get { return storeMisses; }
            }
            
            private long storeHits;
            
            public long StoreHits {
                get { return storeHits; }
            }
            
            private long totalPayloadOutputPercent;
            
            public long TotalPayloadOutputPercent {
                get { return totalPayloadOutputPercent; }
            }
            
            private long storeSize;
            
            public long StoreSize {
                get { return storeSize; }
            }
            
            private long numberOfTooOld;
            
            public long NumberOfTooOld {
                get { return numberOfTooOld; }
            }
            
            private double avgConnectedPeersPerNode;
            
            public double AvgConnectedPeersPerNode {
                get { return avgConnectedPeersPerNode; }
            }
            
            private long availableCPUs;
            
            public long AvailableCPUs {
                get { return availableCPUs; }
            }
            
            private double swapsPerMinute;
            
            public double SwapsPerMinute {
                get { return swapsPerMinute; }
            }
            
            private double noSwapsPerMinute;
            
            public double NoSwapsPerMinute {
                get { return noSwapsPerMinute; }
            }
            
            private long numberOfListening;
            
            public long NumberOfListening {
                get { return numberOfListening; }
            }
            
            private long swapsRejectedAlreadyLocked;
            
            public long SwapsRejectedAlreadyLocked {
                get { return swapsRejectedAlreadyLocked; }
            }
            
            private long maxOverallSize;
            
            public long MaxOverallSize {
                get { return maxOverallSize; }
            }
            
            private long numberOfSimpleConnected;
            
            public long NumberOfSimpleConnected {
                get { return numberOfSimpleConnected; }
            }
            
            private long numberOfRequestSenders;
            
            public long NumberOfRequestSenders {
                get { return numberOfRequestSenders; }
            }
            
            private long overallSize;
            
            public long OverallSize {
                get { return overallSize; }
            }
            
            private long numberOfTransferringRequestSenders;
            
            public long NumberOfTransferringRequestSenders {
                get { return numberOfTransferringRequestSenders; }
            }
            
            private double percentCachedStoreHitsOfAccesses;
            
            public double PercentCachedStoreHitsOfAccesses {
                get { return percentCachedStoreHitsOfAccesses; }
            }
            
            private long swapsRejectedLoop;
            
            public long SwapsRejectedLoop {
                get { return swapsRejectedLoop; }
            }
            
            private double bwlimitDelayTime;
            
            public double BwlimitDelayTime {
                get { return bwlimitDelayTime; }
            }
            
            private double numberOfRemotePeerLocationsSeenInSwaps;
            
            public double NumberOfRemotePeerLocationsSeenInSwaps {
                get { return numberOfRemotePeerLocationsSeenInSwaps; }
            }
            
            private double pInstantReject;
            
            public double PInstantReject {
                get { return pInstantReject; }
            }
            
            private long totalPayloadOutputBytes;
            
            public long TotalPayloadOutputBytes {
                get { return totalPayloadOutputBytes; }
            }
            
            private long numberOfRoutingBackedOff;
            
            public long NumberOfRoutingBackedOff {
                get { return numberOfRoutingBackedOff; }
            }
            
            private long unclaimedFIFOSize;
            
            public long UnclaimedFIFOSize {
                get { return unclaimedFIFOSize; }
            }
            
            private long numberOfConnected;
            
            public long NumberOfConnected {
                get { return numberOfConnected; }
            }
            
            private long cachedStoreHits;
            
            public long CachedStoreHits {
                get { return cachedStoreHits; }
            }
            
            private double recentOutputRate;
            
            public double RecentOutputRate {
                get { return recentOutputRate; }
            }
            
            private long swapsRejectedRecognizedID;
            
            public long SwapsRejectedRecognizedID {
                get { return swapsRejectedRecognizedID; }
            }
            
            private long numberOfTooNew;
            
            public long NumberOfTooNew {
                get { return numberOfTooNew; }
            }
            
            private long numberOfSeedClients;
            
            public long NumberOfSeedClients {
                get { return numberOfSeedClients; }
            }
            
            private long opennetSizeEstimate48hourRecent;
            
            public long OpennetSizeEstimate48hourRecent {
                get { return opennetSizeEstimate48hourRecent; }
            }
            
            private long numberOfSeedServers;
            
            public long NumberOfSeedServers {
                get { return numberOfSeedServers; }
            }
            
            private long opennetSizeEstimateSession;
            
            public long OpennetSizeEstimateSession {
                get { return opennetSizeEstimateSession; }
            }
            
            private long opennetSizeEstimate24hourRecent;
            
            public long OpennetSizeEstimate24hourRecent {
                get { return opennetSizeEstimate24hourRecent; }
            }
            
            private NumberWithRoutingBackoffReasonsType numberWithRoutingBackoffReasons;
            
            public NumberWithRoutingBackoffReasonsType NumberWithRoutingBackoffReasons {
                get { return numberWithRoutingBackoffReasons; }
            }
            
            /*
            private long numberWithRoutingBackoffReasons.ForwardRejectedOverload;
            private long numberWithRoutingBackoffReasons.ForwardRejectedOverload5;
             */
            
            internal VolatileType(MessageParser parsed)  {
                /* TODO: volatile member */
                startedSwaps=long.Parse(parsed["volatile.startedSwaps"]);
                cacheAccesses=long.Parse(parsed["volatile.cacheAccesses"]);
                totalInputBytes=long.Parse(parsed["volatile.totalInputBytes"]);
                networkSizeEstimateSession=long.Parse(parsed["volatile.networkSizeEstimateSession"]);
                storeKeys=long.Parse(parsed["volatile.storeKeys"]);
                cachedKeys=long.Parse(parsed["volatile.cachedKeys"]);
                locationChangePerSwap=double.Parse(parsed["volatile.locationChangePerSwap"]);
                swapsRejectedNowhereToGo=long.Parse(parsed["volatile.swapsRejectedNowhereToGo"]);
                numberOfNotConnected=long.Parse(parsed["volatile.numberOfNotConnected"]);
                numberOfListenOnly=long.Parse(parsed["volatile.numberOfListenOnly"]);
                totalOutputBytes=long.Parse(parsed["volatile.totalOutputBytes"]);
                swapsPerNoSwaps=double.Parse(parsed["volatile.swapsPerNoSwaps"]);
                allocatedJavaMemory=long.Parse(parsed["volatile.allocatedJavaMemory"]);
                percentStoreHitsOfAccesses=double.Parse(parsed["volatile.percentStoreHitsOfAccesses"]);
                networkSizeEstimate24hourRecent=long.Parse(parsed["volatile.networkSizeEstimate24hourRecent"]);
                overallAccesses=long.Parse(parsed["volatile.overallAccesses"]);
                percentOverallKeysOfMax=double.Parse(parsed["volatile.percentOverallKeysOfMax"]);
                locationChangePerMinute=double.Parse(parsed["volatile.locationChangePerMinute"]);
                noSwaps=double.Parse(parsed["volatile.noSwaps"]);
                cachedSize=long.Parse(parsed["volatile.cachedSize"]);
                uptimeSeconds=long.Parse(parsed["volatile.uptimeSeconds"]);
                numberOfARKFetchers=long.Parse(parsed["volatile.numberOfARKFetchers"]);
                networkSizeEstimate48hourRecent=long.Parse(parsed["volatile.networkSizeEstimate48hourRecent"]);
                maxOverallKeys=long.Parse(parsed["volatile.maxOverallKeys"]);
                numberOfDisconnected=long.Parse(parsed["volatile.numberOfDisconnected"]);
                swaps=double.Parse(parsed["volatile.swaps"]);
                maximumJavaMemory=long.Parse(parsed["volatile.maximumJavaMemory"]);
                avgStoreAccessRate=double.Parse(parsed["volatile.avgStoreAccessRate"]);
                totalInputRate=long.Parse(parsed["volatile.totalInputRate"]);
                recentInputRate=double.Parse(parsed["volatile.recentInputRate"]);
                overallKeys=long.Parse(parsed["volatile.overallKeys"]);
                backedOffPercent=double.Parse(parsed["volatile.backedOffPercent"]);
                runningThreadCount=long.Parse(parsed["volatile.runningThreadCount"]);
                storeAccesses=long.Parse(parsed["volatile.storeAccesses"]);
                numberOfDisabled=long.Parse(parsed["volatile.numberOfDisabled"]);
                cachedStoreMisses=long.Parse(parsed["volatile.cachedStoreMisses"]);
                routingMissDistance=double.Parse(parsed["volatile.routingMissDistance"]);
                swapsRejectedRateLimit=long.Parse(parsed["volatile.swapsRejectedRateLimit"]);
                totalOutputRate=long.Parse(parsed["volatile.totalOutputRate"]);
                averagePingTime=double.Parse(parsed["volatile.averagePingTime"]);
                numberOfBursting=long.Parse(parsed["volatile.numberOfBursting"]);
                numberOfInsertSenders=long.Parse(parsed["volatile.numberOfInsertSenders"]);
                usedJavaMemory=long.Parse(parsed["volatile.usedJavaMemory"]);
                startupTime=FCP2.FromUnix(parsed["volatile.startupTime"]);
                locationChangePerSession=double.Parse(parsed["volatile.locationChangePerSession"]);
                numberOfNeverConnected=long.Parse(parsed["volatile.numberOfNeverConnected"]);
                freeJavaMemory=long.Parse(parsed["volatile.freeJavaMemory"]);
                totalPayloadOutputRate=long.Parse(parsed["volatile.totalPayloadOutputRate"]);
                isUsingWrapper=bool.Parse(parsed["volatile.isUsingWrapper"]);
                storeMisses=long.Parse(parsed["volatile.storeMisses"]);
                storeHits=long.Parse(parsed["volatile.storeHits"]);
                totalPayloadOutputPercent=long.Parse(parsed["volatile.totalPayloadOutputPercent"]);
                storeSize=long.Parse(parsed["volatile.storeSize"]);
                numberOfTooOld=long.Parse(parsed["volatile.numberOfTooOld"]);
                avgConnectedPeersPerNode=double.Parse(parsed["volatile.avgConnectedPeersPerNode"]);
                availableCPUs=long.Parse(parsed["volatile.availableCPUs"]);
                swapsPerMinute=double.Parse(parsed["volatile.swapsPerMinute"]);
                noSwapsPerMinute=double.Parse(parsed["volatile.noSwapsPerMinute"]);
                numberOfListening=long.Parse(parsed["volatile.numberOfListening"]);
                swapsRejectedAlreadyLocked=long.Parse(parsed["volatile.swapsRejectedAlreadyLocked"]);
                maxOverallSize=long.Parse(parsed["volatile.maxOverallSize"]);
                numberOfSimpleConnected=long.Parse(parsed["volatile.numberOfSimpleConnected"]);
                numberOfRequestSenders=long.Parse(parsed["volatile.numberOfRequestSenders"]);
                overallSize=long.Parse(parsed["volatile.overallSize"]);
                numberOfTransferringRequestSenders=long.Parse(parsed["volatile.numberOfTransferringRequestSenders"]);
                percentCachedStoreHitsOfAccesses=double.Parse(parsed["volatile.percentCachedStoreHitsOfAccesses"]);
                swapsRejectedLoop=long.Parse(parsed["volatile.swapsRejectedLoop"]);
                bwlimitDelayTime=double.Parse(parsed["volatile.bwlimitDelayTime"]);
                numberOfRemotePeerLocationsSeenInSwaps=double.Parse(parsed["volatile.numberOfRemotePeerLocationsSeenInSwaps"]);
                pInstantReject=double.Parse(parsed["volatile.pInstantReject"]);
                totalPayloadOutputBytes=long.Parse(parsed["volatile.totalPayloadOutputBytes"]);
                numberOfRoutingBackedOff=long.Parse(parsed["volatile.numberOfRoutingBackedOff"]);
                unclaimedFIFOSize=long.Parse(parsed["volatile.unclaimedFIFOSize"]);
                numberOfConnected=long.Parse(parsed["volatile.numberOfConnected"]);
                cachedStoreHits=long.Parse(parsed["volatile.cachedStoreHits"]);
                recentOutputRate=double.Parse(parsed["volatile.recentOutputRate"]);
                swapsRejectedRecognizedID=long.Parse(parsed["volatile.swapsRejectedRecognizedID"]);
                numberOfTooNew=long.Parse(parsed["volatile.numberOfTooNew"]);

                numberOfSeedClients = long.Parse(parsed["volatile.numberOfSeedClients"]);
                opennetSizeEstimate48hourRecent = long.Parse(parsed["volatile.opennetSizeEstimate48hourRecent"]);
                numberOfSeedServers = long.Parse(parsed["volatile.numberOfSeedServers"]);
                opennetSizeEstimateSession = long.Parse(parsed["volatile.opennetSizeEstimateSession"]);
                opennetSizeEstimate24hourRecent = long.Parse(parsed["volatile.opennetSizeEstimate24hourRecent"]);              
                
                numberWithRoutingBackoffReasons = new NumberWithRoutingBackoffReasonsType(parsed);
            }
            
            public class NumberWithRoutingBackoffReasonsType {
                internal NumberWithRoutingBackoffReasonsType(MessageParser parsed) {
                    /* TODO: implementation */
                }
            }
        }
    }
}