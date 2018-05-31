using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileNameOutput
{
    class Program
    {
        static string rootFolderPath;
        static string outputFolderPath;

        static string resultStr;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Root Folder Path");
            rootFolderPath = Console.ReadLine();

            Console.WriteLine("Enter Output Folder Path");
            outputFolderPath = Console.ReadLine();

            resultStr = "+- ["+getFile(rootFolderPath)+"]";

            getFileTree(rootFolderPath);

            Console.Write(resultStr);

            StreamWriter sw = new StreamWriter(outputFolderPath + "\\FileTreeOutput.txt",false,Encoding.UTF8);
            sw.Write(resultStr);
            sw.Close();

            return;
        }

        static void getFileTree(string root,string ex = "| +-")
        {
            string[] subFolders = Directory.GetDirectories(root, "*", SearchOption.TopDirectoryOnly);
            string[] files = Directory.GetFiles(root, "*", SearchOption.TopDirectoryOnly);

            List<string> children = new List<string>();
            children.AddRange(subFolders);
            children.AddRange(files);

            for(int i = 0; i < children.Count(); ++i)
            {
                string prefix = ex;
                string path = children[i];

                bool isDir = File.GetAttributes(path).Equals(FileAttributes.Directory);

                if(isDir)
                {
                    resultStr += System.Environment.NewLine + prefix + " [" + getFile(path) + "]";
                    prefix = "| " + prefix;

                    getFileTree(path, prefix);
                }
                else
                {
                    resultStr += System.Environment.NewLine + ex + " [" + getFile(path) + "]";
                }
            }
        }

        static string getFile(string path)
        {
            string[] strs = path.Split("\\"[0]);

            if(path.EndsWith("\\"))
            {
                return strs[strs.Length - 2];
            }

            return strs[strs.Length - 1];
        }
    }
}
