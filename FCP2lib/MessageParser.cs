/*
 * Erstellt mit SharpDevelop.
 * Benutzer: apophis
 * Datum: 18.02.2009
 * Zeit: 22:38
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Freenet.FCP2
{
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class MessageParser
    {
        private Dictionary<string, string> parameters = new Dictionary<string, string>();
        
        #if DEBUG
        private Dictionary<string, int> debugcount = new Dictionary<string, int>();
        #endif
        
        private bool dataAvailable = false;
        
        public bool DataAvailable {
            get { return dataAvailable; }
        }
        
        public MessageParser(TextReader reader)
        {
            string line;
            int pos;
            while((line = reader.ReadLine()) != FCP2.endMessage) {
                if (line == "End")
                    throw new NotImplementedException("NoEnd");
                if (line == "Data") {
                    dataAvailable = true;
                    break;
                }
                if((pos = line.IndexOf('=')) == -1) {
                    throw new NotImplementedException("EmptyValue:" + line);
                } else {
                    parameters.Add(line.Substring(0, pos), line.Substring(pos+1));
                    #if DEBUG
                    debugcount.Add(line.Substring(0, pos), 0);
                    #endif
                }
            }
        }
        
        /// <summary>
        /// Intelligent Accessor, returns null if a value does not exists!
        /// </summary>
        public string this[string keyword] {
            get {
                if (parameters.ContainsKey(keyword)) {
                    #if DEBUG
                    debugcount[keyword]++;
                    #endif
                    return parameters[keyword];
                } else {
                    #if DEBUG
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("keyword '" + keyword + "' not found!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    #endif
                    return null;
                }
            }
        }
        
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach(KeyValuePair<string, string> kvp in parameters) {
                yield return kvp;
            }
            yield break;
        }

        #if DEBUG
        public void PrintAccessCount() {
            bool allaccessed = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach(string key in debugcount.Keys) {
                Console.WriteLine("[" + debugcount[key] + "] " + key);
                allaccessed = allaccessed && (debugcount[key] != 0);
            }
            if(allaccessed) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All fields have been accessed");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Some fields have not been accessed");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        #endif

    }

}
