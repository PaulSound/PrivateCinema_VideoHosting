using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrivateCinema_VideoHosting.Data
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!; // URL для хранения видео на сервере или облаке
        public DateTime UploadTime { get; set; }
        public int userId { get; set; }
        public User User { get; set; } = null!;
    }
}
