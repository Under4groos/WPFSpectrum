using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFNaudioLib
{
    static class AudioDevice
    {
        /// <summary>
        /// Список устройств.
        /// </summary>
        public static MMDeviceCollection MMDeviceCollection = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        public static int MMDeviceID
        {
            get; set;
        }
        /// <summary>
        /// Проверка работоспособности устройства.
        /// </summary>
        /// <returns></returns>
        public static bool IsWorking()
        {
            return MMDeviceCollection[MMDeviceID].AudioMeterInformation.MasterPeakValue > 0;
        }
        /// <summary>
        /// Поиск активного устройства.
        /// </summary>
        public static void SearchActDeviceID()
        {
            bool act = true;
            while (act)
            {
                for (int i = 0; i < MMDeviceCollection.Count; i++)
                {
                    if (MMDeviceCollection[i].AudioMeterInformation.MasterPeakValue != 0)
                    {
                        MMDeviceID = i;
                        act = false;
                        break;
                    }
                }
            }
        }
    }
}
