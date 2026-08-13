using Microsoft.AspNetCore.Http;
using System.Net;

namespace Stenford.Common.Utility
{
    public class FTPHelper
    {
        public static string ftpServer = "168.231.120.235";
        public static string ftpUsername = "stendfordappftpuser";
        public static string ftpPassword = "Stendford@123";

        public static string SanitizeFileName(string fileName)
        {
            return Path.GetFileName(fileName)
                       .Replace(" ", "_")
                       .Replace("\"", "")
                       .Replace("'", "")
                       .Replace(":", "")
                       .Replace("?", "")
                       .Replace("<", "")
                       .Replace(">", "")
                       .Replace("|", "")
                       .Replace("*", "");
        }

        public static void EnsureDirectoryExists(string folderPath)
        {
            string[] folders = folderPath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string currentPath = $"ftp://{ftpServer}";

            foreach (var folder in folders)
            {
                currentPath += "/" + folder;

                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(currentPath);
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    request.UsePassive = true;
                    request.KeepAlive = false;

                    using (var resp = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine($"Folder created: {resp.StatusDescription}");
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is FtpWebResponse response &&
                        response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        Console.WriteLine($"Folder already exists: {currentPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Error creating folder {folder}: {ex.Message}");
                    }
                }
            }
        }

        public static async Task<string> UploadFileFTPAsync(IFormFile file, string directoryPath)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            string cleanName = SanitizeFileName(file.FileName);
            string fileName = $"{Guid.NewGuid()}_{cleanName}";

            directoryPath = directoryPath.Trim('/');

            string url = $"ftp://{ftpServer}/{directoryPath}/{fileName}";

            var request = (FtpWebRequest)WebRequest.Create(url);
            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = false;

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                await file.CopyToAsync(requestStream);
            }

            using (var response = (FtpWebResponse)await request.GetResponseAsync())
            {
                Console.WriteLine(response.StatusDescription);
            }

            //return $"{directoryPath}/{fileName}";
            return fileName;
        }

        public static void DeleteExistingFile(string directoryPath, string existingFileUrl)
        {
            if (string.IsNullOrEmpty(existingFileUrl))
                return;

            try
            {
                string ftpFilePath = $"{directoryPath}/{existingFileUrl}".Replace("\\", "/").TrimStart('/');
                string url = $"ftp://{ftpServer}/{ftpFilePath}";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine("Delete File Complete, status " + response.StatusDescription);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("FTP Delete Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Delete Error: " + ex.Message);
            }
        }
    }
}