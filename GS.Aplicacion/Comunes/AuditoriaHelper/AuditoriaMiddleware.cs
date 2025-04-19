using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Aplicacion.Comunes.AuditoriaHelper
{
    public class AuditoriaMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtService _jwtService;

        public AuditoriaMiddleware(RequestDelegate next, IJwtService jwtService)
        {
            _next = next;
            _jwtService = jwtService;
        }

        public async Task Invoke(HttpContext context, IAuditoriaHelp currentUserService)
        {
            var jwtToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(jwtToken))
            {
                var userName = _jwtService.GetUserFromToken(jwtToken);
                currentUserService.SetUserName(userName);
            }

            await _next(context);
        }
    }
}
