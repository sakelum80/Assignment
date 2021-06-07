using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Repository;

namespace Service
{
    public class FailoverStudentDataAccess : IFailoverStudent
    {
        public StudentResponse GetStudentById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
