using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ConfigPoc.Tests
{
    [TestClass]
    public class AdvancePlaceholderTests
    {
        [TestMethod]
        public void ReplacePlaceholder_WithPropertyCollision()
        {
            UserBankSection userBankSection = TestUtilities.GetSection<UserBankSection>("UserBankSettings");
            DisplayElement display = userBankSection.Account.Display;

            string expected = "Your account name is MY ACCOUNT.";
            Assert.AreEqual(expected, display.Name);

        }

        [TestMethod]
        public void ReplacePlaceholder_WithFormattedNumber()
        {
            UserBankSection userBankSection = TestUtilities.GetSection<UserBankSection>("UserBankSettings");
            DisplayElement display = userBankSection.Account.Display;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            string expected = "Your account balance is $ 10,200.00.";
            Assert.AreEqual(expected, display.State);
        }

        [TestMethod]
        public void ReplacePlaceholder_WithFormattedDate()
        {
            UserBankSection userBankSection = TestUtilities.GetSection<UserBankSection>("UserBankSettings");
            DisplayElement display = userBankSection.Account.Display;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            string expected = "Your account expires on 10 January 2000.";
            Assert.AreEqual(expected, display.Update);
        }

        [TestMethod]
        public void ReplacePlaceholder_WithNonformattedValue()
        {
            UserBankSection userBankSection = TestUtilities.GetSection<UserBankSection>("UserBankSettings");
            DisplayElement display = userBankSection.Account.Display;

            string expected = "Your account was created on Five Years Ago";
            Assert.AreEqual(expected, display.MissingDateValue);
        }

        [TestMethod]
        public void ReplaceGlobalPlaceholder_WithAppSettingsProperty()
        {
            UserProfileSection userProfileSection = TestUtilities.GetSection<UserProfileSection>("UserProfileSettings");
            SocialMediaCollection socialMedias = userProfileSection.SocialMedias;

            string[] expected = new string[]
            {
                "https://www.facebook.com/john_doe_profile",
                "https://twitter.com/john_doe_profile"
            };
            Assert.IsTrue(expected.SequenceEqual(socialMedias.Select(socialMedia => socialMedia.Location)));
        }

        [TestMethod]
        public void ReplaceGlobalPlaceholder_WithFormattedNumber()
        {
            UserProfileSection userProfileSection = TestUtilities.GetSection<UserProfileSection>("UserProfileSettings");
            BlogElement blog = userProfileSection.Blog;

            string expected = "There are total of 12 posts until 2020.";
            Assert.AreEqual(expected, blog.PostsMessage);
        }

        [TestMethod]
        public void ReplaceGlobalPlaceholder_WithMissingAppSettingsProperty()
        {
            UserProfileSection userProfileSection = TestUtilities.GetSection<UserProfileSection>("UserProfileSettings");
            BlogElement blog = userProfileSection.Blog;
            
            string expected = "john_doe_profile";
            Assert.AreEqual(expected, blog.Location);
        }
    }
}
