using EmiSoft.CleanArchitecture.Application.Attributes;
using EmiSoft.CleanArchitecture.SharedKernel.Resources;

namespace EmiSoft.CleanArchitecture.Application.Enums;

public enum HttpResponseStatusType
{
    Default = 0,

    #region System exceptions [0 - 1000]

    //[LocalizedDescription("Success", typeof(SharedResources))]
    //Success = 200,

    [LocalizedDescription("Exception", typeof(SharedResources))]
    Exception = 500,

    [LocalizedDescription("Failure", typeof(SharedResources))]
    Failure = 600,

    [LocalizedDescription("Permission", typeof(SharedResources))]
    Permission = 403,

    [LocalizedDescription("NotFound", typeof(SharedResources))]
    NotFound = 404,

    [LocalizedDescription("NoContent", typeof(SharedResources))]
    NoContent = 204,

    [LocalizedDescription("OperationCancelled", typeof(SharedResources))]
    OperationCancelled = 410,

    [LocalizedDescription("Unauthorized", typeof(SharedResources))]
    Unauthorized = 999,

    #endregion

    #region Custom exceptions [1000 - 3000]

    [LocalizedDescription("PermissionDenied", typeof(SharedResources))]
    PermissionDenied = 1000,

    [LocalizedDescription("BindError", typeof(SharedResources))]
    BindError = 1001,

    [LocalizedDescription("ValidationError", typeof(SharedResources))]
    ValidationError = 1003,

    [LocalizedDescription("UserNotFound", typeof(SharedResources))]
    UserNotFound = 1993,

    [LocalizedDescription("InvalidPassword", typeof(SharedResources))]
    InvalidPassword = 1994,

    [LocalizedDescription("UserAlreadyLoggedIn", typeof(SharedResources))]
    UserAlreadyLoggedIn = 1995,

    [LocalizedDescription("UserCertificateIsInvalid", typeof(SharedResources))]
    UserCertificateIsInvalid = 1996,

    [LocalizedDescription("Locked", typeof(SharedResources))]
    Locked = 2045,

    #endregion

    #region Database Exception [3000-5000]

    [LocalizedDescription("DbException", typeof(SharedResources))]
    DbException = 3000,

    [LocalizedDescription("UniqeKeyException", typeof(SharedResources))]
    UniqeKeyException = 3001,

    [LocalizedDescription("DuplicateKeyException", typeof(SharedResources))]
    DuplicateKeyException = 3002,

    [LocalizedDescription("ForeignKeyException", typeof(SharedResources))]
    ForeignKeyException = 3003,

    #endregion

    #region Global exceptions [30000 - 40000]

    [LocalizedDescription("LoadResultError", typeof(SharedResources))]
    LoadResultError = 30000,

    [LocalizedDescription("FileNotSave", typeof(SharedResources))]
    FileNotSave = 30001,

    [LocalizedDescription("FileNotFound", typeof(SharedResources))]
    FileNotFound = 30002,

    [LocalizedDescription("FileReadError", typeof(SharedResources))]
    FileReadError = 30003,

    [LocalizedDescription("NotValid", typeof(SharedResources))]
    NotValid = 30004,

    [LocalizedDescription("PropertyIsNull", typeof(SharedResources))]
    PropertyIsNull = 30005,

    [LocalizedDescription("PropertyAlreadyExists", typeof(SharedResources))]
    PropertyAlreadyExists = 30006,

    [LocalizedDescription("IdNotExists", typeof(SharedResources))]
    IdNotExists = 30007,

    [LocalizedDescription("RowAlreadyExists", typeof(SharedResources))]
    RowAlreadyExists = 30008,

    [LocalizedDescription("FileContentIsEmpty", typeof(SharedResources))]
    FileContentIsEmpty = 30009,

    [LocalizedDescription("NoPermission", typeof(SharedResources))]
    NoPermission = 30010,

    [LocalizedDescription("IncorrectSyntax", typeof(SharedResources))]
    IncorrectSyntax = 30011,

    [LocalizedDescription("MoreColumnCount", typeof(SharedResources))]
    MoreColumnCount = 30012,
    #endregion



}
