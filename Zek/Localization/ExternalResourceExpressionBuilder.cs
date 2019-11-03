using System;
using System.Web.Compilation;
using System.Globalization;
using System.Web.UI;
using System.CodeDom;
using System.Threading;
using System.Diagnostics;

namespace Zek.Localization
{


    /// <summary>
    /// Custom expression builder support for $ExternalResources expressions.
    /// </summary>
    public class ExternalResourceExpressionBuilder : ExpressionBuilder
    {
        private static ResourceProviderFactory s_resourceProviderFactory;

        public ExternalResourceExpressionBuilder()
        {
            Debug.WriteLine("ExternalResourceExpressionBuilder");
        }


        public static object GetGlobalResourceObject(string classKey, string resourceKey)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "ExternalResourceExpressionBuilder.GetGlobalResourceObject({0}, {1})", classKey, resourceKey));

            return GetGlobalResourceObject(classKey, resourceKey, null);
        }

        public static object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "ExternalResourceExpressionBuilder.GetGlobalResourceObject({0}, {1}, {2})", classKey, resourceKey, culture));

            EnsureResourceProviderFactory();
            var provider = s_resourceProviderFactory.CreateGlobalResourceProvider(classKey);
            return provider.GetObject(resourceKey, culture);
        }

        public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "ExternalResourceExpressionBuilder.EvaluateExpression({0}, {1}, {2}, {3})", target, entry, parsedData, context));

            var fields = parsedData as ExternalResourceExpressionFields;

            EnsureResourceProviderFactory();
            var provider = s_resourceProviderFactory.CreateGlobalResourceProvider(fields.ClassKey);

            return provider.GetObject(fields.ResourceKey, null);
        }

        public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "ExternalResourceExpressionBuilder.GetCodeExpression({0}, {1}, {2})", entry, parsedData, context));

            var fields = parsedData as ExternalResourceExpressionFields;

            var exp = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(ExternalResourceExpressionBuilder)), "GetGlobalResourceObject", new CodePrimitiveExpression(fields.ClassKey), new CodePrimitiveExpression(fields.ResourceKey));

            return exp;
        }

        public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "ExternalResourceExpressionBuilder.ParseExpression({0}, {1}, {2})", expression, propertyType, context));

            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException(String.Format(Thread.CurrentThread.CurrentUICulture, Properties.Localization.ResourceExpressionTooFewParameters, expression));
            }

            ExternalResourceExpressionFields fields = null;
            string classKey = null;
            string resourceKey = null;

            var expParams = expression.Split(new char[] { ',' });
            if (expParams.Length > 2)
            {
                throw new ArgumentException(String.Format(Thread.CurrentThread.CurrentUICulture, Properties.Localization.ResourceExpressionTooManyParameters, expression));
            }
            if (expParams.Length == 1)
            {
                throw new ArgumentException(String.Format(Thread.CurrentThread.CurrentUICulture, Properties.Localization.ResourceExpressionTooFewParameters, expression));
            }
            else
            {
                classKey = expParams[0].Trim();
                resourceKey = expParams[1].Trim();
            }

            fields = new ExternalResourceExpressionFields(classKey, resourceKey);

            EnsureResourceProviderFactory();
            var rp = s_resourceProviderFactory.CreateGlobalResourceProvider(fields.ClassKey);

            var res = rp.GetObject(fields.ResourceKey, CultureInfo.InvariantCulture);
            if (res == null)
            {
                throw new ArgumentException(String.Format(Thread.CurrentThread.CurrentUICulture, Properties.Localization.ResourceNotFound, fields.ResourceKey));
            }
            return fields;
        }

        private static void EnsureResourceProviderFactory()
        {
            if (s_resourceProviderFactory == null)
            {
                s_resourceProviderFactory = new ExternalResourceProviderFactory();
            }
        }

        public override bool SupportsEvaluate
        {
            get
            {
                Debug.WriteLine("ExternalResourceExpressionBuilder.SupportsEvaluate");
                return true;
            }
        }
    }

    public class ExternalResourceExpressionFields
    {
        internal ExternalResourceExpressionFields(string classKey, string resourceKey)
        {
            m_classKey = classKey;
            m_resourceKey = resourceKey;
        }

        public string ClassKey
        {
            get
            {
                return m_classKey;
            }
        }

        public string ResourceKey
        {
            get
            {
                return m_resourceKey;
            }
        }

        private string m_classKey;
        private string m_resourceKey;


    }


}
