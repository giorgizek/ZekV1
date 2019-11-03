using System;

namespace Zek.Data
{
    /// <summary>
    /// მონაცმებების სტატუსები
    /// </summary>
    [Serializable]
    public enum DatabaseStatus
    {
        /// <summary>
        /// N/A
        /// </summary>
        None = 0,

        /// <summary>
        /// დასადასტურებელი.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// დადასტურებული.
        /// </summary>
        Approved = 2,

        /// <summary>
        /// მოძველებული.
        /// </summary>
        Obsolete = 3,

        /// <summary>
        /// უკან დაბრუნებული.
        /// </summary>
        Backordered = 4,

        /// <summary>
        /// უარყოფილი.
        /// </summary>
        Rejected = 5,

        /// <summary>
        /// გადაზიდული, დატვირთული.
        /// </summary>
        Shipped = 6,

        /// <summary>
        /// გაუქმებული, უარყოფილი. (Annuled)
        /// </summary>
        Cancelled = 7,

        /// <summary>
        /// დასრულებული
        /// </summary>
        Completed = 8,

        /// <summary>
        /// წაშლილი
        /// </summary>
        Deleted = 9,

        /// <summary>
        /// დაბლოკილი
        /// </summary>
        Blocked = 10,
    }
}
