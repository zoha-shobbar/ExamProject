namespace Intsoft.Exam.Application.Response
{
    public class ListResponse<T>
    {
        public List<T>? Result { get; set; }
        public ResponseStatus Status { get; set; }

        public static ListResponse<T> Success(List<T> result)
        {
            return new ListResponse<T> { Status = ResponseStatus.Sucsess, Result =result };
        }

        public static ListResponse<T> Failed(ResponseStatus status)
        {
            return new ListResponse<T> { Status = status, Result =null };
        }


        public static implicit operator ListResponse<T>(List<T> value)
        {
            return Success(value);
        }

        public static implicit operator ListResponse<T>(ResponseStatus value)
        {
            return Failed(value);
        }
    }
}
