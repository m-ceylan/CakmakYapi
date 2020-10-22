using Cakmak.Yapi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Entity.Application
{
    public class FeedBack:BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ip { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }



    }
}
