using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Users.Core.Entities
{
    public class RefreshToken 
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
