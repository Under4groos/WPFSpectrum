using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfSpectrum
{
    class TimerTick
    {
        private Task Task_;
        private DispatcherTimer DispatcherTimer_;
        private int time = 1;
        /// <summary>
        /// Время в миллисекундах.
        /// </summary>
        public int Time
        {
            get => time;
            set
            {
                time = value <= 0 ? 1 : value;
                if (DispatcherTimer_ == null)
                    return;
                Update();
            }
        }
        private void Update() => DispatcherTimer_.Interval = new TimeSpan(0, 0, 0, 0, time);
        /// <summary>
        /// Подписка на событие.
        /// </summary>
        public event EventHandler Tick;
        public TimerTick() => Time = 1;           
        
        /// <summary>
        /// Запуск таймера в др. потоке.
        /// </summary>
        public void Start()
        {
            if(DispatcherTimer_ == null || Tick != null)
            {
                DispatcherTimer_ = new DispatcherTimer();
                DispatcherTimer_.Tick += Tick;
                DispatcherTimer_.Interval = new TimeSpan(0, 0, 0, 0, Time);
                Task_ = new Task(DispatcherTimer_.Start);
                Task_.Start();
            }
        }
        /// <summary>
        /// Остановка таймера и потока.
        /// </summary>
        public void Stop()
        {
            if (DispatcherTimer_ == null)
                return;
            DispatcherTimer_.Stop();
            Task_.Wait();
        }
    }
}
