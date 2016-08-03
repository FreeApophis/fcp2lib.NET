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

    public class CompatibilityModeEventArgs : System.EventArgs
    {
        public string Identifier { get; }
        public string Global { get; }
        public string Min { get; }
        public string MinNumber { get; }
        public string Max { get; }
        public string MaxNumber { get; }
        public string SplitfileCryptoKey { get; }
        public string DontCompress { get; }
        public string Definitive { get; }

        internal CompatibilityModeEventArgs(dynamic parsed)
        {
#if DEBUG
            FCP2Protocol.ArgsDebug(this, parsed);
#endif
            Identifier = parsed.Identifier;
            Global = parsed.Global;
            Min = parsed.Min;
            MinNumber = parsed.MinNumber;
            Max = parsed.Max;
            MaxNumber = parsed.MaxNumber;
            SplitfileCryptoKey = parsed.SplitfileCryptoKey;
            DontCompress = parsed.DontCompress;
            Definitive = parsed.Definitive;

#if DEBUG
            parsed.PrintAccessCount();
#endif
        }
    }
}
