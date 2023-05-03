using Application.Common.Result.Implementations;

namespace Application.Common.Result.Abstractions;

public interface IResult
{
    bool Success { get; }

    ErrorInfo ErrorInfo { get; }
}