namespace HorusUITest.Configuration
{
    using System.Collections.Generic;
    using HorusUITest.Data;
    using HorusUITest.Helper;

    /// <summary>
    /// Provides smartphone device data of supported phones to be consumed by Appium initialization.
    /// </summary>
    public static class SelectPhone
    {
        public static string uId;

        private static readonly Dictionary<AndroidPhoneName, AndroidPhone> androidPhones =
            new Dictionary<AndroidPhoneName, AndroidPhone>()
            {
                // audifon
                {
                    AndroidPhoneName.Audifon_Huawei_P20_Lite,
                    new AndroidPhone("Huawei P20 lite", "9WVDU18317011433", "Android 8.0.0")
                },
                // { AndroidPhoneName.Audifon_LG_G5, new AndroidPhone("LG G5", "LGH840797cd06f", "Android 6.0.1") },
                {
                    AndroidPhoneName.Audifon_HTC_10, new AndroidPhone("audifon HTC 10", "FA68JBN01661", "Android 8.0.0")
                },
                { AndroidPhoneName.Audifon_Galaxy_A7, new AndroidPhone("Galaxy A7", "320080cf5301c545", "Android 9") },
                {
                    AndroidPhoneName.Audifon_Galaxy_S7,
                    new AndroidPhone("Galaxy S7", "ad081603308804b984", "Android 8.0")
                },
                {
                    AndroidPhoneName.Audifon_Nokia_7_plus,
                    new AndroidPhone("Nokia 7 plus", "B2NGAA8881804506", "Android 10")
                },
                { AndroidPhoneName.Audifon_Honor_20, new AndroidPhone("HONOR 20", "NXEDU19830007162", "Android 10") },
                {
                    AndroidPhoneName.Audifon_Find_X2_Pro,
                    new AndroidPhone("Oppo Find X2 Pro", "65913299 ", "Android 11")
                },

                // Mgtech
                {
                    AndroidPhoneName.Microgenisis_Samsung_Galaxy_Note_20,
                    new AndroidPhone("Galaxy Note20 Ultra 5G", "R5CR71PFSZP", "Android 13")
                },
                {
                    AndroidPhoneName.Microgenesis_Samsung_Tab_A7,
                    new AndroidPhone("Galaxy Tab A7", "R9AR712377V", "Android 11.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_Samsung_Galaxy_S_20_FE,
                    new AndroidPhone("Galaxy S20 FE 5G", "RZCT20B347W", "Android 12.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_Samsung_Galaxy_A_51,
                    new AndroidPhone("GalaxyA51", "RZ8R30NJJ8A", "Android 11.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_Samsung_Galaxy_A_12,
                    new AndroidPhone("GalaxyA12", "RZ8T308B1ET", "Android 11.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_Redmi_9,
                    new AndroidPhone("Redmi 9", "9HVKJ7U4Y5MF689", "Android 10.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_Samsung_Galaxy_A_52,
                    new AndroidPhone("Galaxy A52s 5G", "RZCRA04PNSX", "Android 11.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_Moto_G_30,
                    new AndroidPhone("moto g(30)", "ZF6526RVS8", "Android 11.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_Google_Pixle_6,
                    new AndroidPhone("Pixel 6", "23261FDF60044S", "Android 12.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_POCO_M2_Pro,
                    new AndroidPhone("POCO M2 Pro", "68ccbad7", "Android 10.0.0")
                },
                {
                    AndroidPhoneName.Microgenesis_OPPO_Find_X2_Pro,
                    new AndroidPhone("OPPO Reno6 pro 5G", "MFZX99MFHYLV6TWW", "Android 11.0.0")
                },
            };

        private static readonly Dictionary<IosPhoneName, IosPhone> iosPhones = new Dictionary<IosPhoneName, IosPhone>()
        {
            //audifon
            // { IosPhoneName.Audifon_iPhone_5s, new IosPhone("audifon iPhone 5s", "eed848dbfd37b87a6c6de83df050ed6a5d8eeb1e", "iOS 12.4.3") },
            // { IosPhoneName.Audifon_iPhone_6, new IosPhone("audifon iPhone 6", "4785ea490160ccea482434b91a5fba79da6b4d47", "iOS 12.4.7") },
            {
                IosPhoneName.Audifon_iPhone_7,
                new IosPhone("audifon iPhone 7", "98a5ce3685b0acf2d8aaba567c7b6ab160f45021", "iOS 15.4.1")
            },
            {
                IosPhoneName.Audifon_iPhone_7_Plus,
                new IosPhone("audifon iPhone 7Plus", "1124bed998df46a0a2c7e63e1912e4995b203f26", "iOS 13.3.1")
            },
            {
                IosPhoneName.Audifon_iPhone_SE,
                new IosPhone("audifon iPhone SE", "00008030-001E5CC8212B802E", "iOS 13.6")
            },
            {
                IosPhoneName.Audifon_iPhone_8,
                new IosPhone("audifon iPhone 8", "dccb66203f4055186901c1f93ea1c767d0835fe1", "iOS 13.5.1")
            },

            // Mgtech Phones
            {
                IosPhoneName.Microgenisis_iPhone_11,
                new IosPhone("iPhone 11", "00008030-001D683A34E8C02E", "iOS 15.4")
            },
            {
                IosPhoneName.Microgenisis_iPhone_12,
                new IosPhone("iPhone 12", "00008101-0006541434E1001E", "iOS 15.4.1")
            },
            {
                IosPhoneName.Microgenisis_iPhone_12_Mini,
                new IosPhone("iPhone 12 Mini", "00008101-000C51DA0C30001E", "iOS 15.3.1")
            },
            {
                IosPhoneName.Microgenisis_iPhone_13,
                new IosPhone("iPhone 13", "00008110-000229DA14FB801E", "iOS 15.4.1")
            },
            {
                IosPhoneName.Microgenisis_iPhone_13_Pro_Max,
                new IosPhone("iPhone 13 Pro Max", "00008110-001631062180401E", "iOS 15.4.1")
            },
            {
                IosPhoneName.Microgenisis_iPhone_SE_2020,
                new IosPhone("iPhone SE 2020", "00008110-000964D236D0401E", "iOS 15.4")
            },
        };

        public static AndroidPhone Android(AndroidPhoneName name)
        {
            return androidPhones[name];
        }

        public static IosPhone Ios(IosPhoneName name)
        {
            return iosPhones[name];
        }

        public static IosPhone GetPhoneByUdid(string iOSDeviceId)
        {
            foreach (var key in iosPhones.Keys)
            {
                if (iosPhones[key].Udid == iOSDeviceId)
                {
                    Output.Immediately(
                        "Successful detect connected iPhone. Now setting deteced device as DefaultIosPhone for testing.");
                    return iosPhones[key];
                }

            }

            return null;
        }

        public static string GetFirstiOSDevice()
        {
            Output.Immediately(
                "No iPhone explicitly given for testing. Try Auto-detection using 'idevice_id' to get connected iPhone!");
            var command = "idevice_id -l";

            var response = string.Empty;
            try
            {
                response = CommandUtils.ExecuteBash(command);
            }
            catch
            {
                Output.Immediately(
                    "Test platform does not support bash command line tool. Bash command will not work on Windows machine.");
            }

            foreach (var s in response.Replace("\r", "").Split('\n'))
            {
                uId = s.Split('\t')[0];
                return uId;
            }

            return null;
        }
    }

    public enum AndroidPhoneName
    {
        // audifon
        Audifon_Huawei_P20_Lite,
        Audifon_LG_G5,
        Audifon_HTC_10,
        Audifon_Galaxy_A7,
        Audifon_Galaxy_S7,
        Audifon_Nokia_7_plus,
        Audifon_Honor_20,
        Audifon_Find_X2_Pro,
        Galaxy_S20,

        // Mgtech
        Microgenisis_Samsung_Galaxy_Note_20,
        Microgenesis_Samsung_Tab_A7,
        Microgenesis_Samsung_Galaxy_S_20_FE,
        Microgenesis_Samsung_Galaxy_A_51,
        Microgenesis_Samsung_Galaxy_A_12,
        Microgenesis_Redmi_9,
        Microgenesis_Samsung_Galaxy_A_52,
        Microgenesis_Moto_G_30,
        Microgenesis_Google_Pixle_6,
        Microgenesis_POCO_M2_Pro,
        Microgenesis_OPPO_Find_X2_Pro
    }

    public enum IosPhoneName
    {
        // audifon
        Audifon_iPhone_5s,
        Audifon_iPhone_6,
        Audifon_iPhone_7,
        Audifon_iPhone_7_Plus,
        Audifon_iPhone_SE,
        Audifon_iPhone_8,

        // Mgtech
        Microgenisis_iPhone_11,
        Microgenisis_iPhone_12,
        Microgenisis_iPhone_12_Mini,
        Microgenisis_iPhone_13,
        Microgenisis_iPhone_13_Pro_Max,
        Microgenisis_iPhone_SE_2020
    }
}