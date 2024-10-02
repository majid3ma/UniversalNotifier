using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalNotifier.NET.Models.EmailModels;

namespace UniversalNotifier.Test
{
    [TestFixture]
    public class TemplateReplacementTest
    {
        private class TestNotifierContent : EmailNotifierContent
        {

        }

        private EmailNotifierContent _content;

        [SetUp]
        public void Setup()
        {
            _content = new TestNotifierContent();

        }
        [Test]
        public void test1()
        {

            Assert.AreEqual(1, 1);
        }
    }
}
