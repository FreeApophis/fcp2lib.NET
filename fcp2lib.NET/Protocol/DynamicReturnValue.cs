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
using System.Diagnostics;
using System.Dynamic;

namespace FCP2.Protocol
{
    class DynamicReturnValue : DynamicObject
    {
        static readonly Dictionary<Type, ConversionDelegate> TypeTable = new Dictionary<Type, ConversionDelegate>();
        readonly string _dynamicString;

        public bool Test { get; } = true;

        private delegate object ConversionDelegate(string original, out bool successful);

        bool _lastConversionSuccessful;
        public bool LastConversionSuccessful => _lastConversionSuccessful;

        public bool Exists()
        {
            return _dynamicString != null;
        }

        static DynamicReturnValue()
        {
            Debug.Assert(TypeTable.Count != 0);

            TypeTable.Add(typeof(bool), ConvertToBool);
            TypeTable.Add(typeof(string), ConvertToString);
            TypeTable.Add(typeof(int), ConvertToInt);
            TypeTable.Add(typeof(long), ConvertToLong);
            TypeTable.Add(typeof(decimal), ConvertToDecimal);
            TypeTable.Add(typeof(float), ConvertToFloat);
            TypeTable.Add(typeof(double), ConvertToDouble);
            TypeTable.Add(typeof(DateTime), ConvertToDateTime);
            TypeTable.Add(typeof(PeerNoteTypeEnum), ConvertToPeerNoteTypeEnum);
            TypeTable.Add(typeof(PersistenceEnum), ConvertToPersistenceEnum);
            TypeTable.Add(typeof(VerbosityEnum), ConvertToVerbosityEnum);
            TypeTable.Add(typeof(PriorityClassEnum), ConvertToPriorityClassEnum);
            TypeTable.Add(typeof(ReturnTypeEnum), ConvertToReturnTypeEnum);
            TypeTable.Add(typeof(UploadFromEnum), ConvertToUploadFromEnum);
            TypeTable.Add(typeof(UrlTypeEnum), ConvertToUrlTypeEnum);
            TypeTable.Add(typeof(OfficialSourceTypeEnum), ConvertToOfficialSourceTypeEnum);
        }

        public DynamicReturnValue(string value)
        {
            _dynamicString = value;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            ConversionDelegate conversion;
            if (TypeTable.TryGetValue(binder.ReturnType, out conversion))
            {
                result = conversion(_dynamicString, out _lastConversionSuccessful);
                return true;
            }

            result = null;
#if DEBUG
            Console.WriteLine("(" + binder.ReturnType + ") " + _dynamicString + " :: Conversion Failed");
#endif
            return false;
        }

        static object ConvertToString(string original, out bool success)
        {
            success = true;
            return original;
        }

        static object ConvertToBool(string original, out bool success)
        {
            bool boolResult;
            if (bool.TryParse(original, out boolResult))
            {
                success = true;
                return boolResult;
            }
            success = false;
            return false;
        }

        static object ConvertToInt(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return intResult;
            }
            success = false;
            return 0;
        }

        static object ConvertToLong(string original, out bool success)
        {
            long longResult;
            if (long.TryParse(original, out longResult))
            {
                success = true;
                return longResult;
            }
            success = false;
            return 0L;
        }


        static object ConvertToDecimal(string original, out bool success)
        {
            decimal decimalResult;
            if (decimal.TryParse(original, out decimalResult))
            {
                success = true;
                return decimalResult;
            }
            success = false;
            return 0.0m;
        }

        static object ConvertToFloat(string original, out bool success)
        {
            float floatResult;
            if (float.TryParse(original, out floatResult))
            {
                success = true;
                return floatResult;
            }
            success = false;
            return 0.0f;
        }

        static object ConvertToDouble(string original, out bool success)
        {
            double doubleResult;
            if (double.TryParse(original, out doubleResult))
            {
                success = true;
                return doubleResult;
            }
            success = false;
            return 0.0;
        }

        static object ConvertToDateTime(string original, out bool success)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long afterEpoch;
            if (long.TryParse(original, out afterEpoch))
            {
                double seconds = afterEpoch / 1000.0;
                success = true;
                return epoch.AddSeconds(seconds);
            }

            DateTime dateTimeResult;
            if (DateTime.TryParse(original, out dateTimeResult))
            {
                success = true;
                return dateTimeResult;
            }
            success = false;
            return DateTime.MinValue;
        }

        static object ConvertToPeerNoteTypeEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (PeerNoteTypeEnum)intResult;
            }

            PeerNoteTypeEnum peerNoteTypeEnumResult;
            if (Enum.TryParse(original, true, out peerNoteTypeEnumResult))
            {
                success = true;
                return peerNoteTypeEnumResult;
            }

            success = false;
            return null;
        }

        static object ConvertToPersistenceEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (PersistenceEnum)intResult;
            }

            PersistenceEnum persistenceEnumResult;
            if (Enum.TryParse(original, true, out persistenceEnumResult))
            {
                success = true;
                return persistenceEnumResult;
            }

            success = false;
            return null;
        }

        static object ConvertToVerbosityEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (VerbosityEnum)intResult;
            }

            VerbosityEnum verbosityEnumResult;
            if (Enum.TryParse(original, true, out verbosityEnumResult))
            {
                success = true;
                return verbosityEnumResult;
            }

            success = false;
            return null;
        }

        static object ConvertToPriorityClassEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (PriorityClassEnum)intResult;
            }

            PriorityClassEnum priorityClassEnumResult;
            if (Enum.TryParse(original, true, out priorityClassEnumResult))
            {
                success = true;
                return priorityClassEnumResult;
            }

            success = false;
            return null;
        }

        static object ConvertToReturnTypeEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (ReturnTypeEnum)intResult;
            }

            ReturnTypeEnum returnTypeEnumResult;
            if (Enum.TryParse(original, true, out returnTypeEnumResult))
            {
                success = true;
                return returnTypeEnumResult;
            }

            success = false;
            return null;
        }

        static object ConvertToUploadFromEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (UploadFromEnum)intResult;
            }

            UploadFromEnum uploadFromEnumResult;
            if (Enum.TryParse(original, true, out uploadFromEnumResult))
            {
                success = true;
                return uploadFromEnumResult;
            }

            success = false;
            return null;
        }

        static object ConvertToUrlTypeEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (UrlTypeEnum)intResult;
            }

            UrlTypeEnum urlTypeEnumResult;
            if (Enum.TryParse(original, true, out urlTypeEnumResult))
            {
                success = true;
                return urlTypeEnumResult;
            }

            success = false;
            return null;
        }

        static object ConvertToOfficialSourceTypeEnum(string original, out bool success)
        {
            int intResult;
            if (int.TryParse(original, out intResult))
            {
                success = true;
                return (OfficialSourceTypeEnum)intResult;
            }

            OfficialSourceTypeEnum officialSourceTypeEnumResult;
            if (Enum.TryParse(original, true, out officialSourceTypeEnumResult))
            {
                success = true;
                return officialSourceTypeEnumResult;
            }

            success = false;
            return null;
        }
    }
}