using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace Hymnal.UI.Test
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TestPro()
        {
            //app.Repl();

            // Arrange
            app.WaitForElement("OpenHymnButton");
            app.Tap("HymnNumberEntry");
            app.EnterText("133");
            app.DismissKeyboard();

            // Act
            app.Tap("OpenHymnButton");
            app.WaitForElement("HymnTitleLabel");
            app.Back();
            app.WaitForElement("OpenHymnButton");

            // Asset
            var restul = app.Query(e => e.Marked("#")).Any();
            Assert.IsTrue(restul);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
    }
}

