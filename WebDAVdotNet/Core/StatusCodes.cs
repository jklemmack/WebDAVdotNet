using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDAVdotNet
{
    public class StatusCodes
    {
        private static Dictionary<int, string> _codes;

        static StatusCodes()
        {
            _codes = new Dictionary<int, string>();
            _codes.Add(200, "HTTP/1.1 200 OK");
            _codes.Add(401, "HTTP/1.1 401 Unauthorized");
            _codes.Add(403, "HTTP/1.1 403 Forbidden");
            _codes.Add(404, "HTTP/1.1 404 Not Found");
        }

        public static string GetHttpCode(int code)
        {
            return _codes[code];
        }
    }
}
