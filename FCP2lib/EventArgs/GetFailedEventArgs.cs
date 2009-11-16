/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009 Thomas Bruderer <apophis@apophis.ch>
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

namespace Freenet.FCP2
{

    public class GetFailedEventArgs : EventArgs
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
        internal GetFailedEventArgs(MessageParser parsed)
        {
#if DEBUG
            FCP2.ArgsDebug(this, parsed);
#endif
            if (parsed["Global"] != null)
                global = bool.Parse(parsed["Global"]);
            code = long.Parse(parsed["Code"]);
            codeDescription = parsed["CodeDescription"];
            extraDescription = parsed["ExtraDescription"];
            if (parsed["Fatal"] != null)
                fatal = bool.Parse(parsed["Fatal"]);
            shortCodeDescription = parsed["ShortCodeDescription"];
            identifier = parsed["Identifier"];
            if (parsed["ExpectedDataLength"] != null)
                expectedDataLength = long.Parse(parsed["ExpectedDataLength"]);
            expectedMetadata = new ExpectedMetadataType(parsed);
            if (parsed["FinalizedExpected"] != null)
                finalizedExpected = bool.Parse(parsed["FinalizedExpected"]);
            redirectURI = parsed["RedirectURI"];

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

            internal ExpectedMetadataType(MessageParser parsed)
            {
                contentType = parsed["ExpectedMetadata.ContentType"];
            }

            public string ContentType
            {
                get { return contentType; }
            }
        }

        #endregion
    }
}