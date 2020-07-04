using MvvmHelpers;

namespace MagicCrawler.Models
{
    public class JobItem : ObservableObject
    {
        public Endpoint Data { get; }

        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public JobItem(Endpoint data)
        {
            Data = data;
        }

        public void Reset()
        {
            Status = string.Empty;
        }
    }
}
