using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace WindowsServiceSample
{
    public partial class Service1 : ServiceBase
    {
        //Timer timer = new Timer();
        private const string EventLogSource = "FileWatcherService";
        private const string EventLogName = "Application";
        private EventLog eventLog;
        public Service1()
        {
            InitializeComponent();

            if (!EventLog.SourceExists(EventLogSource))
            {
                EventLog.CreateEventSource(EventLogSource, EventLogName);
            }
            eventLog = new EventLog
            {
                Source = EventLogSource,
                Log = EventLogName
            };
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is 1started at " + DateTime.Now);
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        private void OnElapseTime( object source, ElapsedEventArgs e)
        {
            WriteToFile("Service was ran at " + DateTime.Now);
        }

        public void WriteToFile(string Message)
        {
            string path1 = "C:\\Folder1";
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }
            string folder1 = "C:\\Folder1\\ServiceLogs_" + DateTime.Now.ToShortDateString().Replace('/', '_') + ".txt";
            string folder2 = "C:\\Folder2\\ServiceLogs_" + DateTime.Now.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(folder1))
            {
                using (StreamWriter sw = File.CreateText(folder1)) { sw.WriteLine(Message); }
            }
            else
            {
                string path2 = "C:\\Folder2";
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
                using (StreamWriter sw = File.CreateText(folder2)) { sw.WriteLine(Message); }
                //using (StreamWriter sw = File.AppendText(filepath))
                //{

                //sw.WriteLine(Message); 
                //}
            }
            
        }
    }
}
