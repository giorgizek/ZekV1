using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System.Collections.Generic;

namespace Zek.Extensions.Report
{
    public static class StiReportExtensions
    {
        public static void SetVariables(this StiReport stiReport, Dictionary<string, object> variables)
        {
            if (variables == null || variables.Count == 0) return;

            foreach (var kvp in variables)
            {
                stiReport[kvp.Key] = kvp.Value;
            }

            if (!stiReport.IsCompiled)
            {
                foreach (var kvp in variables)
                {
                    if (!stiReport.Dictionary.Variables.Contains(kvp.Key)) continue;

                    stiReport.Dictionary.Variables[kvp.Key].ValueObject = kvp.Value;
                }
            }
        }

        public static void SetPagesSize(this StiReport stiReport, int pageWidth, int pageHeight)
        {
            if (pageWidth != -1)
            {
                foreach (StiPage page in stiReport.Pages)
                    page.PageWidth = pageWidth;
            }
            if (pageHeight != -1)
            {
                foreach (StiPage page in stiReport.Pages)
                    page.PageHeight = pageHeight;
            }
        }
        public static void SetPageSize(this StiPage page, int pageWidth, int pageHeight)
        {
            if (pageWidth != -1)
            {
                page.PageWidth = pageWidth;
            }
            if (pageHeight != -1)
            {
                page.PageHeight = pageHeight;
            }
        }
    }
}
