namespace ExamProject.Domain.Entities.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool IsArchive { get; set; } = false;
    }
}
