using Cakmak.Yapi.Core.Repository;
using Cakmak.Yapi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Repository.Application
{
    public class AboutRepository : BaseRepository<About>
    {
        public AboutRepository(string connectionString, string database, string collection) : base(connectionString, database, collection)
        {
        }
    }
}
