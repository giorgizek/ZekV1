using System;
using System.Web.Compilation;
using System.Diagnostics;
using System.Globalization;

namespace Zek.Localization
{

    public class DBResourceProviderFactory : ResourceProviderFactory
    {

        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "DBResourceProviderFactory.CreateGlobalResourceProvider({0})", classKey));
            return new DBResourceProvider(classKey); 
        }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "DBResourceProviderFactory.CreateLocalResourceProvider({0}", virtualPath));

            // we should always get a path from the runtime
            var classKey = virtualPath;
            if (!string.IsNullOrEmpty(virtualPath))
            {
                virtualPath = virtualPath.Remove(0, 1);
                classKey = virtualPath.Remove(0, virtualPath.IndexOf('/') + 1);
            }

            return new DBResourceProvider(classKey);
        }
    }
}