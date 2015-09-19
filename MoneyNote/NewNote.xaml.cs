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

using System.Windows.Input;

using MoneyNote.DataModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MoneyNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewNote : Page
    {
        private bool isCreating;

        Notes newNote;
        DateTime tempDate;
        double tempValue;
        bool Negative;

        public NewNote()
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
            if (e.Parameter == null)
                isCreating = true;
            else
                isCreating = false;

            if (isCreating)
            {
                newNote = new Notes();
                
                tempDate = DateTime.Now;
                Value.Text = "£0.00";
                Negative = true;
            }
            else
            {
                // get the information about the existing input from the database
                newNote = (Notes)e.Parameter;
                
                // change button content
                ok.Content = "Edit";

                Date2.Date = newNote.date.Date;
                Time.Time = newNote.date.TimeOfDay;

                Description.Text = newNote.Description;

                // set switch position and negative flag
                if (newNote.Value < 0)
                {
                    Value.Text = String.Format("{0:£0.00}", -newNote.Value);
                    Switch.IsOn = false;
                    Negative = true;
                }
                else 
                { 
                    Value.Text = String.Format("{0:£0.00}", newNote.Value);
                    Switch.IsOn = true;
                    Negative = false;
                }

                 //get information
                tempValue = Math.Abs(newNote.Value);

                DateTimeOffset sourcedate = Date2.Date;
                TimeSpan ts = Time.Time;
                tempDate = sourcedate.Date + ts;

            }
            
        }

        private void Date2_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            DateTimeOffset sourcedate = Date2.Date;
            TimeSpan ts = Time.Time;
            tempDate = sourcedate.Date + ts;
        }

        private void Time_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            DateTimeOffset sourcedate = Date2.Date;
            TimeSpan ts = Time.Time;
            tempDate = sourcedate.Date + ts;
        }

        private void Value_GotFocus(object sender, RoutedEventArgs e)
        {
            Value.Text = "";
        }

        private void Value_LostFocus(object sender, RoutedEventArgs e)
        {
            // check if the field still blank
            if (Value.Text == "")
                Value.Text = "0";

            // replace , with . to avoid errors
            Value.Text = Value.Text.Replace(',', '.');

            // convert string to double and stores value
            tempValue = Convert.ToDouble(Value.Text); 

            //shows the value in the correct currency
            Value.Text = String.Format("{0:£0.00}", tempValue);
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            // change from credit to debit and vice versa
            Negative = !Negative;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            newNote.date = tempDate;
            newNote.Description = Description.Text;

            if (Negative)
                newNote.Value = -tempValue;
            else
                newNote.Value = tempValue;

            if (isCreating)
                App.DataModel.AddNote(newNote);
            else
                App.DataModel.EditNote(newNote);
            
            Frame.Navigate(typeof(MainPage));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Description_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                this.Focus(FocusState.Keyboard);
            }
        }   

    }
}
