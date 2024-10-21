using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace GenshinImpactMain.Command
{
    public static class Logger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");

        static Logger()
        {
            // 确保 Log 文件夹存在
            EnsureLogDirectoryExists();
        }

        private static void EnsureLogDirectoryExists()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        public static void LogError(string message, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            string fileName = Path.GetFileName(sourceFilePath); // 只获取文件名

            string logFileName = $"{DateTime.Now:yyyy-MM-dd}_log.txt";
            string logFilePath = Path.Combine(LogDirectory, logFileName);

            var logEntry = new StringBuilder();
            logEntry.AppendLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR in {memberName} at {fileName}:{sourceLineNumber} - {message}");

            try
            {
                File.AppendAllText(logFilePath, logEntry.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        public static async Task LogErrorAsync(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            string fileName = Path.GetFileName(sourceFilePath); // 只获取文件名

            string logFileName = $"{DateTime.Now:yyyy-MM-dd}_log.txt";
            string logFilePath = Path.Combine(LogDirectory, logFileName);

            var logEntry = new StringBuilder();
            logEntry.AppendLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR in {memberName} at {fileName}:{sourceLineNumber} - {message}");

            try
            {
                await File.AppendAllTextAsync(logFilePath, logEntry.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
    }
}
