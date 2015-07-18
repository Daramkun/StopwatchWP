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

namespace Stopwatch
{
	public partial class StopwatchListItem : UserControl
	{
		public string Number { get { return txtNumber.Text; } set { txtNumber.Text = value; } }
		public string Hour { get { return txtHour.Text; } set { txtHour.Text = value; } }
		public string Minute { get { return txtMinute.Text; } set { txtMinute.Text = value; } }
		public string Second { get { return txtSecond.Text; } set { txtSecond.Text = value; } }
		public string Millisecond { get { return txtMillisec.Text; } set { txtMillisec.Text = value; } }

		public StopwatchListItem ()
		{
			InitializeComponent ();
		}
	}
}
