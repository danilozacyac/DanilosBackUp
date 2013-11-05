using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DanilosBackUp.Utils
{
    public class AppSettings
    {
        /// <summary>
        /// Actuliza el valor de las propiedades de configuración de l aplicacií
        /// </summary>
        /// <param name="property">Nombre de la propiedad</param>
        /// <param name="propVaue">Valor que actualizara</param>
        public static void UpdateSettingValue(String property,String propValue)
        {
            Configuration oConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            oConfig.AppSettings.Settings[property].Value = propValue;

            oConfig.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        

        /// <summary>
        /// Devuelve el valor de la propiedad indicada
        /// </summary>
        /// <param name="property">Nombre de la propiedad</param>
        /// <returns></returns>
        public static String GetPropertyValue(String property)
        {
            return ConfigurationManager.AppSettings[property];
        }


        /// <summary>
        /// Devuelve una lista de elementos de la propiedad solicitada al AppConfig, cada una de las
        /// propiedades esta delimitada por el caracter '|'
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static List<String> GetPropertiesList(String property)
        {
            String prop = AppSettings.GetPropertyValue(property);

            if (prop == null)
                return new List<string>();
            else
                return prop.Split('|').ToList();

        }

    }
}
