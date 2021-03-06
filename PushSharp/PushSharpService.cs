﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PushSharp.Common;

namespace PushSharp
{
	public class PushSharpService : IDisposable, IPushSharpService
	{
		public bool WaitForQueuesToFinish { get; set; }

		public ChannelEvents Events { get; set; }

		Apple.ApplePushService appleService = null;
		WindowsPhone.WindowsPhonePushService wpService = null;
		Windows.WindowsPushService winService = null;
		Blackberry.BlackberryPushService bbService = null;
		Android.GcmPushService gcmService = null;

		static PushSharpService instance = null;
		public static PushSharpService Instance
		{
			get { return instance ?? (instance = new PushSharpService()); }
		}

		public PushSharpService()
		{
			this.Events = new ChannelEvents();
		}

		public PushSharpService(bool waitForQueuesToFinish) : this()
		{
			this.WaitForQueuesToFinish = waitForQueuesToFinish;
		}

		public void StartApplePushService(Apple.ApplePushChannelSettings channelSettings, PushServiceSettings serviceSettings = null)
		{
			appleService = new Apple.ApplePushService(channelSettings, serviceSettings);
			appleService.Events.RegisterProxyHandler(this.Events);
		}

		public void StopApplePushService(bool waitForQueueToFinish = true)
		{
			if (appleService != null)
				appleService.Stop(waitForQueueToFinish);
		}

		public void StartGoogleCloudMessagingPushService(Android.GcmPushChannelSettings channelSettings, PushServiceSettings serviceSettings = null)
		{
			gcmService = new Android.GcmPushService(channelSettings, serviceSettings);
			gcmService.Events.RegisterProxyHandler(this.Events);
		}

		public void StopGoogleCloudMessagingPushService(bool waitForQueueToFinish = true)
		{
			if (gcmService != null)
				gcmService.Stop(waitForQueueToFinish);
		}

		public void StartWindowsPhonePushService(WindowsPhone.WindowsPhonePushChannelSettings channelSettings = null, PushServiceSettings serviceSettings = null)
		{
			wpService = new WindowsPhone.WindowsPhonePushService(channelSettings, serviceSettings);
			wpService.Events.RegisterProxyHandler(this.Events);
		}

		public void StopWindowsPhonePushService(bool waitForQueueToFinish = true)
		{
			if (wpService != null)
				wpService.Stop(waitForQueueToFinish);
		}

		public void StartWindowsPushService(Windows.WindowsPushChannelSettings channelSettings, PushServiceSettings serviceSettings = null)
		{
			winService = new Windows.WindowsPushService(channelSettings, serviceSettings);
			winService.Events.RegisterProxyHandler(this.Events);
		}

		public void StopWindowsPushService(bool waitForQueueToFinish = true)
		{
			if (winService != null)
				winService.Stop(waitForQueueToFinish);
		}

		public void StartBlackberryPushService(Blackberry.BlackberryPushChannelSettings channelSettings, PushServiceSettings serviceSettings = null)
		{
			bbService = new Blackberry.BlackberryPushService(channelSettings, serviceSettings);
			bbService.Events.RegisterProxyHandler(this.Events);
		}
		
		public void StopBlackberryPushService(bool waitForQueueToFinish = true)
		{
			if (bbService != null)
				bbService.Stop(waitForQueueToFinish);
		}

		public void QueueNotification(Notification notification)
		{
			switch (notification.Platform)
			{
				case PlatformType.Apple:
					appleService.QueueNotification(notification);
					break;
				case PlatformType.AndroidGcm:
					gcmService.QueueNotification(notification);
					break;
				case PlatformType.WindowsPhone:
					wpService.QueueNotification(notification);
					break;
				case PlatformType.Windows:
					winService.QueueNotification(notification);
					break;
				case PlatformType.Blackberry:
					bbService.QueueNotification(notification);
					break;
			}
		}

		public void StopAllServices(bool waitForQueuesToFinish = true)
		{
			var tasks = new List<Task>();

			if (appleService != null && !appleService.IsStopping)
				tasks.Add(Task.Factory.StartNew(() => appleService.Stop(waitForQueuesToFinish)));

			if (gcmService != null && !gcmService.IsStopping)
				tasks.Add(Task.Factory.StartNew(() => gcmService.Stop(waitForQueuesToFinish)));

			if (wpService != null && !wpService.IsStopping)
				tasks.Add(Task.Factory.StartNew(() => wpService.Stop(waitForQueuesToFinish)));

			if (winService != null && !winService.IsStopping)
				tasks.Add(Task.Factory.StartNew(() => winService.Stop(waitForQueuesToFinish)));

			if (bbService != null && !bbService.IsStopping)
				tasks.Add(Task.Factory.StartNew(() => bbService.Stop(waitForQueuesToFinish)));

			Task.WaitAll(tasks.ToArray());
		}

		void IDisposable.Dispose()
		{
			StopAllServices(this.WaitForQueuesToFinish);
		}
	}	
}
