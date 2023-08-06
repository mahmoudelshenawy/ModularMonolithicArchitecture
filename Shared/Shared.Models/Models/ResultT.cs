namespace Shared.Models.Models
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;
        protected internal Result(TValue? value , bool succeeded , Error error) : base(succeeded, error)
        {
            _value = value;
        }
        protected internal Result(TValue? value , bool succeeded , List<string> errors) : base(succeeded, errors)
        {
            _value = value;
        }
        public TValue Value => Succeeded ? _value! : throw new InvalidOperationException("the value of a failure result can not be accessed!.");

        public static implicit operator Result<TValue>(TValue? value)
        {
            return value;
        }
    }
}
