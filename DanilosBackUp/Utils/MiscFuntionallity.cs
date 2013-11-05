using System;
using System.Linq;

namespace DanilosBackUp.Utils
{
    public class MiscFuntionallity
    {
        public static int GetNumericDayOfWeek()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;

            switch (day.ToString())
            {
                case "Monday": 
                case "Lunes": return 1;
                    
                case "Tuesday":
                case "Martes": return 2;
                    
                case "Wednesday":
                case "Miércoles": return 3;
                    
                case "Thursday":
                case "Jueves": return 4;
                    
                case "Friday":
                case "Viernes": return 5;
                    
                case "Saturday":
                case "Sabado": return 6;
                    
                case "Domingo":
                case "Sunday": return 7;

                default: return 0;
            }
        }
    }
}
