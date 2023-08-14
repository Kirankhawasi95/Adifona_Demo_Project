using HorusUITest.Adapters;
using HorusUITest.Enums;

namespace HorusUITest.Data
{
    public class HearingAid
    {
        public string Name { get; }
        public HearingAidModel Model { get; }
        public Side Side { get; }
        public ChannelMode Channel { get; }
        public string Selector { get; }

        public HearingAid(string name, HearingAidModel model, Side side, ChannelMode channel, string selector)
        {
            Name = name;
            Model = model;
            Side = side;
            Channel = channel;
            Selector = selector;
        }

        public override string ToString()
        {
            return Name;
        }

        public void Enable()
        {
            SwitchBox.EnableHearingAid(this);
        }

        public void Disable()
        {
            SwitchBox.DisableHearingAid(this);
        }

        public bool IsEnabled()
        {
            return SwitchBox.GetIsHearingAidEnabled(this);
        }
    }
}
