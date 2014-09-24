/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2014 Thomas Bruderer <apophis@apophis.ch>
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
using System.Dynamic;

namespace FCP2
{
    public class MessageParserSubElement : DynamicObject
    {
        readonly MessageParser parentParser;
        readonly string name;

        public bool DataAvailable
        {
            get { return parentParser.DataAvailable; }
        }

        public bool Exists()
        {
            return parentParser.SafeGet(name) != null;
        }

        public MessageParserSubElement(MessageParser messageParser, string element)
        {
            parentParser = messageParser;
            name = element;

        }
        public override bool TryConvert(ConvertBinder binder, out object result)
        {
#if DEBUG
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("keyword '" + name + "' not found! (Type: " + binder.Type + ")");
            Console.ForegroundColor = ConsoleColor.Gray;
#endif
            result = GetDefault(binder.Type);
            return true;
        }

        static object GetDefault(Type type)
        {
            if (type == typeof(bool)) { return false; }
            if (type == typeof(char)) { return '\x0'; }
            if (type == typeof(int)) { return 0; }
            if (type == typeof(long)) { return 0L; }
            if (type == typeof(float)) { return 0.0f; }
            if (type == typeof(double)) { return 0.0; }
            if (type == typeof(decimal)) { return 0.0m; }
            if (type == typeof(DateTime)) { return DateTime.Now; }
             if (type == typeof(PriorityClassEnum)) { return PriorityClassEnum.Medium; }
            return null;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var element = name + "." + binder.Name;
            var temp = parentParser.SafeGet(element);
            if (temp != null)
            {
                result = new DynamicReturnValue(temp);
            }
            else
            {
                result = new MessageParserSubElement(parentParser, element);
            }
            return true;
        }
    }
}