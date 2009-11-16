using System;

namespace Freenet.FCP2
{
    /// <summary>
    /// Specify the type of the peer note, by code number (currently, may change in the future). 
    /// Type codes are: 1-peer private note
    /// </summary>
    public enum PeerNoteTypeEnum
    {
        PeerPrivateNote = 1
    }

    public enum PersistenceEnum
    {
        Connection,
        Reboot,
        Forever
    }

    [Flags]
    public enum VerbosityEnum
    {
        SimpleMessages = 1,
        CompressionMessages = 512
    }

    public enum PriorityClassEnum
    {
        Maximum = 0,
        VeryHigh = 1,
        High = 2,
        Medium = 3,
        Low = 4,
        VeryLow = 5,
        NeverFinish = 6
    }

    public enum ReturnTypeEnum
    {
        Direct,
        Disk,
        //      Chunked,
        None
    }

    public enum UploadFromEnum
    {
        Direct,
        Disk,
        Redirect
    }


}


