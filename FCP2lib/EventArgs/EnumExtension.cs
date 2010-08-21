/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2010 Thomas Bruderer <apophis@apophis.ch>
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
using FCP2.Protocol;

namespace FCP2.EventArgs
{
    public static class EnumExtension
    {
        public static string ToProtocolString(this bool boolean)
        {
            return boolean ? "true" : "false";
        }

        public static string ToProtocolString(this PersistenceEnum persistenceEnum)
        {
            switch (persistenceEnum)
            {
                case PersistenceEnum.Connection:
                    return "connection";
                case PersistenceEnum.Reboot:
                    return "reboot";
                case PersistenceEnum.Forever:
                    return "forever";
                default:
                    throw new ArgumentOutOfRangeException("persistenceEnum");
            }
        }

        public static string ToProtocolString(this ReturnTypeEnum returnTypeEnum)
        {
            switch (returnTypeEnum)
            {
                case ReturnTypeEnum.Direct:
                    return "direct";
                case ReturnTypeEnum.Disk:
                    return "disk";
                case ReturnTypeEnum.None:
                    return "none";
                default:
                    throw new ArgumentOutOfRangeException("returnTypeEnum");
            }
        }

        public static string ToProtocolString(this UploadFromEnum uploadFromEnum)
        {
            switch (uploadFromEnum)
            {
                case UploadFromEnum.Direct:
                    return "direct";
                case UploadFromEnum.Disk:
                    return "disk";
                case UploadFromEnum.Redirect:
                    return "redirect";
                default:
                    throw new ArgumentOutOfRangeException("uploadFromEnum");
            }
        }

        public static string ToProtocolString(this OfficialSourceTypeEnum officialSourceType)
        {
            switch (officialSourceType)
            {
                case OfficialSourceTypeEnum.Freenet:
                    return "freenet";
                case OfficialSourceTypeEnum.Https:
                    return "https";
                default:
                    throw new ArgumentOutOfRangeException("officialSourceType");
            }
        }

        public static string ToProtocolString(this UrlTypeEnum urlType)
        {
            switch (urlType)
            {
                case UrlTypeEnum.Official:
                    return "official";
                case UrlTypeEnum.File:
                    return "file";
                case UrlTypeEnum.Freenet:
                    return "freenet";
                case UrlTypeEnum.Url:
                    return "url";
                default:
                    throw new ArgumentOutOfRangeException("urlType");
            }
        }

    }
}
