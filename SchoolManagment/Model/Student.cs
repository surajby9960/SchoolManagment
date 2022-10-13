namespace SchoolManagment.Model
{
    public class Student
    {
        public int studid { get; set; }
        public string? studname { get; set; }
        public DateTime studdob { get; set; }
        public string? studaddress { get; set; }
        public string? studgender { get; set; }
        public DateTime admissiondate { get; set; }
        public int sportid { get; set; }
        public int classid { get; set; }
        public int schoolid { get; set; }
        public bool isdeleted { get; set; }

    }
}
//studid ,studname ,studdob ,studaddress ,studgender ,admissiondate, sportid, classid ,isdeleted