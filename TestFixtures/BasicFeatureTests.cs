using HorusUITest.Helper;
using HorusUITest.PageObjects.Menu;
using HorusUITest.PageObjects.Menu.Programs;
using NUnit.Framework;
using Platform = HorusUITest.Enums.Platform;

namespace HorusUITest.TestFixtures
{
    public class BasicFeatureTests : BaseResettingTestFixture
    {
        public BasicFeatureTests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        [Category("BasicFeatureTest")]
        public void TestSubMenuBackNavigation()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans()
                .Page.OpenMenuUsingTap();
            new MainMenuPage().OpenPrograms();
            new ProgramsMenuPage().TapBack();
            new MainMenuPage().OpenPrograms();
            new ProgramsMenuPage().SwipeBack();
            new MainMenuPage().CloseMenuUsingTap();
        }

        [Test]
        [Category("BasicFeatureTest")]
        public void OpenAndCloseMainMenu()
        {
            LaunchHelper.StartAppInDemoModeByAnyMeans()
                .Page.OpenMenuUsingTap();
            new MainMenuPage().CloseMenuUsingTap();
        }
    }
}
