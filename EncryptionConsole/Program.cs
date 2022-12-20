// See https://aka.ms/new-console-template for more information
Console.WriteLine("Encryption Console Running...");
EncryptionProvider ep = new EncryptionProvider();
//ep.EncryptFile("Data\\Text.dat");

await ep.DecryptFile("Data\\Text.dat");





