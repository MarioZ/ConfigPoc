using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ConfigPoc.Tests
{
    [TestClass]
    public class BasicPlaceholderTests
    {
        [TestMethod]
        public void ReplacePlaceholders_WithSameElementProperties()
        {
            UserSection userSection = TestUtilities.GetSection<UserSection>("UserSettings");
            UserElement user = userSection.User;

            string expected = "John Doe";
            Assert.AreEqual(expected, user.FullName);
        }

        [TestMethod]
        public void ReplacePlaceholders_WithParentElementProperties()
        {
            UserSection userSection = TestUtilities.GetSection<UserSection>("UserSettings");
            UserElement user = userSection.User;

            string expected = "Hello John Doe, welcome to MY APP v1.0!";
            Assert.AreEqual(expected, user.WelcomeMessage);
        }

        [TestMethod]
        public void ReplacePlaceholders_InsideCollection()
        {
            UserFileSection userFileSection = TestUtilities.GetSection<UserFileSection>("UserFileSettings");
            FileCollection files = userFileSection.Files;

            string[] expected = new string[]
            {
                @"C:\Users\John\Documents\Word.docx",
                @"C:\Users\John\Documents\Excel.xlsx",
                @"C:\Users\John\Documents\PowerPoint.pptx"
            };
            Assert.IsTrue(expected.SequenceEqual(files.Select(file => file.Path)));
        }

        [TestMethod]
        public void ReplacePlaceholders_WithMissingProperties()
        {
            UserFileSection userFileSection = TestUtilities.GetSection<UserFileSection>("UserFileSettings");
            FileCollection files = userFileSection.Files;

            string expected = @"\Videos\";
            Assert.AreEqual(expected, files.Videos);
        }
    }
}
