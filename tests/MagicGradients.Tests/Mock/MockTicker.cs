namespace MagicGradients.Tests.Mock
{
    internal class MockTicker : Xamarin.Forms.Internals.Ticker
    {
        bool _enabled;

        protected override void EnableTimer()
        {
            _enabled = true;

            while (_enabled)
            {
                SendSignals(16);
            }
        }

        protected override void DisableTimer()
        {
            _enabled = false;
        }
    }
}
