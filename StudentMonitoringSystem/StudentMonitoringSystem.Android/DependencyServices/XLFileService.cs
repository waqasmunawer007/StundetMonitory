using System;
using System.Collections;
using System.IO;
using StudentMonitoringSystem.Database;
using StudentMonitoringSystem.Droid.DependencyServices;
using StudentMonitoringSystem.Model;
using StudentMonitoringSystem.Services;
using Syncfusion.XlsIO;
using StudentMonitoringSystem.Model;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(XLFileService))]
namespace StudentMonitoringSystem.Droid.DependencyServices
{
    public class XLFileService:IXLFile
    {
        private ArrayList RepName;
        List<Student> students = new List<Student>();
        public XLFileService()
        {
        }

        public void ReadXLFile()
        {
            RepName = new ArrayList();
            string sheetName = "Eight-Attendance";

            var assembly = typeof(MainActivity).Assembly;
            Stream stream = assembly.GetManifestResourceStream("StudentMonitoringSystem.Droid.records.xlsx");

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                stream.Position = 0;
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(stream);
                IWorksheet worksheet = workbook.Worksheets[sheetName];
                IRange usedRange = worksheet.UsedRange;
                for (int i = 1; i <= usedRange.LastRow; i++)
                {
                    Student student = new Student(); 
                    student.StudentRollNumber = worksheet[i, 1].DisplayText;
                    student.StudentName = worksheet[i, 2].DisplayText;
                    student.FatherName = worksheet[i, 3].DisplayText;
                    student.CellNo = "+92"+worksheet[i, 4].DisplayText;
                    student.ClassId = "acedd98e-a255-480b-add0-5c3988ca0826";
                    student.ClassName = "Eight";
                    student.StudentId = Guid.NewGuid().ToString();

                    students.Add(student);
                    DatabaseHelper.GetInstance().AddNewStudent(student);

                  
                }
                int count = students.Count;
                //DatabaseHelper.GetInstance().AddNewStudent(null);
            }
        }
    }
}
