using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SC.Packer
{
    class VfsPack
    {
        private static List<VfsEntry> m_EntryTable = new List<VfsEntry>();

        public static void iDoIt(String m_VfsFile, String m_SrcFolder)
        {
            var m_Header = new VfsHeader();

            m_Header.dwMagic = 0xF11350D;
            m_Header.dwTotalFiles = Directory.GetFiles(m_SrcFolder, "*.*", SearchOption.AllDirectories).Length;

            Byte[] lpTable = new Byte[m_Header.dwTotalFiles * 12];

            using (BinaryWriter TVfsStream = new BinaryWriter(File.Open(m_VfsFile, FileMode.Create)))
            {
                TVfsStream.Write(m_Header.dwMagic);
                TVfsStream.Write(m_Header.dwTotalFiles);
                TVfsStream.Write(lpTable);

                foreach (String m_File in Directory.GetFiles(m_SrcFolder, "*.*", SearchOption.AllDirectories))
                {
                    String m_FileName = m_File.Replace(m_SrcFolder, "").Replace(@"\", "/");

                    Utils.iSetInfo("[PACKING]: " + m_FileName);

                    var m_Entry = new VfsEntry();

                    if (!m_File.Contains("__Unknown"))
                    {
                        m_Entry.dwNameHash = VfsHash.iGetHash(m_FileName);
                    }
                    else
                    {
                        m_Entry.dwNameHash = Convert.ToUInt32(Path.GetFileNameWithoutExtension(m_FileName), 16);
                    }

                    var lpBuffer = File.ReadAllBytes(m_File);

                    m_Entry.dwOffset = (UInt32)TVfsStream.BaseStream.Position;
                    m_Entry.dwSize = lpBuffer.Length;

                    TVfsStream.Write(lpBuffer);

                    m_EntryTable.Add(m_Entry);
                }

                TVfsStream.Seek(8, SeekOrigin.Begin);

                foreach (var m_Entry in m_EntryTable)
                {
                    TVfsStream.Write(m_Entry.dwNameHash);
                    TVfsStream.Write(m_Entry.dwSize);
                    TVfsStream.Write(m_Entry.dwOffset);
                }

                TVfsStream.Dispose();
            }
        }
    }
}
