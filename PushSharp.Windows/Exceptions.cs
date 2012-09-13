using System;

namespace PushSharp.Windows
{
	public class WindowsNotificationSendFailureException : Exception
	{
		public WindowsNotificationSendFailureException(WindowsNotificationStatus status)
			: base()
		{
			this.NotificationStatus = status;
		}

		public WindowsNotificationStatus NotificationStatus
		{
			get;
			set;
		}
	}
}
