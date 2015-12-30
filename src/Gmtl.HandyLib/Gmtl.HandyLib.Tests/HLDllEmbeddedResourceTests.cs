// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="DllEmbeddedResourceTests.cs" project="Gmtl.HandyLib.Tests" date="2015-12-30 10:33">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLDllEmbeddedResourceTests
    {
        [Test]
        public void DllEmbeddedResource_ShouldReatTextResourceFromCurrentAssembly()
        {
            //Assert
            string path = "Resources.EmbeddedResourceTextFile.txt";
            string expectedContent = "Test 123";

            //Act
            string content = HLDllEmbeddedResource.GetTextResource(path);

            //Assert
            StringAssert.IsMatch(expectedContent, content);
        }

        [Test]
        public void DllEmbeddedResource_ShouldReatTextResourceFromProvidedAssembly()
        {
            //Assert
            string path = "Resources.EmbeddedResourceTextFile.txt";
            string expectedContent = "Test 123";
            Assembly assembly = GetType().Assembly;

            //Act
            string content = HLDllEmbeddedResource.GetTextResource(path, assembly);

            //Assert
            StringAssert.IsMatch(expectedContent, content);
        }
    }
}