namespace Intsoft.Exam.Domain.Entities
{
    public abstract class BaseEntitiy
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
