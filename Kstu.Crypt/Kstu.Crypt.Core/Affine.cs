namespace Kstu.Crypt.Core;

public class Affine : CryptBase
{
	private int _a;
	private int _b;
	private int _inv;

	public Affine(int a, int b)
	{
		A = a;
		B = b;
	}

	public Affine(int a, int b, string alphabet) : base(alphabet)
	{
		A = a;
		B = b;
	}

	private int A 
	{
		get => _a;
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException(
					nameof(value),
					$"A ({value}) inferior to minimum expected (1)"
				);
			}

			if (Gcd(value % Alphabet.Length, Alphabet.Length) != 1)
			{
				throw new ArgumentOutOfRangeException(
					nameof(value),
					$"A ({value}) and alphabet length ({Alphabet.Length}) aren't coprime"
				);
			}
			_a = value > 0 && value < Alphabet.Length ? value : value % Alphabet.Length;
			_inv = Inverse(_a, Alphabet.Length);
		}
	}

	private int B 
	{
		get => _b;
		set 
		{
			if (value < 0)
				throw new ArgumentOutOfRangeException(
					nameof(value),
					$"A ({value}) inferior to minimum expected (0)"
				);
			_b = value > 0 && value < Alphabet.Length ? value : value % Alphabet.Length;
		}
	}

	private protected override Func<char, int> EncryptFunc =>
		(c) => (A * Alphabet.IndexOf(c) + B) % Alphabet.Length;

	private protected override Func<char, int> DecryptFunc =>
		(c) => (_inv * (Alphabet.IndexOf(c) + Alphabet.Length - B)) % Alphabet.Length;

	public static string BruteForce(string alphabet, string ciphertext)
	{
		var affine = new Affine(1, 1);
		var analisys = new Cryptanalisys(affine);

		for (var i = 1; i < alphabet.Length; i++)
		{
			if (affine.Gcd(i % alphabet.Length, alphabet.Length) != 1)
				continue;
			
			affine.A = i;

			for (var j = 1; j < alphabet.Length; j++)
			{
				affine.B = j;

				analisys.CheckPotential(ciphertext);
			}
		}

		return analisys.Plaintext;
	}
	private int Inverse(int a, int n)
	{
		for (int i = 0; i < n; i++)
		{
			if ((a * i) % n == 1)
			{
				return i;
			}
		}
		return 1;
	}

	private int Gcd(int a, int b)
	{
		while (a != b)
		{
			if (a < b)
			{
				var tmp = a;
				a = b;
				b = tmp;
			}
			a -= b;
		}
		return a;
	}
}