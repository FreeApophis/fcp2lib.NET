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

using System.IO;

namespace FCP2.Protocol
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
