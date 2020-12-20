using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFNaudioLib
{
    public class AudioChannelFFT
    {
        public enum ChannelFFT
        {
            FFT_256 = 128,
            FFT_512 = 256,
            FFT_1024 = 512,
            FFT_2048 = 1024,
            FFT_4096 = 2048,
            FFT_8192 = 4096,
            FFT_16384 = 8192,
            FFT_32768 = 16384,
        }
    }
    public class Audio
    {
        IWaveIn waveIn;
        AudioLib audio;
        public  List<double> list_array = new List<double>();
        
        public int Length
        {
            get; set;
        }
        public int CSmoothHistogram
        {
            get;set;
        }

        public double Db
        {
            get; set;
        } = -90;


        public Audio()
        {
            Length = (int)AudioChannelFFT.ChannelFFT.FFT_2048;
            CSmoothHistogram = 1;
        }

        /// <summary>
        /// Старт записи.
        /// </summary>
        public void StartRecording()
        {
            AudioDevice.SearchActDeviceID();
            audio = new AudioLib(Length);
            audio.FftCalculated += new EventHandler<FftEventArgs>(FFT);
            audio.PerformFFT = true;

            if (AudioDevice.MMDeviceCollection[AudioDevice.MMDeviceID] != null)
            {
                waveIn = new WasapiLoopbackCapture(AudioDevice.MMDeviceCollection[AudioDevice.MMDeviceID]);
                waveIn.DataAvailable += WaveIn_DataAvailable; ;
                waveIn.StartRecording();
            }
        }
        /// <summary>
        /// Остановка записи.
        /// </summary>
        public void StopRecording()
        {
            waveIn.StopRecording();

            audio = null;
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (audio == null)
                return;
            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            int bufferIncrement = waveIn.WaveFormat.BlockAlign;
            for (int index = 0; index < bytesRecorded; index += bufferIncrement)
            {
                if (audio != null)
                {
                    float sample32 = BitConverter.ToSingle(buffer, index);
                    audio.Add(sample32);
                }
                else
                {
                    break;
                }
            }
        }
        private void FFT(object sender, FftEventArgs e)
        {
            int Length_FFT = e.Result.Length;
            for (int i = 1; i < Length_FFT; i++)
            {
                double _d = DLib.Deb(e.Result[i].X, e.Result[i].Y);

                double res = DLib.Map(_d, 0, Db, 0, 12000);

                switch (list_array.Count == Length_FFT)
                {
                    case false:
                        list_array.Add(res);
                        break;
                    case true:
                        list_array[i - 1] = res;
                        break;
                }
            }
            if( CSmoothHistogram <= 0)
            {
                list_array = DLib.SmoothHistogram(list_array);
            }
            else if(CSmoothHistogram >= 1)
            {
                for (int i = 0; i < CSmoothHistogram; i++)
                {
                    list_array = DLib.SmoothHistogram(list_array);

                    //list_array = DLib.SmoothHistogram2(list_array);
                }
            }  
        }
    }
}
