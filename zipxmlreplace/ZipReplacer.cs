using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zipxmlreplace
{
    public static class ZipReplacer
    {
        public static void Replace(string dir,string template,string from, string to)
        {
            var files=Directory.EnumerateFiles(dir, template);
            foreach (var file in files) {
                ReplaceOneZIP(file,from,to);
            }
            var dirs = Directory.EnumerateDirectories(dir);
            foreach (var vdir in dirs) {  
                Replace(vdir,template,from,to);
            }

        }

        private static void ReplaceOneZIP(string file, string from, string to)
        {
            Console.WriteLine(file);
            //return;
            FileStream stream = new FileStream(file, FileMode.Open);
            ZipArchive zip=new ZipArchive(stream,ZipArchiveMode.Update);
            foreach (ZipArchiveEntry entry in zip.Entries)
            {
               if (entry.Name.ToLower().EndsWith(".xml"))
                {
                    string entryname =entry.Name;
                    Console.WriteLine(entryname);
                    Stream xxx = entry.Open();
                    StreamReader reader=new StreamReader(xxx,Encoding.UTF8);
                    string line=reader.ReadToEnd();
                    reader.Close();
                    entry.Delete();
                    string newline = line.Replace(from, to);
                    ZipArchiveEntry newentry= zip.CreateEntry(entryname);
                    xxx=newentry.Open();
                    StreamWriter wrt = new StreamWriter(xxx,Encoding.UTF8);
                    wrt.Write(newline); 
                    wrt.Close();
                    break;
                }
            }
            zip.Dispose();
            stream.Close();
        }
    }
}
