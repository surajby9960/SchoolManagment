namespace SchoolManagment.Model
{
    public class InsertClass
    {
       // public int classid { get; set; }
        public string? classstd { get; set; }
        public int classfess { get; set; }
        public int schoolid { get; set; }
        public int teacherid { get; set; }
        public DateTime createddate { get; set; }
        public DateTime modifydate { get; set; }
        public bool isdeleted { get; set; }
        public List<Student>? students { get; set; }
    }
    public class UpdateClass
    {
        public int classid { get; set; }
        public string? classstd { get; set; }
        public int classfess { get; set; }
        public int schoolid { get; set; }
        public int teacherid { get; set; }
        public DateTime createddate { get; set; }
        public DateTime modifydate { get; set; }
        public bool isdeleted { get; set; }
        public List<Student>? students { get; set; }
    }

}
//classid classstd classfess schoolid teacherid createddate 