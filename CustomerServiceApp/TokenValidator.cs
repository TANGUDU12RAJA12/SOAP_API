using System;
using System.Text;

public static class TokenValidator
{
    public static bool IsValid(string token)
    {
        try
        {
            var data = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var parts = data.Split('|');

            DateTime expiry = DateTime.Parse(parts[1]);
            return expiry > DateTime.Now;
        }
        catch
        {
            return false;
        }
    }
}
