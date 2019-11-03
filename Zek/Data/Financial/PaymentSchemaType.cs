using System;

namespace Zek.Data.Financial
{
    /// <summary>
    /// გადახდის სქემის ტიპების ენუმერატორი.
    /// </summary>
    [Flags]
    [Serializable]
    public enum PaymentSchemaType
    {
        None = 0,
        /// <summary>
        /// ერთჯერადი.
        /// </summary>
        OnceThrough,
        /// <summary>
        /// ერთჯერადი ფასდაკლებით.
        /// </summary>
        OnceThroughWithDiscount,
        /// <summary>
        /// წელიწადში ორჯერ.
        /// </summary>
        HalfYear,
        /// <summary>
        /// წელიწადში ორჯერ ფასდაკლებით.
        /// </summary>
        HalfYearWithDiscount,
        /// <summary>
        /// კვატრლური.
        /// </summary>
        Quarterly,
        /// <summary>
        /// კვატრლური ფასდაკლებით.
        /// </summary>
        QuarterlyWithDiscount,
        /// <summary>
        /// ყოველთვიური.
        /// </summary>
        Monthly,
        /// <summary>
        /// ყოველთვიური ფასდაკლებით.
        /// </summary>
        MonthlyWithDiscount,

        /// <summary>
        /// სტანდარტული.
        /// </summary>
        All = OnceThrough | HalfYear | Quarterly | Monthly
    }
}
