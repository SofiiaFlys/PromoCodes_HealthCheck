using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TakePromoCodes
{
    class PromoCodesProviderFromAPI : IPromoCodesProvider
    {
        public async Task<PromoCodes> ReadPromoCodesAsync(string path)
        {
            String localFilePath = "downloadedPromoCodes.txt";

            bool downloadSuccess = await DownloadFileFromApiAsync(path, localFilePath);

            PromoCodes promoCodes;

            PromoCodesProviderFromFile promoCodesProviderFromFile = new PromoCodesProviderFromFile();

            if (downloadSuccess && File.Exists(localFilePath))
            {
                promoCodes = await promoCodesProviderFromFile.ReadPromoCodesAsync(localFilePath);
            }
            else
            {
                promoCodes = new PromoCodes(new List<String>());
            }

            return promoCodes;
        }

        public static async Task<bool> DownloadFileFromApiAsync(string apiUrl, string localFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    using (FileStream fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading file: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
