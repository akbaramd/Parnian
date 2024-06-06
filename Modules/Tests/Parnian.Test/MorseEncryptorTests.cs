using System;
using Parnian.Security;
using Xunit;

public class MorseEncryptorTests
{
    private readonly IMorseEncryptor _translator;

    private const string TextToEncrypt = "AKBAR";
    private const string EncryptedMorseCode = ".- -.- -... .- .-.";
    private const string DefacedCode = "aabacabaaabacabaaccaaabacaaabaaa";

    public MorseEncryptorTests()
    {
        _translator = new MorseEncryptor();
    }

    [Fact]
    public void Encrypt_ShouldReturnCorrectMorseCode()
    {
        // Act
        string result = _translator.Encrypt(TextToEncrypt);

        // Assert
        Assert.Equal(EncryptedMorseCode, result);
    }
    
    [Fact]
    public void Encrypt_ShouldReturnCorrectDefacedMorseCode()
    {
      // Act
      string result = _translator.Encrypt(TextToEncrypt,true);

      // Assert
      Assert.Equal(DefacedCode, result);
    }

    [Fact]
    public void Decrypt_ShouldReturnCorrectText()
    {
        // Act
        string result = _translator.Decrypt(EncryptedMorseCode);

        // Assert
        Assert.Equal(TextToEncrypt, result);
    }
    
    [Fact]
    public void Decrypt_ShouldReturnCorrectRefacedText()
    {
      // Act
      string result = _translator.Decrypt(DefacedCode,true);

      // Assert
      Assert.Equal(TextToEncrypt, result);
    }
    
    [Fact]
    public void Deface_ShouldReturnCompressedDefacedCode()
    {
        // Act
        string result = _translator.Deface(EncryptedMorseCode);

        // Assert
        Assert.Equal(DefacedCode, result);
    }

    [Fact]
    public void Reface_ShouldReturnOriginalMorseCode()
    {
        // Act
        string result = _translator.Reface(DefacedCode);

        // Assert
        Assert.Equal(EncryptedMorseCode, result);
    }

    [Fact]
    public void FullCycle_ShouldReturnOriginalText()
    {
        // Act
        string morseCode = _translator.Encrypt(TextToEncrypt);
        string defaced = _translator.Deface(morseCode);
        string refaced = _translator.Reface(defaced);
        string result = _translator.Decrypt(refaced);

        // Assert
        Assert.Equal(TextToEncrypt, result);
    }

   
}
