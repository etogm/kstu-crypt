namespace Kstu.Crypt.Tests;

using Xunit;
using Kstu.Crypt.Core;
using System.Text;

public class CaesarTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(26)]
    [InlineData(-6524)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void Encrypt_ShouldCorrectEncryptText(int key)
    {
        // Arrange
        var crypt = new Caesar(key);
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
        var plaintext = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        var crypt = new Caesar(9);
        var ciphertext = crypt.Encrypt(plaintext);

        // Act
        var result = Caesar.BruteForce(alphabet, ciphertext);

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