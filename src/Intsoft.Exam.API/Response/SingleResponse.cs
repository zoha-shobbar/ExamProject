namespace Intsoft.Exam.API.Response
{
    public class SingleResponse<T>
    {
        public T? Result { get; set; }
        public ResponseStatus Status { get; set; }

        public static SingleResponse<T> Success(T result)
        {
            return new SingleResponse<T> { Status = ResponseStatus.Sucsess, Result =result };
        }

        public static SingleResponse<T> Failed(ResponseStatus status)
        {
            return new SingleResponse<T> { Status = status, Result =default(T) };
        }


        public static implicit operator SingleResponse<T>(T value)
        {
            return Success(value);
        }

        public static implicit operator SingleResponse<T>(ResponseStatus value)
        {
            return Failed(value);
        }
    }
}
