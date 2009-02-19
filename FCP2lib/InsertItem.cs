/*
 * Erstellt mit SharpDevelop.
 * Benutzer: apophis
 * Datum: 19.02.2009
 * Zeit: 11:29
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */

using System;
using System.IO;

namespace Freenet.FCP2 
{
    /// <summary>
    /// Description of InsertItem.
    /// </summary>
    public abstract class InsertItem
    {
        private string name;
        
        public string Name {
            get { return name; }
        }
        
        public InsertItem(string name)
        {
            this.name = name;
        }
    }
    
    public class DataItem : InsertItem 
    {
        private Stream data;
        
        public Stream Data {
            get { return data; }
        }
        
        private string contentType;
        
        public string ContentType {
            get { return contentType; }
        }
        
        public DataItem(string name, Stream data, string contentType) : base(name) {
            this.data = data;
            this.contentType = contentType;
        }
    }

    public class FileItem : InsertItem 
    {
        private string filename;
        
        public string Filename {
            get { return filename; }
        }
        
        private string contentType;
        
        public string ContentType {
            get { return contentType; }
        }
        
        public FileItem(string name, string filename, string contentType) : base(name) {
            this.filename = filename;
            this.contentType = contentType;
        }
    }

    public class RedirectItem : InsertItem 
    {
        private string targetURI;
        
        public string TargetURI {
            get { return targetURI; }
        }

        public RedirectItem(string name, string targetURI) : base(name) {
            this.targetURI = targetURI;
        }        
    }
}
