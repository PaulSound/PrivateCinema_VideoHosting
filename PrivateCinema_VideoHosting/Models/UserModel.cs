using PrivateCinema_VideoHosting.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateCinema_VideoHosting.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LibraryNumb { get; set; } = null!;

    }
}
