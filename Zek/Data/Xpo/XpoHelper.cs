using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using Zek.Configuration;

namespace Zek.Data.Xpo
{
    public class XpoHelper
    {
        /*
         if (OptionsGrid.GridView.GridControl != null)
                    OptionsGrid.GridView.GridControl.ForceInitialize();
                OptionsGrid.GridView.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
         */

        private static string ConnectionString { get; set; }


        public static void Connect()
        {
            Connect(BaseAppConfig.ConnectionString);
        }
        public static void Connect(string connectionString)
        {
            Connect(connectionString, AutoCreateOption.None);
        }
        public static void Connect(string connectionString, AutoCreateOption autoCreateOption)
        {
            ConnectionString = connectionString;

            XpoDefault.DataLayer = XpoDefault.GetDataLayer(ConnectionString, autoCreateOption);
            XpoDefault.Session = null;
        }


        //public static IDataStore GetConnectionProvider(AutoCreateOption autoCreateOption)
        //{
        //    return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption);
        //}
        //public static IDataStore GetConnectionProvider(AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        //{
        //    return XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOption, out objectsToDisposeOnDisconnect);
        //}


        //public static IDataLayer GetDataLayer()
        //{
        //    return GetDataLayer(AutoCreateOption.None);
        //}
        //public static IDataLayer GetDataLayer(AutoCreateOption autoCreateOption)
        //{
        //    return XpoDefault.GetDataLayer(autoCreateOption);
        //}


        public static XPServerCollectionSource GetNewXPServerCollectionSource<T>(string fixedFilterString = null)
        {
            return GetNewXPServerCollectionSource(typeof(T), fixedFilterString);
        }

        public static XPServerCollectionSource GetNewXPServerCollectionSource(Type classType, string fixedFilterString = null)
        {
            //string fixedFilterString = string.Format("BranchID = {0} AND Quantity > \'0.00\'", BranchID);

            // Create a Session object.
            var session = new Session(XpoDefault.DataLayer);

            // Create an XPClassInfo object corresponding to the Person_Contact class.
            var classInfo = session.GetClassInfo(classType);

            // Create an XPServerCollectionSource object.
            var xpServerCollectionSource = new XPServerCollectionSource(session, classInfo) { FixedFilterString = fixedFilterString };

            return xpServerCollectionSource;
        }

        //private static IDataLayer GetDataLayer()
        //{
        //    XpoDefault.Session = null;

        //    string conn = MSSqlConnectionProvider.GetConnectionString("(local)", "XpoWebTest");

        //    XPDictionary dict = new ReflectionDictionary();

        //    IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.SchemaAlreadyExists);

        //    dict.GetDataStoreSchema(typeof(PersistentObjects.Customer).Assembly);

        //    IDataLayer dl = new ThreadSafeDataLayer(dict, store);
        //    return dl;
        //}



        //public static Session GetNewSession()
        //{
        //    return new Session(XpoDefault.DataLayer);
        //}
        //public static UnitOfWork GetNewUnitOfWork()
        //{
        //    return new UnitOfWork(XpoDefault.DataLayer);
        //}

        //private readonly static object _lockObject = new object();
        //private static volatile IDataLayer _dataLayer;
        //private static IDataLayer DataLayer
        //{
        //    get
        //    {
        //        if (_dataLayer == null)
        //        {
        //            lock (_lockObject)
        //            {
        //                if (_dataLayer == null)
        //                {
        //                    _dataLayer = GetDataLayer();
        //                }
        //            }
        //        }

        //        return _dataLayer;
        //    }
        //}



    }




