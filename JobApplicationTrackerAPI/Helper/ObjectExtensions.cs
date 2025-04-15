public static class ObjectExtensions
{
    public static TResult? Let<T, TResult>(this T? value, Func<T, TResult> func)
        where T : class
        where TResult : class =>
        value == null ? null : func(value);
}