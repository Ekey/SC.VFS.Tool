using System;
using System.IO;

namespace SC.Packer
{
    class Program
    {
        private static String m_Title = "Sacred Citadel VFS Packer";

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
                Console.WriteLine("    SC.Packer <m_File> <m_Directory>\n");
                Console.WriteLine("    m_File - Destination VFS archive file");
                Console.WriteLine("    m_Directory - Source directory\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    SC.Packer D:\\NEW.vfs D:\\Unpacked");
                Console.ResetColor();
                return;
            }

            String m_VfsFile = args[0];
            String m_Input = Utils.iCheckArgumentsPath(args[1]);

            if (!Directory.Exists(m_Input))
            {
                Utils.iSetError("[ERROR]: Input directory -> " + m_Input + " <- does not exist");
                return;
            }

            VfsPack.iDoIt(m_VfsFile, m_Input);
        }
    }
}
