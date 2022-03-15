namespace Kstu.Crypt.Tests;

using Xunit;
using Kstu.Crypt.Core;
using System.Text;

public class AffineTest
{
    [Theory]
    [InlineData(3, 4)]
    //[InlineData(0, 0)]
    [InlineData(int.MaxValue, int.MaxValue)]
    public void Encrypt_ShouldCorrectEncryptText(int a, int b)
    {
        // Arrange
        var crypt = new Affine(a, b);
        var plaintext = @"Lorem ipsum dolor, sit amet consectetur adipisicing elit.";

        // Act
        var ciphertext = crypt.Encrypt(plaintext);
        var result = crypt.Decrypt(ciphertext);
        
        // Assert
        Assert.Equal(plaintext, result);
    }

    [Fact]
    public void BruteForce_ShouldDoCorrectCryptanalisys()
    {
        // Arrange
        var alphabet = GetAlphabet();
        var plaintext = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.";

        var crypt = new Affine(15, 7);
        var ciphertext = crypt.Encrypt(plaintext);

        // Act
        var result = Affine.BruteForce(alphabet, ciphertext);

        // Assert
        Assert.Equal(plaintext, result);
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