using System.Threading.Tasks;

namespace CardinalPOS.Services.Interfaces
{
    public delegate void TagDetectedDelegate(INfcDefTag tag);

    public interface INFCService
    {
        event TagDetectedDelegate TagDetected;

        Task<bool> IsAvailableAsync();
        Task<bool> IsEnabledAsync();

        Task StartListeningAsync();
        Task StopListeningAsync();
    }

    public interface INfcDefTag
    {
        bool IsWriteable { get; }
        NfcDefRecord[] Records { get; }
    }

    public enum NDefTypeNameFormat
    {
        AbsoluteUri,
        Empty,
        Media,
        External,
        WellKnown,
        Unchanged,
        Unknown
    }

    public class NfcDefRecord
    {
        public NDefTypeNameFormat TypeNameFormat { get; set; }
        public byte[] Payload { get; set; }
    }
}
