namespace SchoolManagment.Model
{
    public class Teacher
    {
        public int teacherid { get; set; }
        public int schoolid { get; set; }
        public string? teachername { get; set; }
        public string? teachermobile { get; set; }
        public string? teacheremail { get; set; }
        public string? teacheraddress { get; set; }
        public DateTime teacherjoindate { get; set; }
        public string? teachersubject { get; set; }
        public bool isdeleted { get; set; }
    }
    public class ViewTeacher
    {
         public int SrNo { get; set; }
        public int teacherid { get; set; }
        public int schoolid { get; set; }
        public string? teachername { get; set; }
        public string? teachermobile { get; set; }
        public string? teacheremail { get; set; }
        public string? teacheraddress { get; set; }
        public DateTime teacherjoindate { get; set; }
        public string? teachersubject { get; set; }
    }
}
//teacherid, schoolid, teachername, teachermobile ,teacheremail ,teacheraddress ,teacherjoindate ,teachersubject,isdeleted