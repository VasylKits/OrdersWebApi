namespace Application.Common.Result.Abstractions.Generics;

public interface IResult<out TData> : IResult
{
    TData Data { get; }
}