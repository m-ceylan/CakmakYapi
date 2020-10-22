using Cakmak.Yapi.Core.Repository;
using Cakmak.Yapi.Entity.Definition;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Repository.Definition
{
    public class WorkRepository : BaseRepository<Work>
    {
        public WorkRepository(string connectionString, string database, string collection) : base(connectionString, database, collection)
        {
        }
    }
}
