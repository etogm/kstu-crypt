namespace Kstu.Crypt.Core;

using System.Text;

public abstract class CryptBase
{
	public CryptBase()
	{
		Alphabet = GetAlphabet();
	}

	public CryptBase(string alphabet)
	{
		Alphabet = alphabet;
	}

	private protected string Alphabet { get; set; }

	private protected abstract Func<char, int> EncryptFunc { get; }

	private protected abstract Func<char, int> DecryptFunc { get; }

	public string Encrypt(string plaintext) =>
		ConvertsText(plaintext, EncryptFunc);

	public string Decrypt(string ciphertext)=>
		ConvertsText(ciphertext, DecryptFunc);

	private protected string ConvertsText(string text, Func<char, int> func)
	{
		var ciphertext = new StringBuilder();

		foreach (var c in text)
		{
			ciphertext.Append(Alphabet.Contains(c) ? Alphabet[func(c)] : c);
		}

		return ciphertext.ToString();
	}
	
	private string GetAlphabet()
	{
		var alphabet = new StringBuilder();

		for (var c = 'a'; c <= 'z'; c++)
		{
			alphabet.Append(c);
		}

		return alphabet.ToString();
	}
}
