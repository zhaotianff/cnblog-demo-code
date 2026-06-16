using System;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.DevTools.Accessibility;
using CefSharp.OffScreen;

namespace CEFSharpOffScreenDemo
{
    class Program
    {
        // 全局浏览器实例
        private static ChromiumWebBrowser _browser;

        //需要指定STA线程
        [STAThread] 
        static void Main(string[] args)
        {
            //CEF全局配置
            var cefSettings = new CefSettings
            {
                //缓存路径
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),

                //自定义UA
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.6261.95 Safari/537.36"
            };

            //最优离屏渲染参数
            cefSettings.SetOffScreenRenderingBestPerformanceArgs();

            //初始化CEF
            if (!Cef.Initialize(cefSettings, true))
            {
                Console.WriteLine("CEF初始化失败");
                return;
            }

            try
            {
                _browser = new ChromiumWebBrowser();

                //绑定事件
                BindBrowserEvent();

                //加载网页
                _browser.Load("https://www.baidu.com");

                //等待加载完成
                LoadPageAsync(_browser).Wait();

                //功能调用
               CefFuncDemo().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //释放资源
                _browser?.Dispose();
                Cef.Shutdown();

                Console.WriteLine("请输入任意键退出");
                Console.ReadLine();
            }
        }

        private static async Task CefFuncDemo()
        {
            // 执行JS获取返回值
            var jsResult = await _browser.EvaluateScriptAsync("document.title");
            if (jsResult.Success)
            {
                Console.WriteLine("网页标题：" + jsResult.Result);
            }

            var host = _browser.GetBrowser().GetHost();

            // 点击页面元素（坐标点击）
            // x,y 网页相对坐标
            //按下
            host.SendMouseClickEvent(120, 120, MouseButtonType.Left, false, 1, CefEventFlags.None);
            //等待
            await Task.Delay(200);
            //松开
            host.SendMouseClickEvent(120, 120, MouseButtonType.Left, true, 1, CefEventFlags.None);

            //输入键盘内容
            KeyEvent keyEvent = new KeyEvent();
            keyEvent.WindowsKeyCode = (int)Keys.H;
            keyEvent.Type = KeyEventType.KeyDown;
            //按下
            host.SendKeyEvent(keyEvent);
            //松开
            keyEvent.Type = KeyEventType.KeyUp;
            host.SendKeyEvent(keyEvent);
            
            // 网页后退/前进/刷新
            //_browser.Back();
            //_browser.Forward();
            //_browser.Reload();

            //导出网页为PDF
            var pdfSettings = new PdfPrintSettings
            {
                Landscape = false,
                PrintBackground = true
            };
            await _browser.PrintToPdfAsync("网页导出.pdf", pdfSettings);
            Console.WriteLine("PDF导出完成");

            //设置Cookie
            //var cookieManager = Cef.GetGlobalCookieManager();
            //cookieManager.SetCookie("https://www.baidu.com", new Cookie
            //{
            //    Name = "testCookie",
            //    Value = "123456",
            //    Domain = ".baidu.com"
            //});

            //获取当前URL
            Console.WriteLine("当前地址：" + _browser.Address);
        }

        /// <summary>
        /// 等待页面加载完成
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static Task LoadPageAsync(IWebBrowser browser)
        {
            var tcs = new TaskCompletionSource<bool>();
            EventHandler<LoadingStateChangedEventArgs> handler = null;
            handler = (sender, args) =>
            {
                if (!args.IsLoading)
                {
                    browser.LoadingStateChanged -= handler;
                    tcs.TrySetResult(true);
                }
            };
            browser.LoadingStateChanged += handler;
            return tcs.Task;
        }

        private static void BindBrowserEvent()
        {

            // 渲染画面回调（每一帧都会触发）
            _browser.Paint += _browser_Paint;

            // JS控制台日志捕获
            _browser.ConsoleMessage += _browser_ConsoleMessage;

            _browser.FrameLoadEnd += _browser_FrameLoadEnd;
        }

        /// <summary>
        /// 浏览器完成页面框架加载时触发的事件处理程序。
        /// 页面可能同时存在多个框架正在加载；
        /// 主框架加载完成后，子框架仍可能发起或继续加载流程。
        /// 无论请求是否加载成功，该方法都会对所有框架执行回调。
        /// 请注意：此事件在 CEF 界面线程触发，默认情况下该线程与应用程序界面主线程不是同一个线程。
        /// 不要在该线程中进行任何耗时阻塞操作，否则浏览器会无响应甚至卡死。
        /// 如果需要操作界面控件，必须通过Invoke/Dispatch切换至应用 UI 主线程。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <see cref="https://cefsharp.github.io/api/118.6.x/html/E_CefSharp_IChromiumWebBrowserBase_FrameLoadEnd.htm"/>
        private static async void _browser_FrameLoadEnd(object? sender, FrameLoadEndEventArgs e)
        {
            //参考：https://www.cnblogs.com/zhaotianff/p/9556270.html
            var html = await e.Browser.GetSourceAsync();
            Console.WriteLine(html);
            Console.WriteLine("frame 加载完成");
        }

        /// <summary>
        /// 用于接收网页发送的 JavaScript 控制台消息的事件处理程序。
        /// 需要重点注意：该事件在 CEF 界面线程触发，默认情况下该线程与你的应用程序界面线程并非同一线程。
        /// 切勿在此线程中执行任何耗时阻塞操作，否则浏览器会失去响应甚至卡死。
        /// 若要操作界面控件，你必须通过调用Invoke/Dispatch切换至应用主线程。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <see cref="https://cefsharp.github.io/api/118.6.x/html/E_CefSharp_IChromiumWebBrowserBase_ConsoleMessage.htm"/>
        private static void _browser_ConsoleMessage(object? sender, ConsoleMessageEventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private static void _browser_Paint(object? sender, OnPaintEventArgs e)
        {
            // 该回调在 CEF 界面线程触发，默认情况下该线程与应用程序主线程并非同一线程。
            // 页面元素需要绘制时触发此方法

            //BGRA 4字节/像素
            int stride = e.Width * 4; 

            //创建Bitmap：CEF像素格式 BGRA
            Bitmap bmp = new Bitmap(e.Width, e.Height, stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, e.BufferHandle);

            bmp.Save(DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_ffff") + ".jpg");

            bmp.Dispose();
        }
    }
}