using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SHA1_Check.sha1
{
    public class GenerateSha1
    {
        private readonly string _path;

        public GenerateSha1(string path)
        {
            _path = path;
        }

        public async Task<string> GenerateFromFileAsync()
        {
            string result = string.Empty;
            await Task.Run(() =>
            {
                using (FileStream fs = new FileStream(_path, FileMode.Open))
                using (BufferedStream bs = new BufferedStream(fs))
                {
                   using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        byte[] hash = sha1.ComputeHash(bs);
                        StringBuilder formatted = new StringBuilder(2 * hash.Length);

                        foreach (byte b in hash)
                        {
                            formatted.AppendFormat("{0:X2}", b);
                        }

                        result =  formatted.ToString();
                    }
                }

            });
            return result;
        }
        
    }
}