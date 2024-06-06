using System.Text;

namespace Parnian.Security;

public class MorseEncryptor : IMorseEncryptor
{
  private static readonly Dictionary<char, string> MorseCodeMap = new()
  {
    { 'A', ".-" },
    { 'B', "-..." },
    { 'C', "-.-." },
    { 'D', "-.." },
    { 'E', "." },
    { 'F', "..-." },
    { 'G', "--." },
    { 'H', "...." },
    { 'I', ".." },
    { 'J', ".---" },
    { 'K', "-.-" },
    { 'L', ".-.." },
    { 'M', "--" },
    { 'N', "-." },
    { 'O', "---" },
    { 'P', ".--." },
    { 'Q', "--.-" },
    { 'R', ".-." },
    { 'S', "..." },
    { 'T', "-" },
    { 'U', "..-" },
    { 'V', "...-" },
    { 'W', ".--" },
    { 'X', "-..-" },
    { 'Y', "-.--" },
    { 'Z', "--.." },
    { '0', "-----" },
    { '1', ".----" },
    { '2', "..---" },
    { '3', "...--" },
    { '4', "....-" },
    { '5', "....." },
    { '6', "-...." },
    { '7', "--..." },
    { '8', "---.." },
    { '9', "----." },
    { '.', ".-.-.-" },
    { ',', "--..--" },
    { '?', "..--.." },
    { '\'', ".----." },
    { '!', "-.-.--" },
    { '/', "-..-." },
    { '(', "-.--." },
    { ')', "-.--.-" },
    { '&', ".-..." },
    { ':', "---..." },
    { ';', "-.-.-." },
    { '=', "-...-" },
    { '+', ".-.-." },
    { '-', "-....-" },
    { '_', "..--.-" },
    { '"', ".-..-." },
    { '$', "...-..-" },
    { '@', ".--.-." },
    { ' ', "/" }
  };

  private static readonly Dictionary<string, char> ReverseMorseCodeMap = new();

  static MorseEncryptor()
  {
    foreach (var pair in MorseCodeMap)
    {
      ReverseMorseCodeMap[pair.Value] = pair.Key;
    }
  }


  public string Deface(string morseCode)
  {
    if (string.IsNullOrEmpty(morseCode))
    {
      throw new ArgumentException("Morse code input cannot be null or empty.", nameof(morseCode));
    }

    var simplifiedBuilder = new StringBuilder();
    foreach (var symbol in morseCode)
    {
      switch (symbol)
      {
        case '.':
          simplifiedBuilder.Append('a');
          break;
        case '-':
          simplifiedBuilder.Append('b');
          break;
        case ' ':
          simplifiedBuilder.Append('c');
          break;
        case '/':
          simplifiedBuilder.Append('d');
          break;
        default:
          throw new ArgumentException($"Invalid Morse code symbol '{symbol}'.");
      }
    }

    return Compress(simplifiedBuilder.ToString());
  }

  public string Reface(string defacedCode)
  {
    if (string.IsNullOrEmpty(defacedCode))
    {
      throw new ArgumentException("Defaced code input cannot be null or empty.", nameof(defacedCode));
    }

    var decompressedBuilder = new StringBuilder();
    for (var i = 0; i < defacedCode.Length; i += 2)
    {
      var symbol = defacedCode[i];
      var count = ConvertAlphabetToNumber(defacedCode[i + 1]);

      switch (symbol)
      {
        case 'a':
          decompressedBuilder.Append('.', count);
          break;
        case 'b':
          decompressedBuilder.Append('-', count);
          break;
        case 'c':
          decompressedBuilder.Append(' ', count);
          break;
        case 'd':
          decompressedBuilder.Append('/', count);
          break;
        default:
          throw new ArgumentException($"Invalid defaced code symbol '{symbol}'.");
      }
    }

    return decompressedBuilder.ToString();
  }

  public string Encrypt(string input, bool deface = false)
  {
    if (string.IsNullOrEmpty(input))
    {
      throw new ArgumentException("Input cannot be null or empty.", nameof(input));
    }

    var morseBuilder = new StringBuilder();

    foreach (var character in input.ToUpper())
    {
      if (MorseCodeMap.ContainsKey(character))
      {
        morseBuilder.Append(MorseCodeMap[character] + " ");
      }
      else
      {
        throw new ArgumentException($"Character '{character}' cannot be translated to Morse code.");
      }
    }

    return deface ? Deface(morseBuilder.ToString().Trim()) : morseBuilder.ToString().Trim();
  }

  public string Decrypt(string input, bool reface = false)
  {
    if (reface)
    {
      input = Reface(input);
    }
    if (string.IsNullOrEmpty(input))
    {
      throw new ArgumentException("Morse code input cannot be null or empty.", nameof(input));
    }

    var textBuilder = new StringBuilder();
    var morseCharacters = input.Split(' ');

    foreach (var morseChar in morseCharacters)
    {
      if (ReverseMorseCodeMap.ContainsKey(morseChar))
      {
        textBuilder.Append(ReverseMorseCodeMap[morseChar]);
      }
      else
      {
        throw new ArgumentException($"Invalid Morse code symbol '{morseChar}'.");
      }
    }

    return  textBuilder.ToString();
  }

  private string Compress(string input)
  {
    if (string.IsNullOrEmpty(input))
    {
      return "";
    }

    var compressedBuilder = new StringBuilder();
    var currentChar = input[0];
    var count = 1;

    for (var i = 1; i < input.Length; i++)
    {
      if (input[i] == currentChar)
      {
        count++;
      }
      else
      {
        compressedBuilder.Append(currentChar);
        compressedBuilder.Append(ConvertNumberToAlphabet(count));
        currentChar = input[i];
        count = 1;
      }
    }

    compressedBuilder.Append(currentChar);
    compressedBuilder.Append(ConvertNumberToAlphabet(count));

    return compressedBuilder.ToString();
  }

  private string ConvertNumberToAlphabet(int number)
  {
    var result = new StringBuilder();
    while (number > 0)
    {
      var rem = (number - 1) % 26;
      result.Insert(0, (char)(rem + 'a'));
      number = (number - 1) / 26;
    }

    return result.ToString();
  }

  private int ConvertAlphabetToNumber(char alphabet)
  {
    return alphabet - 'a' + 1;
  }
}
