
namespace PushSharp.Blackberry
{
	public class BlackberryPushService : Common.PushServiceBase<BlackberryPushChannelSettings>
	{
		public BlackberryPushService(BlackberryPushChannelSettings channelSettings, Common.PushServiceSettings serviceSettings)
			: base(channelSettings, serviceSettings)
		{
		}

		protected override Common.PushChannelBase CreateChannel(Common.PushChannelSettings channelSettings)
		{
			return new BlackberryPushChannel(channelSettings as BlackberryPushChannelSettings);
		}

		public override Common.PlatformType Platform
		{
			get { return Common.PlatformType.Blackberry; }
		}
	}
}
