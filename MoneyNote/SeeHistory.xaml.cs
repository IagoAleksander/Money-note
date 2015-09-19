using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using MoneyNote.DataModel;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MoneyNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SeeHistory : Page
    {

        ObservableCollection<Notes> Note = new ObservableCollection<Notes>();
        ObservableCollection<Notes> noteSort;
        ObservableCollection<Notes> noteFilter;
        string flag = null;

        public SeeHistory()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Note = await App.DataModel.get_Notes();

            // sort the ObservableCollection by date (the default property)
            noteSort = new ObservableCollection<Notes>(Note.OrderBy(note => note));
            noteSort = new ObservableCollection<Notes>(noteSort.Reverse());

            noteFilter = noteSort;

            this.DataContext = noteFilter;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(NoteDetails), e.ClickedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void sortDate_Click(object sender, RoutedEventArgs e)
        {
            
            // sort the ObservableCollection by date (the default property)
            if (flag != "Date")
            {
                flag = "Date";
                noteSort = new ObservableCollection<Notes>(Note.OrderBy(note => note));
            }
            else
            {
                flag = null;
                noteSort = new ObservableCollection<Notes>(noteSort.Reverse());
            }
            Filter_sorted();
        }

        private void sortDescription_Click(object sender, RoutedEventArgs e)
        {
            // sort the ObservableCollection by description
            if (flag != "Description")
            {
                flag = "Description";
                noteSort = new ObservableCollection<Notes>(Note.OrderBy(note => note.Description));
            }
            else
            {
                flag = null;
                noteSort = new ObservableCollection<Notes>(noteSort.Reverse());
            }

            Filter_sorted();
        }

        private void sortValue_Click(object sender, RoutedEventArgs e)
        {
            // sort the ObservableCollection by value (bug sometimes =/)
            if (flag != "Value")
            {
                flag = "Value";
                noteSort = new ObservableCollection<Notes>(Note.OrderBy(note => note.Value));
            }
            else
            {
                flag = null;
                noteSort = new ObservableCollection<Notes>(noteSort.Reverse());
            }

            Filter_sorted();       
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Filter_sorted();
        }

        private void Filter_GotFocus(object sender, RoutedEventArgs e)
        {
            Filter.Text = "";
            filter.IsChecked = false;
            Filter_sorted();
        }

        private void Filter_sorted()
        {
            if (filter.IsChecked == true)
            {
                string Tag = Filter.Text;
                noteFilter = new ObservableCollection<Notes>(noteSort.Where(x => x.Description.Contains(Tag)));
                
                //show balance
                double filterBalance = 0;
                foreach (var Notes in noteFilter)
                    filterBalance += Notes.Value;
           
                filter_balance.Text = String.Format("{0:£0.00}", filterBalance);

                //show a new row with the balance value of the selection
                main_grid.RowDefinitions[2].Height = new GridLength(100, GridUnitType.Pixel);
                this.DataContext = noteFilter;
            }
            else
            {
                this.DataContext = noteSort;
                main_grid.RowDefinitions[2].Height = new GridLength(0, GridUnitType.Pixel);
            }
        }

        private void Filter_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                this.Focus(FocusState.Keyboard);

                if (Filter.Text != "")
                {
                    filter.IsChecked = true;
                    Filter_sorted();
                }
            }
        }     
    }
}
