using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalNotifier.NET.Models;
using UniversalNotifier.NET.Models.EmailModels;

namespace UniversalNotifier.Test
{
    [TestFixture]
    public class TemplateReplacementTest
    {
        private class TestNotifierContent : NotifierContent
        {

        }

        private NotifierContent _content;
        private string mainTemplate = "Hello %name%, your order {order} with ID {{id}} is confirmed!";

        [SetUp]
        public void Setup()
        {
            _content = new TestNotifierContent();
        }
        [Test]
        public void Test_TemplateReplacer_ReplacesCorrectly()
        {
            // Arrange
            var replacements = new Dictionary<string, string>()
            {
                { "name", "John" },
                { "order", "Laptop" },
                { "id", "12345" }
            };

            // Act
            var result =
                _content.ParseContent(mainTemplate, replacements);
            // Assert
            var validResult = "Hello John, your order Laptop with ID 12345 is confirmed!";
            Assert.That(validResult, Is.EqualTo(result));
        }

        [Test]
        public void Test_TemplateReplacer_NoReplacementIfKeyNotFound()
        {
            // Arrange
            var replacements = new Dictionary<string, string>()
            {
                { "name", "John" }
                // 'order' and 'id' keys are not provided
            };

            // Act
            var result =
                _content.ParseContent(mainTemplate, replacements);
            var validResult = "Hello John, your order {order} with ID {{id}} is confirmed!";
            // Assert
            Assert.That(validResult, Is.EqualTo(result));
        }

        [Test]
        public void Test_TemplateReplacer_EmptyTemplate()
        {
            // Arrange
            string template = "";
            var replacements = new Dictionary<string, string>()
            {
                { "name", "John" }
            };

            // Act
            var result =
                _content.ParseContent(template, replacements);

            var validResult = "";
            // Assert
            Assert.That(validResult, Is.EqualTo(result));
        }
    }
}

