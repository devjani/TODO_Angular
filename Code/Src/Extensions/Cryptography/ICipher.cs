﻿namespace Cryptography
{
    public interface ICipher
    {
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
    }
}
