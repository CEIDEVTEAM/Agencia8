using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace ETLProcess.Services.Interfaces
{
    public interface IProcessData
    {
        void Execute(ExcelApp.Range excelRange, IDapper dapper, ILogger logger);
    }
}
