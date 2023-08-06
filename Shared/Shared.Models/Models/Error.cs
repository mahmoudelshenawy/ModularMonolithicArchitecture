﻿namespace Shared.Models.Models
{
    public class Error : IEquatable<Error>
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result calue is null.");

        public Error(string code , string message)
        {
            Code = code;
            Message = message;
        }
        public string Code { get; }
        public string Message { get; }

        public static implicit operator string(Error error) => error.Code;
        public static bool operator ==(Error? a , Error? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a is not null && b is not null && a.Equals(b);

        }
        public static bool operator !=(Error? a, Error? b)
        {
            return !(a == b);

        }

        public bool Equals(Error? other)
        {
            if (other is null) return false;

            if (other.GetType() != GetType()) return false;

            if (other is not Error error) return false;

            return error.Code == Code;
        }
    }
}
