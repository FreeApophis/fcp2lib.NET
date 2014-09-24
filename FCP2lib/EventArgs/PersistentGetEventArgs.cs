/*
 *  The FCP2.0 Library, complete access to freenets FCP 2.0 Interface
 * 
 *  Copyright (c) 2009-2014 Thomas Bruderer <apophis@apophis.ch>
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

namespace FCP2
{

    public class PersistentGetEventArgs : System.EventArgs
    {
        readonly string clientToken;
        readonly string filename;
        readonly bool global;
        readonly long maxRetries;
        readonly PersistenceEnum persistenceType;
        readonly PriorityClassEnum priorityClass;
        readonly ReturnTypeEnum returnType;
        readonly string tempFilename;
        readonly string uri;
        readonly VerbosityEnum verbosity;

        /// <summary>
        /// PersistentGetEventArgs Constructor
        /// </summary>
        /// <param name="parsed">a simple MessageParse</param>
        internal PersistentGetEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif

            uri = parsed.URI;
            verbosity = parsed.Verbosity;
            returnType = parsed.ReturnType;
            filename = parsed.Filename;
            tempFilename = parsed.TempFilename;
            clientToken = parsed.ClientToken;
            priorityClass = parsed.PriorityClass;
            persistenceType = parsed.PersistenceType;
            global = parsed.Global;
            maxRetries = parsed.MaxRetries;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string URI
        {
            get { return uri; }
        }

        public VerbosityEnum Verbosity
        {
            get { return verbosity; }
        }

        public ReturnTypeEnum ReturnType
        {
            get { return returnType; }
        }

        public string Filename
        {
            get { return filename; }
        }

        public string TempFilename
        {
            get { return tempFilename; }
        }

        public string ClientToken
        {
            get { return clientToken; }
        }

        public PriorityClassEnum PriorityClass
        {
            get { return priorityClass; }
        }

        public PersistenceEnum PersistenceType
        {
            get { return persistenceType; }
        }

        public bool Global
        {
            get { return global; }
        }

        public long MaxRetries
        {
            get { return maxRetries; }
        }
    }
}