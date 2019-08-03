﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Stashdex
{
    /// <summary>
    /// Interaction logic for FilterOptionsWindow.xaml
    /// </summary>
    public partial class FilterOptionsWindow : Window
    {
        public FilterOptionsWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            List<Filter> filterlist = new List<Filter>();
            Filter filter = new Filter();
            string suffixNumber = "";


            foreach (TextBox tb in FindVisualChildren<TextBox>(this)) {
                if (tb.Name.Contains("filterModTxtBox") && !string.IsNullOrEmpty(tb.Text)) {
                    filter = new Filter();
                    suffixNumber = tb.Name.Split('_').Last();
                    filter.filtername = tb.Text;
                }
                if (tb.Name == $"numberMinTxtBox_{suffixNumber}") {
                    if (tb.Text != "") filter.minValue = Convert.ToInt16(tb.Text);
                }
                if (tb.Name == $"numberMaxTxtBox_{suffixNumber}") {
                    if (tb.Text != "") filter.maxValue = Convert.ToInt16(tb.Text);
                    filterlist.Add(filter);
                }
            }
                SearchFunctions.search(filterlist);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        //TODO Programm LOESCHBUTTON

        private void buttonAdd_Click(object sender, RoutedEventArgs e) {
            //foreach (var item in Control.) {

            //}

            //HIER WEITERMACHEN!!!
            foreach (TextBox tb in FindVisualChildren<TextBox>(this)) {
                if (tb.Name.Contains("filterModTxtBox"))  {
                    if (string.IsNullOrEmpty(tb.Text)) {
                        tb.Text = giveBackFilterBox(this);
                        break;
                    }
                    if (!string.IsNullOrEmpty(tb.Text) && tb.Text == giveBackFilterBox(this)) {
                        break;
                    }


                }
            }
            //TODO - go through all fucking non existing shit controls
            

        }


        /// <summary>
        /// gives back the selected Value from the filterbox
        /// </summary>
        /// <param name="filterWindow"></param>
        /// <returns></returns>
        public static string giveBackFilterBox(FilterOptionsWindow filterWindow) {
            ListBoxItem X = (ListBoxItem)filterWindow.filterBox.SelectedValue;
            if (X != null) {
                return X.Content.ToString();
            } else {
                return "";
            }
        }

        /// <summary>
        /// Collects all mods you found, to show them in your filter
        /// </summary>
        /// <param name="filterWindow"></param>
        public static void fillFilterBox(FilterOptionsWindow filterWindow) {
            Stashes.getAbsolutelyAllMods();

            foreach (string mod in Stashes.absolutelyAllMods) {
                ListBoxItem item = new ListBoxItem();
                item.Content = mod;
                filterWindow.filterBox.Items.Add(item);
            }

        }

        /// <summary>
        /// You can go through all objects of a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
            if (depObj != null) {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T) {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child)) {
                        yield return childOfChild;
                    }
                }
            }
        }


    }
}
