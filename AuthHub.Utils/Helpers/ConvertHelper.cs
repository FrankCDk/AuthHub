namespace AuthHub.Utils.Helpers
{
    public static class ConvertHelper
    {

        public static string ToNonNullString(object? value)
        {
            return value?.ToString() ?? string.Empty;
        }




    }
}
