namespace SchoolManagment.Model
{
    public class Sports
    {
        public int sportid { get; set; }
        public string? sportname { get; set; }
        public bool isdeleted { get; set; }
        public List<Student>? students { get; set; }
    }
}
//sportid sportname isdeleted