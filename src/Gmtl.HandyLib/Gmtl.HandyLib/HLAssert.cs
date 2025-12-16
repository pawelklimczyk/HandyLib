// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLPhoneValidatorTests.cs" project="Gmtl.HandyLib.Tests" date="2025-12-15 17:22">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Provides static methods for validating method arguments and ensuring they meet required conditions.
    /// </summary>
    /// <remarks>The HLAssert class contains utility methods for performing common argument validation checks,
    /// such as verifying that parameters are not null or that strings are not empty or whitespace. These methods are
    /// intended to be used at the start of methods to enforce preconditions and throw appropriate exceptions when
    /// arguments are invalid. All methods are static and thread safe.</remarks>
    public static class HLAssert
    {
        /// <summary>
        /// Ensures string parameter is not null, empty, or whitespace. 
        /// </summary>
        public static void AssertArgument(string argument, string paramName)
        {
            if (paramName is null) throw new ArgumentNullException(nameof(paramName));
            if (argument is null) throw new ArgumentNullException(paramName);
            if (string.IsNullOrWhiteSpace(argument)) throw new ArgumentException("Argument cannot be empty or whitespace.", paramName);
        }

        /// <summary>
        /// Ensures parameter is not null.
        /// </summary>
        public static void AssertArgument(object argument, string paramName)
        {
            if (paramName is null) throw new ArgumentNullException(nameof(paramName));
            if (argument is null) throw new ArgumentNullException(paramName);
        }
    }
}
