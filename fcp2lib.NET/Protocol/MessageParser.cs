/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
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
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace FCP2.Protocol
{
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class MessageParser : DynamicObject
    {
        readonly Dictionary<string, string> _parameters = new Dictionary<string, string>();

#if DEBUG
        readonly Dictionary<string, long> _debugCount = new Dictionary<string, long>();
#endif

        public bool DataAvailable { get; }

        /// <exception cref="T:System.NotImplementedException"></exception>
        public MessageParser(TextReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != nameof(FCP2Protocol.EndMessage))
            {
                int pos;
                if (line == "End")
                    throw new NotImplementedException("NoEnd");
                if (line == "Data")
                {
                    DataAvailable = true;
                    break;
                }
                if ((pos = line.IndexOf('=')) == -1)
                {
                    throw new NotImplementedException("EmptyValue:" + line);
                }
                var key = line.Substring(0, pos);
                var val = line.Substring(pos + 1);
                _parameters.Add(key, val);
#if DEBUG
                _debugCount.Add(line.Substring(0, pos), 0);
#endif
            }
        }

        /// <summary>
        /// Intelligent Accessor, returns null if a value does not exists! And does not bark out at all!
        /// </summary>
        internal string SafeGet(string keyword)
        {
            if (_parameters.ContainsKey(keyword))
            {
#if DEBUG
                _debugCount[keyword]++;
#endif
                return _parameters[keyword];
            }
            return null;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var temp = SafeGet(binder.Name);
            result = temp != null
                ? (object)new DynamicReturnValue(temp)
                : new MessageParserSubElement(this, binder.Name);

            return true;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, string>>)_parameters).GetEnumerator();
        }

#if DEBUG
        public void PrintAccessCount()
        {
            bool allAccessed = true;
            foreach (string key in _debugCount.Keys)
            {
                Console.ForegroundColor = _debugCount[key] == 0 ? ConsoleColor.Yellow : ConsoleColor.Green;
                Console.WriteLine("[" + _debugCount[key] + "] " + key + " (" + _parameters[key] + ")");

                allAccessed = allAccessed && (_debugCount[key] != 0);
            }
            if (allAccessed)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All fields have been accessed");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Some fields have not been accessed");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
#endif

    }
}
