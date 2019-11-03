using System;

namespace Zek.Data
{
    /// <summary>
    /// მონაცემთა ბაზის ბრაძანებები
    /// </summary>
    [Flags]
    [Serializable]
    public enum DatabaseAction
    {
        /// <summary>
        /// არაფერი (ცარიელი).
        /// </summary>
        None = 0,

        /// <summary>
        /// ნახვა.
        /// </summary>
        View = 1,

        /// <summary>
        /// ვალიდაცია.
        /// </summary>
        Validate = 2,

        /// <summary>
        /// ჩაწერა.
        /// </summary>
        Add = 4,

        /// <summary>
        /// წაშლა.
        /// </summary>
        Delete = 8,

        /// <summary>
        /// შეცვლა.
        /// </summary>
        Edit = 16,

        /// <summary>
        /// ჩაწერა | წაშლა | შეცვლა.
        /// </summary>
        Manage = Add | Delete | Edit,

        /// <summary>
        /// დამოწმება.
        /// </summary>
        Approve = 32,

        /// <summary>
        /// დამოწმების მოხსნა.
        /// </summary>
        Disapprove = 64,

        /// <summary>
        /// დაბლოკვა.
        /// </summary>
        Block = 128,

        /// <summary>
        /// დაბლოკვის მოხსნა.
        /// </summary>
        Unblock = 256,

        /// <summary>
        /// გაუქმება.
        /// </summary>
        Cancel = 512,

        /// <summary>
        /// გაუქმების მოხსნა.
        /// </summary>
        Uncancel = 1024,

        /// <summary>
        /// ამობეჭვდა.
        /// </summary>
        Print = 2048,

        /// <summary>
        /// იმპორტი.
        /// </summary>
        Import = 4096,

        /// <summary>
        /// ექპორტი.
        /// </summary>
        Export = 8192,

        /// <summary>
        /// კოპირება.
        /// </summary>
        Copy = 16384,

        /// <summary>
        /// დამალული სვეტი.
        /// </summary>
        HiddenColumn = 32768,

        /// <summary>
        /// სინქრონიზაცია
        /// </summary>
        Synchronize = 65536,

        /// <summary>
        /// უფლებები რომლის გაკეთებაც შესაძლებელია ლიცენზიის გარეშეც.
        /// View | Print | Export | Copy | HiddenColumn
        /// </summary>
        IsPermittedWithoutLicense = View | Print | Export | Copy | HiddenColumn
    }
}
