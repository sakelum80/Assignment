using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;


namespace Repository
{
    public interface IStudentRepo
    {    
       Student GetStudent(int studentId, bool isStudentArchived);
      
    }
} 
