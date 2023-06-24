using System.Security.Cryptography;

namespace MAmail.Utils
{
    public static class PasswordSecurity
    {
        private static readonly int _PBKDF2IterCount = 1000;
        private static readonly int _PBKDF2SubkeyLength = 256 / 8;
        private static readonly int _saltSize = 128 / 8;

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("PASSWORD_CANNOT_BE_NULL");
            }

            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, _saltSize, _PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(_PBKDF2SubkeyLength);
            }

            var outputBytes = new byte[1 + _saltSize + _PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, _saltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + _saltSize, _PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            if (hashedPasswordBytes.Length != (1 + _saltSize + _PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                return false;
            }

            var salt = new byte[_saltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, _saltSize);
            var storedSubkey = new byte[_PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + _saltSize, storedSubkey, 0, _PBKDF2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, _PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(_PBKDF2SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
