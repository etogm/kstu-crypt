namespace Kstu.Crypt.Tests;

using Xunit;
using Kstu.Crypt.Core;

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
}