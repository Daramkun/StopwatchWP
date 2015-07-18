using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Stopwatch
{
	public static class TimeSplit
	{
		private const int Hour = 0, Minute = 1, Second = 2, Millisecond = 3;

		public static int [] Split ( int tickCount )
		{
			int [] split = new int [ 4 ];
			split [ Millisecond ] = tickCount % 1000 / 10;
			split [ Second ] = ( tickCount / 1000 ) % 60;
			split [ Minute ] = (( tickCount / 1000 ) / 60) % 60;
			split [ Hour ] = ( ( tickCount / 1000 ) / 60 ) / 60;

			return split;
		}
	}
}