   /* /// <summary>
    /// Static useful method for XPO.
    /// </summary>
    public static class XpoHelper
    {
        private static Session _Session;

        [ThreadStatic]
        private static Session _SessionPerThread;

        private const int DefaultMaxRowsCount = 2000;

        /// <summary>
        /// XPO session.
        /// </summary>
        public static Session Session
        {
            get { return GetSession(); }
        }

        /// <summary>
        /// XPO session.
        /// </summary>
        public static Session NewSession
        {
            get { return GetNewSession(); }
        }

        /// <summary>
        /// Return all objects of certain type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetObjects<T>()
        {
            return GetObjects(typeof(T), null).Cast<T>().ToList();
        }

        /// <summary>
        /// Return all objects of certain type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetObjects<T>(Session session)
        {
            return GetObjects(typeof(T), null).Cast<T>().ToList();
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="criteria"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetObjects<T>(string criteria)
        {
            return GetObjects(typeof(T), CriteriaOperator.Parse(criteria)).Cast<T>().ToList();
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="criteria"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetObjects<T>(CriteriaOperator criteria)
        {
            return GetObjects(typeof(T), criteria).Cast<T>().ToList();
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="session"> </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="criteria">
        /// The criteria.
        /// </param>
        /// <returns>
        /// Collection of certain type.
        /// </returns>
        public static ICollection GetObjects(Session session, Type type, CriteriaOperator criteria)
        {
            XPClassInfo objectInfo = GetSession().GetClassInfo(type);
            return session.GetObjects(objectInfo, criteria, null, DefaultMaxRowsCount, false, true);
        }

        /// <summary>
        /// Return collection of certain type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="criteria">
        /// The criteria.
        /// </param>
        /// <returns>
        /// Collection of certain type.
        /// </returns>
        public static ICollection GetObjects(Type type, CriteriaOperator criteria)
        {
            return GetObjects(GetSession(), type, criteria);
        }

        /// <summary>
        /// Find object of certain type by criteria.
        /// </summary>
        /// <typeparam name="T">Required type</typeparam>
        /// <param name="criteria">Criteria for researching</param>
        /// <returns>Return an object of certain type by criteria</returns>
        public static T FindObjectById<T>(CriteriaOperator criteria)
        {
            return GetSession().FindObject<T>(criteria);
        }

        /// <summary>
        /// Find object of certain type by criteria.
        /// </summary>
        /// <param name="type">Required type</param>
        /// <param name="criteria">Criteria for researching</param>
        /// <returns>Return an object of certain type by criteria</returns>
        public static object FindObjectById(Type type, CriteriaOperator criteria)
        {
            return GetSession().FindObject(type, criteria);
        }

        /// <summary>
        /// Save XPO object.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public static void Save(object xpoObject)
        {
            GetSession().Save(xpoObject);
        }

        /// <summary>
        /// Save XPO object and if open transaction close it.
        /// </summary>
        /// <param name="xpoObject">XPO object.</param>
        public static void SaveAndCommit(object xpoObject)
        {
            var session = xpoObject is IXPObject ? (xpoObject as IXPObject).Session : GetSession();
            session.Save(xpoObject);
            if (session.InTransaction)
            {
                session.CommitTransaction();
            }
        }

        public static void DeleteAndCommit(object xpoObject)
        {
            if (xpoObject == null) return;

            var session = GetSession();
            session.Delete(xpoObject);
            if (session.InTransaction)
            {
                session.CommitTransaction();
            }
        }
        public static void DeleteAndCommit(object xpoObject, Session session)
        {
            if (xpoObject == null) return;
            if (session == null) return;

            session.Delete(xpoObject);
            if (session.InTransaction)
            {
                session.CommitTransaction();
            }
        }


        public static void DeleteAndCommit(IEnumerable xpoObjects)
        {
            var session = GetSession();

            foreach (var obj in xpoObjects)
            {
                session.Delete(obj);
            }

            if (session.InTransaction)
            {
                session.CommitTransaction();
            }
        }

        public static void CommitTransaction()
        {
            if (Session.InTransaction)
            {
                Session.CommitTransaction();
            }
        }

        public static void RefreshSession()
        {
            _Session = null;
        }

        /// <summary>
        /// Return new XPO session.
        /// </summary>
        /// <returns></returns>
        private static Session GetNewSession()
        {
            return new Session(XpoDefault.DataLayer);
        }

        /// <summary>
        /// Return current XPO session.
        /// </summary>
        /// <returns>XPO session.</returns>
        private static Session GetSession()
        {
            // if there is HTTP context then session is already there (created per request)
            // otherwise it is created as usual
            var current = HttpContext.Current;
            var sessionPerRequest = current == null ? null : current.Items[typeof(Session)] as Session;

            // we are trying to use session for current thread or for current request or ordinary session
            return _Session = _SessionPerThread ?? sessionPerRequest ?? _Session ?? GetNewSession();
        }

        /// <summary>
        /// Deletes XPO Object.
        /// </summary>
        public static void Delete(object xpoObject)
        {
            GetSession().Delete(xpoObject);
        }

        /// <summary>
        /// Returns XPO object of given type and id.
        /// </summary>
        /// <param name="id">
        /// The id of object to return.
        /// </param>
        /// <returns>
        /// Object of given type and with provided id.
        /// </returns>
        public static T GetById<T>(object id)
        {
            return GetSession().GetObjectByKey<T>(id);
        }

        /// <summary>
        /// Creates object of given type.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <returns>Instance of given type.</returns>
        public static object Create<T>()
        {
            return Create(typeof(T));
        }

        /// <summary>
        /// Creates object of given type.
        /// </summary>
        /// <param name="type">Type of object to create.</param>
        /// <returns>Instance of given type.</returns>
        public static object Create(Type type)
        {
            return Activator.CreateInstance(type, GetSession());
        }

        /// <summary>
        /// Queries objects of given type and returns them as array.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="T">
        /// Type of objects.
        /// </typeparam>
        /// <returns>
        /// Array of objects.
        /// </returns>
        public static IEnumerable<T> Query<T>(Func<XPQuery<T>, IEnumerable<T>> filter = null)
        {
            var xpQuery = new XPQuery<T>(Session);
            return (filter == null ? xpQuery : filter(xpQuery)).ToArray();
        }

        /// <summary>
        /// Queries objects of given type and returns them as array.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="T">
        /// Type of objects.
        /// </typeparam>
        /// <returns>
        /// Array of objects.
        /// </returns>
        public static T QueryFirstOrDefault<T>(Func<XPQuery<T>, IEnumerable<T>> filter = null) where T : class
        {
            var xpQuery = new XPQuery<T>(Session);
            return (filter == null ? xpQuery : filter(xpQuery)).FirstOrDefault();
        }

        /// <summary>
        /// Queries objects of given type and returns them as array.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <typeparam name="T">
        /// Type of objects.
        /// </typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <returns>
        /// Array of objects.
        /// </returns>
        public static IEnumerable<TOut> Query<T, TOut>(Func<XPQuery<T>, IEnumerable<TOut>> filter)
        {
            var xpQuery = new XPQuery<T>(Session);
            return filter(xpQuery).ToArray();
        }

        public static void SetSession(Session session)
        {
            _Session = session;
        }

        /// <summary>
        /// Sets session for current thread and returns previous one if it was not disposed or null otherwise.
        /// </summary>
        /// <param name="session">
        /// Session to set.
        /// </param>
        /// <param name="disposePrevious">
        /// Should previous session (for current thread) be disposed?
        /// </param>
        /// <returns>
        /// Previous session if it was not disposed or null otherwise.
        /// </returns>
        public static Session SetSessionForThread(Session session, bool disposePrevious = true)
        {
            if (disposePrevious && _SessionPerThread != null)
            {
                _SessionPerThread.Dispose();
            }

            var previous = _SessionPerThread;
            _SessionPerThread = session;

            return disposePrevious ? null : previous;
        }

        /// <summary>
        /// Sets session for current HTTP request and returns previous one if it was not disposed or null otherwise.
        /// </summary>
        /// <param name="session">
        /// Session to set.
        /// </param>
        /// <param name="disposePrevious">
        /// Should previous session (for current HTTP request) be disposed?
        /// </param>
        /// <returns>
        /// Previous session if it was not disposed or null otherwise.
        /// </returns>
        public static Session SetSessionForRequest(Session session, bool disposePrevious = true)
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                throw new ApplicationException("There is no HTTP context.");
            }

            var previous = context.Items[typeof(Session)] as Session;
            if (disposePrevious && previous != null)
            {
                previous.Dispose();
            }

            context.Items[typeof(Session)] = session;

            return disposePrevious ? null : previous;
        }
    }/*/
}
