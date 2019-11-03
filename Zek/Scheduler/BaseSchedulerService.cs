using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using Zek.Configuration;
using Zek.Utils;
using Timer = System.Timers.Timer;


namespace Zek.Scheduler
{
    public class BaseSchedulerService : ServiceBase
    {
        public BaseSchedulerService()
        {
            _readLine = new StringBuilder();
            IsConsole = Environment.UserInteractive;
        }

        //~BaseSchedulerService()
        //{
        //    if (_trayIcon != null)
        //    {
        //        _trayIcon.Dispose();
        //        _trayIcon = null;
        //    }
        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (_trayIcon != null))
        //    {
        //        _trayIcon.Dispose();
        //        _trayIcon = null;
        //    }

        //    base.Dispose(disposing);
        //}

        #region Configuraion
        /// <summary>
        /// Loads configuration.
        /// </summary>
        protected virtual void LoadConfig()
        {
            try
            {
                var exists = AppConfigHelper.ExistsConfigFile();
                if (!exists)
                {
                    StartTime = new TimeSpan(0, 0, 0);
                    EndTime = new TimeSpan(23, 59, 59);
                    Interval = TimeSpan.FromDays(1d);
                    Days = new[] { 1, 2, 3, 4, 5, 6, 0 };
                    SaveConfig();
                }

                TimeSpan tmp;

                if (TimeSpan.TryParseExact(AppConfigHelper.GetString("SchedulerStartTime"), @"h\:m\:s", null, out tmp))
                    StartTime = tmp;
                else
                    throw new FormatException("Can't parse 'SchedulerStartTime' from app.config (e.g. h:m:s)");


                if (TimeSpan.TryParseExact(AppConfigHelper.GetString("SchedulerEndTime"), @"h\:m\:s", null, out tmp))
                    EndTime = tmp;
                else
                    throw new FormatException("Can't parse 'SchedulerEndTime' from app.config (e.g. h:m:s)");


                if (TimeSpan.TryParseExact(AppConfigHelper.GetString("SchedulerInterval"), @"d\:h\:m\:s", null, out tmp)
                    || TimeSpan.TryParseExact(AppConfigHelper.GetString("SchedulerInterval"), @"h\:m\:s", null, out tmp))
                    Interval = tmp;
                else
                    throw new FormatException("Can't parse 'SchedulerInterval' from app.config (e.g. d:h:m:s or h:m:s)");


                var schedulerDays = string.Empty;
                foreach (var c in AppConfigHelper.GetString("SchedulerDays"))
                {
                    if (char.IsNumber(c) || c == ';' || c == ',' || c == '|' || c == '/' || c == '-' || c == '_')
                        schedulerDays += c;
                }
                Days = Array.ConvertAll(schedulerDays.Split(';', ',', '|', '/', '-', '_').Where(x => x.Length == 1).ToArray(), int.Parse);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while loading configuration (see inner exception).", ex);
            }
        }
        /// <summary>
        /// Saves configuration.
        /// </summary>
        protected virtual void SaveConfig()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppConfigHelper.Set(config, "SchedulerStartTime", StartTime.ToString(@"hh\:mm\:ss"));
                AppConfigHelper.Set(config, "SchedulerEndTime", EndTime.ToString(@"hh\:mm\:ss"));
                AppConfigHelper.Set(config, "SchedulerInterval", Interval.ToString(@"dd\:hh\:mm\:ss"));
                AppConfigHelper.Set(config, "SchedulerDays", string.Join(";", Days));//string.Join(";", Array.ConvertAll<int, string>(Days, Convert.ToString));

                config.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving configuration (see inner exception).", ex);
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Params from exe
        /// </summary>
        [Browsable(false)]
        public string[] Args { get; private set; }
        /// <summary>
        /// Start Date
        /// </summary>
        protected TimeSpan StartTime { get; private set; }
        /// <summary>
        /// End Date
        /// </summary>
        protected TimeSpan EndTime { get; private set; }
        /// <summary>
        /// Interval between ExecuteStep
        /// </summary>
        protected TimeSpan Interval
        {
            get;
            private set;
        }
        /// <summary>
        /// Sunday = 0,
        /// Monday = 1,
        /// Tuesday = 2,
        /// Wednesday = 3,
        /// Thursday = 4,
        /// Friday = 5,
        /// Saturday = 6
        /// </summary>
        protected int[] Days
        {
            get;
            private set;
        }
        /// <summary>
        /// If it's console or windows form.
        /// </summary>
        protected bool IsConsole { get; private set; }
        /// <summary>
        /// Gets or Sets if application executed by Task Scheduler.
        /// </summary>
        protected bool IsTaskScheduler { get; set; }
        /// <summary>
        /// Checks if you can use Console.WriteLine() method.
        /// </summary>
        protected virtual bool IsWriteLine => IsConsole && !IsTaskScheduler;

        #endregion

