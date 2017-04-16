using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SHA1_Check.Properties;

namespace SHA1_Check.Common
{
    public class SupportedCryptography
    {
        public List<string> GetSupportedCryptography()
        {
            return Resources.SupportedCryptography.Split(',').ToList();
        }
    }
}
