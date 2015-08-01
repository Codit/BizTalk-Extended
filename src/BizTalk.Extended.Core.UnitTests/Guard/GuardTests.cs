using System;

using Xunit;

namespace BizTalk.Extended.Core.UnitTests.Guard
{
    public class GuardTests
    {
        [Fact]
        public void NoTNull_ParameterValueIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            object parameterValue = null;
            string parameterName = "MyParameter";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NoTNull_ParameterNameIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NoTNull_ParameterNameIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = "";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NoTNull_ParameterNameIsWhitespace_ThrowsArgumentNullException()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = " ";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NoTNull_ValueIsNotNull_Succeeds()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = "MyParameter";

            // Act
            Guards.Guard.NotNull(parameterValue, parameterName);
        }
    }
}
