using System;
using SQLite;

namespace StudentMonitoringSystem.Model
{
    public class Student
    {
        #region Data members
        private string _studentName;
        private string _fatherName;
        private string _cellNo;
        private string _studentRollNumber;
        private string _studentId;
        private string _classId;
        private string _className;
        private int _id;
        private bool _isSelected;
        #endregion
        #region Properties
        [AutoIncrement, PrimaryKey]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }
        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }
        public string StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }
        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value; }
        }
        public string CellNo
        {
            get { return _cellNo; }
            set { _cellNo = value; }
        }
        public string StudentRollNumber
        {
            get { return _studentRollNumber; }
            set { _studentRollNumber = value; }
        }
        public string ClassId
        {
            get { return _classId; }
            set { _classId = value; }
        }
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        #endregion
    
       
    }
}