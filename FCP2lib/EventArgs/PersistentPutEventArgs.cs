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

using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class PersistentPutEventArgs : System.EventArgs
    {
        public string URI { get; }
        public VerbosityEnum Verbosity { get; }
        public PriorityClassEnum PriorityClass { get; }
        public UploadFromEnum UploadFrom { get; }
        public string Filename { get; }
        public string TargetFilename { get; }
        public MetadataType Metadata { get; }
        public string ClientToken { get; }
        public bool Global { get; }
        public long DataLength { get; }
        public long MaxRetries { get; }

        /// <summary>
        /// PersistentPutEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentPutEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            URI = parsed.URI;
            Verbosity = parsed.Verbosity;
            PriorityClass = parsed.PriorityClass;
            UploadFrom =parsed.UploadFrom;
            Filename = parsed.Filename;
            TargetFilename = parsed.TargetFilename;
            Metadata = new MetadataType(parsed.Metadata);
            ClientToken = parsed.ClientToken;
            Global = parsed.Global;
            DataLength = parsed.DataLength;
            MaxRetries = parsed.MaxRetries;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        #region Nested type: MetadataType

        public class MetadataType
        {
            public string ContentType { get; }

            internal MetadataType(dynamic metadata)
            {
                ContentType = metadata.ContentType;
            }
        }

        #endregion
    }
}