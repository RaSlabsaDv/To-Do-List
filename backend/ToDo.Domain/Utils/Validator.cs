public static class Validator
{
    public static string RequiredString(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new Exception($"{fieldName} cannot be emty!");
            
        return value;
    }
}