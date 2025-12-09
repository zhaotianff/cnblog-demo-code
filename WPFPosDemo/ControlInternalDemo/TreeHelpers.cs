using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace ControlInternalDemo
{
    public static class TreeHelpers
    {
        public static T FindParent<T>(this DependencyObject obj) where T : DependencyObject
        {
            return obj.GetAncestors().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<DependencyObject> GetAncestors(this DependencyObject element)
        {
            do
            {
                yield return element;
                element = VisualTreeHelper.GetParent(element);
            } while (element != null);
        }


        public static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject source)
        {
            List<DependencyObject> children = new List<DependencyObject>();

            if (source == null)
                return children;

            children.Add(source);

            int count = VisualTreeHelper.GetChildrenCount(source);

            for (int i = 0; i < count; i++)
            {
                children.AddRange(GetVisualChildren(VisualTreeHelper.GetChild(source, i)));
            }

            return children;
        }

        public static UIElement GetItemContainerFromChildElement(ItemsControl itemsControl, UIElement child)
        {
            if (itemsControl.Items.Count > 0)
            {
                var item = itemsControl.ItemContainerGenerator.ContainerFromIndex(0);

                if (item == null)
                {
                    item = TreeHelpers.FindParent<ListBoxItem>(child);

                    if (item == null)
                        return null;
                }

                Panel panel = VisualTreeHelper.GetParent(item) as Panel;

                if (panel != null)
                {

                    UIElement parent;
                    do
                    {
                        parent = VisualTreeHelper.GetParent(child) as UIElement;
                        if (parent == panel)
                        {
                            return child;
                        }
                        child = parent;
                    }
                    while (parent != null);
                }
            }
            return null;
        }
    }
}
