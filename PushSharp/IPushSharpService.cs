using PushSharp.Common;

namespace PushSharp
{
	public interface IPushSharpService
	{
		bool WaitForQueuesToFinish { get; set; }
		ChannelEvents Events { get; set; }
		void StartApplePushService(Apple.ApplePushChannelSettings channelSettings, PushServiceSettings serviceSettings = null);
		void StopApplePushService(bool waitForQueueToFinish = true);
		void StartGoogleCloudMessagingPushService(Android.GcmPushChannelSettings channelSettings, PushServiceSettings serviceSettings = null);
		void StopGoogleCloudMessagingPushService(bool waitForQueueToFinish = true);
		void StartWindowsPhonePushService(WindowsPhone.WindowsPhonePushChannelSettings channelSettings = null, PushServiceSettings serviceSettings = null);
		void StopWindowsPhonePushService(bool waitForQueueToFinish = true);
		void StartWindowsPushService(Windows.WindowsPushChannelSettings channelSettings, PushServiceSettings serviceSettings = null);
		void StopWindowsPushService(bool waitForQueueToFinish = true);
		void StartBlackberryPushService(Blackberry.BlackberryPushChannelSettings channelSettings, PushServiceSettings serviceSettings = null);
		void StopBlackberryPushService(bool waitForQueueToFinish = true);
		void QueueNotification(Notification notification);
		void StopAllServices(bool waitForQueuesToFinish = true);
	}
}
