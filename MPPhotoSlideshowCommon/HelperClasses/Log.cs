using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MPPhotoSlideshowCommon
{
    public static class Log
    {
        private static StreamWriter log;
        public static LogType logType {get;set;}
        /// <summary>
        /// The path to the directory containing the log
        /// </summary>
        public static string DirectoryPath { get; set; }
        /// <summary>
        /// The name of the log file
        /// </summary>
        public static string FileName { get; set; }
        /// <summary>
        /// The full path to the log file
        /// </summary>
        public static string FullPath { get; set; }
        /// <summary>
        /// The file extension that the log has
        /// </summary>
        public static string Extension { get; set; }
        /// <summary>
        /// Whether the log is appending each time it is created or overwritting
        /// </summary>
        public static bool AppendLog { get; set; }
        /// <summary>
        /// The maximum log size (in bytes) before creating a new file
        /// </summary>
        public static long MaxLogSize { get; set; }
        ///<summary>
        ///A simple class for logging based on the info received
        ///</summary>  
        ///<param name="directory">The directory where the log should be created</param>
        ///<param name="filename">The name of the log file</param>
        ///<param name="extension">The extension of the log file excluding a '.'</param>
        ///<param name="type">The type of logging that should be written</param>
        ///<param name="append">Should the log be overwritten or added on to</param>
        ///<param name="maxsize">The maximum size (in bytes) the log can become before creating a new log</param>
        public static void Init(string directory, string filename, string extension, LogType type, bool append, long maxsize)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            DirectoryPath = directory;
            FileName = filename;
            Extension = extension;
            logType = type;
            AppendLog = append;
            MaxLogSize = maxsize;
            FullPath = String.Format(@"{0}\{1}.{2}", DirectoryPath, FileName, Extension);
        }
        ///<summary>
        ///A simple class for logging based on the info received
        ///</summary>  
        ///<param name="directory">The directory where the log should be created</param>
        ///<param name="filename">The name of the log file</param>
        ///<param name="extension">The extension of the log file excluding a '.'</param>
        ///<param name="type">The type of logging that should be written</param>
        public static void Init(string directory, string filename, string extension, LogType type)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            DirectoryPath = directory;
            FileName = filename;
            Extension = extension;
            logType = type;
            AppendLog = true;
            MaxLogSize = 5242880; //5MB
            FullPath = String.Format(@"{0}\{1}.{2}", DirectoryPath, FileName, Extension);
        }
        ///<summary>
        ///Writes to the log, if the file size has grown to large it will rename the log with a date stamp
        ///</summary>  
        ///<param name="format">The text to be written to the log</param>
        ///<param name="arg">The args of information that is being sent</param>
        public static void Debug(string format, params object[] arg)
        {
          if (format != null)
          {
            if (logType == LogType.Debug)
            {
              checkFileSize();
              using (StreamWriter streamWriter = new StreamWriter(FullPath, AppendLog))
              {
                streamWriter.WriteLine(DateTime.Now.ToString() + "   DEBUG         " + String.Format(format, arg));
              }
            }
          }
        }
        ///<summary>
        ///Writes to the log, if the file size has grown to large it will rename the log with a date stamp
        ///</summary>  
        ///<param name="format">The text to be written to the log</param>
        ///<param name="arg">The args of information that is being sent</param>
        public static void Error(string format, params object[] arg)
        {
          if (format != null)
          {
            checkFileSize();
            using (StreamWriter streamWriter = new StreamWriter(FullPath, AppendLog))
            {
              streamWriter.WriteLine(DateTime.Now.ToString() + "   ERROR         " + String.Format(format, arg));
            }
          }
        }
        private static void checkFileSize()
        {
          if (File.Exists(FullPath))
          {
            FileInfo logInfo = new FileInfo(FullPath);
            if (logInfo.Length > MaxLogSize)
            {
              string changeName = String.Format(@"{0}\{1}{2}.{3}", DirectoryPath, FileName, Guid.NewGuid().ToString(), Extension);
              File.Copy(FullPath, changeName);
              File.Delete(FullPath);
            }
          }
        }
    }
  
    /// <summary>
    /// The type of log that is create
    /// </summary>
    public enum LogType 
    {
        /// <summary>
        /// A log that only displays errors
        /// </summary>
        Error,
        /// <summary>
        /// A log that displays errors and all debugging text
        /// </summary>
        Debug 
    }
}
