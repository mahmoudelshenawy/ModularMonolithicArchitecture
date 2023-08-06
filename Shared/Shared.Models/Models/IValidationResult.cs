namespace Shared.Models.Models
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new Error("ValidationError", "A Validation Problem occurred.");

        Error[] Errors { get; }
    }
}
