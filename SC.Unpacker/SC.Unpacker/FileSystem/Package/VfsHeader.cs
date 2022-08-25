using System;

namespace SC.Unpacker
{
    class VfsHeader
    {
        public UInt32 dwMagic { get; set; } // 0xF11350D
        public Int32 dwTotalFiles { get; set; }
    }
}
