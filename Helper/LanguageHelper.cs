using HorusUITest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace HorusUITest.Helper
{
    public static class LanguageHelper
    {
        /// <summary>
        /// Method to get All Audifon Languages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllAudifonLanguages()
        {
            return Enum.GetNames(typeof(Language_Audifon)).ToList();
        }

        /// <summary>
        /// Method to get All Puretone Languages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllPuretoneLanguages()
        {
            return Enum.GetNames(typeof(Language_Puretone)).ToList();
        }

        /// <summary>
        /// Method to get All Persona Languages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllPersonaLanguages()
        {
            return Enum.GetNames(typeof(Language_Persona)).ToList();
        }

        /// <summary>
        /// Method to get All Kind Languages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllKindLanguages()
        {
            return Enum.GetNames(typeof(Language)).ToList();
        }

        /// <summary>
        /// Method to get All Hormann Languages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllHormannLanguages()
        {
            return Enum.GetNames(typeof(Language_Hormann)).ToList();
        }

        /// <summary>
        /// Method to get All RxEarsPro Languages
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllRxEarsProLanguages()
        {
            return Enum.GetNames(typeof(Language_RxEarsPro)).ToList();
        }

        /// <summary>
        /// If this method thorws error then the Langauge has not been added in the switch case
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Language_Device GetDeviceLanguageForAudifonLanguage(Language_Audifon language)
        {
            switch (language)
            {
                case Language_Audifon.German: return Language_Device.German_Germany;
                case Language_Audifon.English: return Language_Device.English_UK;
                case Language_Audifon.Italian: return Language_Device.Italian_Italy;
                case Language_Audifon.French: return Language_Device.French_France;
                case Language_Audifon.Polish: return Language_Device.Polish_Poland;
                case Language_Audifon.Dutch: return Language_Device.Dutch_Netherlands;
                case Language_Audifon.Spanish: return Language_Device.Spanish_Spain;
                case Language_Audifon.Portuguese: return Language_Device.Portuguese_Portugal;
                case Language_Audifon.Czech: return Language_Device.Czech_Czech;
                case Language_Audifon.Turkish: return Language_Device.Turkish_Turkish;
                case Language_Audifon.Russian: return Language_Device.Russian_Russia;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        /// <summary>
        /// If this method thorws error then the Langauge has not been added in the switch case
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Language_Device GetDeviceLanguageForPuretoneLanguage(Language_Puretone language)
        {
            switch (language)
            {
                case Language_Puretone.English: return Language_Device.English_UK;
                case Language_Puretone.Spanish: return Language_Device.Spanish_Spain;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        /// <summary>
        /// If this method thorws error then the Langauge has not been added in the switch case
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Language_Device GetDeviceLanguageForPersonaLanguage(Language_Persona language)
        {
            switch (language)
            {
                case Language_Persona.English: return Language_Device.English_UK;
                case Language_Persona.Spanish: return Language_Device.Spanish_Spain;
                case Language_Persona.Dutch: return Language_Device.Dutch_Netherlands;
                case Language_Persona.Portuguese: return Language_Device.Portuguese_Portugal;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        /// <summary>
        /// If this method thorws error then the Langauge has not been added in the switch case
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Language_Device GetDeviceLanguageForKindLanguage(Language language)
        {
            switch (language)
            {
                case Language.German: return Language_Device.German_Germany;
                case Language.English: return Language_Device.English_UK;
                case Language.Italian: return Language_Device.Italian_Italy;
                case Language.French: return Language_Device.French_France;
                case Language.Polish: return Language_Device.Polish_Poland;
                case Language.Dutch: return Language_Device.Dutch_Netherlands;
                case Language.Turkish: return Language_Device.Turkish_Turkish;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        /// <summary>
        /// If this method thorws error then the Langauge has not been added in the switch case
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Language_Device GetDeviceLanguageForHormannLanguage(Language_Hormann language)
        {
            switch (language)
            {
                case Language_Hormann.English: return Language_Device.English_US;
                case Language_Hormann.Spanish: return Language_Device.Spanish_Spain;
                case Language_Hormann.Portuguese: return Language_Device.Portuguese_Portugal;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        /// <summary>
        /// If this method thorws error then the Langauge has not been added in the switch case
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public static Language_Device GetDeviceLanguageForRxEarsProLanguage(Language_RxEarsPro language)
        {
            switch (language)
            {
                case Language_RxEarsPro.English: return Language_Device.English_UK;
                default: throw new NotImplementedException($"Unknown language '{language}'.");
            }
        }

        /// <summary>
        /// Get Resource Text by Key
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="language"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetResourceTextByKey(Brand brand, Enum language, string key)
        {
            string ResourceNamespace = "HorusUITest.Resources.Oem." + brand.ToString() + "." + language.ToString() + "";
            ResourceManager rm = new ResourceManager(ResourceNamespace, Assembly.GetExecutingAssembly());
            return rm.GetString(key);
        }
    }
}