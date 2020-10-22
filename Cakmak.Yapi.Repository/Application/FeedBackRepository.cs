using Cakmak.Yapi.Core.Repository;
using Cakmak.Yapi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Repository.Application
{
    public class FeedBackRepository : BaseRepository<FeedBack>
    {
        public FeedBackRepository(string connectionString, string database, string collection) : base(connectionString, database, collection)
        {
        }
    }
}
