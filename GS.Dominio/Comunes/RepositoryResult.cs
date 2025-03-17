using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Dominio.Comunes
{
    public abstract class RepositoryResult
    {
        public int StatusCode { set; get; } = 0;
        public int ErrorCode { set; get; } = 0;
        public string ErrorMessage { set; get; } = "";
        public string StatusMessage { set; get; } = "Correcto";
        public string StatusType { set; get; }
    }
}
