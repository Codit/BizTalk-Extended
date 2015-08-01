using System;

using Xunit;

namespace BizTalk.Extended.Core.UnitTests.Guard
{
    public class GuardTests
    {
        [Fact]
        public void NotNullOrWhitespace_ParameterValueHasTextParameterNameHasText_Succeeds()
        {
            // Arrange
            string parameterValue = "MyValue";
            string parameterName = "MyParameter";

            // Act
            Guards.Guard.NotNullOrWhitespace(parameterValue, parameterName);
        }

        [Fact]
        public void NotNullOrWhitespace_ParameterValueHasTextParameterNameIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            string parameterValue = "MyValue";
            string parameterName = "";

            // Act
            Assert.Throws<ArgumentException>(() => Guards.Guard.NotNullOrWhitespace(parameterValue, parameterName));
        }

        [Fact]
        public void NotNullOrWhitespace_ParameterValueHasTextParameterNameIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            string parameterValue = "MyValue";
            string parameterName = " ";

            // Act
            Assert.Throws<ArgumentException>(() => Guards.Guard.NotNullOrWhitespace(parameterValue, parameterName));
        }

        [Fact]
        public void NotNullOrWhitespace_ParameterValueHasTextParameterNameIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            string parameterValue = "MyValue";
            string parameterName = null;

            // Act
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNullOrWhitespace(parameterValue, parameterName));
        }

        [Fact]
        public void NotNullOrWhitespace_ParameterValueIsEmptyParameterNameHasText_ThrowsArgumentException()
        {
            // Arrange
            string parameterValue = "";
            string parameterName = "MyParameter";

            // Act
            Assert.Throws<ArgumentException>(() => Guards.Guard.NotNullOrWhitespace(parameterValue, parameterName));
        }

        [Fact]
        public void NotNullOrWhitespace_ParameterValueIsWhitespaceParameterNameHasText_ThrowsArgumentException()
        {
            // Arrange
            string parameterValue = " ";
            string parameterName = "MyParameter";

            // Act
            Assert.Throws<ArgumentException>(() => Guards.Guard.NotNullOrWhitespace(parameterValue, parameterName));
        }

        [Fact]
        public void NotNullOrWhitespace_ParameterValueIsNullParameterNameHasText_ThrowsArgumentNullException()
        {
            // Arrange
            string parameterValue = null;
            string parameterName = "MyParameter";

            // Act
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNullOrWhitespace(parameterValue, parameterName));
        }

        [Fact]
        public void NotNullOrEmpty_ParameterValueHasTextParameterNameHasText_Succeeds()
        {
            // Arrange
            string parameterValue = "MyValue";
            string parameterName = "MyParameter";

            // Act
            Guards.Guard.NotNullOrEmpty(parameterValue, parameterName);
        }

        [Fact]
        public void NotNullOrEmpty_ParameterValueIsWhitespaceParameterNameHasText_Succeeds()
        {
            // Arrange
            string parameterValue = " ";
            string parameterName = "MyParameter";

            // Act
            Guards.Guard.NotNullOrEmpty(parameterValue, parameterName);
        }

        [Fact]
        public void NotNullOrEmpty_ParameterValueIsEmptyParameterNameHasText_ThrowsArgumentException()
        {
            // Arrange
            string parameterValue = "";
            string parameterName = "MyParameter";

            // Act
            Assert.Throws<ArgumentException>(() => Guards.Guard.NotNullOrEmpty(parameterValue, parameterName));
        }

        [Fact]
        public void NotNullOrEmpty_ParameterValueIsNullParameterNameHasText_ThrowsArgumentNullException()
        {
            // Arrange
            string parameterValue = null;
            string parameterName = "MyParameter";

            // Act
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNullOrEmpty(parameterValue, parameterName));
        }

        [Fact]
        public void NotNull_ParameterValueIsNullParameterNameHasText_ThrowsArgumentNullException()
        {
            // Arrange
            object parameterValue = null;
            string parameterName = "MyParameter";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NotNull_ParameterValueHasTextParameterNameIsNull_ThrowsArgumentException()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NotNull_ParameterValueHasTextParameterNameIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NotNull_ParameterValueHasTextParameterNameIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = " ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guards.Guard.NotNull(parameterValue, parameterName));
        }

        [Fact]
        public void NotNull_ParameterValueHasTextParameterNameHasText_Succeeds()
        {
            // Arrange
            object parameterValue = "MyValue";
            string parameterName = "MyParameter";

            // Act
            Guards.Guard.NotNull(parameterValue, parameterName);
        }

        [Fact]
        public void NotNull_ParameterValueIsEmptyParameterNameHasText_Succeeds()
        {
            // Arrange
            object parameterValue = "";
            string parameterName = "MyParameter";

            // Act & Assert
            Guards.Guard.NotNull(parameterValue, parameterName);
        }

        [Fact]
        public void NotNull_ParameterValueIsWhitespaceParameterNameHasText_Succeeds()
        {
            // Arrange
            object parameterValue = " ";
            string parameterName = "MyParameter";

            // Act & Assert
            Guards.Guard.NotNull(parameterValue, parameterName);
        }
    }
}
