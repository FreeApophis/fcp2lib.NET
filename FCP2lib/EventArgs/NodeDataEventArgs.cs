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
            
            private int number;
            
            public int Number {
                get { return number; }
            }
            
            internal ArkType(MessageParser parsed) {
                this.pubURI = parsed["ark.pubURI"];
                this.privURI = parsed["ark.privURI"];
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
            private List<int> negTypes = new List<int>();
            
            public List<int> NegTypes {
                get { return negTypes; }
            }
            
            internal AuthType(MessageParser parsed) {
                if (parsed["auth.negTypes"]!=null) {
                    foreach(string an in parsed["auth.negTypes"].Split(new char[] {';'})) {
                        this.negTypes.Add(int.Parse(an));
                    }
                }
            }
        }
        
        public class VolatileType {
            
            private int startedSwaps;
            
            public int StartedSwaps {
                get { return startedSwaps; }
            }
            
            private int cacheAccesses;
            
            public int CacheAccesses {
                get { return cacheAccesses; }
            }
            
            private long totalInputBytes;
            
            public long TotalInputBytes {
                get { return totalInputBytes; }
            }
            
            private int networkSizeEstimateSession;
            
            public int NetworkSizeEstimateSession {
                get { return networkSizeEstimateSession; }
            }
            
            private int storeKeys;
            
            public int StoreKeys {
                get { return storeKeys; }
            }
            
            private int cachedKeys;
            
            public int CachedKeys {
                get { return cachedKeys; }
            }
            
            private double locationChangePerSwap;
            
            public double LocationChangePerSwap {
                get { return locationChangePerSwap; }
            }
            
            private int swapsRejectedNowhereToGo;
            
            public int SwapsRejectedNowhereToGo {
                get { return swapsRejectedNowhereToGo; }
            }
            
            private int numberOfNotConnected;
            
            public int NumberOfNotConnected {
                get { return numberOfNotConnected; }
            }
            
            private int numberOfListenOnly;
            
            public int NumberOfListenOnly {
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
            
            private int allocatedJavaMemory;
            
            public int AllocatedJavaMemory {
                get { return allocatedJavaMemory; }
            }
            
            private double percentStoreHitsOfAccesses;
            
            public double PercentStoreHitsOfAccesses {
                get { return percentStoreHitsOfAccesses; }
            }
            
            private int networkSizeEstimate24hourRecent;
            
            public int NetworkSizeEstimate24hourRecent {
                get { return networkSizeEstimate24hourRecent; }
            }
            
            private int overallAccesses;
            
            public int OverallAccesses {
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
            
            private int cachedSize;
            
            public int CachedSize {
                get { return cachedSize; }
            }
            
            private int uptimeSeconds;
            
            public int UptimeSeconds {
                get { return uptimeSeconds; }
            }
            
            private int numberOfARKFetchers;
            
            public int NumberOfARKFetchers {
                get { return numberOfARKFetchers; }
            }
            
            private int networkSizeEstimate48hourRecent;
            
            public int NetworkSizeEstimate48hourRecent {
                get { return networkSizeEstimate48hourRecent; }
            }
            
            private int maxOverallKeys;
            
            public int MaxOverallKeys {
                get { return maxOverallKeys; }
            }
            
            private int numberOfDisconnected;
            
            public int NumberOfDisconnected {
                get { return numberOfDisconnected; }
            }
            
            private double swaps;
            
            public double Swaps {
                get { return swaps; }
            }
            
            private int maximumJavaMemory;
            
            public int MaximumJavaMemory {
                get { return maximumJavaMemory; }
            }
            
            private double avgStoreAccessRate;
            
            public double AvgStoreAccessRate {
                get { return avgStoreAccessRate; }
            }
            
            private int totalInputRate;
            
            public int TotalInputRate {
                get { return totalInputRate; }
            }
            
            private double recentInputRate;
            
            public double RecentInputRate {
                get { return recentInputRate; }
            }
            
            private int overallKeys;
            
            public int OverallKeys {
                get { return overallKeys; }
            }
            
            private double backedOffPercent;
            
            public double BackedOffPercent {
                get { return backedOffPercent; }
            }
            
            private int runningThreadCount;
            
            public int RunningThreadCount {
                get { return runningThreadCount; }
            }
            
            private int storeAccesses;
            
            public int StoreAccesses {
                get { return storeAccesses; }
            }
            
            private int numberOfDisabled;
            
            public int NumberOfDisabled {
                get { return numberOfDisabled; }
            }
            
            private int cachedStoreMisses;
            
            public int CachedStoreMisses {
                get { return cachedStoreMisses; }
            }
            
            private double routingMissDistance;
            
            public double RoutingMissDistance {
                get { return routingMissDistance; }
            }
            
            private int swapsRejectedRateLimit;
            
            public int SwapsRejectedRateLimit {
                get { return swapsRejectedRateLimit; }
            }
            
            private int totalOutputRate;
            
            public int TotalOutputRate {
                get { return totalOutputRate; }
            }
            
            private double averagePingTime;
            
            public double AveragePingTime {
                get { return averagePingTime; }
            }
            
            private int numberOfBursting;
            
            public int NumberOfBursting {
                get { return numberOfBursting; }
            }
            
            private int numberOfInsertSenders;
            
            public int NumberOfInsertSenders {
                get { return numberOfInsertSenders; }
            }
            
            private int usedJavaMemory;
            
            public int UsedJavaMemory {
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
            
            private int numberOfNeverConnected;
            
            public int NumberOfNeverConnected {
                get { return numberOfNeverConnected; }
            }
            
            private int freeJavaMemory;
            
            public int FreeJavaMemory {
                get { return freeJavaMemory; }
            }
            
            private int totalPayloadOutputRate;
            
            public int TotalPayloadOutputRate {
                get { return totalPayloadOutputRate; }
            }
            
            private bool isUsingWrapper;
            
            public bool IsUsingWrapper {
                get { return isUsingWrapper; }
            }
            
            private int storeMisses;
            
            public int StoreMisses {
                get { return storeMisses; }
            }
            
            private int storeHits;
            
            public int StoreHits {
                get { return storeHits; }
            }
            
            private int totalPayloadOutputPercent;
            
            public int TotalPayloadOutputPercent {
                get { return totalPayloadOutputPercent; }
            }
            
            private int storeSize;
            
            public int StoreSize {
                get { return storeSize; }
            }
            
            private int numberOfTooOld;
            
            public int NumberOfTooOld {
                get { return numberOfTooOld; }
            }
            
            private double avgConnectedPeersPerNode;
            
            public double AvgConnectedPeersPerNode {
                get { return avgConnectedPeersPerNode; }
            }
            
            private int availableCPUs;
            
            public int AvailableCPUs {
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
            
            private int numberOfListening;
            
            public int NumberOfListening {
                get { return numberOfListening; }
            }
            
            private int swapsRejectedAlreadyLocked;
            
            public int SwapsRejectedAlreadyLocked {
                get { return swapsRejectedAlreadyLocked; }
            }
            
            private int maxOverallSize;
            
            public int MaxOverallSize {
                get { return maxOverallSize; }
            }
            
            private int numberOfSimpleConnected;
            
            public int NumberOfSimpleConnected {
                get { return numberOfSimpleConnected; }
            }
            
            private int numberOfRequestSenders;
            
            public int NumberOfRequestSenders {
                get { return numberOfRequestSenders; }
            }
            
            private int overallSize;
            
            public int OverallSize {
                get { return overallSize; }
            }
            
            private int numberOfTransferringRequestSenders;
            
            public int NumberOfTransferringRequestSenders {
                get { return numberOfTransferringRequestSenders; }
            }
            
            private double percentCachedStoreHitsOfAccesses;
            
            public double PercentCachedStoreHitsOfAccesses {
                get { return percentCachedStoreHitsOfAccesses; }
            }
            
            private int swapsRejectedLoop;
            
            public int SwapsRejectedLoop {
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
            
            private int numberOfRoutingBackedOff;
            
            public int NumberOfRoutingBackedOff {
                get { return numberOfRoutingBackedOff; }
            }
            
            private int unclaimedFIFOSize;
            
            public int UnclaimedFIFOSize {
                get { return unclaimedFIFOSize; }
            }
            
            private int numberOfConnected;
            
            public int NumberOfConnected {
                get { return numberOfConnected; }
            }
            
            private int cachedStoreHits;
            
            public int CachedStoreHits {
                get { return cachedStoreHits; }
            }
            
            private double recentOutputRate;
            
            public double RecentOutputRate {
                get { return recentOutputRate; }
            }
            
            private int swapsRejectedRecognizedID;
            
            public int SwapsRejectedRecognizedID {
                get { return swapsRejectedRecognizedID; }
            }
            
            private int numberOfTooNew;
            
            public int NumberOfTooNew {
                get { return numberOfTooNew; }
            }
            
            private int numberOfSeedClients;
            
            public int NumberOfSeedClients {
                get { return numberOfSeedClients; }
            }
            
            private int opennetSizeEstimate48hourRecent;
            
            public int OpennetSizeEstimate48hourRecent {
                get { return opennetSizeEstimate48hourRecent; }
            }
            
            private int numberOfSeedServers;
            
            public int NumberOfSeedServers {
                get { return numberOfSeedServers; }
            }
            
            private int opennetSizeEstimateSession;
            
            public int OpennetSizeEstimateSession {
                get { return opennetSizeEstimateSession; }
            }
            
            private int opennetSizeEstimate24hourRecent;
            
            public int OpennetSizeEstimate24hourRecent {
                get { return opennetSizeEstimate24hourRecent; }
            }
            
            private NumberWithRoutingBackoffReasonsType numberWithRoutingBackoffReasons;
            
            public NumberWithRoutingBackoffReasonsType NumberWithRoutingBackoffReasons {
                get { return numberWithRoutingBackoffReasons; }
            }
            
            /*
            private int numberWithRoutingBackoffReasons.ForwardRejectedOverload;
            private int numberWithRoutingBackoffReasons.ForwardRejectedOverload5;
             */
            
            internal VolatileType(MessageParser parsed)  {
                /* TODO: volatile member */
                startedSwaps=int.Parse(parsed["volatile.startedSwaps"]);
                cacheAccesses=int.Parse(parsed["volatile.cacheAccesses"]);
                totalInputBytes=long.Parse(parsed["volatile.totalInputBytes"]);
                networkSizeEstimateSession=int.Parse(parsed["volatile.networkSizeEstimateSession"]);
                storeKeys=int.Parse(parsed["volatile.storeKeys"]);
                cachedKeys=int.Parse(parsed["volatile.cachedKeys"]);
                locationChangePerSwap=double.Parse(parsed["volatile.locationChangePerSwap"]);
                swapsRejectedNowhereToGo=int.Parse(parsed["volatile.swapsRejectedNowhereToGo"]);
                numberOfNotConnected=int.Parse(parsed["volatile.numberOfNotConnected"]);
                numberOfListenOnly=int.Parse(parsed["volatile.numberOfListenOnly"]);
                totalOutputBytes=long.Parse(parsed["volatile.totalOutputBytes"]);
                swapsPerNoSwaps=double.Parse(parsed["volatile.swapsPerNoSwaps"]);
                allocatedJavaMemory=int.Parse(parsed["volatile.allocatedJavaMemory"]);
                percentStoreHitsOfAccesses=double.Parse(parsed["volatile.percentStoreHitsOfAccesses"]);
                networkSizeEstimate24hourRecent=int.Parse(parsed["volatile.networkSizeEstimate24hourRecent"]);
                overallAccesses=int.Parse(parsed["volatile.overallAccesses"]);
                percentOverallKeysOfMax=double.Parse(parsed["volatile.percentOverallKeysOfMax"]);
                locationChangePerMinute=double.Parse(parsed["volatile.locationChangePerMinute"]);
                noSwaps=double.Parse(parsed["volatile.noSwaps"]);
                cachedSize=int.Parse(parsed["volatile.cachedSize"]);
                uptimeSeconds=int.Parse(parsed["volatile.uptimeSeconds"]);
                numberOfARKFetchers=int.Parse(parsed["volatile.numberOfARKFetchers"]);
                networkSizeEstimate48hourRecent=int.Parse(parsed["volatile.networkSizeEstimate48hourRecent"]);
                maxOverallKeys=int.Parse(parsed["volatile.maxOverallKeys"]);
                numberOfDisconnected=int.Parse(parsed["volatile.numberOfDisconnected"]);
                swaps=double.Parse(parsed["volatile.swaps"]);
                maximumJavaMemory=int.Parse(parsed["volatile.maximumJavaMemory"]);
                avgStoreAccessRate=double.Parse(parsed["volatile.avgStoreAccessRate"]);
                totalInputRate=int.Parse(parsed["volatile.totalInputRate"]);
                recentInputRate=double.Parse(parsed["volatile.recentInputRate"]);
                overallKeys=int.Parse(parsed["volatile.overallKeys"]);
                backedOffPercent=double.Parse(parsed["volatile.backedOffPercent"]);
                runningThreadCount=int.Parse(parsed["volatile.runningThreadCount"]);
                storeAccesses=int.Parse(parsed["volatile.storeAccesses"]);
                numberOfDisabled=int.Parse(parsed["volatile.numberOfDisabled"]);
                cachedStoreMisses=int.Parse(parsed["volatile.cachedStoreMisses"]);
                routingMissDistance=double.Parse(parsed["volatile.routingMissDistance"]);
                swapsRejectedRateLimit=int.Parse(parsed["volatile.swapsRejectedRateLimit"]);
                totalOutputRate=int.Parse(parsed["volatile.totalOutputRate"]);
                averagePingTime=double.Parse(parsed["volatile.averagePingTime"]);
                numberOfBursting=int.Parse(parsed["volatile.numberOfBursting"]);
                numberOfInsertSenders=int.Parse(parsed["volatile.numberOfInsertSenders"]);
                usedJavaMemory=int.Parse(parsed["volatile.usedJavaMemory"]);
                startupTime=FCP2.FromUnix(parsed["volatile.startupTime"]);
                locationChangePerSession=double.Parse(parsed["volatile.locationChangePerSession"]);
                numberOfNeverConnected=int.Parse(parsed["volatile.numberOfNeverConnected"]);
                freeJavaMemory=int.Parse(parsed["volatile.freeJavaMemory"]);
                totalPayloadOutputRate=int.Parse(parsed["volatile.totalPayloadOutputRate"]);
                isUsingWrapper=bool.Parse(parsed["volatile.isUsingWrapper"]);
                storeMisses=int.Parse(parsed["volatile.storeMisses"]);
                storeHits=int.Parse(parsed["volatile.storeHits"]);
                totalPayloadOutputPercent=int.Parse(parsed["volatile.totalPayloadOutputPercent"]);
                storeSize=int.Parse(parsed["volatile.storeSize"]);
                numberOfTooOld=int.Parse(parsed["volatile.numberOfTooOld"]);
                avgConnectedPeersPerNode=double.Parse(parsed["volatile.avgConnectedPeersPerNode"]);
                availableCPUs=int.Parse(parsed["volatile.availableCPUs"]);
                swapsPerMinute=double.Parse(parsed["volatile.swapsPerMinute"]);
                noSwapsPerMinute=double.Parse(parsed["volatile.noSwapsPerMinute"]);
                numberOfListening=int.Parse(parsed["volatile.numberOfListening"]);
                swapsRejectedAlreadyLocked=int.Parse(parsed["volatile.swapsRejectedAlreadyLocked"]);
                maxOverallSize=int.Parse(parsed["volatile.maxOverallSize"]);
                numberOfSimpleConnected=int.Parse(parsed["volatile.numberOfSimpleConnected"]);
                numberOfRequestSenders=int.Parse(parsed["volatile.numberOfRequestSenders"]);
                overallSize=int.Parse(parsed["volatile.overallSize"]);
                numberOfTransferringRequestSenders=int.Parse(parsed["volatile.numberOfTransferringRequestSenders"]);
                percentCachedStoreHitsOfAccesses=double.Parse(parsed["volatile.percentCachedStoreHitsOfAccesses"]);
                swapsRejectedLoop=int.Parse(parsed["volatile.swapsRejectedLoop"]);
                bwlimitDelayTime=double.Parse(parsed["volatile.bwlimitDelayTime"]);
                numberOfRemotePeerLocationsSeenInSwaps=double.Parse(parsed["volatile.numberOfRemotePeerLocationsSeenInSwaps"]);
                pInstantReject=double.Parse(parsed["volatile.pInstantReject"]);
                totalPayloadOutputBytes=long.Parse(parsed["volatile.totalPayloadOutputBytes"]);
                numberOfRoutingBackedOff=int.Parse(parsed["volatile.numberOfRoutingBackedOff"]);
                unclaimedFIFOSize=int.Parse(parsed["volatile.unclaimedFIFOSize"]);
                numberOfConnected=int.Parse(parsed["volatile.numberOfConnected"]);
                cachedStoreHits=int.Parse(parsed["volatile.cachedStoreHits"]);
                recentOutputRate=double.Parse(parsed["volatile.recentOutputRate"]);
                swapsRejectedRecognizedID=int.Parse(parsed["volatile.swapsRejectedRecognizedID"]);
                numberOfTooNew=int.Parse(parsed["volatile.numberOfTooNew"]);

                numberOfSeedClients = int.Parse(parsed["volatile.numberOfSeedClients"]);
                opennetSizeEstimate48hourRecent = int.Parse(parsed["volatile.opennetSizeEstimate48hourRecent"]);
                numberOfSeedServers = int.Parse(parsed["volatile.numberOfSeedServers"]);
                opennetSizeEstimateSession = int.Parse(parsed["volatile.opennetSizeEstimateSession"]);
                opennetSizeEstimate24hourRecent = int.Parse(parsed["volatile.opennetSizeEstimate24hourRecent"]);              
                
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