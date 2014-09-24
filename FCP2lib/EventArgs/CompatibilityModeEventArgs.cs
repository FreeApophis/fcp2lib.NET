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

    public class CompatibilityModeEventArgs : System.EventArgs
    {
        readonly string identifier;
        readonly string global;
        readonly string min;
        readonly string minNumber;
        readonly string max;
        readonly string maxNumber;
        readonly string splitfileCryptoKey;
        readonly string dontCompress;
        readonly string definitive;

        internal CompatibilityModeEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            identifier = parsed.Identifier;
            global = parsed.Global;
            min = parsed.Min;
            minNumber = parsed.MinNumber;
            max = parsed.Max;
            maxNumber = parsed.MaxNumber;
            splitfileCryptoKey = parsed.SplitfileCryptoKey;
            dontCompress = parsed.DontCompress;
            definitive = parsed.Definitive;

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

        public string Global
        {
            get
            {
                return global;
            }
        }
        public string Min
        {
            get
            {
                return min;
            }
        }
        public string MinNumber
        {
            get
            {
                return minNumber;
            }
        }
        public string Max
        {
            get
            {
                return max;
            }
        }
        public string MaxNumber
        {
            get
            {
                return maxNumber;
            }
        }
        public string SplitfileCryptoKey
        {
            get
            {
                return splitfileCryptoKey;
            }
        }
        public string DontCompress
        {
            get
            {
                return dontCompress;
            }
        }
        public string Definitive
        {
            get
            {
                return definitive;
            }
        }
    }
}
