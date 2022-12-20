using System;

public interface IEncryptor
{
	void EncryptText(string text);
	void DecryptText(string text);
}
