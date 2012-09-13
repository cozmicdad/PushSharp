using PushSharp.Common;

namespace PushSharp.WindowsPhone
{
	public class WindowsPhonePushService : PushServiceBase<WindowsPhonePushChannelSettings>
	{
		public WindowsPhonePushService(WindowsPhonePushChannelSettings channelSettings, PushServiceSettings serviceSettings = null)
			: base(channelSettings, serviceSettings)
		{
		}

		protected override PushChannelBase CreateChannel(PushChannelSettings channelSettings)
		{
			return new WindowsPhonePushChannel(channelSettings as WindowsPhonePushChannelSettings);
		}

		public override PlatformType Platform
		{
			get { return PlatformType.WindowsPhone; }
		}
	}
}
