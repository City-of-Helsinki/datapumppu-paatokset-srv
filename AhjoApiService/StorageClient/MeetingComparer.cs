using AhjoApiService.StorageClient.DTOs;
using System.Reflection;

namespace AhjoApiService.StorageClient
{
    /// <summary>
    /// Defines a contract for comparing two <see cref="StorageMeetingDTO"/> instances for equality.
    /// </summary>
    internal interface IMeetingComparer
    {
        /// <summary>
        /// Determines whether two meetings are equal by comparing all of their public properties.
        /// </summary>
        /// <param name="meeting1">The first meeting to compare.</param>
        /// <param name="meeting2">The second meeting to compare.</param>
        /// <returns><c>true</c> if all property values are equal; otherwise <c>false</c>.</returns>
        bool IsSameMeeting(StorageMeetingDTO meeting1, StorageMeetingDTO meeting2);
    }

    /// <summary>
    /// Compares two <see cref="StorageMeetingDTO"/> instances using reflection to check
    /// equality across all public properties. Handles null values correctly.
    /// </summary>
    internal class MeetingComparer : IMeetingComparer
    {
        /// <inheritdoc />
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
