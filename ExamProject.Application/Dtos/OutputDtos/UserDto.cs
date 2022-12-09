namespace ExamProject.Application.Dtos.OutputDtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }
        public string LastName { get; set; }
        
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
