using BizTalk.Extended.Core.Exceptions;
using Microsoft.XLANGs.BaseTypes;
using Moq;
using System;
using Xunit;

namespace BizTalk.Extended.Orchestrations.Extensions.UnitTests.Extensions
{
    public class XLANGMessageExtensionsTests
    {
        [Fact]
        public void WriteContextProperty_MessageIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            XLANGMessage message = (XLANGMessage)null;

            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentNullException>("message", () => message.WriteContextProperty<WCF.Action>(propertyValue));
        }

        [Fact]
        public void WriteContextProperty_ValidInput_Succeeds()
        {
            // Arrange
            var mockedMessage = new Mock<XLANGMessage>();

            object propertyValue = "MyValue";

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessage.Verify(msg => msg.SetPropertyValue(typeof(WCF.Action), propertyValue), Times.Once);
        }

        [Fact]
        public void WriteContextProperty_PropertyValueIsNull_Succeeds()
        {
            // Arrange
            var mockedMessage = new Mock<XLANGMessage>();

            object propertyValue = null;

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessage.Verify(msg => msg.SetPropertyValue(typeof(WCF.Action), propertyValue), Times.Once);
        }

        [Fact]
        public void WriteContextProperty_PropertyValueIsEmpty_Succeeds()
        {
            // Arrange
            var mockedMessage = new Mock<XLANGMessage>();

            object propertyValue = string.Empty;

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessage.Verify(msg => msg.SetPropertyValue(typeof(WCF.Action), propertyValue), Times.Once);
        }

        [Fact]
        public void WriteContextProperty_PropertyValueHasWhitespace_Succeeds()
        {
            // Arrange
            var mockedMessage = new Mock<XLANGMessage>();

            object propertyValue = " ";

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessage.Verify(msg => msg.SetPropertyValue(typeof(WCF.Action), propertyValue), Times.Once);
        }

        [Fact]
        public void ReadContextProperty_IsMandatoryButPropertyNotPresent_ThrowsContextPropertyNotFoundException()
        {
            // Arrange
            var mockedMessage = new Mock<XLANGMessage>();
            mockedMessage.Setup(msg => msg.GetPropertyValue(typeof(WCF.Action)))
                         .Returns(null);

            // Act & Assert
            Assert.Throws<ContextPropertyNotFoundException>(() => mockedMessage.Object.ReadContextProperty<WCF.Action, string>());
        }

        [Fact]
        public void ReadContextProperty_IsMandatoryPropertyOfTypeStringContainsValue_Succeeds()
        {
            // Arrange
            const string ExpectedAction = "Send";
            var mockedMessage = new Mock<XLANGMessage>();
            mockedMessage.Setup(msg => msg.GetPropertyValue(typeof(WCF.Action)))
                         .Returns(ExpectedAction);

            // Act
            string action = mockedMessage.Object.ReadContextProperty<WCF.Action, string>();

            // Assert
            Assert.Equal(ExpectedAction, action);
        }

        [Fact]
        public void ReadContextProperty_OptionalPropertyOfTypeStringContainsValue_Succeeds()
        {
            // Arrange
            const string ExpectedAction = "Send";
            var mockedMessage = new Mock<XLANGMessage>();
            mockedMessage.Setup(msg => msg.GetPropertyValue(typeof(WCF.Action)))
                         .Returns(ExpectedAction);

            // Act
            string action = mockedMessage.Object.ReadContextProperty<WCF.Action, string>(isMandatory: false);

            // Assert
            Assert.Equal(ExpectedAction, action);
        }

        [Fact]
        public void ReadContextProperty_OptionalPropertyOfTypeStringContainsNull_Succeeds()
        {
            // Arrange
            var mockedMessage = new Mock<XLANGMessage>();
            mockedMessage.Setup(msg => msg.GetPropertyValue(typeof(WCF.Action)))
                         .Returns(null);

            // Act
            string action = mockedMessage.Object.ReadContextProperty<WCF.Action, string>(isMandatory: false);

            // Assert
            Assert.Null(action);
        }

    }
}
