using AhjoApiService.StorageClient.DTOs;
using System.Reflection;

namespace AhjoApiService.StorageClient
{
    internal interface IMeetingComparer
    {
        bool IsSameMeeting(StorageMeetingDTO meeting1, StorageMeetingDTO meeting2);
    }

    internal class MeetingComparer : IMeetingComparer
    {
        public bool IsSameMeeting (StorageMeetingDTO meeting1, StorageMeetingDTO meeting2)
        {
            List<PropertyInfo> differences = new List<PropertyInfo>();
            return meeting1.GetType().GetProperties().All(property =>
            {
                object? value1 = property.GetValue(meeting1);
                object? value2 = property.GetValue(meeting2);

                return value1 != null && value2 != null ? value1.Equals(value2) : value1 == value2;
            });
        }
    }
}
