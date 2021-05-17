using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace TestDataGridExtensions.Controls
{
    internal static class  ExtensionMethods
    {
        internal static T  FindAncestorOrSelf<T>(this DependencyObject item) where T : class
        {
            while (item != null)
            {
                if (item is T target)
                    return target;

                item = LogicalTreeHelper.GetParent(item) ?? VisualTreeHelper.GetParent(item);
            }

            return null;
        }

        public static IEnumerable<DependencyObject> AncestorsAndSelf(this DependencyObject self)
        {
            while (self != null)
            {
                yield return self;
                self = LogicalTreeHelper.GetParent(self) ?? VisualTreeHelper.GetParent(self);
            }
        }
    }
}
