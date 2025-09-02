using fullstackBackend.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fullstackBackend.Domain.Entities
{
   public class DepartmentEntity : BaseEntity
   {
      [Key]
      [Column("Id")]
      public int Id { get; set; }
      [Required]
      [Column("Description")]
      [StringLength(50)]
      public string Description { get; set; } = string.Empty;
   }
}
