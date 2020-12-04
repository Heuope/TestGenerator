using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestGeneratorLib
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

        public async Task WriteFileAsync(string destination, List<TestFile> files)
        {
            foreach (var file in files)
            {
                using (var writer = new StreamWriter($@"{destination}\{file.FileName}.cs"))
                {
                    await writer.WriteAsync(file.FileCode);
                }
            }
        }
    }
}
