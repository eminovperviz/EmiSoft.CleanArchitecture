namespace EmiSoft.CleanArchitecture.Application.Models.DTOs;

public class PagingRequest : BaseDTO
{
    public PagingRequest()
    {
        Filters = new List<PagingRequestFilter>();
    }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 15;
    public List<PagingRequestFilter> Filters { get; set; }
}

public class PagingRequestFilter : BaseDTO
{

    private object? _v;
    private string? _fieldName;

    public object Value
    {
        get
        {
            if (_fieldName.IndexOf(HashName) != -1 && FieldName.Substring(_fieldName.Length - 4, 4) == HashName)
                return Decrypt<int>(_v.ToString());

            return _v;
        }
        set { _v = value; }
    }

    public string FieldName
    {
        get { return _fieldName; }
        set { _fieldName = value; }
    }

    public string EqualityType { get; set; }
}