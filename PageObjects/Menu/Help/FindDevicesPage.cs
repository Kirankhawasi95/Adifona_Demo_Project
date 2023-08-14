using System;
using System.Threading;
using AventStack.ExtentReports;
using HorusUITest.Data;
using HorusUITest.Enums;
using HorusUITest.Helper;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Menu.Help
{
    public class FindDevicesPage : BaseNavigationPage
    {
        public FindDevicesPage(bool assertOnPage = true) : base(assertOnPage)
        {
        }

        public FindDevicesPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
        }

        protected override AppiumWebElement TraitNavBar => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.NavigationBar");

        private AppiumWebElement LeftDeviceTab => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.LeftDeviceTab");
        private AppiumWebElement LeftDeviceTabSelected => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.LeftDeviceTabSelected");
        private AppiumWebElement RightDeviceTab => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.RightDeviceTab");
        private AppiumWebElement RightDeviceTabSelected => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.RightDeviceTabSelected");
        private AppiumWebElement MapGrid => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.MapGrid", verifyVisibility: true);
        private AppiumWebElement CustomMapView => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.CustomMapView");
        private AppiumWebElement NearFieldGrid => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.NearFieldGrid", verifyVisibility: true);
        private AppiumWebElement ChangeViewButton => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.ChangeViewButton");
        private AppiumWebElement LeftSignalStrengthControl => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.LeftSignalStrengthControl", verifyVisibility: true);
        private AppiumWebElement RightSignalStrengthControl => App.FindElementByAutomationId("Horus.Views.Menu.Help.FindDevicesPage.RightSignalStrengthControl", verifyVisibility: true);

        //TODO Low priority: Add map functionalities.

        public string GetLeftDeviceText()
        {
            return LeftDeviceTab.Text;
        }

        public string GetRightDeviceText()
        {
            return RightDeviceTab.Text;
        }

        public string GetToggleViewButtonText()
        {
            return ChangeViewButton.Text;
        }

        public bool GetIsLeftDeviceSelected()
        {
            return TryInvokeQuery(() => LeftDeviceTabSelected, out _);
        }

        public bool GetIsRightDeviceSelected()
        {
            return TryInvokeQuery(() => RightDeviceTabSelected, out _);
        }

        public bool GetIsMapViewSelected()
        {
            return TryInvokeQuery(() => MapGrid, out _);
        }

        public bool GetIsNearFieldViewSelected()
        {
            return TryInvokeQuery(() => NearFieldGrid, out _);
        }

        public bool GetIsLeftSignalStrengthControlVisible()
        {
            return TryInvokeQuery(() => LeftSignalStrengthControl, out _);
        }

        public bool GetIsRightSignalStrengthControlVisible()
        {
            return TryInvokeQuery(() => RightSignalStrengthControl, out _);
        }

        public FindDevicesPage SelectLeftDevice()
        {
            App.Tap(LeftDeviceTab);
            return this;
        }

        public FindDevicesPage SelectRightDevice()
        {
            App.Tap(RightDeviceTab);
            return this;
        }

        public FindDevicesPage ToggleView()
        {
            App.Tap(ChangeViewButton);
            Thread.Sleep(1000);         //The app takes 600 ms to fade the controls. 400 ms extra just for good measure.
            return this;
        }

        public FindDevicesPage SelectMapView()
        {
            if (!GetIsMapViewSelected())
                ToggleView();
            return this;
        }

        public FindDevicesPage SelectNearFieldView()
        {
            if (!GetIsNearFieldViewSelected())
                ToggleView();
            return this;
        }
        /// <summary>
        /// The text shown on Map view is slightly different on iOS hence the if else condition
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        public bool GetIsHearingAidVisibleOnMap(string deviceName, string side)
        {
            string iOSDeviceAID = deviceName + " " + side;
            string deviceAID = deviceName + " " + side + "." + " ";
            Console.WriteLine(deviceAID);

            if(OniOS)
                return App.WaitForElement(() => App.FindElementByAutomationId(iOSDeviceAID)).Displayed;
            else
                return App.WaitForElement(() => App.FindElementByAutomationId(deviceAID)).Displayed;
       
           
        }

        public FindDevicesPage CheckFindDevicesPage(ChannelMode channel, HearingAid firstHearingAid, HearingAid secondHearingAid = null)
        {
            ReportHelper.LogTest(Status.Info, "Checking if first hearing aid initialized...");
            Assert.IsNotNull(firstHearingAid, "First hearing aid not initialized");
            ReportHelper.LogTest(Status.Info, "First hearing aid initialized");
            ReportHelper.LogTest(Status.Info, "Waiting till find devices page is loaded...");
            Assert.IsTrue(new FindDevicesPage().IsShowingBeforeTimeout(TimeSpan.FromSeconds(5)), "Find devices page is not loaded");
            var findDevicesPage = new FindDevicesPage();
            ReportHelper.LogTest(Status.Info, "Find devices page is loaded");
            ReportHelper.LogTest(Status.Info, "Checking if map view is selected...");
            Assert.IsTrue(findDevicesPage.GetIsMapViewSelected(), "Map view is not selected");
            ReportHelper.LogTest(Status.Info, "Map view is selected");
            ReportHelper.LogTest(Status.Info, "Checking toggle view button...");
            Assert.IsNotEmpty(findDevicesPage.GetToggleViewButtonText(), "Toggle view button text is empty");
            ReportHelper.LogTest(Status.Info, "Toggle view button text is not empty");

            switch (channel)
            {
                case ChannelMode.Monaural:
                    {
                        switch (firstHearingAid.Side)
                        {
                            case Side.Left:
                                {
                                    ReportHelper.LogTest(Status.Info, "Selecting left tab...");
                                    findDevicesPage.SelectLeftDevice();
                                    ReportHelper.LogTest(Status.Info, "Selected left tab");
                                    ReportHelper.LogTest(Status.Info, "Checking if left tab is selected...");
                                    Assert.IsTrue(findDevicesPage.GetIsLeftDeviceSelected(), "Monaural Left-: Preselected device is incorrect");
                                    ReportHelper.LogTest(Status.Pass, "Left side is preselected in monaural left config");
                                    Thread.Sleep(3000);
                                    ReportHelper.LogTest(Status.Info, "Checking if left hearing aid is visible on map...");
                                    findDevicesPage.GetIsHearingAidVisibleOnMap(firstHearingAid.Name, firstHearingAid.Side.ToString());
                                    ReportHelper.LogTest(Status.Pass, "Monaural Left-: hearing aid is visible on map view");
                                    ReportHelper.LogTest(Status.Info, "Checking if left device is selected...");
                                    Assert.IsTrue(findDevicesPage.SelectNearFieldView().GetIsLeftDeviceSelected(), "Left device is not selected");
                                    ReportHelper.LogTest(Status.Info, "Left device is selected");
                                    ReportHelper.LogTest(Status.Info, "Checking if left device signal strength is visible...");
                                    Assert.IsTrue(findDevicesPage.GetIsLeftSignalStrengthControlVisible(), "Left device signal strength is not visible");
                                    ReportHelper.LogTest(Status.Info, "Left device signal strength is visible");
                                    break;
                                }
                            case Side.Right:
                                {
                                    ReportHelper.LogTest(Status.Info, "Selecting right tab...");
                                    findDevicesPage.SelectRightDevice();
                                    ReportHelper.LogTest(Status.Info, "Selected right tab");
                                    ReportHelper.LogTest(Status.Info, "Checking if right tab is selected...");
                                    Assert.IsTrue(findDevicesPage.GetIsRightDeviceSelected(), "Monaural Right-: Preselected device is incorrect");
                                    ReportHelper.LogTest(Status.Pass, "Right side is preselected in monaural left config");
                                    Thread.Sleep(3000);
                                    ReportHelper.LogTest(Status.Info, "Checking if right hearing aid is visible on map...");
                                    findDevicesPage.GetIsHearingAidVisibleOnMap(firstHearingAid.Name, firstHearingAid.Side.ToString());
                                    ReportHelper.LogTest(Status.Info, "Monaural Right-: hearing aid is visible on map view");
                                    ReportHelper.LogTest(Status.Info, "Checking if right device is selected...");
                                    Assert.IsTrue(findDevicesPage.SelectNearFieldView().GetIsRightDeviceSelected(), "Right device is not selected");
                                    ReportHelper.LogTest(Status.Info, "Right device is selected");
                                    ReportHelper.LogTest(Status.Info, "Checking if right device signal strength is visible...");
                                    Assert.IsTrue(findDevicesPage.GetIsRightSignalStrengthControlVisible(), "Right device signal strength is not visible");
                                    ReportHelper.LogTest(Status.Info, "Right device signal strength is visible");
                                    break;
                                }
                            default:
                                {
                                    throw new Exception("Unexpected side.");
                                }
                        }
                        break;
                    }
                case ChannelMode.Binaural:
                    {
                        ReportHelper.LogTest(Status.Info, "Checking if second hearing aid initialized...");
                        Assert.IsNotNull(secondHearingAid, "For Binaural, second hearing aid not initialized");
                        ReportHelper.LogTest(Status.Info, "Second hearing aid initialized");

                        ReportHelper.LogTest(Status.Info, "Selecting left tab...");
                        findDevicesPage.SelectLeftDevice();
                        ReportHelper.LogTest(Status.Info, "Selected left tab");
                        ReportHelper.LogTest(Status.Info, "Checking if left tab is selected...");
                        Assert.IsTrue(findDevicesPage.GetIsLeftDeviceSelected(), "Binaural -: Preselected device is incorrect");
                        ReportHelper.LogTest(Status.Pass, "Left side is preselected in binaural config");
                        Thread.Sleep(3000);
                        ReportHelper.LogTest(Status.Info, "Checking if left hearing aid is visible on map...");
                        findDevicesPage.GetIsHearingAidVisibleOnMap(firstHearingAid.Name, firstHearingAid.Side.ToString());
                        ReportHelper.LogTest(Status.Pass, "Binaural Left-: hearing aid is visible on map view");

                        ReportHelper.LogTest(Status.Info, "Selecting right tab...");
                        findDevicesPage.SelectRightDevice();
                        ReportHelper.LogTest(Status.Info, "Selected right tab");
                        ReportHelper.LogTest(Status.Info, "Checking if right tab is selected...");
                        Assert.IsTrue(findDevicesPage.GetIsRightDeviceSelected(), "Right tab is not");
                        ReportHelper.LogTest(Status.Pass, "Right side is preselected in binaural config");
                        Thread.Sleep(3000);
                        ReportHelper.LogTest(Status.Info, "Checking if right hearing aid is visible on map...");
                        findDevicesPage.GetIsHearingAidVisibleOnMap(secondHearingAid.Name, secondHearingAid.Side.ToString());
                        ReportHelper.LogTest(Status.Pass, "Binaural Right-: hearing aid is visible on map view");
                        ReportHelper.LogTest(Status.Info, "Checking if right device is selected...");
                        Assert.IsTrue(findDevicesPage.SelectNearFieldView().GetIsRightDeviceSelected(), "Right device is not selected");
                        ReportHelper.LogTest(Status.Info, "Right device is selected");
                        ReportHelper.LogTest(Status.Info, "Checking if right device signal strength is visible...");
                        Assert.IsTrue(findDevicesPage.GetIsRightSignalStrengthControlVisible(), "Right device signal strength is not visible");
                        ReportHelper.LogTest(Status.Info, "Right device signal strength is visible");
                        break;
                    }
                default:
                    {
                        throw new Exception("Unexpected channel.");
                    }
            }

            return new FindDevicesPage();
        }
    }
}
