using System.IO;

namespace Freenet.FCP2
{
    /// <summary>
    /// Description of InsertItem.
    /// </summary>
    public abstract class InsertItem
    {
        private readonly string name;

        protected InsertItem(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }
    }

    public class DataItem : InsertItem
    {
        private readonly string contentType;
        private readonly Stream data;

        public DataItem(string name, Stream data, string contentType)
            : base(name)
        {
            this.data = data;
            this.contentType = contentType;
        }

        public Stream Data
        {
            get { return data; }
        }

        public string ContentType
        {
            get { return contentType; }
        }
    }

    public class FileItem : InsertItem
    {
        private readonly string contentType;
        private readonly string filename;

        public FileItem(string name, string filename, string contentType)
            : base(name)
        {
            this.filename = filename;
            this.contentType = contentType;
        }

        public string Filename
        {
            get { return filename; }
        }

        public string ContentType
        {
            get { return contentType; }
        }
    }

    public class RedirectItem : InsertItem
    {
        private readonly string targetURI;

        public RedirectItem(string name, string targetURI)
            : base(name)
        {
            this.targetURI = targetURI;
        }

        public string TargetURI
        {
            get { return targetURI; }
        }
    }
}
