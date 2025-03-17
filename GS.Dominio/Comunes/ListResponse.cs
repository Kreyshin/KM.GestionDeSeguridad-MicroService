using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Comunes
{
    public class ListResponse<T> : RepositoryResult
    {
        public IEnumerable<T> Data { set; get; } = new List<T>();
    }
}
