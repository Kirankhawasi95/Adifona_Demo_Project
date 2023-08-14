using System.Collections.Generic;
using System.Linq;
using HorusUITest.Enums;
using System.Threading;

namespace HorusUITest.Configuration
{
    /// <summary>
    /// Provides names of the supported hearing aids.
    /// </summary>
    public static class HearingAidModels
    {
        private static List<Aid> aids = new List<Aid>()
        {
            //family 'lewi R'
            new Aid(HearingAidModel.LewiR, HearingAidModel.LewiR, Brand.Audifon, "lewi R"),
            new Aid(HearingAidModel.KindWings2400, HearingAidModel.LewiR, Brand.Kind, "KINDwings 2400"),
            new Aid(HearingAidModel.RicCharmK820, HearingAidModel.LewiR, Brand.HuiEr, "RIC Charm K820"),
            new Aid(HearingAidModel.SombraXR, HearingAidModel.LewiR, Brand.PersonaMedical, "Sombra XR"),
            new Aid(HearingAidModel.Ev16R, HearingAidModel.LewiR, Brand.Puretone, "Ev16-R"),
            new Aid(HearingAidModel.LewiR, HearingAidModel.LewiR, Brand.Hormann, "spatial 4.0"),
            new Aid(HearingAidModel.BT2R, HearingAidModel.LewiR, Brand.RxEarsPro, "BT2R"),

            //family 'lewi S'
            new Aid(HearingAidModel.LewiS, HearingAidModel.LewiS, Brand.Audifon, "lewi S"),
            new Aid(HearingAidModel.KindWings2200, HearingAidModel.LewiS, Brand.Kind, "KINDwings 2200"),
            new Aid(HearingAidModel.BteCharmK820, HearingAidModel.LewiS, Brand.HuiEr, "BTE Charm K820"),
            new Aid(HearingAidModel.SombraXS, HearingAidModel.LewiS, Brand.PersonaMedical, "Sombra XS"),
            new Aid(HearingAidModel.Ev16S, HearingAidModel.LewiS, Brand.Puretone, "Ev16-S"),
            new Aid(HearingAidModel.LewiS, HearingAidModel.LewiS, Brand.Hormann, "spatial 2.0"),
            new Aid(HearingAidModel.BT2S, HearingAidModel.LewiS, Brand.RxEarsPro, "BT2S"),

            //family 'risa R'
            new Aid(HearingAidModel.RisaR, HearingAidModel.RisaR, Brand.Audifon, "risa R"),
            new Aid(HearingAidModel.KindWings1400, HearingAidModel.RisaR, Brand.Kind, "KINDwings 1400"),
            new Aid(HearingAidModel.RicCharmN520, HearingAidModel.RisaR, Brand.HuiEr, "RIC Charm N520"),
            new Aid(HearingAidModel.SombraVR, HearingAidModel.RisaR, Brand.PersonaMedical, "Sombra VR"),
            new Aid(HearingAidModel.RisaR, HearingAidModel.RisaR, Brand.Hormann, "natural 4.0"),
            new Aid(HearingAidModel.BT1R, HearingAidModel.RisaR, Brand.RxEarsPro, "BT1R"),

            //family 'risa S'
            new Aid(HearingAidModel.RisaS, HearingAidModel.RisaS, Brand.Audifon, "risa S"),
            new Aid(HearingAidModel.KindWings1200, HearingAidModel.RisaS, Brand.Kind, "KINDwings 1200"),
            new Aid(HearingAidModel.BteCharmN520, HearingAidModel.RisaS, Brand.HuiEr, "BTE Charm N520"),
            new Aid(HearingAidModel.SombraVS, HearingAidModel.RisaS, Brand.PersonaMedical, "Sombra VS"),
            new Aid(HearingAidModel.RisaS, HearingAidModel.RisaS, Brand.Hormann, "natural 2.0"),
            new Aid(HearingAidModel.BT1S, HearingAidModel.RisaS, Brand.RxEarsPro, "BT1S"),
        };

        /// <summary>
        /// Returns the <see cref="Brand"/>-specific <see cref="HearingAidModel"/> of the model family represented by <paramref name="model"/>.
        /// </summary>
        /// <param name="model">Any hearing aid type of a given device family.</param>
        /// <param name="brand">The OEM brand to convert to.</param>
        /// <returns></returns>
        public static HearingAidModel GetBrandSpecificModel(HearingAidModel model, Brand brand)
        {
            var family = aids.FirstOrDefault((x) => x.Model == model).Family;
            Thread.Sleep(2000);
            return aids.FindAll((x) => x.Family == family).First((y) => y.Brand == brand).Model;
        }

        /// <summary>
        /// Returns the name of a <see cref="HearingAidModel"/> in respect to the <see cref="Brand"/> of the device familiy.
        /// </summary>
        /// <param name="model">Any hearing aid type of a given device family.</param>
        /// <param name="brand">The OEM brand to convert to.</param>
        public static string GetName(HearingAidModel model, Brand brand)
        {
            return GetName(GetBrandSpecificModel(model, brand));
        }

        /// <summary>
        /// Returns the name of a <see cref="HearingAidModel"/>.
        /// </summary>
        /// <param name="model">The type of the hearing aid.</param>
        public static string GetName(HearingAidModel model)
        {
            return aids.First((x) => x.Model == model).Name;
        }

        private struct Aid
        {
            public HearingAidModel Model;
            public HearingAidModel Family;  //A "family" of hearing aids is a set of hearing aids with different brands that share the same underlying hardware.
                                            //Example: lewi S, KINDwings 2200, Sombra XS and BTE Charm K820 are considered one family.
                                            //The audifon device alway serves as the respective family's identifier.
            public Brand Brand;
            public string Name;

            public Aid(HearingAidModel model, HearingAidModel family, Brand brand, string name)
            {
                Model = model;
                Family = family;
                Brand = brand;
                Name = name;
            }
        }
    }
}
