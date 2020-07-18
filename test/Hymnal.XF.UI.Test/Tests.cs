using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;

namespace Hymnal.UI.Test
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
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
        public void OpenHymnUsingNumber()
        {
            //app.Repl();

            // Arranque
            app.WaitForElement("NumberPage");

            // Act
            app.Tap("HymnNumberEntry");
            app.EnterText("133");
            app.DismissKeyboard();
            app.Tap("OpenHymnButton");
            app.WaitForElement("HymnPage");
            app.Back();
            app.WaitForElement("NumberPage");

            // Assert
            var result = app.Query(e => e.Marked("NumberPage")).Any();
            Assert.IsTrue(result);
        }

        [Test]
        public void OpenHymnUsingSearch()
        {

            // Arranque
            app.WaitForElement("NumberPage");

            // Act
            app.Tap("OpenSearchPageToolbarItem");

            app.WaitForElement("SearchPage");
            app.Tap("HymnSearchBar");
            app.EnterText("13");
            app.DismissKeyboard();
            app.Tap("2_ViewCell_Grid");

            app.WaitForElement("HymnPage");
            app.Back();

            app.WaitForElement("SearchPage");
            app.DismissKeyboard();
            app.Back();

            app.WaitForElement("NumberPage");

            // Assert
            var result = app.Query(e => e.Marked("NumberPage")).Any();
            Assert.IsTrue(result);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
    }
}

