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
namespace FCP2.EventArgs
{

    public class GetFailedEventArgs : System.EventArgs
    {
        private readonly long code;
        private readonly string codeDescription;
        private readonly long expectedDataLength;
        private readonly ExpectedMetadataType expectedMetadata;
        private readonly string extraDescription;
        private readonly bool fatal;
        private readonly bool finalizedExpected;
        private readonly bool global;
        private readonly string identifier;
        private readonly string redirectURI;
        private readonly string shortCodeDescription;

        /// <summary>
        /// GetFailedEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal GetFailedEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            global = parsed.Global;
            code = parsed.Code;
            codeDescription = parsed.CodeDescription;
            extraDescription = parsed.ExtraDescription;
            fatal = parsed.Fatal;
            shortCodeDescription = parsed.ShortCodeDescription;
            identifier = parsed.Identifier;
            expectedDataLength = parsed.ExpectedDataLength;
            expectedMetadata = new ExpectedMetadataType(parsed.ExpectedMetadata);
            finalizedExpected = parsed.FinalizedExpected;
            redirectURI = parsed.RedirectURI;

            /* TODO: Complex Get Failed */

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public bool Global
        {
            get { return global; }
        }

        public long Code
        {
            get { return code; }
        }

        public string CodeDescription
        {
            get { return codeDescription; }
        }

        public string ExtraDescription
        {
            get { return extraDescription; }
        }

        public bool Fatal
        {
            get { return fatal; }
        }

        public string ShortCodeDescription
        {
            get { return shortCodeDescription; }
        }

        public string Identifier
        {
            get { return identifier; }
        }

        public long ExpectedDataLength
        {
            get { return expectedDataLength; }
        }

        public ExpectedMetadataType ExpectedMetadata
        {
            get { return expectedMetadata; }
        }

        public bool FinalizedExpected
        {
            get { return finalizedExpected; }
        }

        public string RedirectURI
        {
            get { return redirectURI; }
        }

        #region Nested type: ExpectedMetadataType

        public class ExpectedMetadataType
        {

            private readonly string contentType;

            internal ExpectedMetadataType(dynamic expectedMetadata)
            {
                contentType = expectedMetadata.ContentType;
            }

            public string ContentType
            {
                get { return contentType; }
            }
        }

        #endregion
    }
}