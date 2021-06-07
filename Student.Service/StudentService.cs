using System;
using System.Configuration; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Repository;

namespace Service
{
    public class StudentService : IStudentRepo
    {
        //Added new code on 07/06/21 
        private readonly IStudentRepo _studentRepo;
        private readonly IArchivedStudentRepo _archivedStudentRepo;
        private readonly IFailoverRepository _failoverRepository;
        private readonly IFailoverStudent _failoverStudent;

        public StudentService (IStudentRepo studentRepo, IArchivedStudentRepo archivedStudentRepo, IFailoverRepository failoverRepository, IFailoverStudent failoverStudent)
        {
            _studentRepo = studentRepo;
            _archivedStudentRepo = archivedStudentRepo;
            _failoverRepository = failoverRepository;
            _failoverStudent = failoverStudent;
        }

        //Changes ends here

        public Student GetStudent(int studentId, bool isStudentArchived)
            {

            Student archivedStudent = null;

            if (isStudentArchived)
            {
                //Added new code on 07/06/21 
                archivedStudent = _archivedStudentRepo.GetArchivedStudent(studentId);
                
                //Commented below lines of code and repaced with above line of code
               // var archivedDataService = new ArchivedDataService();
               //archivedStudent = archivedDataService.GetArchivedStudent(studentId);
               //Changes ends here

                return archivedStudent;
            }
            else
            {
                //Added new code on 07/06/21 
                var failoverEntries = _failoverRepository.GetFailOverEntries();

                //Commented below lines of code and repaced with above line of code
                //var failoverRespository = new FailoverRepository();
                //var failoverEntries = failoverRespository.GetFailOverEntries();
                //Changes ends here

                var failedRequests = 0;

                foreach (var failoverEntry in failoverEntries)
                {
                    if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                    {
                        failedRequests++;
                    }
                }

                StudentResponse studentResponse = null;
                Student student = null;

                if (failedRequests > 100 && (ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "true" || ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "True"))
                {
                    //Added new code on 07/06/21 
                    studentResponse = _failoverStudent.GetStudentById(studentId);

                    //Commented below lines of code and repaced with above line of code
                    // studentResponse = FailoverStudentDataAccess.GetStudentById(studentId);
                    //Changes ends here
                }
                else
                {
                    var dataAccess = new StudentDataAccess();
                    studentResponse = dataAccess.LoadStudent(studentId);


                }

                if (studentResponse.IsArchived)
                {
                    //Added new code on 07/06/21 
                    student = _archivedStudentRepo.GetArchivedStudent(studentId);

                    //Commented below lines of code and repaced with above line of code
                    //var archivedDataService = new ArchivedDataService();
                    //student = archivedDataService.GetArchivedStudent(studentId);
                    //Changes ends here

                }
                else
                {
                    student = studentResponse.Student;
                }
                return student;
            }
            }        
    }
}
