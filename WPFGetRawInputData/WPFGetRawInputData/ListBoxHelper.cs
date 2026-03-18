using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFGetRawInputData
{
   
    public static class ListBoxHelper
    {
        /// <summary>
        /// 让ListBox滚动到最后一项
        /// </summary>
        /// <param name="listBox">目标ListBox</param>
        public static void ScrollToEnd(this System.Windows.Controls.ListBox listBox)
        {
            if (listBox == null || listBox.Items.Count == 0)
                return;

            var scrollViewer = FindVisualChild<ScrollViewer>(listBox);
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToBottom();
                scrollViewer.UpdateLayout();
            }
        }

        /// <summary>
        /// 递归查找Visual树中的指定类型子元素
        /// </summary>
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;

                var foundChild = FindVisualChild<T>(child);
                if (foundChild != null)
                    return foundChild;
            }
            return null;
        }
    }
}
