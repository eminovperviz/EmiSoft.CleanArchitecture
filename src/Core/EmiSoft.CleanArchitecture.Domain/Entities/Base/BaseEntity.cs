using EmiSoft.CleanArchitecture.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmiSoft.CleanArchitecture.Domain.Entity;

public abstract class BaseEntity<T> : BaseEntity, IEntity, ISoftDelete
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; }

    public bool IsDeleted { get; set; } = true;
}


public class BaseEntity
{

}
