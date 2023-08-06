namespace Shared.Models.Models
{
    public class Result
    {
        internal protected Result(bool succeeded, IEnumerable<string> errors )
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }
        internal protected Result(bool succeeded , Error error)
        {
            Succeeded = succeeded;
            Error = error;
        }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; } = new string[] { };
        public Error? Error { get; set; }
        public bool IsFailure => Succeeded == false;
        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }
        public static Result<T> Success<T>(T? value) => new Result<T>(value, true ,Error.None);

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
        public static Result Failure(Error error)
        {
            return new Result(false, error);
        }
        public static Result<T> Failure<T>(Error error)
        {
            return new Result<T>(default(T),false, error);
        }
          public static Result<T> Failure<T>(List<string> errors)
        {
            return new Result<T>(default(T),false, errors);
        }

        public static Result<bool> Success<T>()
        {
            throw new NotImplementedException();
        }
    }
}
