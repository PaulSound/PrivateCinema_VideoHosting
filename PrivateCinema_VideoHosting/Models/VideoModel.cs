namespace PrivateCinema_VideoHosting.Models
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime UploadTime { get; set; }
    }
}
