using System;
using System.Collections.Generic;
using System.IO;

namespace Freenet.FCP2
{
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class MessageParser
    {
        private readonly Dictionary<string, string> parameters = new Dictionary<string, string>();

#if DEBUG
        private readonly Dictionary<string, long> debugcount = new Dictionary<string, long>();
#endif

        private readonly bool dataAvailable;

        public bool DataAvailable
        {
            get { return dataAvailable; }
        }

        public MessageParser(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != FCP2.EndMessage)
            {
                int pos;
                if (line == "End")
                    throw new NotImplementedException("NoEnd");
                if (line == "Data")
                {
                    dataAvailable = true;
                    break;
                }
                if ((pos = line.IndexOf('=')) == -1)
                {
                    throw new NotImplementedException("EmptyValue:" + line);
                }
                parameters.Add(line.Substring(0, pos), line.Substring(pos + 1));
#if DEBUG
                debugcount.Add(line.Substring(0, pos), 0);
#endif
            }
        }

        /// <summary>
        /// Intelligent Accessor, returns null if a value does not exists! And does not bark out at all!
        /// </summary>
        public string this[string keyword]
        {
            get
            {
                if (parameters.ContainsKey(keyword))
                {
#if DEBUG
                    debugcount[keyword]++;
#endif
                    return parameters[keyword];
                }
#if DEBUG
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("keyword '" + keyword + "' not found!");
                Console.ForegroundColor = ConsoleColor.Gray;
#endif
                return null;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach (var kvp in parameters)
            {
                yield return kvp;
            }
            yield break;
        }

#if DEBUG
        public void PrintAccessCount()
        {
            //            bool allaccessed = true;
            //            Console.ForegroundColor = ConsoleColor.Yellow;
            //            foreach(string key in debugcount.Keys) {
            //                Console.WriteLine("[" + debugcount[key] + "] " + key);
            //                allaccessed = allaccessed && (debugcount[key] != 0);
            //            }
            //            if(allaccessed) {
            //                Console.ForegroundColor = ConsoleColor.Green;
            //                Console.WriteLine("All fields have been accessed");
            //            } else {
            //                Console.ForegroundColor = ConsoleColor.Red;
            //                Console.WriteLine("Some fields have not been accessed");
            //            }
            //            Console.ForegroundColor = ConsoleColor.Gray;
        }
#endif

    }

}
