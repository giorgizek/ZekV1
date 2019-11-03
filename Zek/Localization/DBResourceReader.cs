using System;
using System.Resources;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Zek.Localization
{

   /// <summary>
   /// Implementation of IResourceReader required to retrieve a dictionary
   /// of resource values for implicit localization. 
   /// </summary>
   public class DBResourceReader : DisposableBaseType, IResourceReader, IEnumerable<KeyValuePair<string, object>> 
   {

       private ListDictionary _resourceDictionary;
       public DBResourceReader(ListDictionary resourceDictionary)
       {
           Debug.WriteLine("DBResourceReader()");

           _resourceDictionary = resourceDictionary;
       }

       protected override void Cleanup()
       {
           try
           {
               _resourceDictionary = null;
           }
           finally
           {
               base.Cleanup();
           }
       }

       #region IResourceReader Members

       public void Close()
       {
           Dispose();
       }

       public IDictionaryEnumerator GetEnumerator()
       {
           Debug.WriteLine("DBResourceReader.GetEnumerator()");
           
           // NOTE: this is the only enumerator called by the runtime for 
           // implicit expressions

           if (Disposed)
           {
               throw new ObjectDisposedException("DBResourceReader object is already disposed.");
           }

           return _resourceDictionary.GetEnumerator();
       }

       #endregion

       #region IEnumerable Members

       IEnumerator IEnumerable.GetEnumerator()
       {
           if (Disposed)
           {
               throw new ObjectDisposedException("DBResourceReader object is already disposed.");
           }

           return _resourceDictionary.GetEnumerator();
       }

       #endregion




       #region IEnumerable<KeyValuePair<string,object>> Members

       IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
       {
           if (Disposed)
           {
               throw new ObjectDisposedException("DBResourceReader object is already disposed.");
           }

           return _resourceDictionary.GetEnumerator() as IEnumerator<KeyValuePair<string, object>>;
       }

       #endregion
   }

}