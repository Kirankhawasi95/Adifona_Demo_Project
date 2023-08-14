using System;
using System.Collections.Generic;
using HorusUITest.Data;
using HorusUITest.Enums;

namespace HorusUITest.Configuration
{
    /// <summary>
    /// Provides smartphone device data of supported phones to be consumed by Appium initialization.
    /// </summary>
    public static class SelectHearingAid
    {
        private static readonly Dictionary<LeftHearingAid, HearingAid> leftHearingAids = new Dictionary<LeftHearingAid, HearingAid>()
        {
            //audifon "Android"
            { LeftHearingAid.Audifon_ST0001_HG_026123, new HearingAid("ST0001 HG#026123", HearingAidModel.RisaR, Side.Left, ChannelMode.Monaural, "TODO") },
            { LeftHearingAid.Audifon_ST0003_HG_026006, new HearingAid("ST0003 HG#026006", HearingAidModel.RisaS, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Audifon_ST0005_HG_030644, new HearingAid("ST0005 HG#030644", HearingAidModel.LewiS, Side.Left, ChannelMode.Binaural, "TODO") },
            
            //audifon "iOS"
            { LeftHearingAid.Audifon_ST0001_HG_026125, new HearingAid("ST0001 HG#026125", HearingAidModel.RisaR, Side.Left, ChannelMode.Monaural, "TODO") },
            { LeftHearingAid.Audifon_ST0003_HG_026008, new HearingAid("ST0003 HG#026008", HearingAidModel.RisaS, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Audifon_ST0005_HG_025863, new HearingAid("ST0005 HG#025863", HearingAidModel.LewiS, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Audifon_Functiona_HG_082280, new HearingAid("Functiona HG#082280", HearingAidModel.LewiS, Side.Left, ChannelMode.Binaural, "TODO") },

            // Microgenisis
            { LeftHearingAid.Microgenisis_Lewi_R_Left_068845, new HearingAid("Lewi R 45 HA#068845", HearingAidModel.LewiR, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Microgenisis_Lewi_R_Left_068840, new HearingAid("Lewi R 40 HA#068840", HearingAidModel.LewiR, Side.Left, ChannelMode.Monaural, "TODO") },
            { LeftHearingAid.Microgenisis_Lewi_R_Left_068843, new HearingAid("Lewi R 43 HA#068843", HearingAidModel.LewiR, Side.Left, ChannelMode.Monaural, "TODO") },
            { LeftHearingAid.Microgenisis_Risa_R_Left_068821, new HearingAid("Risa R 21 HA#068821", HearingAidModel.RisaR, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Microgenisis_Lewi_S_Left_068828, new HearingAid("Lewi S 28 HA#068828", HearingAidModel.LewiS, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Microgenisis_Risa_S_Left_068807, new HearingAid("Risa S 07 HA#068807", HearingAidModel.RisaS, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Microgenisis_Lewi_R_Left_068842, new HearingAid("Lewi R 42 HA#068842", HearingAidModel.LewiR, Side.Left, ChannelMode.Binaural, "TODO") },
            { LeftHearingAid.Microgenisis_Risa_R_Left_068817, new HearingAid("Risa R 17 HA#068817", HearingAidModel.RisaR, Side.Left, ChannelMode.Monaural, "TODO") },
            { LeftHearingAid.Microgenisis_Risa_R_Left_068826, new HearingAid("Risa R 26 HA#068826", HearingAidModel.RisaR, Side.Left, ChannelMode.Binaural, "TODO") },
        };

        private static readonly Dictionary<RightHearingAid, HearingAid> rightHearingAids = new Dictionary<RightHearingAid, HearingAid>()
        {
            //audifon "Android"
            { RightHearingAid.Audifon_ST0002_HG_025956, new HearingAid("ST0002 HG#025956", HearingAidModel.LewiR, Side.Right, ChannelMode.Monaural, "TODO") },
            { RightHearingAid.Audifon_ST0003_HG_026007, new HearingAid("ST0003 HG#026007", HearingAidModel.RisaS, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Audifon_ST0005_HG_030600, new HearingAid("ST0005 HG#030600", HearingAidModel.LewiS, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Audifon_Functiona_HG_082279, new HearingAid("Functiona HG#082279" ,HearingAidModel.LewiS, Side.Right, ChannelMode.Binaural, "TODO") },
            
            //audifon "iOS"
            { RightHearingAid.Audifon_ST0002_HG_025957, new HearingAid("ST0002 HG#025957", HearingAidModel.LewiR, Side.Right, ChannelMode.Monaural, "TODO") },
            { RightHearingAid.Audifon_ST0003_HG_026010, new HearingAid("ST0003 HG#026010", HearingAidModel.RisaS, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Audifon_ST0005_HG_025874, new HearingAid("ST0005 HG#025874", HearingAidModel.LewiS, Side.Right, ChannelMode.Binaural, "TODO") },

            // Microgenisis
            { RightHearingAid.Microgenisis_Lewi_R_Right_068844, new HearingAid("Lewi R 45 HA#068844", HearingAidModel.LewiR, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Microgenisis_Lewi_R_Right_068837, new HearingAid("Lewi R 37 HA#068837", HearingAidModel.LewiR, Side.Right, ChannelMode.Monaural, "TODO") },
            { RightHearingAid.Microgenisis_Risa_R_Right_068818, new HearingAid("Risa R 21 HA#068818", HearingAidModel.RisaR, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Microgenisis_Lewi_S_Right_068836, new HearingAid("Lewi S 28 HA#068836", HearingAidModel.LewiS, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Microgenisis_Risa_S_Right_068812, new HearingAid("Risa S 07 HA#068812", HearingAidModel.RisaS, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Microgenisis_Lewi_R_Right_068846, new HearingAid("Lewi R 42 HA#068846", HearingAidModel.LewiR, Side.Right, ChannelMode.Binaural, "TODO") },
            { RightHearingAid.Microgenisis_Risa_R_Right_068822, new HearingAid("Risa R 26 HA#068822", HearingAidModel.RisaR, Side.Right, ChannelMode.Binaural, "TODO") },
        };

        public static HearingAid Left(string deviceName)
        {
            foreach (var phone in leftHearingAids)
            {
                if (phone.Value.Name.ToLower() == deviceName.ToLower())
                    return phone.Value;
            }
            throw new ArgumentException($"An Android phone with the following name does not exist in {nameof(SelectPhone)}: {deviceName}.");
        }

        /// <summary>
        /// This method has a hard coded value of the left hearing aid and has been configured for both audifon and mgtech testers.
        /// </summary>
        /// <returns>enum value of the left hearing aid name is returned</returns>
        public static HearingAid Left()
        {
            var leftHearingAid = leftHearingAids[LeftHearingAid.Microgenisis_Lewi_R_Left_068845];
            if (AppConfig.DefaultOrganizationName == "audifon")
                leftHearingAid = leftHearingAids[LeftHearingAid.Audifon_ST0003_HG_026006];
            return leftHearingAid;
        }

        /// <summary>
        /// This method has a overload to pass the specific left hearing aid which needs to be tested. 
        /// This method is used for the test cases belonging to the category SystemTestsDeviceSpecificConfig.
        /// For audifon testers left hearing aid need not be passed to this method and will use single hearing aid for SystemTestsDeviceSpecificConfig category test cases.
        /// </summary>
        /// <param name="name">enum value of the left hearing aid is passed</param>
        /// <returns>enum value of the left hearing aid name is returned</returns>
        public static HearingAid Left(LeftHearingAid name)
        {
            var leftHearingAid = leftHearingAids[name];
            return leftHearingAid;
        }

        /// <summary>
        /// This method has a hard coded value of the left hearing aid and has been configured for both audifon and mgtech testers.
        /// </summary>
        /// <returns>enum value of the left hearing aid name is returned</returns>
        public static HearingAid Right()
        {
            var rightHearingAid = rightHearingAids[RightHearingAid.Microgenisis_Lewi_R_Right_068844];
            if (AppConfig.DefaultOrganizationName == "audifon")
                rightHearingAid = rightHearingAids[RightHearingAid.Audifon_ST0003_HG_026007];
            return rightHearingAid;
        }

        /// <summary>
        /// This method has a overload to pass the specific right hearing aid which needs to be tested. 
        /// This method is used for the test cases belonging to the category SystemTestsDeviceSpecificConfig.
        /// For audifon testers right hearing aid need not be passed to this method and will use single hearing aid for SystemTestsDeviceSpecificConfig category test cases.
        /// </summary>
        /// <param name="name">enum value of the right hearing aid is passed</param>
        /// <returns>enum value of the right hearing aid name is returned</returns>
        public static HearingAid Right(RightHearingAid name)
        {
            var rightHearingAid = rightHearingAids[name];
            return rightHearingAid;
        }

        /// <summary>
        /// This method has a hard coded value of the left hearing aid and has been configured for both audifon and mgtech testers. 
        /// </summary>
        /// <returns>string value of the left hearing aid name is returned</returns>
        public static string GetLeftHearingAid()
        {
            var hearingAid = Left(LeftHearingAid.Microgenisis_Lewi_R_Left_068845);
            if (AppConfig.DefaultOrganizationName == "audifon")
                hearingAid = Left(LeftHearingAid.Audifon_ST0003_HG_026006);
            return hearingAid.Name;
        }

        /// <summary>
        /// This method has a overload to pass the specific left hearing aid which needs to be tested. 
        /// This method is used for the test cases belonging to the category SystemTestsDeviceSpecificConfig.
        /// For audifon testers left hearing aid need not be passed to this method and will use single hearing aid for SystemTestsDeviceSpecificConfig category test cases.
        /// </summary>
        /// <param name="leftHearingAid">enum value of the left hearing aid is passed</param>
        /// <returns>string value of the left hearing aid name is returned</returns>
        public static string GetLeftHearingAid(LeftHearingAid leftHearingAid)
        {
            var hearingAid = Left(leftHearingAid);
            return hearingAid.Name;
        }

        /// <summary>
        /// This method has a hard coded value of the right hearing aid and has been configured for both audifon and mgtech testers. 
        /// </summary>
        /// <returns>string value of the right hearing aid name is returned</returns>
        public static string GetRightHearingAid()
        {
            var hearingAid = Right(RightHearingAid.Microgenisis_Lewi_R_Right_068844);
            if (AppConfig.DefaultOrganizationName == "audifon")
                hearingAid = Right(RightHearingAid.Audifon_ST0003_HG_026007);
            return hearingAid.Name;
        }

        /// <summary>
        /// This method has a overload to pass the specific right hearing aid which needs to be tested. 
        /// This method is used for the test cases belonging to the category SystemTestsDeviceSpecificConfig.
        /// For audifon testers right hearing aid need not be passed to this method and will use single hearing aid for SystemTestsDeviceSpecificConfig category test cases.
        /// </summary>
        /// <param name="rightHearingAid">enum value of the right hearing aid is passed</param>
        /// <returns>string value of the right hearing aid name is returned</returns>
        public static string GetRightHearingAid(RightHearingAid rightHearingAid)
        {
            var hearingAid = Right(rightHearingAid);
            return hearingAid.Name;
        }
    }

    public enum LeftHearingAid
    {
        // audifon "Android"
        Audifon_ST0001_HG_026123,
        Audifon_ST0003_HG_026006,
        Audifon_ST0005_HG_030644,
        Audifon_Functiona_HG_082280,

        // audifon "iOS"
        Audifon_ST0001_HG_026125,
        Audifon_ST0003_HG_026008,
        Audifon_ST0005_HG_025863,

        // Microgenisis
        // Lewi R Binaural Normal Configuration
        Microgenisis_Lewi_R_Left_068845,
        // Lewi R Monaural Normal Configuration
        Microgenisis_Lewi_R_Left_068843,
        Microgenisis_Lewi_R_Left_068840,
        // Risa R Binaural Noise Only Configuration
        Microgenisis_Risa_R_Left_068821,
        // Lewi S Normal Configuration
        Microgenisis_Lewi_S_Left_068828,
        // Risa S Normal Configuration
        Microgenisis_Risa_S_Left_068807,
        // Lewi R 1.13.1507 Firmware
        Microgenisis_Lewi_R_Left_068842,
        // Risa R Monanural Left
        Microgenisis_Risa_R_Left_068817,
        // Risa R Binaural Speech in Noise Configuration
        Microgenisis_Risa_R_Left_068826
    }

    public enum RightHearingAid
    {
        // audifon "Android"
        Audifon_ST0002_HG_025956,
        Audifon_ST0003_HG_026007,
        Audifon_ST0005_HG_030600,
        Audifon_Functiona_HG_082279,

        // audifon "iOS"
        Audifon_ST0002_HG_025957,
        Audifon_ST0003_HG_026010,
        Audifon_ST0005_HG_025874,

        // Microgenisis
        // Lewi R Binaural Normal Configuration
        Microgenisis_Lewi_R_Right_068844,
        // Lewi R Monaural Normal Configuration
        Microgenisis_Lewi_R_Right_068837,
        // Risa R Binaural Noise Only Configuration
        Microgenisis_Risa_R_Right_068818,
        // Lewi S Normal Configuration
        Microgenisis_Lewi_S_Right_068836,
        // Risa S Normal Configuration
        Microgenisis_Risa_S_Right_068812,
        // Lewi R 1.13.1507 Firmware
        Microgenisis_Lewi_R_Right_068846,
        // Risa R Binaural Speech in Noise Configuration
        Microgenisis_Risa_R_Right_068822
    }
}