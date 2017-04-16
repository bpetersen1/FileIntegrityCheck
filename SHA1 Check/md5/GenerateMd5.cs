using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SHA1_Check.md5
{
    public class GenerateMd5
    {
        private readonly string _path;

        public GenerateMd5(string path)
        {
            _path = path;
        }

        public async Task<string> GenerateFromFileAsync()
        {
            string result = string.Empty;
            await Task.Run(() =>
            {
               using (MD5 md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(_path))
                    {
                        result = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-",string.Empty);
                    }
                }
            });
            return result;
        }
    }
}
