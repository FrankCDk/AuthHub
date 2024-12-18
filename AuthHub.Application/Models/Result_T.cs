﻿using System.Collections.Immutable;

namespace AuthHub.Application.Models
{
    public struct Result<T>
    {

        public readonly T Value;
        public static implicit operator Result<T>(T value) => new Result<T>(value);
        public readonly ImmutableArray<string> Errors;
        public bool Success => Errors.Length == 0;

        public Result(T value)
        {
            Value = value;
            Errors = ImmutableArray<string>.Empty;
        }

        public Result(ImmutableArray<string> errors)
        {

            if (errors.Length == 0)
            {
                throw new InvalidOperationException("debes indicar al menos un error.");
            }

            Value = default;
            Errors = errors;
        }
    }
}
