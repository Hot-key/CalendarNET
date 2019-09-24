using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Calendar.NET;

namespace Calendar.NETDemo
{
    public partial class Form1 : Form
    {
        [CustomRecurringFunction("RehabDates", "Calculates which days I should be getting Rehab")]
        private bool RehabDays(IEvent evnt, DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Monday || day.DayOfWeek == DayOfWeek.Friday)
            {
                if (day.Ticks >= (new DateTime(2012, 7, 1)).Ticks)
                    return false;
                return true;
            }

            return false;
        }

        public Form1()
        {
            InitializeComponent();

            calendar1.CalendarView = CalendarViews.Month;
            calendar1.AllowEditingEvents = true;

            var groundhogEvent = new CustomEvent()
                                     {
                                         Date = new DateTime(2019, 9, 1),
                                         EventText = "Groundhog Day",
                                         RecurringFrequency = RecurringFrequencies.Yearly
                                     };

            calendar1.AddEvent(groundhogEvent);
        }

        [CustomRecurringFunction("Get Monday and Wednesday", "Selects the Monday and Wednesday of each month")]
        public bool GetMondayAndWednesday(IEvent evnt, DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Monday || dt.DayOfWeek == DayOfWeek.Wednesday)
                return true;
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var date in calendar1.dateRectangles)
            {
                listBox1.Items.Add(date.Key.ToShortDateString() + " " + date.Value.X + ", " + date.Value.Y);
            }
        }

        private void Calendar1_DateCalenderClick(object sender, EventArgs e)
        {

            if (sender is DateTime date)
            {
                calendar1.CalendarDate = date;
                listBox1.Items.Add(calendar1.CalendarDate.ToString("yyyy-MM-dd dddd"));
            }
        }
    }
}
