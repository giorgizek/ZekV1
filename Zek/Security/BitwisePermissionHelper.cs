using Zek.Core;

namespace Zek.Security
{
    /// <summary>
    /// ორობიტი უფლებების კლასი.
    /// (1,2,4,8,16...)
    /// </summary>
    public class BitwisePermissionHelper
    {
        /// <summary>
        /// უფლებების შემოწმება.
        /// </summary>
        /// <param name="permissions">ლოგიკური უფლება (1 | 2 | 4 | 8).</param>
        /// <param name="permissionToCheck">მოქმედება (ნახვა, დამატება, შეცვლა, წაშლა...).</param>
        /// <returns>აბრუნებს true-ს თუ აქვს უფლება.</returns>
        public static bool IsPermitted(int permissions, int permissionToCheck)
        {
            return BitwiseHelper.HasFlag(permissions, permissionToCheck);
        }

        public static int AddPermission(int permissions, int permissionToAdd)
        {
            return AddPermissions(permissions, permissionToAdd);
        }
        public static int AddPermissions(int permissions, params int[] permissionsToAdd)
        {
            return BitwiseHelper.AddFlags(permissions, permissionsToAdd);
        }


        public static int DeletePermission(int permissions, int permissionToDelete)
        {
            return DeletePermissions(permissions, permissionToDelete);
        }
        public static int DeletePermissions(int permissions, params int[] permissionsToDelete)
        {
            return BitwiseHelper.DeleteFlags(permissions, permissionsToDelete);
        }


        public static int TogglePermission(int permissions, int permissionToToggle)
        {
            return BitwiseHelper.ToggleFlag(permissions, permissionToToggle);
        }
    }
}
