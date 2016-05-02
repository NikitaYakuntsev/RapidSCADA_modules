using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDTO
{
    public interface IConvertable<F, T>
    {
        T Convert(F from);
    }
}
