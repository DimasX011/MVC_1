using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReadAsyncLines
{
    public class ReadAsync
    {   
        public async Task readStrings(params string[] strings)
        {
            foreach(var c in strings)
            {
                using (FileStream fstream = File.OpenRead(c))
                {
                    byte[] buffer = new byte[fstream.Length];
                    await fstream.ReadAsync(buffer, 0, buffer.Length);
                    string textFromFile = Encoding.Default.GetString(buffer);
                    Console.WriteLine($"Текст из файла: {textFromFile}");
                }
            }
        }
    }
}