        #region Static
        /// <summary>
        /// Executes service from static void Main(string[] args)
        /// </summary>
        /// <typeparam name="T">Type of BaseSchedulerService</typeparam>
        /// <param name="args">String array args</param>
        public static void Execute<T>(string[] args) where T : BaseSchedulerService, new()
        {
            var service = new T();
            Execute(service, args);
        }

        /// <summary>
        /// Executes service from static void Main(string[] args)
        /// </summary>
        /// <param name="service">Service to start.</param>
        /// <param name="args">String array args</param>
        public static void Execute(BaseSchedulerService service, string[] args)
        {
            if (service == null)
                service = new BaseSchedulerService();

            if (service.IsTaskScheduler)
            {
                //service.EventLog.WriteEntry("IsTaskScheduler");
                service.OnStart(args);
            }
            else if (service.IsConsole)
            {
                if (service.IsWriteLine)
                    Console.WriteLine(@"Press ESC to exit...");
                service.OnStart(args);

                while (true)
                {
                    //bool bln = true;

                    while (!Console.KeyAvailable)
                    {
                        //if (!bln)
                        //    Console.WriteLine("{0:s}", "x");
                        //bln = true;
                    }
                    //bln = false;

                    var cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape)
                        break;

                    //if (ReadKey != null)
                    //    ReadKey(null, new ReadKeyArgs(cki));
                    service.OnReadKey(cki);
                }
                service.OnStop();
            }
            else
            {
                Run(service);
            }
        }

        //public class ReadKeyArgs : EventArgs
        //{
        //    public ReadKeyArgs(ConsoleKeyInfo cki)
        //    {
        //        KeyInfo = cki;
        //    }

        //    public ConsoleKeyInfo KeyInfo { get; private set; }
        //}
        //public delegate void ReadKeyEventHandler(object source, ReadKeyArgs e);
        //public static event ReadKeyEventHandler ReadKey;

        #endregion

        #region NotifyIcon
        //private System.Windows.Forms.NotifyIcon _trayIcon = new System.Windows.Forms.NotifyIcon();

        //[System.Runtime.InteropServices.DllImport("kernel32.dll", ExactSpelling = true)]
        //private static extern IntPtr GetConsoleWindow();

        //private static IntPtr ThisConsole = GetConsoleWindow();

        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        //private static Int32 _showWindow = 0; //0 - SW_HIDE - Hides the window and activates another window.

        //private void TrayIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (e.Button != System.Windows.Forms.MouseButtons.Right)//reserve right click for context menu
        //    {
        //        _showWindow = ++_showWindow % 2;
        //        ShowWindow(ThisConsole, _showWindow);
        //    }
        //}
        //private void smoothExit(object sender, EventArgs e)
        //{
        //    _trayIcon.Visible = false;
        //    System.Windows.Forms.Application.Exit();
        //    Environment.Exit(1);
        //}
        //private void InitTryIcon()
        //{
        //    #region TrayIcon Icon
        //    var assembly = System.Reflection.Assembly.GetEntryAssembly();
        //    try { _trayIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(assembly.Location); }
        //    catch { }
        //    if (_trayIcon.Icon == null) _trayIcon.Icon = Zek.Properties.Icons.application;
        //    #endregion

        //    #region TrayIcon Text
        //    var attributes = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false);
        //    if (attributes.Length > 0)
        //    {
        //        var titleAttribute = (System.Reflection.AssemblyTitleAttribute)attributes[0];
        //        if (!string.IsNullOrEmpty(titleAttribute.Title))
        //            _trayIcon.Text = titleAttribute.Title;
        //    }
        //    else
        //        _trayIcon.Text = System.IO.Path.GetFileNameWithoutExtension(assembly.CodeBase);

        //    attributes = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyDescriptionAttribute), false);
        //    if (attributes.Length > 0)
        //    {
        //        var descriptionAttribute = (System.Reflection.AssemblyDescriptionAttribute)attributes[0];
        //        if (!string.IsNullOrEmpty(descriptionAttribute.Description))
        //            _trayIcon.Text += " (" + descriptionAttribute.Description + ")";
        //    }
        //    #endregion



        //    _trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(TrayIcon_Click);
        //    //_trayIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
        //    //_trayIcon.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        //    //        new System.Windows.Forms.ToolStripMenuItem { Text = "Exit" }
        //    //    });
        //    //_trayIcon.ContextMenuStrip.Items[0].Click += new EventHandler(smoothExit);
        //    _trayIcon.Visible = true;
        //    System.Windows.Forms.Application.Run();
        //}
        #endregion

        #region Methods
        public void Execute(string[] args)
        {
            Execute(this, args);
        }
        #endregion

        #region Override
        private bool _startTimerAfterElapsed;
        protected override void OnStart(string[] args)
        {
            _startTimerAfterElapsed = true;
            try
            {
                Args = args;

                //InitTryIcon();
                LoadConfig();

                if (IsWriteLine)
                {
                    Console.WriteLine(@"StartTime: " + StartTime);
                    Console.WriteLine(@"EndTime: " + EndTime);
                    Console.WriteLine(@"DayOfWeek: " + string.Join(", ", ((DayOfWeek[])Enum.GetValues(typeof(DayOfWeek))).Where(d => Days.Contains((int)d)).Select(d => d.ToString()).ToArray()));

                    Console.WriteLine(@"Interval (d:hh:mm:ss): " + Interval.ToString(@"d\:hh\:mm\:ss"));
                    Console.WriteLine(@"   Days:         {0,3}", Interval.Days);
                    Console.WriteLine(@"   Hours:        {0,3}", Interval.Hours);
                    Console.WriteLine(@"   Minutes:      {0,3}", Interval.Minutes);
                    Console.WriteLine(@"   Seconds:      {0,3}", Interval.Seconds);
                }

                _timer = new Timer
                {
                    Interval = Interval.TotalMilliseconds,
                    AutoReset = true //keep calling elapsed method. 
                };
                _timer.Elapsed += _timer_Elapsed;//sets event
                _timer_Elapsed(_timer, null);//execute (we don't need to wait when interval elapsed).
                _timer.Start();//start timer.
            }
            catch (Exception ex)
            {
                Stop();
                LogException(ex);
                return;
            }

            base.OnStart(args);
        }
        protected override void OnStop()
        {
            _startTimerAfterElapsed = false;
            TryStopTimer();
            base.OnStop();
        }
        protected override void OnShutdown()
        {
            _startTimerAfterElapsed = false;
            TryStopTimer();
            base.OnShutdown();
        }
        protected override void OnPause()
        {
            _startTimerAfterElapsed = false;
            if (_timerEnabled == null)
                _timerEnabled = _timer != null && _timer.Enabled;
            TryStopTimer();
            base.OnPause();
        }
        protected override void OnContinue()
        {
            _startTimerAfterElapsed = true;
            if (_timerEnabled != null)
            {
                if (_timerEnabled.Value)
                    TryStartTimer();
                _timerEnabled = null;
            }
            base.OnContinue();
        }
        #endregion

        #region Timer
        private Timer _timer;
        private bool? _timerEnabled;
        private static readonly object Lock = new object();
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (Lock)
            {
                TryStopTimer();
                TryExecuteStep();
                TryStartTimer();
            }
        }
        private void TryStartTimer()
        {
            if (_startTimerAfterElapsed && _timer != null && !_timer.Enabled)
            {
                _timer.Start();
            }
        }
        private void TryStopTimer()
        {
            if (_timer != null && _timer.Enabled)
            {
                _timer.Stop();
            }
        }
        #endregion

        #region Virtual

        /// <summary>
        /// Save exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="fileName"></param>
        protected virtual void LogException(Exception ex, string fileName = null)
        {
            if (IsWriteLine)
            {
                Console.WriteLine();
                var foregroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = foregroundColor;
                Console.WriteLine();
            }

            ExceptionLogHelper.Write(ex, fileName);
        }

        /// <summary>
        /// Checks StartTime, EndTime, Days
        /// </summary>
        /// <returns>Returns true if everything is valid.</returns>
        protected virtual bool Check()
        {
            return (DateTime.Now.TimeOfDay >= StartTime) && (DateTime.Now.TimeOfDay <= EndTime) && Days.Contains((int)DateTime.Now.DayOfWeek);
        }

        /// <summary>
        /// Checks StartTime, EndTime, Days and executes 'ExecuteStep' method.
        /// </summary>
        protected virtual void TryExecuteStep()
        {
            if (!Check()) return;

            ExecuteStep();
        }
        /// <summary>
        /// Override this for your logic.
        /// </summary>
        protected virtual void ExecuteStep()
        {
            if (IsWriteLine)
                Console.WriteLine(@"Executing...");

            Debug.Print(@"Executing...");

            Thread.Sleep(1000);

            if (IsWriteLine)
                Console.WriteLine(@"Done.");

            Debug.Print(@"Done.");
        }

        private readonly StringBuilder _readLine;
        /// <summary>
        /// Invokes when key readed (When executes as console).
        /// </summary>
        /// <param name="cki">Console key info.</param>
        protected virtual void OnReadKey(ConsoleKeyInfo cki)
        {
            if (cki.Key == ConsoleKey.Enter)
            {
                OnReadLine(_readLine.ToString());
                _readLine.Clear();
                return;
            }

            if (cki.Key != ConsoleKey.Backspace)
            {
                _readLine.Append(cki.KeyChar);
                Console.Write(cki.KeyChar);
            }
            else if (cki.Key == ConsoleKey.Backspace && _readLine.Length > 0)
            {
                if (_readLine.Length > 0)
                    _readLine.Remove(_readLine.Length - 1, 1);
                Console.Write("\b \b");
            }

            //Console.WriteLine(@"--- {0} ---", cki.Key);
        }

        protected virtual void OnReadLine(string readLine)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(readLine))
                Console.WriteLine(@"--- {0} ---", readLine);
            //Console.WriteLine(readLine);
        }
        #endregion
    }
}