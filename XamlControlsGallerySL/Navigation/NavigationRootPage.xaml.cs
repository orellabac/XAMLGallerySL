using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using AppUIBasics.Data;

namespace AppUIBasics.Navigation
{
    public partial class NavigationRootPage : UserControl
    {
        public NavigationRootPage()
        {
            InitializeComponent();
            this.AddNavigationMenuItems();
            RootFrame = frame;
        }

        static public Frame RootFrame;

        private void AddNavigationMenuItems()
        {
            foreach (var group in ControlInfoDataSource.Instance.Groups.OrderBy(i => i.Title))
            {
                var itemGroup = new NavigationViewControl() { Tag = group.UniqueId, DataContext = group };
                //var hyperLink = new HyperlinkButton() { Content = group.Title };
                this.NavigationViewControl.Children.Add(itemGroup);
                //var itemGroup = new Microsoft.UI.Xaml.Controls.NavigationViewItem() { Content = group.Title, Tag = group.UniqueId, DataContext = group, Icon = GetIcon(group.ImagePath) };
                //AutomationProperties.SetName(itemGroup, group.Title);

                foreach (var item in group.Items)
                {
                    var itemInGroup = new HyperlinkButton() { DataContext = item, Height=28 };
                    itemInGroup.Click += ItemInGroup_Click;
                    var content = new StackPanel() { Orientation = Orientation.Horizontal };
                    content.Children.Add(Utils.GetIcon(item.ImagePath));
                    content.Children.Add(new TextBlock() { Text = item.Title, VerticalAlignment = VerticalAlignment.Center });
                    itemInGroup.Content = content;
                    //var itemInGroup = new Microsoft.UI.Xaml.Controls.NavigationViewItem() { Content = item.Title, Tag = item.UniqueId, DataContext = item, Icon = GetIcon(item.ImagePath) };
                    itemGroup.Items.Children.Add(itemInGroup);
                    //AutomationProperties.SetName(itemInGroup, item.Title);
                }


            }
        }

        private void ItemInGroup_Click(object sender, RoutedEventArgs e)
        {
            var linkButton = sender as HyperlinkButton;
            if (linkButton != null)
            {
                var dataItem = linkButton.DataContext as ControlInfoDataItem;
                if (dataItem != null && dataItem.Page != null)
                {
                    frame.Navigate(new Uri(dataItem.Page, UriKind.Relative));
                }
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
