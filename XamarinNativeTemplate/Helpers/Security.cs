using System;
using Plugin.KeyChain;

namespace XamarinNativeTemplate
{
    public static class Security
    {
        public static string KeySeed()
        {
            var keychain = CrossKeyChain.Current;
            var keySeed = keychain.GetKey("KeySeed");
            if (string.IsNullOrEmpty(keySeed))
            {
                // Bytes in 256-bit (32)
                keySeed = GenerateKeyOrSalt(32);
                keychain.SetKey("KeySeed", keySeed);
            }
            return keySeed;
        }

        public static byte[] SaltText()
        {
            var keychain = CrossKeyChain.Current;
            var saltText = keychain.GetKey("SaltText");
            if (string.IsNullOrEmpty(saltText))
            {
                saltText = GenerateKeyOrSalt(8);
                keychain.SetKey("SaltText", saltText);
            }
            return saltText.GetBytes();
        }

        public static string GenerateKeyOrSalt(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_$()+-/";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }
    }
}