using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEGEE_Project.Interfaces
{
    interface IDataErrorInfo
    {
        string Error { get; }
        string this[string columnName] { get; }
    }
}
