/*
 *  The FCP2.0 Library, complete access to freenet's FCP 2.0 Interface
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

using System.IO;

namespace FCP2.Protocol
{
    /// <summary>
    /// Description of InsertItem.
    /// </summary>
    public abstract class InsertItem
    {
        public string Name { get; }

        protected InsertItem(string name)
        {
            Name = name;
        }
    }

    public class DataItem : InsertItem
    {
        public DataItem(string name, Stream data, string contentType)
            : base(name)
        {
            Data = data;
            ContentType = contentType;
        }

        public Stream Data { get; }

        public string ContentType { get; }
    }

    public class FileItem : InsertItem
    {
        public string Filename { get; }
        public string ContentType { get; }

        public FileItem(string name, string filename, string contentType)
            : base(name)
        {
            Filename = filename;
            ContentType = contentType;
        }
    }

    public class RedirectItem : InsertItem
    {
        public string TargetURI { get; }

        public RedirectItem(string name, string targetURI)
            : base(name)
        {
            TargetURI = targetURI;
        }
    }
}
