using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TakePromoCodes
{
    public interface IPromoCodesProvider
    {
        Task<PromoCodes> ReadPromoCodesAsync(String path);
    }
}
