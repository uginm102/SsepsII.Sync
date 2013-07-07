using SsepsII.Synchronisation.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SsepsII.Synchronisation.Services
{
    public static class ExtensionMethods
    {
        //public static bool CanManageServiceRecords(this IUserContext userContext)
        //{
        //    return userContext.HasPermission(StandardPermissionProvider.PromoteEmployee) ||
        //        userContext.HasPermission(StandardPermissionProvider.DemoteEmployee) ||
        //        userContext.HasPermission(StandardPermissionProvider.InternalTransfer) ||
        //        userContext.HasPermission(StandardPermissionProvider.IncrementEmployee);
        //}

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T element in enumerable)
            {
                action(element);
            }
        }

        public static bool IsNull<T>(this T @object)
        {
            return Equals(@object, null);
        }

        public static bool IsNotNull<T>(this T @object)
        {
            return !IsNull(@object);
        }

        public static string StringJoin<T>(this IEnumerable<T> enumerable, string valueEnclosure, string separator)
        {
            var stringBuilder = new StringBuilder();
            if (enumerable.IsNotNull())
                enumerable.ForEach(
                    element => stringBuilder.Append(stringBuilder.Length == 0
                                                        ? string.Format("{0}{1}{0}", valueEnclosure, element)
                                                        : string.Format("{2}{0}{1}{0}", valueEnclosure, element, separator)));
            return stringBuilder.ToString();
        }

        public static bool IsNotNullOrWhiteSpace(this String value)
        {
            return !String.IsNullOrWhiteSpace(value);
        }

        public static XmlAttributeCollection GetNewAttributesFromXml(this TransData table)
        {
            XmlDocument xRowDoc;
            xRowDoc = new XmlDocument();
            xRowDoc.LoadXml(table.NewValues);
            XmlAttribute logRefId = xRowDoc.CreateAttribute("LogRefID");
            logRefId.Value = table.LogRefID;
            xRowDoc.FirstChild.Attributes.Append(logRefId);
            return xRowDoc.FirstChild.Attributes;
        }

        public static XmlAttributeCollection GetOldAttributesFromXml(this TransData table)
        {
            XmlDocument xRowDoc;
            xRowDoc = new XmlDocument();
            if (table.OldValues == null || table.OldValues == string.Empty) return null; ;
            xRowDoc.LoadXml(table.OldValues);
            XmlAttribute logRefId = xRowDoc.CreateAttribute("LogRefID");
            logRefId.Value = table.LogRefID;
            xRowDoc.FirstChild.Attributes.Append(logRefId);
            return xRowDoc.FirstChild.Attributes;
        }

        public static string GetValue(this XmlAttributeCollection collection, string name)
        {
            return collection != null ? collection.GetNamedItem(name) != null ? collection.GetNamedItem(name).Value : string.Empty : string.Empty;
        }

        public static bool HasOldValue(this TransData table)
        {
            return !String.IsNullOrEmpty(table.OldValues);
        }

        public static bool IsApproved(this TransData table)
        {
            return table.state == 1;
        }
    }
}
