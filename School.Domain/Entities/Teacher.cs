namespace School.Domain.Entities
{
    public class Teacher : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
        public string Image {  get; set; } = string.Empty;
        public ICollection<Subject>? Subjects { get; set; }
    }
}
