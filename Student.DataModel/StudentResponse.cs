using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class StudentResponse
    {
        public bool IsArchived { get; set; }

        public Student Student { get; set; }
    }
}
