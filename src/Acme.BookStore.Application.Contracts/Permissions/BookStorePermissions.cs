namespace Acme.BookStore.Permissions;

public static class BookStorePermissions
{
    public const string GroupName = "BookStore";



    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public const string Books = GroupName + ".Books";
    public const string Books_Create = GroupName + ".Books.Create";
    public const string Books_Delete = GroupName + ".Books.Delete";
}
