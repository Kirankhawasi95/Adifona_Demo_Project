using HorusUITest.Configuration;

namespace HorusUITest.Enums
{
    public enum HearingAidModel
    {
        LewiR,              // lewi R
        KindWings2400,      // KINDwings 2400
        RicCharmK820,       // RIC Charm K820
        SombraXR,           // Sombra XR
        Ev16R,              // Ev16-R
        BT2R,               // BT2R

        LewiS,              // lewi S
        KindWings2200,      // KINDwings 2200
        BteCharmK820,       // BTE Charm K820
        SombraXS,           // Sombra XS
        Ev16S,              // Ev16-S
        BT2S,               // BT2S

        RisaR,              // risa R
        KindWings1400,      // KINDwings 1400
        RicCharmN520,       // RIC Charm N520
        SombraVR,           // Sombra VR
        BT1R,               // BT1R

        RisaS,              // risa S
        KindWings1200,      // KINDwings 1200
        BteCharmN520,       // BTE Charm N520
        SombraVS,           // Sombra VS
        BT1S                // BT1S
    }

    public static class HearingAidModeLExtensions
    {
        public static string GetDisplayName(this HearingAidModel model)
        {
            return HearingAidModels.GetName(model);
        }

        public static string GetDisplayName(this HearingAidModel model, Brand brand)
        {
            return HearingAidModels.GetName(model, brand);
        }

        public static HearingAidModel ToBrandSpecific(this HearingAidModel model, Brand brand)
        {
            return HearingAidModels.GetBrandSpecificModel(model, brand);
        }
    }
}
