using System;
using BizTalk.Extended.Core.Exceptions;
using Microsoft.BizTalk.Message.Interop;
using Moq;
using Xunit;

namespace BizTalk.Extended.Pipeline.Core.UnitTests.Extensions
{
    public class BaseMessageExtensionsTests
    {
        private const string ActionPropertyName = "Action";
        private const string ActionPropertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";

        [Fact]
        public void ReadContextProperty_IsMandatoryButPropertyNotPresent_ThrowsContextPropertyNotFoundException()
        {
            // Arrange
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.Setup(msg => msg.Context.Read(ActionPropertyName, ActionPropertyNamespace))
                         .Returns(null);

            // Act & Assert
            Assert.Throws<ContextPropertyNotFoundException>(() => mockedMessage.Object.ReadContextProperty<WCF.Action, string>());
        }

        [Fact]
        public void ReadContextProperty_IsMandatoryPropertyOfTypeStringContainsValue_Succeeds()
        {
            // Arrange
            const string ExceptedAction = "Send";
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.Setup(msg => msg.Context.Read(ActionPropertyName, ActionPropertyNamespace))
                         .Returns(ExceptedAction);

            // Act
            string action = mockedMessage.Object.ReadContextProperty<WCF.Action, string>();

            // Assert
            Assert.Equal(ExceptedAction, action);
        }

        [Fact]
        public void ReadContextProperty_OptionalPropertyOfTypeStringContainsValue_Succeeds()
        {
            // Arrange
            const string ExpectedAction = "Send";
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.Setup(msg => msg.Context.Read(ActionPropertyName, ActionPropertyNamespace))
                         .Returns(ExpectedAction);

            // Act
            string action = mockedMessage.Object.ReadContextProperty<WCF.Action, string>(false);

            // Assert
            Assert.Equal(ExpectedAction, action);
        }
        
        [Fact]
        public void ReadContextProperty_OptionalPropertyOfTypeStringContainsNull_Succeeds()
        {
            // Arrange
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.Setup(msg => msg.Context.Read(ActionPropertyName, ActionPropertyNamespace))
                         .Returns(null);

            // Act
            string action = mockedMessage.Object.ReadContextProperty<WCF.Action, string>(false);

            // Assert
            Assert.Null(action);
        }
    }
}
