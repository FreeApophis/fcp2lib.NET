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

namespace Freenet.FCP2 {
    /// <summary>
    /// Specify the type of the peer note, by code number (currently, may change in the future). 
    /// Type codes are: 1-peer private note
    /// </summary>
    public enum  PeerNoteType {
        PeerPrivateNote=1
    }    
    
    public enum Persistence {
        connection,
        reboot,
        forever
    }
    
    [Flags]
    public enum Verbosity{
        SimpleMessages = 1,
        CompressionMessages = 512
    }
    
    public enum PriorityClass {
        Maximum = 0,
        VeryHigh = 1,
        High = 2,
        Medium = 3,
        Low = 4,
        VeryLow = 5,
        NeverFinish = 6
    }
    
    public enum ReturnType {
        direct, 
        disk, 
//      chunked,
        none
    }
    
      	 
}


