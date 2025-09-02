using fullstackBackend.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fullstackBackend.Domain.Entities
{
   public class EmployeeEntity : BaseEntity
   {
      [Key]
      [Column("Id")]
      public int Id { get; set; }
      [Required]
      [Column("FisrtName")]
      [StringLength(50)]
      public string FirstName { get; set; } = string.Empty;
      [Required]
      [Column("LastName")]
      [StringLength(50)]
      public string LastName { get; set; } = string.Empty;
      [Required]
      [Column("HireDate")]
      [DataType(DataType.DateTime)]
      public DateTime HireDate { get; set; }
      [Column("Address")]
      [StringLength(100)]
      public string Address { get; set; } = string.Empty;
      [Column("Phone")]
      [StringLength(15)]
      [DataType(DataType.PhoneNumber)]
      public string Phone { get; set; } = string.Empty;
      [Required]
      [Column("DepartmentId")]
      public int DepartmentId { get; set; }
      public virtual DepartmentEntity? Department { get; set; }
   }
}
