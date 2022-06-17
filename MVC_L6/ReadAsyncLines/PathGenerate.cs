using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAsyncLines
{
    public class PathManager
    {
       private List<string> files;
       private List<string> folders;

        public PathManager(string path)
        {
            files = Directory.GetFiles(path).ToList();
            folders = Directory.GetDirectories(path).ToList();
            foreach(var dir in folders)
            {
               List<string> direct = Directory.GetFiles(dir).ToList();
               foreach(var f in direct)
                {
                    files.Add(f);
                }
            }
        }

        public List<string> GetAllFiles()
        {
            return files;
        }
       
     
    }
}       


