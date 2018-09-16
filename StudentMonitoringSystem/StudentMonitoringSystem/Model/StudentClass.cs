using System;
namespace StudentMonitoringSystem.Model
{
    public class StudentClass
    {
		private string _className;
        private string _classId;

        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        public string ClassId
        {
            get { return _classId; }
            set { _classId = value; }
        }
    }
}
