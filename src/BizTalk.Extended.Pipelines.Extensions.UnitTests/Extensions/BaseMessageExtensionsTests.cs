using BizTalk.Extended.Core.Exceptions;
using Microsoft.BizTalk.Message.Interop;
using Moq;
using System;
using Xunit;

namespace BizTalk.Extended.Pipeline.Core.UnitTests.Extensions
{
    public class BaseMessageExtensionsTests
    {
        // TODO: Add tests where property schemas have different types such as enums

        private const string ActionPropertyName = "Action";
        private const string ActionPropertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";

        [Fact]
        public void PromoteContextProperty_MessageIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IBaseMessage message = (IBaseMessage)null;

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentNullException>("message", () => message.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void PromoteContextProperty_MessageContextIsNull_ThrowsArgumentException()
        {
            // TOCOVER test
            // Arrange
            var mockedMessage = new Mock<IBaseMessage>();

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("message", () => mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void PromoteContextProperty_PropertyNameIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("name", () => mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void PromoteContextProperty_PropertyNameIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = " ";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("name", () => mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void PromoteContextProperty_PropertyNameIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = null;
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentNullException>("name", () => mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void PromoteContextProperty_PropertyNamespaceIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("ns", () => mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void PromoteContextProperty_PropertyNamespaceIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = " ";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("ns", () => mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));

        }

        [Fact]
        public void PromoteContextProperty_PropertyNamespaceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = null;
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentNullException>("ns", () => mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void PromoteContextProperty_PropertyValueIsNull_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = null;

            // Act
            mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextProperty_PropertyValueIsEmpty_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "";

            // Act
            mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextProperty_PropertyValueHasWhitespace_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = " ";

            // Act
            mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextProperty_PropertyValueHasTextValue_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextProperty_ValidInput_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            mockedMessage.Object.PromoteContextProperty(propertyName, propertyNamespace, propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextPropertyTyped_MessageIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IBaseMessage message = (IBaseMessage)null;
            object propertyValue = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>("message", () => message.PromoteContextProperty<WCF.Action>(propertyValue));
        }

        [Fact]
        public void PromoteContextPropertyTyped_MessageContextIsNull_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessage = new Mock<IBaseMessage>();
            object propertyValue = null;

            // Act & Assert
            Assert.Throws<ArgumentException>("message", () => mockedMessage.Object.PromoteContextProperty<WCF.Action>(propertyValue));
        }

        [Fact]
        public void PromoteContextPropertyTyped_PropertyValueIsNull_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = null;

            // Act
            mockedMessage.Object.PromoteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextPropertyTyped_PropertyValueIsEmpty_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "";

            // Act
            mockedMessage.Object.PromoteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextPropertyTyped_PropertyValueHasWhitespace_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = " ";

            // Act
            mockedMessage.Object.PromoteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void PromoteContextPropertyTyped_PropertyValueHasTextValue_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            mockedMessage.Object.PromoteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Promote(propertyName, propertyNamespace, propertyValue), Times.Once);
        }
        
        [Fact]
        public void WriteContextProperty_MessageIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IBaseMessage message = (IBaseMessage)null;

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentNullException>("message", () => message.WriteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void WriteContextProperty_MessageContextIsNull_ThrowsArgumentException()
        {
            // TOCOVER test
            // Arrange
            var mockedMessage = new Mock<IBaseMessage>();

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("message", () => mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void WriteContextProperty_PropertyNameIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("name", () => mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void WriteContextProperty_PropertyNameIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = " ";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("name", () => mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void WriteContextProperty_PropertyNameIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = null;
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentNullException>("name", () => mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void WriteContextProperty_PropertyNamespaceIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("ns", () => mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void WriteContextProperty_PropertyNamespaceIsWhitespace_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = " ";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentException>("ns", () => mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue));

        }

        [Fact]
        public void WriteContextProperty_PropertyNamespaceIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = null;
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            Assert.Throws<ArgumentNullException>("ns", () => mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue));
        }

        [Fact]
        public void WriteContextProperty_ValidInput_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            mockedMessage.Object.WriteContextProperty(propertyName, propertyNamespace, propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Write(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void WriteContextPropertyTyped_MessageIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            IBaseMessage message = (IBaseMessage)null;
            object propertyValue = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>("message", () => message.WriteContextProperty<WCF.Action>(propertyValue));
        }

        [Fact]
        public void WriteContextPropertyTyped_MessageContextIsNull_ThrowsArgumentException()
        {
            // Arrange
            var mockedMessage = new Mock<IBaseMessage>();
            object propertyValue = null;

            // Act & Assert
            Assert.Throws<ArgumentException>("message", () => mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue));
        }

        [Fact]
        public void WriteContextPropertyTyped_PropertyValueIsNull_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = null;

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Write(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void WriteContextPropertyTyped_PropertyValueIsEmpty_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "";

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Write(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void WriteContextPropertyTyped_PropertyValueHasWhitespace_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = " ";

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Write(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

        [Fact]
        public void WriteContextPropertyTyped_PropertyValueHasValue_Succeeds()
        {
            // Arrange
            var mockedMessageContext = new Mock<IBaseMessageContext>();
            var mockedMessage = new Mock<IBaseMessage>();
            mockedMessage.SetupGet(msg => msg.Context).Returns(mockedMessageContext.Object);

            string propertyNamespace = "http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties";
            string propertyName = "Action";
            object propertyValue = "MyValue";

            // Act
            mockedMessage.Object.WriteContextProperty<WCF.Action>(propertyValue);

            // Assert
            mockedMessageContext.Verify(ctx => ctx.Write(propertyName, propertyNamespace, propertyValue), Times.Once);
        }

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

