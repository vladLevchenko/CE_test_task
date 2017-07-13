using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IStudentOperations
    {
        void SaveStudent(IStudent student);
        void EditStudent(IStudent student);
        IStudent GetByName(string studentName);
    }
}
