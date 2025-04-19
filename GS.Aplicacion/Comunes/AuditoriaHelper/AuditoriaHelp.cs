using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Comunes.AuditoriaHelper
{
    public class AuditoriaHelp : IAuditoriaHelp
    {
        private string _userName = "Unknown";
        public string UserName => _userName;

        public void SetUserName(string userName)
        {
            _userName = userName;
        }
    }
}
