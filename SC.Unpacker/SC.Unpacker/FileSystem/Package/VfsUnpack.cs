using System;
using System.IO;
using System.Collections.Generic;

namespace SC.Unpacker
{
    class VfsUnpack
    {
        private static List<VfsEntry> m_EntryTable = new List<VfsEntry>();

        public static void iDoIt(String m_Archive, String m_DstFolder)
        {
            VfsHashList.iLoadProject();

            using (FileStream TVfsStream = File.OpenRead(m_Archive))
            {
                var m_Header = new VfsHeader();

                m_Header.dwMagic = TVfsStream.ReadUInt32();
                m_Header.dwTotalFiles = TVfsStream.ReadInt32();

                if (m_Header.dwMagic != 0xF11350D)
                {
                    throw new Exception("[ERROR]: Invalid magic of VFS archive file!");
                }

                m_EntryTable.Clear();
                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    var m_Entry = new VfsEntry();

                    m_Entry.dwNameHash = TVfsStream.ReadUInt32();
                    m_Entry.dwSize = TVfsStream.ReadInt32();
                    m_Entry.dwOffset = TVfsStream.ReadUInt32();

                    m_EntryTable.Add(m_Entry);
                }

                foreach (var m_Entry in m_EntryTable)
                {
                    String m_FileName = VfsHashList.iGetNameFromHashList(m_Entry.dwNameHash).Replace("/", @"\");
                    String m_FullPath = m_DstFolder + m_FileName;

                    Utils.iSetInfo("[UNPACKING]: " + m_FileName);
                    Utils.iCreateDirectory(m_FullPath);

                    TVfsStream.Seek(m_Entry.dwOffset, SeekOrigin.Begin);
                    var lpBuffer = TVfsStream.ReadBytes(m_Entry.dwSize);

                    File.WriteAllBytes(m_FullPath, lpBuffer);
                }

                TVfsStream.Dispose();
            }
        }
    }
}
