using System.Text;
using System.Security.Cryptography;

namespace Rogedo.LifeEngine.Tools;

public static class Hash
{
    public static byte[] GetHash(string inputString)
    {
        using HashAlgorithm algorithm = SHA256.Create();

        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        var sb = new StringBuilder();

        foreach (var b in GetHash(inputString))
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }
}