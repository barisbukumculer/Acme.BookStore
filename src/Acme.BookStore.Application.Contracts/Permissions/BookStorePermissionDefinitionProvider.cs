using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookStorePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookStorePermissions.MyPermission1, L("Permission:MyPermission1"));

        var booksPermission = myGroup.AddPermission(BookStorePermissions.Books, new FixedLocalizableString("Books page"));
        booksPermission.AddChild(BookStorePermissions.Books_Create, new FixedLocalizableString("Create a new book"));
        booksPermission.AddChild(BookStorePermissions.Books_Delete, new FixedLocalizableString("Delete books"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
