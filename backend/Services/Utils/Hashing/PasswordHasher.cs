using System.Security.Cryptography;

namespace Services.Utils.Hashing;

public class PasswordHasher : IPasswordHasher
{
    private readonly int saltSize = 128 / 8;
    private readonly int keySize = 256 / 8;
    private readonly int iterations = 10_000;
    private static readonly HashAlgorithmName hashAlgName = HashAlgorithmName.SHA256;
    private readonly char delimiter = ';';

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgName, keySize);

        return string.Join(delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool Verify(string passwordHash, string inputPassword)
    {
        string[] elements = passwordHash.Split(delimiter);
        byte[] salt = Convert.FromBase64String(elements[0]);
        byte[] hash = Convert.FromBase64String(elements[1]);

        byte[] hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, iterations, hashAlgName, keySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}