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

using FCP2.Protocol;

namespace FCP2.EventArgs
{

    public class GetFailedEventArgs : System.EventArgs
    {
        public bool Global { get; }
        public long Code { get; }
        public string CodeDescription { get; }
        public string ExtraDescription { get; }
        public bool Fatal { get; }
        public string ShortCodeDescription { get; }
        public string Identifier { get; }
        public long ExpectedDataLength { get; }
        public ExpectedMetadataType ExpectedMetadata { get; }
        public bool FinalizedExpected { get; }
        public string RedirectURI { get; }

        /// <summary>
        /// GetFailedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal GetFailedEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            Global = parsed.Global;
            Code = parsed.Code;
            CodeDescription = parsed.CodeDescription;
            ExtraDescription = parsed.ExtraDescription;
            Fatal = parsed.Fatal;
            ShortCodeDescription = parsed.ShortCodeDescription;
            Identifier = parsed.Identifier;
            ExpectedDataLength = parsed.ExpectedDataLength;
            ExpectedMetadata = new ExpectedMetadataType(parsed.ExpectedMetadata);
            FinalizedExpected = parsed.FinalizedExpected;
            RedirectURI = parsed.RedirectURI;

            /* TODO: Complex Get Failed */

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        #region Nested type: ExpectedMetadataType

        public class ExpectedMetadataType
        {
            public string ContentType { get; }

            internal ExpectedMetadataType(dynamic expectedMetadata)
            {
                ContentType = expectedMetadata.ContentType;
            }

        }

        #endregion
    }
}