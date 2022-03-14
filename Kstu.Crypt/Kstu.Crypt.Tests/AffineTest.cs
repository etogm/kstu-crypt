namespace Kstu.Crypt.Tests;

using Xunit;
using Kstu.Crypt.Core;

public class AffineTest
{
    [Theory]
    [InlineData(3, 4)]
    //[InlineData(0, 0)]
    [InlineData(int.MaxValue, int.MaxValue)]
    [InlineData(int.MinValue, int.MinValue)]
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
}