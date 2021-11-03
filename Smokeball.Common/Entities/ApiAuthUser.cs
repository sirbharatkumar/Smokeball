using System;
using System.Collections.Generic;
using System.Text;

namespace Smokeball.Common.Entities
{
    /// <summary>
    /// API Authenication user model
    /// </summary>
    public class ApiAuthUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
