using System;
using System.Linq;
using System.Text;
using Zek.Utils;

namespace Zek.Scheduler
{
    public class SchedulerConsole : BaseScheduler
    {
        public SchedulerConsole()
        {
            _readLine = new StringBuilder();
        }

        #region Fields

        /// <summary>
        /// Checks if you can use Console.WriteLine() method.
        /// </summary>
        public virtual bool IsWriteLine => !IsTaskScheduler;

        #endregion

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            while (true)
            {
                while (!Console.KeyAvailable)
                {
                }

                var cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape)
                    break;

                //if (ReadKey != null)
                //    ReadKey(null, new ReadKeyArgs(cki));
                OnReadKey(cki);
            }
            OnStop();

        }


        protected override void OnConfigLoaded()
        {
            if (IsWriteLine)
            {
                Console.WriteLine(@"Press ESC to exit...");
                Console.WriteLine();
                Console.WriteLine(@"StartTime: " + StartTime);
                Console.WriteLine(@"EndTime: " + EndTime);
                Console.WriteLine(@"DayOfWeek: " + string.Join(", ", ((DayOfWeek[])Enum.GetValues(typeof(DayOfWeek))).Where(d => Days.Contains((int)d)).Select(d => d.ToString()).ToArray()));

                Console.WriteLine(@"Interval (d:hh:mm:ss): " + Interval.ToString(@"d\:hh\:mm\:ss"));
                Console.WriteLine(@"   Days:         {0,3}", Interval.Days);
                Console.WriteLine(@"   Hours:        {0,3}", Interval.Hours);
                Console.WriteLine(@"   Minutes:      {0,3}", Interval.Minutes);
                Console.WriteLine(@"   Seconds:      {0,3}", Interval.Seconds);
                Console.WriteLine();
            }

            base.OnConfigLoaded();
        }

        #region Virtual

        /// <summary>
        /// Save exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="fileName"></param>
        public override void LogException(Exception ex, string fileName = null)
        {
            if (IsWriteLine)
                ConsoleHelper.WriteException(ex);

            base.LogException(ex, fileName);
        }


        ///// <summary>
        ///// Override this for your logic.
        ///// </summary>
        //protected override void OnExecuteStep()
        //{
        //    if (ExecuteStep != null)
        //        ExecuteStep(this, EventArgs.Empty);

        //    //if (IsWriteLine)
        //    //    Console.WriteLine(@"Executing...");

        //    //Debug.Print(@"Executing...");

        //    //Thread.Sleep(1000);

        //    //if (IsWriteLine)
        //    //{
        //    //    Console.WriteLine(@"Done");
        //    //    Console.WriteLine();
        //    //}

        //    //Debug.Print(@"Done");
        //}

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
        }

        protected virtual void OnReadLine(string readLine)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(readLine))
                Console.WriteLine(@"--- {0} ---", readLine);
        }
        #endregion
    }
}
