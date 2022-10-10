namespace SchoolManagment.Model
{
    public class School
    {
        public int schoolid { get; set; }
        public string? schoolname { get; set; }
        public string? schoolgrade { get; set; }
        public int? noofteacher { get; set; }
        public string? schooladdress { get; set; }
        public string? schooltelephone { get; set; }
        public string? schoolemail { get; set; }
        public string? schooltype { get; set; }
        public DateTime schoolestablisheddate { get; set; }
        public bool isdeleted { get; set; }
        public List<InsertClass>? classes { get; set; }
        public List<Teacher>? teachers { get; set; }
    }
}
//schoolid schoolname schoolgrade noofteacher schooladdress  schooltelephone schoolemail schooltype schoolestablisheddate
