using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DanilosBackUp.Utils;

namespace DanilosBackUp.VentanasAuxiliares
{
    /// <summary>
    /// Interaction logic for WinBackUpTimeSetting.xaml
    /// </summary>
    public partial class WinBackUpTimeSetting
    {
        List<CheckBox> diasSemana = null;
        private String diasInteger = "";

        public WinBackUpTimeSetting()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            diasSemana = new List<CheckBox>() { ChkLunes, ChkMartes, ChkMiercoles, ChkJueves, ChkViernes, ChkSabado, ChkDomingo };

            int tipoRespaldo = Convert.ToInt16(AppSettings.GetPropertyValue("PeriodoRespaldo"));

            if (tipoRespaldo == 1)
                RradDiario.IsChecked = true;
            else if (tipoRespaldo == 2)
            {
                RradSemal.IsChecked = true;
                List<String> dias = AppSettings.GetPropertyValue("SemanaNumericos").ToString().Split(',').ToList();

                foreach (CheckBox check in diasSemana)
                    if (dias.Contains(check.Tag.ToString()))
                        check.IsChecked = true;
            }
            else
            {
                RradMensual.IsChecked = true;
                RdpMensual.SelectedDate = DateTime.Parse(AppSettings.GetPropertyValue("DiaRespaldoMensual"));
            }

            string[] numeric = AppSettings.GetPropertyValue("LeyendaHorario").ToString().Split(':');
            if (numeric.Count() > 1)
            {
                TimeSpan? time = new TimeSpan(Convert.ToInt32(numeric[0]), Convert.ToInt32(numeric[1]), 0);

                RtpRespaldoTime.SelectedTime = time;
            }
        }
        
        private void RbtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            String periodo = "";
            int tipoBack = 0;
            
            if (RradDiario.IsChecked == true)
            {
                periodo = "El respaldo se llevará a cabo de manera diaria";
                tipoBack = 1;
            }
            else if (RradSemal.IsChecked == true)
            {
                periodo = this.GetWeekBackUpInfo();
                tipoBack = 2;
            }
            else if (RradMensual.IsChecked == true)
            {
                tipoBack = 3;
                periodo = "el día " + ((DateTime)RdpMensual.SelectedDate).Day + " de cada mes";
            }

            String tiempo = RtpRespaldoTime.SelectedTime.Value.ToString();

            AppSettings.UpdateSettingValue("PeriodoRespaldo", tipoBack.ToString());
            AppSettings.UpdateSettingValue("LeyendaPeriodo", periodo);
            AppSettings.UpdateSettingValue("LeyendaHorario", tiempo);
            AppSettings.UpdateSettingValue("SemanaNumericos", diasInteger);
            AppSettings.UpdateSettingValue("DiaRespaldoMensual", RdpMensual.SelectedDate.ToString());

            this.Close();
        }

        private String GetWeekBackUpInfo()
        {
            String cadenaDias = "los días ";
            foreach (CheckBox dia in diasSemana)
                if (dia.IsChecked == true)
                {
                    cadenaDias += dia.Content.ToString() + ", ";
                    diasInteger += dia.Tag.ToString() + ",";
                }

            return cadenaDias = ((cadenaDias.EndsWith(", ")) ? cadenaDias.Substring(0, cadenaDias.Length - 2) : cadenaDias) + " de cada semana";
        }

        private void RradSemal_Checked(object sender, RoutedEventArgs e)
        {
            Periodo.IsEnabled = true;
            Semanal.Visibility = System.Windows.Visibility.Visible;
            Mensual.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void RradMensual_Checked(object sender, RoutedEventArgs e)
        {
            Periodo.IsEnabled = true;
            Semanal.Visibility = System.Windows.Visibility.Collapsed;
            Mensual.Visibility = System.Windows.Visibility.Visible;
        }

        private void RradDiario_Checked(object sender, RoutedEventArgs e)
        {
            Periodo.IsEnabled = false;
        }

        private void RbtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}