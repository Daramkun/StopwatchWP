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
using Microsoft.Phone.Controls;
using System.Threading;

namespace Stopwatch
{
	public partial class MainPage : PhoneApplicationPage
	{
		Thread watchThread;
		int startTickCount;
		bool isRecordStart = false;

		public MainPage ()
		{
			InitializeComponent ();
		}

		private void btnStart_Click ( object sender, EventArgs e )
		{
			if ( isRecordStart ) return;
			isRecordStart = true;
			startTickCount = Environment.TickCount;
			watchThread = new Thread ( () => {
				while ( isRecordStart )
				{
					int [] time = TimeSplit.Split ( Environment.TickCount - startTickCount );
					Dispatcher.BeginInvoke ( () =>
					{
						txtHour.Text = String.Format("{0:00}", time [ 0 ]);
						txtMinute.Text = String.Format ( "{0:00}", time [ 1 ] );
						txtSecond.Text = String.Format ( "{0:00}", time [ 2 ] );
						txtMillisec.Text = String.Format ( "{0:00}", time [ 3 ] );
					} );
					Thread.Sleep ( 10 );
				}
				Dispatcher.BeginInvoke ( () =>
				{
					txtHour.Text = txtMinute.Text = txtSecond.Text = txtMillisec.Text = "00";
				} );
			} );
			watchThread.Start ();
		}

		private void btnStop_Click ( object sender, EventArgs e )
		{
			isRecordStart = false;
		}

		private void btnRecord_Click ( object sender, EventArgs e )
		{
			StopwatchListItem item = new StopwatchListItem ();
			item.SetValue ( TiltEffect.IsTiltEnabledProperty, true );
			item.Number = String.Format ( "{0:00}", lstRecord.Items.Count + 1 );
			item.Hour = txtHour.Text;
			item.Minute = txtMinute.Text;
			item.Second = txtSecond.Text;
			item.Millisecond = txtMillisec.Text;

			ContextMenu menu = new ContextMenu ();
			MenuItem menuItem = new MenuItem () { Header = "삭제" };
			menuItem.Tap += ( object s, System.Windows.Input.GestureEventArgs ee ) => 
			{
				if ( MessageBox.Show ( "이 기록을 삭제하시겠습니까?\n이 기록보다 뒤의 순위가 있어도 순위가 변경되지는 않습니다.", "스톱워치", MessageBoxButton.OKCancel )
					== MessageBoxResult.OK )
				{
					lstRecord.Items.Remove ( item );
				}
			};
			menu.Items.Add ( menuItem );
			ContextMenuService.SetContextMenu ( item, menu );

			lstRecord.Items.Insert ( 0, item );
		}

		protected override void OnBackKeyPress ( System.ComponentModel.CancelEventArgs e )
		{
			if ( MessageBox.Show ( "종료하시겠습니까?", "스톱워치", MessageBoxButton.OKCancel )
				== MessageBoxResult.Cancel )
			{
				e.Cancel = true;
			}
			else isRecordStart = false;
			base.OnBackKeyPress ( e );
		}

		private void mnuClearRecord_Click ( object sender, EventArgs e )
		{
			lstRecord.Items.Clear ();
		}
	}
}