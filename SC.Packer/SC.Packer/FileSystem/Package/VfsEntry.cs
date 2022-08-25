using System;

namespace SC.Packer
{
    class VfsEntry
    {
        public UInt32 dwNameHash { get; set; }
        public Int32 dwSize { get; set; }
        public UInt32 dwOffset { get; set; }
    }
}
