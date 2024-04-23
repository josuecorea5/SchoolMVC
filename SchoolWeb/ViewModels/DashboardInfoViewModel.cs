namespace SchoolWeb.ViewModels
{
    public class DashboardInfoViewModel
    {
        public int TotalGrades { get; set; }
        public int TotalStudents { get; set; }
        public int TotalStudyPlans { get; set; }
        public int TotalTeachers { get; set; }
    }

    public class EnrollmentStudentsInformation
    {
        public int GradeId { get; set; }
        public int TotalEnrollment { get; set; }
        public string GradeName { get; set; } = string.Empty;
    }
}
