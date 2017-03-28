using System.IO;
using FCP2.Keys;

namespace FCP2.Protocol
{
    public class FCP2Download
    {
        public FCP2Key Key { get; }
        public FileInfo File { get; }

        //private long _currentBlocks;
        //private long _totalBlocks;

        internal FCP2Download(FCP2Key key, FileInfo file)
        {
            Key = key;
            File = file;
        }
    }
}