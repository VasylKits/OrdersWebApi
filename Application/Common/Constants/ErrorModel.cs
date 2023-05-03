namespace Application.Common.Constants;

public static class ErrorModel
{
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string UserIsNotSaved = "USER_IS_NOT_SAVED";
    public const string UserIsNotCreated = "USER_IS_NOT_CREATED";
    public const string UserIsNotUpdated = "USER_IS_NOT_UPDATED";
    public const string UserIsNotDeleted = "USER_IS_NOT_DELETED";

    public const string LoginNameAlreadyTaken = "LOGIN_NAME_ALREADY_TAKEN";
    public const string UserHasOrdersAndCannotBeDeleted = "USER_HAS_ORDERS_AND_CANNOT_BE_DELETED";

    public const string OrderNotFound = "ORDER_NOT_FOUND";
    public const string OrderIsNotSaved = "ORDER_IS_NOT_SAVED";
    public const string OrderIsNotCreated = "ORDER_IS_NOT_CREATED";
    public const string OrderIsNotUpdated = "ORDER_IS_NOT_UPDATED";
    public const string OrderIsNotDeleted = "ORDER_IS_NOT_DELETED";
   
    public const string OnlyOneOrderMayBeCreatedForUserPerDay = "ONLY_ONE_ORDER_MAY_BE_CREATED_FOR_USER_PER_DAY";
}