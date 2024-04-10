using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSMEnterpriseIntegrationsAPI.Domain.Models
{
  public class UserLogin
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
  }
}
