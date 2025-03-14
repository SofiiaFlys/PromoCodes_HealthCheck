using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TakePromoCodes
{
    public class PromoCodesProviderFromFile : IPromoCodesProvider
    {
        public async Task<PromoCodes> ReadPromoCodesAsync(String path)
        {
            List<String> codes = new List<String>();
            using (StreamReader reader = new StreamReader(path))
            {
                String line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (!line.Equals(String.Empty) && !String.IsNullOrWhiteSpace(line))
                        codes.Add(line);
                }
            }
            PromoCodes promoCodes = new PromoCodes(codes);
            return promoCodes;
        }
    }
}
