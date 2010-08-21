using System;
using System.Dynamic;

namespace FCP2
{
    public class MessageParserSubElement : DynamicObject
    {
        private readonly MessageParser parentParser;
        private readonly string name;

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

        private static object GetDefault(Type type)
        {
            if (type == typeof(bool)) { return false; }
            if (type == typeof(char)) { return '\x0'; }
            if (type == typeof(int)) { return 0; }
            if (type == typeof(long)) { return 0L; }
            if (type == typeof(float)) { return 0.0f; }
            if (type == typeof(double)) { return 0.0; }
            if (type == typeof(decimal)) { return 0.0m; }
            if (type == typeof(DateTime)) { return DateTime.Now; }
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