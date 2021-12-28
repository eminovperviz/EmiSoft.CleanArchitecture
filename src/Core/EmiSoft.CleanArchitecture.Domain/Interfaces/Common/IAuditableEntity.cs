namespace EmiSoft.CleanArchitecture.Domain.Interfaces;

public interface IAuditableEntity
{
    DateTime CreatedDate { get; set; }
    int CreatedBy { get; set; }

    DateTime? LastModifiedDate { get; set; }
    int? LastModifiedBy { get; set; }
}
