using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace PracticalTest.Domain.Write.Common;

public static class ResultExtensions
{
    public static async Task<Maybe<T>> MaybeFindAsync<T>(this DbSet<T> obj, object keyValue) where T : class =>
        Maybe<T>.From(await obj.FindAsync(keyValue));
}