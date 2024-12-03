

namespace PrivateCinema_VideoHosting.Services
{
    public class VideoCollection // Класс для присвоения номера библиотеки при регистрации
    {
        private const string CoreFolder = "PrivateCinema_VideoLibrary";
        private static string coreLibraryPath = $"{Directory.GetCurrentDirectory()}\\\\wwwroot\\\\{CoreFolder}";
        private static long uniqueCollectionId = 0;
        public static string GetNewCollectionFolder()
        {
            string path = $"{coreLibraryPath}";
            while (Directory.Exists(path + $"\\{uniqueCollectionId}"))
            {
                ++uniqueCollectionId;
            }
            DirectoryInfo newDir = Directory.CreateDirectory(path + $"\\{uniqueCollectionId}");
            newDir.Create();
            return newDir.FullName;
        }
        public static string GetCollectionFolder() {
            return coreLibraryPath;
        }
    }
}
