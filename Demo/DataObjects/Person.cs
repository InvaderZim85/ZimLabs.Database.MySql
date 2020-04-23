namespace Demo.DataObjects
{
    internal sealed class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
