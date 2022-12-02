using AhjoApiService.StorageClient;
using AhjoApiService.StorageClient.DTOs;
using Xunit;

namespace AhjoApiServiceUnitTests.StorageClient
{
    public class MeetingComparerTests
    {
        [Fact]
        public void SameMeetingId()
        {
            StorageMeetingDTO meeting1 = new StorageMeetingDTO
            {
                MeetingID = "1"
            };

            StorageMeetingDTO meeting2 = new StorageMeetingDTO
            {
                MeetingID = "1"
            };

            var meetingComparer = new MeetingComparer();
            Assert.True(meetingComparer.IsSameMeeting(meeting1, meeting2));
        }

        [Fact]
        public void DifferentMeetingId()
        {
            StorageMeetingDTO meeting1 = new StorageMeetingDTO
            {
                MeetingID = "1"
            };

            StorageMeetingDTO meeting2 = new StorageMeetingDTO
            {
                MeetingID = "2"
            };

            var meetingComparer = new MeetingComparer();
            Assert.False(meetingComparer.IsSameMeeting(meeting1, meeting2));
        }
    }
}
