using Cakmak.Yapi.Core.Repository;
using Cakmak.Yapi.Entity.ProjectUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cakmak.Yapi.Repository.ProjectUser
{
    public class UserRepository : BaseRepository<Cakmak.Yapi.Entity.ProjectUser.User>
    {
        public UserRepository(string connectionString, string database, string collection) : base(connectionString, database, collection)
        {
        }
    }
}
