using System.Text.Json.Serialization;

namespace EmiSoft.CleanArchitecture.Application.Models.DTOs;

public sealed class DropDownDto : BaseDTO
{

    public string ValueHash { get; set; }

    [JsonIgnore]
    public int Value { get { return Decrypt<int>(ValueHash); } set { ValueHash = Encrypt(value); } }

    public string DisplayText { get; set; }
    public bool Select { get; set; }
}
