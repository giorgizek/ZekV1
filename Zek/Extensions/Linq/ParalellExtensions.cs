using System.Globalization;
using System.Linq;
using System.Threading;

namespace Zek.Extensions.Linq
{
    /// <summary>
    /// provides different extension methods to make it easy to work with
    /// TPL library. u can use those extension methods directly from your objects
    /// as they are extension methods to u do not have to initialize instance of
    /// this class to access them 
    /// </summary>
    public static class ParalellExtensions
    {
        /// <summary>
        /// When a thread is started, its culture is initially determined
        /// by using GetUserDefaultLCID from the Windows API. There seems to be no way
        /// to override this on initialization so this extension method provides
        /// functionality, to switch thread after it has been initialized
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        public static ParallelQuery<TSource> SetCulture<TSource>(this ParallelQuery<TSource> source, CultureInfo cultureInfo)
        {
            SetCulture(cultureInfo);
            return source
                .Select(
                    item =>
                        {
                            SetCulture(cultureInfo);
                            return item;
                        });
        }

        private static void SetCulture(CultureInfo cultureInfo)
        {
            if (!Equals(Thread.CurrentThread.CurrentCulture, cultureInfo))
            {
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
        }
    }
}