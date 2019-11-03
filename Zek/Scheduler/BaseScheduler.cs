using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using Zek.Configuration;
using Zek.Extensions;
using Zek.Utils;
using CancelEventArgs = Zek.Core.CancelEventArgs;
using CancelEventHandler = Zek.Core.CancelEventHandler;
using Timer = System.Timers.Timer;

namespace Zek.Scheduler
{
    public class BaseScheduler
    {
        public BaseScheduler()
        {
            AutoLog = true;
            _name = string.Empty;
        }

        #region Configuraion

        public event CancelEventHandler ConfigLoading;
        public event EventHandler ConfigLoaded;

        /// <summary>
        /// Loads configuration.
        /// </summary>
        protected virtual void OnConfigLoad()
        {
            var e = new CancelEventArgs();
            if (ConfigLoading != null)
                ConfigLoading(this, e);
            if (e.Cancel)
                return;

            try
            {
                var exists = AppConfigHelper.ExistsConfigFile();
                if (!exists)
                {
                    StartTime = new TimeSpan(0, 0, 0);
                    EndTime = new TimeSpan(23, 59, 59);
                    Interval = TimeSpan.FromDays(1d);
                    Days = new[] { 1, 2, 3, 4, 5, 6, 0 };
                    OnConfigSave();
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


                //var schedulerDays = string.Empty;
                //foreach (var c in AppConfigHelper.GetString("SchedulerDays"))
                //{
                //    if (char.IsNumber(c) || c == ';' || c == ',' || c == '|' || c == '/' || c == '-' || c == '_')
                //        schedulerDays += c;
                //}
                var schedulerDays = AppConfigHelper.GetString("SchedulerDays")
                    .Where(c => char.IsNumber(c) || c == ';' || c == ',' || c == '|' || c == '/' || c == '-' || c == '_')
                    .Aggregate(string.Empty, (current, c) => current + c);

                Days = Array.ConvertAll(schedulerDays.Split(';', ',', '|', '/', '-', '_').Where(x => x.Length == 1).ToArray(), int.Parse);

                OnConfigLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while loading configuration (see inner exception).", ex);
            }
        }
        /// <summary>
        /// Executes after configuration loaded.
        /// </summary>
        protected virtual void OnConfigLoaded()
        {
            if (ConfigLoaded != null)
                ConfigLoaded(this, EventArgs.Empty);
        }
        /// <summary>
        /// Saves configuration.
        /// </summary>
        protected virtual void OnConfigSave()
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppConfigHelper.Set(config, "SchedulerStartTime", StartTime.ToString(@"hh\:mm\:ss"));
                AppConfigHelper.Set(config, "SchedulerEndTime", EndTime.ToString(@"hh\:mm\:ss"));
                AppConfigHelper.Set(config, "SchedulerInterval", Interval.ToString(@"dd\:hh\:mm\:ss"));
                AppConfigHelper.Set(config, "SchedulerDays", string.Join(";", Days));
                //string.Join(";", Array.ConvertAll<int, string>(Days, Convert.ToString));

                config.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving configuration (see inner exception).", ex);
            }
        }
        #endregion

        #region Fields
        private static readonly object Lock = new object();
        /// <summary>
        /// Params from exe
        /// </summary>
        public string[] Args { get; private set; }
        /// <summary>
        /// Start Date
        /// </summary>
        public TimeSpan StartTime { get; private set; }
        /// <summary>
        /// End Date
        /// </summary>
        public TimeSpan EndTime { get; private set; }
        /// <summary>
        /// Interval between ExecuteStep
        /// </summary>
        public TimeSpan Interval { get; private set; }
        /// <summary>
        /// Sunday = 0,
        /// Monday = 1,
        /// Tuesday = 2,
        /// Wednesday = 3,
        /// Thursday = 4,
        /// Friday = 5,
        /// Saturday = 6
        /// </summary>
        public int[] Days { get; private set; }

        /// <summary>
        /// Gets or Sets if application executed by Task Scheduler.
        /// </summary>
        public bool IsTaskScheduler { get; private set; }
        #endregion

        #region Virtual Methods
        protected virtual void OnStart(string[] args)
        {
            try
            {
                Args = args;

                OnConfigLoad();

                if (IsTaskScheduler)
                {
                    TryExecuteStep();
                }
                else
                {
                    _timer = new Timer
                    {
                        Interval = Interval.TotalMilliseconds,
                        AutoReset = true//keep calling elapsed method.
                    };
                    _timer.Elapsed += _timer_Elapsed; //sets event
                    _timer_Elapsed(_timer, null); //execute (we don't need to wait when interval elapsed).
                    _timer.Start(); //start timer.
                }
            }
            catch (Exception ex)
            {
                AutoLogException("OnStart", ex);
                OnStop();
            }
        }
        protected virtual void OnStop()
        {
            TryStopTimer();
        }
        #endregion

        #region Public Methods
        public void Start(bool isTaskScheduler = false, string[] args = null)
        {
            IsTaskScheduler = isTaskScheduler;

            _startTimerAfterElapsed = true;
            OnStart(args);
        }

        public void Stop()
        {
            _startTimerAfterElapsed = false;
            OnStop();
        }
        #endregion

        #region Timer
        private bool _startTimerAfterElapsed;
        private Timer _timer;

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
        private void AutoLogException(string name, Exception ex, string fileName = null)
        {
            if (!AutoLog) return;

            LogException(ex, fileName);
            WriteEventLogEntry(name.Add(": ", ex.ToString()), EventLogEntryType.Error);
        }
        /// <summary>
        /// Save exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="fileName"></param>
        public virtual void LogException(Exception ex, string fileName = null)
        {
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

            try
            {
                OnExecuteStep();
            }
            catch (Exception ex)
            {
                AutoLogException("OnExecuteStep", ex);
            }
        }

        public event EventHandler ExecuteStep;

        /// <summary>
        /// Override this for your logic.
        /// </summary>
        protected virtual void OnExecuteStep()
        {
            if (ExecuteStep != null)
                ExecuteStep(this, EventArgs.Empty);
        }
        #endregion


        #region EventLog

        [DefaultValue(true)]
        public bool AutoLog { get; set; }


        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != string.Empty && !ValidServiceName(value))
                    throw new ArgumentException("Invalid ServiceName");
                _name = value;
            }
        }
        internal static bool ValidServiceName(string serviceName)
        {
            if (serviceName == null)
            {
                return false;
            }
            if ((serviceName.Length > 80) || (serviceName.Length == 0))
            {
                return false;
            }
            foreach (var ch in serviceName)
            {
                switch (ch)
                {
                    case '\\':
                    case '/':
                        return false;
                }
            }
            return true;
        }



        private EventLog _eventLog;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual EventLog EventLog => _eventLog ?? (_eventLog = new EventLog { Source = Name, Log = "Application" });

        private void WriteEventLogEntry(string message, EventLogEntryType errorType = EventLogEntryType.Information)
        {
            try
            {
                EventLog.WriteEntry(message, errorType);
            }
            catch (StackOverflowException)
            {
                throw;
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    
                }
            }
        }
        
        #endregion
    }
}
