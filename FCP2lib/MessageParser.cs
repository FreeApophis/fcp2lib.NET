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
using System.Collections.Generic;
using System.IO;

namespace FCP2
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
            while ((line = reader.ReadLine()) != FCP2Protocol.EndMessage)
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
