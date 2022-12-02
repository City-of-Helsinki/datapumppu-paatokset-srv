using AhjoApiService.StorageClient;
using AhjoApiService.StorageClient.DTOs;
using Moq;
using Xunit;

namespace AhjoApiServiceUnitTests.StorageClient
{
    public class StorageCacheTests
    {
        [Fact]
        public void SameMeeting()
        {
            var comparer = new Mock<IMeetingComparer>();
            comparer.Setup(c => c.IsSameMeeting(It.IsAny<StorageMeetingDTO>(), It.IsAny<StorageMeetingDTO>()))
                .Returns(true);

            var cache = new StorageCache(comparer.Object);
            cache.Add(new List<StorageMeetingDTO>() { new StorageMeetingDTO() });

            Assert.False(cache.IsUpdated(new StorageMeetingDTO()));
        }

        [Fact]
        public void DifferentMeeting()
        {
            var comparer = new Mock<IMeetingComparer>();
            comparer.Setup(c => c.IsSameMeeting(It.IsAny<StorageMeetingDTO>(), It.IsAny<StorageMeetingDTO>()))
                .Returns(false);

            var cache = new StorageCache(comparer.Object);
            cache.Add(new List<StorageMeetingDTO>() { new StorageMeetingDTO() });

            Assert.True(cache.IsUpdated(new StorageMeetingDTO()));
        }
    }
}
