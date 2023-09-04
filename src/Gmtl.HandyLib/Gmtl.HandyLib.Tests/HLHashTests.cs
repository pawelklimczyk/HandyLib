using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Gmtl.HandyLib.Tests;

[TestFixture]
public class HLHashTests
{
    [TestCaseSource(nameof(TestCases))]
    public void HLHash_hasingSameInput_shouldReturnOtherHash(string input)
    {
        string hash1 = HLHash.ComputeHash(input);
        string hash2 = HLHash.ComputeHash(input);

        Assert.AreNotEqual(hash1, hash2);
    }

    [TestCaseSource(nameof(TestCases))]
    public void HLHash_providingSameInputAndSameSalt_shouldGenerateSameHash(string input)
    {
        byte[] salt1 = Encoding.UTF8.GetBytes("salt1");

        string hash1 = HLHash.ComputeHashWithSalt(input, salt1);
        string hash2 = HLHash.ComputeHashWithSalt(input, salt1);

        Assert.AreEqual(hash1, hash2);
    }

    [TestCaseSource(nameof(TestCases))]
    public void HLHash_forProvidedInputAndHash_validationShouldBeTrue(string input)
    {
        string hash = HLHash.ComputeHash(input);

        bool isTheSamePassword = HLHash.ValidateHash(hash, input);

        Assert.IsTrue(isTheSamePassword);
    }

    [TestCaseSource(nameof(TestCases))]
    public void HLHash_forProvidedInputAndHash_validationShouldBeFalse(string input)
    {
        string hash1 = HLHash.ComputeHash(input);

        bool isTheSamePassword = HLHash.ValidateHash(hash1, "otherInput");

        Assert.IsFalse(isTheSamePassword);
    }

    [Test]
    public void HLHash_forNullInput_computeHashShouldThrowArgumentNullException()
    {
        string input = null;

        Assert.Throws<ArgumentNullException>(() => HLHash.ComputeHash(input));
    }

    [TestCaseSource(nameof(TestCases))]
    [Ignore("AppVeyor throw timeout error")]
    public void HLHash_PerformanceTest(string input)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        for (int i = 0; i < 100; i++)
        {
            HLHash.ComputeHash(input);
        }

        watch.Stop();
        long elapsedMs = watch.ElapsedMilliseconds;

        Assert.LessOrEqual(elapsedMs, 1000);
    }

    private static IEnumerable<string> TestCases()
    {
        yield return "test";
        yield return "test2";
        yield return "!@#$%^&*()_+{}[]|\"':;<>,.?/~";
        yield return "";
        yield return "a";
        yield return new string('a', 1000);
    }
}
