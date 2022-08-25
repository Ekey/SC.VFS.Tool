using System;

namespace SC.Unpacker
{
    class VfsHash
    {
        public static UInt32 iGetHash(String m_String)
        {
            UInt32 dwHash = 0;

            for (Int32 i = 0; i < m_String.Length; i++)
            {
                dwHash = (dwHash + ((Byte)m_String[i])) * 1025;
                dwHash = dwHash ^ (dwHash >> 6);
            }

            dwHash *= 9;
            dwHash = ((dwHash >> 11) ^ dwHash) * 32769;

            return dwHash;
        }
    }
}
