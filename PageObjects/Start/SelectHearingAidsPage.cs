using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using HorusUITest.Enums;
using HorusUITest.Extensions;
using HorusUITest.Helper;
using HorusUITest.PageObjects.Controls;
using HorusUITest.PageObjects.Dialogs;
using HorusUITest.PageObjects.Interfaces;
using OpenQA.Selenium.Appium;

namespace HorusUITest.PageObjects.Start
{
    public class SelectHearingAidsPage : BasePage, IHasBackNavigation
    {
        private AppiumWebElement Header => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.InitializationPageHeader");
        private AppiumWebElement Description => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.Description");
        private AppiumWebElement FoundDevicesTitle => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.FoundDevicesTitle");
        private AppiumWebElement IsScanningLabel => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.IsScanningLabel", verifyVisibility: true);
        private AppiumWebElement ListViewElement => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.ListView");
        private IReadOnlyCollection<AppiumWebElement> ListViewCells => App.FindElementsByAutomationId("Horus.Views.Start.SelectHearingAidsPage.ListViewCell");
        private IReadOnlyCollection<AppiumWebElement> ListViewGrids => App.FindElementsByAutomationId("Horus.Views.Start.SelectHearingAidsPage.ListViewGrid");
        private IReadOnlyCollection<AppiumWebElement> ListViewItems => OnAndroid ? ListViewGrids : ListViewCells;
        private Func<AppiumWebElement, AppiumWebElement> DeviceName => (e) => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.DeviceName", root: e);
        private Func<AppiumWebElement, AppiumWebElement> DeviceSwitch => (e) => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.DeviceSwitch", root: e);
        private AppiumWebElement CancelButton => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.CancelButton");
        private AppiumWebElement ConnectButton => App.FindElementByAutomationId("Horus.Views.Start.SelectHearingAidsPage.ConnectButton");


        private ListView listView;

        public SelectHearingAidsPage(bool assertOnPage = true) : base(assertOnPage)
        {
            listView = new ListView(() => ListViewElement, () => ListViewItems, null, null, DeviceName);
            listView.CacheEnabled = false;
        }

        public SelectHearingAidsPage(TimeSpan assertOnPageTimeout) : base(assertOnPageTimeout)
        {
            listView = new ListView(() => ListViewElement, () => ListViewItems, null, null, DeviceName);
            listView.CacheEnabled = false;
        }

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = () => Header,
            iOS = () => Header
        };

        protected override void ClearCache()
        {
            base.ClearCache();
            listView.InvalidateCache();
        }

        public string GetDescription()
        {
            return Description.Text;
        }

        public string GetFoundDevicesTitle()
        {
            return FoundDevicesTitle.Text;
        }

        public bool GetIsScanning()
        {
            if (OnAndroid)
                return TryInvokeQuery(() => IsScanningLabel, out _);
            else
                return true;
        }

        /// <summary>
        /// Waits until the scanning process has finished.
        /// </summary>
        /// <param name="timeout">The maximum waiting time. Defaults to 60 seconds.</param>
        public SelectHearingAidsPage WaitUntilScanFinished(TimeSpan? timeout = null)
        {
            if (timeout == null)
                timeout = TimeSpan.FromSeconds(60);
            App.WaitForNoElement(() => IsScanningLabel, timeout);
            return this;
        }

        /// <summary>
        /// Waits until a device is found.
        /// </summary>
        /// <param name="deviceName">If provided, the method waits for a particular device name.</param>
        /// <param name="timeout">The maximum waiting time. Defaults to 60 seconds.</param>
        public SelectHearingAidsPage WaitUntilDeviceFound(string deviceName = null, TimeSpan? timeout = null)
        {
            if (timeout == null)
                timeout = TimeSpan.FromSeconds(60);
            if (deviceName == null)
                return WaitUntilDevicesFound(1, timeout);
            listView.CacheEnabled = true;
            Wait.UntilTrue(() =>
            {
                listView.InvalidateCache();
                int count = listView.GetNumberOfItemsOnScreen();
                for (int i = 0; i < count; i++)
                {
                    var item = listView.GetItem(i, IndexType.Relative);
                    var label = item.FindElementDontThrow(DeviceName);
                    if (label?.Text == deviceName)
                        return true;
                }
                return false;
            }, timeout.Value, $"Timeout after {timeout.Value} while waiting for hearing aid {deviceName}.");
            listView.CacheEnabled = false;
            return this;
        }

        /// <summary>
        /// Waits until a given number of devices are found.
        /// </summary>
        /// <param name="minimumDeviceCount">The minimum number of devices that must be found.</param>
        /// <param name="timeout">The maximum waiting time. Defaults to 60 seconds.</param>
        public SelectHearingAidsPage WaitUntilDevicesFound(int minimumDeviceCount = 1, TimeSpan? timeout = null)
        {
            if (timeout == null)
                timeout = TimeSpan.FromSeconds(60);
            Wait.UntilTrue(() =>
            {
                if (listView.GetNumberOfItemsOnScreen() >= minimumDeviceCount)
                    return true;
                else
                    return false;
            }, timeout.Value, $"Timeout after {timeout.Value} while waiting for {minimumDeviceCount} hearing aid(s).");
            return this;
        }

