using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SHA1_Check.sha256
{
    public class GenerateSha256
    {
        private readonly string _path;

        public GenerateSha256(string path)
        {
            _path = path;
        }

        public async Task<string> GenerateFromFileAsync()
        {
            string result = string.Empty;
            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(_path, FileMode.Open))
                using (BufferedStream bs = new BufferedStream(fs, 1024 * 32))
                {
                    using (SHA256Managed shat256 = new SHA256Managed())
                    {
                        byte[] hash = shat256.ComputeHash(bs);
                        result = BitConverter.ToString(hash).Replace("-", string.Empty);

                    }
                }

            });
            return result;
        }

    }
}