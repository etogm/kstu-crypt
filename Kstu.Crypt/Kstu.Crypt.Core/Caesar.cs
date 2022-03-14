namespace Kstu.Crypt.Core;

public class Caesar : CryptBase
{
	private int _key;

	public Caesar(int key)
	{
		Key = key;
	}

	public Caesar(int key, string alphabet) : base(alphabet)
	{
		Key = key;
	}

	private protected int Key 
	{
		get => _key;
		set => _key = value > 0 && value < Alphabet.Length ? value : value % Alphabet.Length;
	}

	private protected override Func<char, int> DecryptFunc =>
		(c) => (Alphabet.Length + Alphabet.IndexOf(c) + Key) % Alphabet.Length;
		
	private protected override Func<char, int> EncryptFunc =>
		(c) => (Alphabet.IndexOf(c) - Key + Alphabet.Length) % Alphabet.Length;
}
