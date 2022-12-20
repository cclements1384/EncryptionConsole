using System;

public interface IEncryptor
{
	void EncryptText(string text);
	void DecryptText(string text);

	void EncryptFile(string filePath);
	Task DecryptFile(string filePath);
}
