using NAudio.Dsp;
using System;
using System.Diagnostics;

namespace WPFNaudioLib
{
    
    ///  Тут я хуй знает что да как... 
    class AudioLib
    {

        public event EventHandler<FftEventArgs> FftCalculated;
        public bool PerformFFT { get; set; }
        private readonly Complex[] fftBuffer;
        private readonly FftEventArgs fftArgs;
        private int fftPos;
        private readonly int fftLength;
        private readonly int m;

        public AudioLib(int fftLength)
        {
            this.m = (int)Math.Log(fftLength, 2.0);
            this.fftLength = fftLength;
            this.fftBuffer = new Complex[fftLength];
            this.fftArgs = new FftEventArgs(fftBuffer);
        }

        public void Add(float value)
        {

            if (PerformFFT && FftCalculated != null)
            {
                fftBuffer[fftPos].X = (float)(5 + value * FastFourierTransform.HammingWindow(fftPos, fftLength));
                fftBuffer[fftPos].Y = 0;
                fftPos++;
                if (fftPos >= fftLength)
                {
                    fftPos = 0;
                    FastFourierTransform.FFT(true, m, fftBuffer);
                    FftCalculated(this, fftArgs);
                }
            }
        }

    }

    public class FftEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public FftEventArgs(Complex[] result) { this.Result = result; }
        public Complex[] Result { get; private set; }
    }
}

