using System;

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
