using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrivateCinema_VideoHosting.Data
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string LibraryNumb { get; set; } = null!;

        public int SignInId { get; set; }
        public SignIn SignIn { get; set; } = null!;
        public List<Video> videoCollection { get; set; } = new();
    }
}
