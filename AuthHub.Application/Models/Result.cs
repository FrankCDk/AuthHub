using System.Collections.Immutable;

namespace AuthHub.Application.Models
{
    public static class Result
    {

        public static Result<T> Success<T>(this T value) => new Result<T>(value); // Crea un objeto exitoso
        public static Result<T> Failure<T>(ImmutableArray<string> errors) => new Result<T>(errors); // Crea un objeto fallido con una lista de errores
        public static Result<T> Failure<T>(string error) => new Result<T>(ImmutableArray.Create(error)); // Crea un objeto fallido con un solo mensaje de error

    }
}
