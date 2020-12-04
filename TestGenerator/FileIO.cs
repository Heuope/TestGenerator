using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    class FileIO
    {
        public async Task<string> ReadFileAsync(string source)
        {
            using (var reader = new StreamReader(source))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task WriteFileAsync(string destination, List<string> files)
        {
            foreach (var file in files)
            {
                using (var writer = new StreamWriter($@"{destination}\"))
                {
                    await writer.WriteAsync(file);
                }
            }
        }
    }
}