        /// <summary>
        /// Waits until the list of found devices and their respective selection status hasn't changed for <paramref name="duration"/> amount of time.
        /// </summary>
        /// <param name="duration">How long the list of devices must not have been changed. Defaults to 5 seconds.</param>
        /// <param name="timeout">Maximum time spent for waiting. Defaults to 45 seconds.</param>
        /// <returns></returns>
        public SelectHearingAidsPage WaitUntilDeviceListNotChanging(TimeSpan? duration = null, TimeSpan? timeout = null)
        {
            duration = duration ?? TimeSpan.FromSeconds(5);
            timeout = timeout ?? TimeSpan.FromSeconds(45);
            List<(string, bool)> previousDevices = GetAllDeviceNamesAndStatuses();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Wait.UntilTrue(() =>
            {
                TimeSpan currentUnchangedDuration = timer.Elapsed;            //elapsed time needs to be saved before fetching the device list since that in itself might take some time
                List<(string, bool)> currentDevices = GetAllDeviceNamesAndStatuses();
                if (currentDevices.SequenceEqual(previousDevices))
                {
                    return currentUnchangedDuration >= duration;
                }
                else
                {
                    previousDevices = currentDevices;
                    timer.Restart();
                    return false;
                }
            }, timeout.Value, $"Timeout after {timeout.Value} while waiting for the scan result list to not change for at least {duration.Value}.");

            return this;
        }

        public bool GetIsDeviceFound(string deviceName, out int index)
        {
            listView.CacheEnabled = true;
            int count = GetNumberOfDevices();
            for (int i = 0; i < count; i++)
            {
                if (GetDeviceName(i) == deviceName)
                {
                    index = i;
                    listView.CacheEnabled = false;
                    return true;
                }
            }
            index = -1;
            listView.CacheEnabled = false;
            return false;
        }

        public bool GetIsDeviceFound(string deviceName)
        {
            return GetIsDeviceFound(deviceName, out _);
        }

        public int GetDeviceIndex(string deviceName)
        {
            if (GetIsDeviceFound(deviceName, out var index))
                return index;
            else
                throw new ArgumentOutOfRangeException($"The given device name does not exist: {deviceName}");
        }

        public int GetDeviceIndex(string deviceName, bool IsExceptionThrown)
        {
            if (!IsExceptionThrown)
            {
                if (GetIsDeviceFound(deviceName, out var index))
                    return index;
                else
                    return -1;
            }
            else
                return GetDeviceIndex(deviceName);
        }

        public int GetNumberOfDevices()
        {
            return listView.GetNumberOfItemsOnScreen();
        }

        public string GetDeviceName(int index)
        {
            return listView.GetItem(index, IndexType.Relative).FindElement(DeviceName).Text;
        }

        public bool GetIsDeviceSelected(int index)
        {
            return App.GetToggleSwitchState(listView.GetItem(index, IndexType.Relative).FindElement(DeviceSwitch));
        }

        public bool GetIsDeviceSelected(string deviceName)
        {
            return GetIsDeviceSelected(GetDeviceIndex(deviceName));
        }

        public bool GetIsDeviceSelected(string deviceName, bool IsExceptionThrown)
        {
            int index = GetDeviceIndex(deviceName, IsExceptionThrown);
            if (index != -1)
                return GetIsDeviceSelected(index);
            else
                return false;
        }

        /// <summary>
        /// Toggles the selection. WARNING: If this leads to no devices being selected, an <see cref="AppDialog>"/> will be shown. A value for <paramref name="keepScanningWhenAsked"/> can be provided to answer the dialog accordingly.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="keepScanningWhenAsked">Defines how a potential <see cref="AppDialog>"/> is answered. If no value is provided, the dialog remains open.</param>
        public SelectHearingAidsPage ToggleDevice(int index, bool? keepScanningWhenAsked = null)
        {
            App.Tap(listView.GetItem(index, IndexType.Relative).FindElement(DeviceSwitch));
            if (keepScanningWhenAsked != null)
            {
                if (keepScanningWhenAsked.Value)
                    DialogHelper.ConfirmIfDisplayed(TimeSpan.FromSeconds(1));
                else
                    DialogHelper.DenyIfDisplayed(TimeSpan.FromSeconds(1));
            }
            return this;
        }

        /// <summary>
        /// Toggles the selection. WARNING: If this leads to no devices being selected, an <see cref="AppDialog>"/> will be shown. A value for <paramref name="keepScanningWhenAsked"/> can be provided to answer the dialog accordingly.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="keepScanningWhenAsked">Defines how a potential <see cref="AppDialog>"/> is answered. If no value is provided, the dialog remains open.</param>
        public SelectHearingAidsPage ToggleDevice(string deviceName, bool? keepScanningWhenAsked = null)
        {
            return ToggleDevice(GetDeviceIndex(deviceName), keepScanningWhenAsked);
        }

