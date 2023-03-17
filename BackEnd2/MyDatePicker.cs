using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BackEnd2
{
    public class MyDatePicker:DatePicker
    {
        protected override void OnCalendarOpened(RoutedEventArgs e)
        {
            var popup = this.Template.FindName(
                "PART_Popup", this) as Popup;
            if (popup != null && popup.Child is System.Windows.Controls.Calendar)
            {
                ((Calendar)popup.Child).DisplayMode = CalendarMode.Decade;
            }

            ((Calendar)popup.Child).DisplayModeChanged +=new EventHandler<CalendarModeChangedEventArgs>(DatePickerCo_DisplayModeChanged);
               
        }
        void DatePickerCo_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            var popup = this.Template.FindName(
                "PART_Popup", this) as Popup;
            if (popup != null && popup.Child is System.Windows.Controls.Calendar)
            {
                var _calendar = popup.Child as System.Windows.Controls.Calendar;
                
                _calendar.DisplayMode = CalendarMode.Year;

                if (IsDropDownOpen)
                {
                    this.SelectedDate = new DateTime(_calendar.DisplayDate.Year, 1, 1);
                    this.IsDropDownOpen = false;
                    ((Calendar)popup.Child).DisplayModeChanged -= new EventHandler<CalendarModeChangedEventArgs>(DatePickerCo_DisplayModeChanged);
                }

            }
        }
    }
}