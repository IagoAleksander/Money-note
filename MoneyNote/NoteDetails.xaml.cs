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
using Windows.UI.Popups;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MoneyNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoteDetails : Page
    {
        private Notes note;

        public NoteDetails()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.note = (Notes)e.Parameter;
 
            dp.Date = note.date.Date;
            tp.Time = note.date.TimeOfDay;

            _2.DataContext = note.Description;
            _3.DataContext = note.Value;
 
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewNote), note);
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog("Are you sure?");

            messageDialog.Commands.Add(new UICommand(
                "Delete",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            messageDialog.Commands.Add(new UICommand(
                "Cancel",
                new UICommandInvokedHandler(this.CommandInvokedHandler)));

            await messageDialog.ShowAsync();

        }

        private void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "Delete")
            {
                App.DataModel.DeleteNote(note);
                Frame.Navigate(typeof(SeeHistory));
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SeeHistory));
        }
    }
}
