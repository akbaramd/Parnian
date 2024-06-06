namespace Parnian.Security;

public interface IMorseEncryptor
{
  string Encrypt(string input, bool deface = false);
  string Decrypt(string input, bool reface = false);

  string Deface(string morseCode);
  string Reface(string defacedCode);
}
