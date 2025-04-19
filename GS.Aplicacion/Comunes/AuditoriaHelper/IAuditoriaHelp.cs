using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Comunes.AuditoriaHelper
{
    public interface IAuditoriaHelp
    {
        string UserName { get; }
        public void SetUserName(string userName);
    }
}
