using EmiSoft.CleanArchitecture.SharedKernel.Utility;


namespace EmiSoft.CleanArchitecture.Application.Models.DTOs;

public class BaseDTO
{
    protected const string HashName = "Hash";

    protected string Encrypt(int? id)
    {
        if (id == null)
            return null;

        return TextEncryption.Encrypt(id.ToString());
    }

    protected T Decrypt<T>(string id)
    {
        if (id is null)

            return default;

        return (T)Convert.ChangeType(TextEncryption.Decrypt(id.ToString()), typeof(T));

    }
}

