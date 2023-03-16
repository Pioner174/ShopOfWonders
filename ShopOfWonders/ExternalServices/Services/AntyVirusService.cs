using SOW.ShopOfWonders.ExternalServices.Interfaces;
using System.Diagnostics;
using System.IO;

namespace SOW.ShopOfWonders.ExternalServices.Services
{
    public class AntyVirusService : IAntyVirusService
    {

        private static bool _isDefenderAvailable;
        private static string _defenderPath;
        private static SemaphoreSlim _lock = new SemaphoreSlim(5); //лимит ограничен 5 потоками

        //static ctor
        public AntyVirusService()
        {
            if (OperatingSystem.IsWindows())
            {
                _defenderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Windows Defender", "MpCmdRun.exe");
                _isDefenderAvailable = File.Exists(_defenderPath);
            }
            else
                _isDefenderAvailable = false;
        }

        public async Task<bool> IsVirus(string filePath, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);

            try
            {
                using (var process = Process.Start(_defenderPath, $"-Scan -ScanType 3 -File \"{filePath}\" "))
                {
                    if (process == null)
                    {
                        _isDefenderAvailable = false; // Пометка о недоступности антивируса

                        throw new InvalidOperationException("Ошибка в запуске MpCmdRun.exe");// исключение антивируса
                    }

                    try
                    {
                        await process.WaitForExitAsync();
                    }
                    catch (Exception ex) //
                    {
                        throw new TimeoutException("Ошибка в работе антивирусного модуля", ex);
                    }
                    finally
                    {
                        process.Kill(); //always kill the process, it's fine if it's already exited, but if we were timed out or cancelled via token - let's kill it
                    }

                    return !File.Exists(filePath);
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<bool> IsVirus(IFormFile file, CancellationToken cancellationToken)
        {
            if (_isDefenderAvailable)
                throw new InvalidOperationException("Ошибка в запуске MpCmdRun.exe");

            string path = Path.GetTempPath();

            string fullPath = Path.Combine(path, file.FileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return await IsVirus(fullPath, cancellationToken);
        }

    }
}
