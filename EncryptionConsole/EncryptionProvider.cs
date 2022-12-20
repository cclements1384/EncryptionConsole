﻿using System.Security.Cryptography;

public class EncryptionProvider : IEncryptor

{
    private byte[] _key;


	public EncryptionProvider()
	{
        _key = CreateKey();
	}

	public async Task DecryptFile(string filePath)
	{
        try
        {
            using (FileStream fileStream = new(filePath, FileMode.Open))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] iv = new byte[aes.IV.Length];
                    int numBytesToRead = aes.IV.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                        if (n == 0) break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }

                    byte[] key =
                    {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };

                    using (CryptoStream cryptoStream = new(
                       fileStream,
                       aes.CreateDecryptor(key, iv),
                       CryptoStreamMode.Read))
                    {
                        // By default, the StreamReader uses UTF-8 encoding.
                        // To change the text encoding, pass the desired encoding as the second parameter.
                        // For example, new StreamReader(cryptoStream, Encoding.Unicode).
                        using (StreamReader decryptReader = new(cryptoStream))
                        {
                            string decryptedMessage = await decryptReader.ReadToEndAsync();
                            Console.WriteLine($"The decrypted original message: {decryptedMessage}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The decryption failed. {ex}");
        }
    }

	public void DecryptText(string text)
	{
		throw new NotImplementedException();
	}

	public void EncryptFile(string filePath)
	{

        try
        {
            CreateOutputDirectory(filePath);
      
            using (FileStream fileStream = new(filePath, FileMode.OpenOrCreate))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] key =
                    {
                        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                    };
                    aes.Key = key;

                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);

                    using (CryptoStream cryptoStream = new(
                        fileStream,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        // By default, the StreamWriter uses UTF-8 encoding.
                        // To change the text encoding, pass the desired encoding as the second parameter.
                        // For example, new StreamWriter(cryptoStream, Encoding.Unicode).
                        using (StreamWriter encryptWriter = new(cryptoStream))
                        {
                            encryptWriter.WriteLine("Hello World!");
                        }
                    }
                }
            }

            Console.WriteLine("The file was encrypted.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The encryption failed. {ex}");
        }
    }

    private static void CreateOutputDirectory(string filePath)
    {
        var outputDir = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(Path.GetDirectoryName(outputDir)))
            Directory.CreateDirectory(outputDir);
    }

    public void EncryptText(string text)
	{
		throw new NotImplementedException();
	}

    private byte[] CreateKey()
    {
        byte[] key;
        using(Aes aes = Aes.Create())
        {
            key = aes.Key;
        }

        return key;
    }
}
