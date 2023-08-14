using System;

namespace HorusUITest.PageObjects.Start.Intro
{
    public class IntroPageOne : WelcomePage
    {
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Horus.Views.Start.Intro.IntroPageOne.introPage1_Title"),
            iOS = x => x.Marked("Horus.Views.Start.Intro.IntroPageOne.introPage1_Title")
        };
    }
}
