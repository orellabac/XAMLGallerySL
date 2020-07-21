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

namespace AppUIBasics
{
    public partial class AllControls : Page
    {
        public AllControls()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            const int itemsPerRow = 2;
            StackPanel currentRow = new StackPanel() { Orientation = Orientation.Horizontal };
            this.Items.Children.Add(currentRow);
            foreach(var group in ControlInfoDataSource.Instance.GetGroups())
            {
                foreach(var item in group.Items)
                {
                    if (currentRow.Children.Count == itemsPerRow)
                    {
                        currentRow = new StackPanel() { Orientation = Orientation.Horizontal };
                        this.Items.Children.Add(currentRow);
                    }
                    currentRow.Children.Add(new ControlItemTemplate() { DataContext = item });
                }
            }
        }

    }
}
