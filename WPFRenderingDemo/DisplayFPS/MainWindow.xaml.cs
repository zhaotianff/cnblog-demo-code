using System.Diagnostics;
using System.Windows;
using System.Windows.Media;


namespace DisplayFPS
{
    public partial class MainWindow : TianXiaTech.BlurWindow
    {
        Stopwatch _stopwatch = new Stopwatch();
        int _frameCounter = 0;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
        }

        // 在帧渲染之前调用，以允许自定义绘图。
        protected void UpdateTotalAverageFPS(object sender, EventArgs e)
        {
            if (_frameCounter++ == 0)
            {
                // Starting timing.
                _stopwatch.Start();
            }

            // 以fps（每秒帧数）为单位确定帧率。
            long frameRate = (long)(_frameCounter / this._stopwatch.Elapsed.TotalSeconds);
            if (frameRate > 0)
            {
                // 更新已用时间、总帧数和帧率。
                myStopwatchLabel.Content = "Total Time: " + _stopwatch.Elapsed.ToString();
                myFrameCounterLabel.Content = "Total Frames: " + _frameCounter.ToString();
                myFrameRateLabel.Content = "FPS: " + frameRate.ToString();
            }
        }

        protected void UpdateFPS(object sender,EventArgs e)
        {
            if(_stopwatch.IsRunning == false)
            {
                _stopwatch.Start();
            }

            _frameCounter++;

            //粗略统计1秒钟渲染的帧率
            if(_stopwatch.Elapsed.TotalSeconds >= 1.0)
            {
                myFrameRateLabel.Content = "FPS: " + _frameCounter.ToString();

                //如果需要再准确一点，可以使用 _frameCounter / _stopwatch.Elapsed.TotalSeconds

                _frameCounter = 0;
                _stopwatch.Restart();
            }
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            //取消订阅方法1
            //CompositionTarget.Rendering -= UpdateTotalAverageFPS;

            //取消订阅方法2
            CompositionTarget.Rendering -= UpdateFPS;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //订阅方法1
            //CompositionTarget.Rendering += UpdateTotalAverageFPS;

            //订阅方法2
            CompositionTarget.Rendering += UpdateFPS;
        }
    }
}