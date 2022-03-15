namespace Kstu.Crypt.Core;

internal class Cryptanalisys
{
	private readonly string _alphabet;
	private readonly Dictionary<char, double> _alphabetLf;
	private CryptBase _crypt;

	public Cryptanalisys(CryptBase crypt)
	{
		Potential = 0.0;
		Plaintext = string.Empty;

		_crypt = crypt;
		_alphabet = CryptBase.GetAlphabet();
		_alphabetLf = GetEngLettersFrequencies();
	}

	public string Plaintext { get; private set; }

	public double Potential { get; private set; }

	internal void CheckPotential(string ciphertext)
	{
		var plaintext = _crypt.Decrypt(ciphertext);
		var lf = GetLettersFrequencies(plaintext);
		var x = 0.0;

		foreach (var aLf in _alphabetLf)
		{
			if (!lf.ContainsKey(aLf.Key))
			{
				continue;
			}

			x += aLf.Value + lf[aLf.Key];
		}

		if (Potential < x)
		{
			Potential = x;
			Plaintext = plaintext;
		}
	}

	private Dictionary<char, double> GetLettersFrequencies(string text) => 
		text
			.Where(c => _alphabet.Contains(c))
			.GroupBy(c => c)
			.ToDictionary(c => c.Key, c => (double)c.Count() / _alphabet.Length * 100);

	private Dictionary<char, double> GetEngLettersFrequencies()
	{
		var d = new Dictionary<char, double>();

		d.Add('a', 8.167);
		d.Add('b', 1.492);
		d.Add('c', 2.782);
		d.Add('d', 4.253);
		d.Add('e', 12.70);
		d.Add('f', 2.228);
		d.Add('g', 2.015);
		d.Add('h', 6.094);
		d.Add('i', 6.966);
		d.Add('j', 0.153);
		d.Add('k', 0.772);
		d.Add('l', 4.025);
		d.Add('m', 2.406);
		d.Add('n', 6.749);
		d.Add('o', 7.507);
		d.Add('p', 1.929);
		d.Add('q', 0.095);
		d.Add('r', 5.987);
		d.Add('s', 6.327);
		d.Add('t', 9.056);
		d.Add('u', 2.758);
		d.Add('v', 0.978);
		d.Add('w', 2.360);
		d.Add('x', 0.150);
		d.Add('y', 1.974);
		d.Add('z', 0.074);

		return d;
	}
}