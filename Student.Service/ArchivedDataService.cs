using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Repository;

namespace Service
{
    public class ArchivedDataService : IFailoverRepository
    {
        public List<FailoverEntry> GetFailOverEntries()
        {
            throw new NotImplementedException();
        }
    }
}
