using System;
using System.IO;

namespace SC.Unpacker
{
    class Program
    {
        private static String m_Title = "Sacred Citadel VFS Unpacker";

        static void Main(String[] args)
        {
            Console.Title = m_Title;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(m_Title);
            Console.WriteLine("(c) 2022 Ekey (h4x0r) / v{0}\n", Utils.iGetApplicationVersion());
            Console.ResetColor();

            if (args.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[Usage]");
                Console.WriteLine("    SC.Unpacker <m_File> <m_Directory>\n");
                Console.WriteLine("    m_File - Source of VFS archive file");
                Console.WriteLine("    m_Directory - Destination directory\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    SC.Unpacker E:\\Games\\SC\\data\\main.vfs D:\\Unpacked");
                Console.ResetColor();
                return;
            }

            String m_VfsFile = args[0];
            String m_Output = Utils.iCheckArgumentsPath(args[1]);

            if (!File.Exists(m_VfsFile))
            {
                Utils.iSetError("[ERROR]: Input VFS file -> " + m_VfsFile + " <- does not exist");
                return;
            }

            VfsUnpack.iDoIt(m_VfsFile, m_Output);
        }
    }
}
