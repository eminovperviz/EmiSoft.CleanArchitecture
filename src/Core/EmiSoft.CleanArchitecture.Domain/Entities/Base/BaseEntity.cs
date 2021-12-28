using EmiSoft.CleanArchitecture.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmiSoft.CleanArchitecture.Domain.Entity;

public abstract class BaseEntity<T> : BaseEntity, IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual T Id { get; set; }
}


public class BaseEntity
{

}
