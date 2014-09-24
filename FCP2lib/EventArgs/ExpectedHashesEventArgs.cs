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

    public class ExpectedHashesEventArgs : System.EventArgs
    {
        readonly string identifier;
        readonly bool global;
        readonly HashesType hashes;


        internal ExpectedHashesEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.Identifier;
            global = parsed.Global;
            hashes = new HashesType(parsed.Hashes);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        public string Identifier
        {
            get
            {
                return identifier;
            }
        }

        public bool Global
        {
            get
            {
                return global;
            }
        }


        public HashesType Hashes
        {
            get
            {
                return hashes;
            }
        }

        #region Nested type: HashesType
        public class HashesType
        {
            readonly string sha512;
            readonly string sha256;
            readonly string md5;
            readonly string sha1;
            readonly string tth;
            readonly string ed2k;

            public HashesType(dynamic hashes)
            {
                sha512 = hashes.SHA512;
                sha256 = hashes.SHA256;
                md5 = hashes.MD5;
                sha1 = hashes.SHA1;
                tth = hashes.TTH;
                ed2k = hashes.ED2K;
            }

            public string SHA512
            {
                get
                {
                    return sha512;
                }
            }
            public string SHA256
            {
                get
                {
                    return sha256;
                }
            }
            public string MD5
            {
                get
                {
                    return md5;
                }
            }
            public string SHA1
            {
                get
                {
                    return sha1;
                }
            }
            public string TTH
            {
                get
                {
                    return tth;
                }
            }
            public string ED2K
            {
                get
                {
                    return ed2k;
                }
            }
        }


        #endregion
    }
}
