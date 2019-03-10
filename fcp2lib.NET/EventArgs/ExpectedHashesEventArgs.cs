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

    public class ExpectedHashesEventArgs : System.EventArgs
    {
        public string Identifier { get; }
        public bool Global { get; }
        public HashesType Hashes { get; }

        internal ExpectedHashesEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            Identifier = parsed.Identifier;
            Global = parsed.Global;
            Hashes = new HashesType(parsed.Hashes);

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }

        #region Nested type: HashesType
        public class HashesType
        {
            public string SHA512 { get; }
            public string SHA256 { get; }
            public string MD5 { get; }
            public string SHA1 { get; }
            public string TTH { get; }
            public string ED2K { get; }

            public HashesType(dynamic hashes)
            {
                SHA512 = hashes.SHA512;
                SHA256 = hashes.SHA256;
                MD5 = hashes.MD5;
                SHA1 = hashes.SHA1;
                TTH = hashes.TTH;
                ED2K = hashes.ED2K;
            }
        }


        #endregion
    }
}