        public SelectHearingAidsPage SelectDevice(int index)
        {
            if (!GetIsDeviceSelected(index))
                ToggleDevice(index);
            return this;
        }

        public SelectHearingAidsPage SelectDevice(string deviceName)
        {
            return SelectDevice(GetDeviceIndex(deviceName));
        }

        /// <summary>
        /// Selects the desired devices while making sure that every other device is deselected.
        /// </summary>
        /// <param name="firstDeviceName"></param>
        /// <param name="secondDeviceName"></param>
        /// <returns></returns>
        public SelectHearingAidsPage SelectDevicesExclusively(string firstDeviceName, string secondDeviceName = null)
        {
            void DeselectUnwantedDevices()
            {
                var devicesToDeselect = GetSelectedDeviceNames();
                devicesToDeselect.Remove(firstDeviceName);
                devicesToDeselect.Remove(secondDeviceName);
                foreach (var deviceName in devicesToDeselect)
                {
                    DeselectDevice(deviceName, false);
                }
            }

            DeselectUnwantedDevices();
            SelectDevice(firstDeviceName);
            if (secondDeviceName != null)
                SelectDevice(secondDeviceName);
           //DeselectUnwantedDevices();
           GetIsDeviceSelected(firstDeviceName);
            return this;
        }

        /// <summary>
        /// Deselects the given devices. WARNING: If this leads to no devices being selected, an <see cref="AppDialog"/> will be shown. A value for <paramref name="keepScanningWhenAsked"/> can be provided to answer the dialog accordingly.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="keepScanningWhenAsked">Defines how a potential <see cref="AppDialog>"/> is answered. If no value is provided, the dialog remains open.</param>
        public SelectHearingAidsPage DeselectDevice(int index, bool? keepScanningWhenAsked = null)
        {
            listView.CacheEnabled = true;
            if (GetIsDeviceSelected(index))
                ToggleDevice(index, keepScanningWhenAsked);
            listView.CacheEnabled = false;
            return this;
        }

        /// <summary>
        /// Deselects the given devices. WARNING: If this leads to no devices being selected, an <see cref="AppDialog>"/> will be shown. A value for <paramref name="keepScanningWhenAsked"/> can be provided to answer the dialog accordingly.
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="keepScanningWhenAsked">Defines how a potential <see cref="AppDialog>"/> is answered. If no value is provided, the dialog remains open.</param>
        /// <returns></returns>
        public SelectHearingAidsPage DeselectDevice(string deviceName, bool? keepScanningWhenAsked = null)
        {
            return DeselectDevice(GetDeviceIndex(deviceName), keepScanningWhenAsked);
        }

        private List<string> GetDevicesBySelectionStatus(bool getSelected, bool getDeselected)
        {
            listView.CacheEnabled = true;
            var result = new List<string>();
            int count = GetNumberOfDevices();
            for (int i = 0; i < count; i++)
            {
                if ((getSelected && getDeselected) || (getSelected && GetIsDeviceSelected(i)) || (getDeselected && !GetIsDeviceSelected(i)))
                    result.Add(GetDeviceName(i));
            }
            listView.CacheEnabled = false;
            return result;
        }

        public List<string> GetAllDeviceNames()
        {
            return GetDevicesBySelectionStatus(true, true);
        }

        public List<string> GetSelectedDeviceNames()
        {
            return GetDevicesBySelectionStatus(true, false);
        }

        public List<string> GetDeselectedDeviceNames()
        {
            return GetDevicesBySelectionStatus(false, true);
        }

        public List<(string Name, bool Selected)> GetAllDeviceNamesAndStatuses()
        {
            listView.CacheEnabled = true;
            var result = new List<(string Name, bool Selected)>();
            int count = GetNumberOfDevices();
            for (int i = 0; i < count; i++)
            {
                string name = GetDeviceName(i);
                bool selected = GetIsDeviceSelected(i);
                result.Add((name, selected));
            }
            listView.CacheEnabled = false;
            return result;
        }

        /// <summary>
        /// Navitates back to the <see cref="InitializeHardwarePage"/>.
        /// </summary>
        public void Cancel()
        {
            App.Tap(CancelButton);
        }

        /// <summary>
        /// Get Cancel Text
        /// </summary>
        /// <returns></returns>
        public string GetCancelText()
        {
            return CancelButton.Text;
        }

        /// <summary>
        /// Navigates to <see cref="HearingAidInitPage"/> and (if the connection is successful) automatically proceeds to the <see cref="DashboardPage"/>.
        /// </summary>
        public void Connect()
        {
            App.Tap(ConnectButton);
        }

        public string GetConnectButtonText()
        {
            return ConnectButton.Text;
        }

        public void NavigateBack()
        {
            Cancel();
        }
    }
}
