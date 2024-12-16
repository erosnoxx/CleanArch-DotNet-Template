namespace Application.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsEnum<T>(this string valor, T enumValor) where T : struct, Enum
        {
            return Enum.TryParse<T>(valor, out var result) && result.Equals(enumValor);
        }
    }
}
