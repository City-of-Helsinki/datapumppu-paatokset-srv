using AhjoApiService.StorageClient.DTOs;

namespace AhjoApiService.StorageClient
{
    internal interface IStorageCache
    {
        bool IsUpdated(StorageMeetingDTO meeting);

        void Add(List<StorageMeetingDTO> meetings);
    }

    internal class StorageCache : IStorageCache
    {
        private readonly List<StorageMeetingDTO> _meetings = new List<StorageMeetingDTO>();
        private readonly IMeetingComparer _meetingComparer;

        public StorageCache(IMeetingComparer meetingComparer)
        {
            _meetingComparer = meetingComparer;
        }

        public bool IsUpdated(StorageMeetingDTO meeting)
        {
            return _meetings.All(existing => !_meetingComparer.IsSameMeeting(meeting, existing));
        }

        public void Add(List<StorageMeetingDTO> meetings)
        {
            if (_meetings.Count > 1000)
            {
                _meetings.Clear();
            }

            _meetings.AddRange(meetings);
        }
    }
}
