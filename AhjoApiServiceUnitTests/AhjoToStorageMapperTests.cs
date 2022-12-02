using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using Xunit;

namespace AhjoApiServiceUnitTests
{
    public class AhjoToStorageMapperTests
    {
        [Fact]
        public void CreateStorageMeeting_StorageAgendaDTO_HasCorrectValues()
        {
            var meeting = new AhjoMeetingDTO
            {
                MeetingID = "1",
            };
            var agendas = new AhjoAgendaItemDTO[]
            {
                 new AhjoAgendaItemDTO
                 {
                     AgendaPoint = 123,
                     Section = "section",
                     AgendaItem = "agenda item",
                     Html = "html",
                     CaseIDLabel = "caseid",
                     DecisionHistoryHTML = "decision",
                 },
            };
            var meetingData = new List<AhjoMeetingData>();
            meetingData.Add(new AhjoMeetingData(meeting, agendas));

            var storageMeeting = AhjoApiService.AhjoToStorageMapper.CreateStorageMeetingDTOs(meetingData).First();

            Assert.Equal(storageMeeting.Agendas?[0].AgendaPoint, agendas[0].AgendaPoint);
            Assert.Equal(storageMeeting.Agendas?[0].Section, agendas[0].Section);
            Assert.Equal(storageMeeting.Agendas?[0].Title, agendas[0].AgendaItem);
            Assert.Equal(storageMeeting.Agendas?[0].Html, agendas[0].Html);
            Assert.Equal(storageMeeting.Agendas?[0].CaseIDLabel, agendas[0].CaseIDLabel);
            Assert.Equal(storageMeeting.Agendas?[0].DecisionHistoryHTML, agendas[0].DecisionHistoryHTML);
        }

        [Fact]
        public void CreateStorageMeeting_StorageMeetingDTO_HasCorrectValues()
        {
            var meeting = new AhjoMeetingDTO
            {
                MeetingID = "999",
                Name = "Valtuuston kokous",
                DateMeeting = new DateTime(2023, 1, 1, 17, 0, 0),
                AgendaPublished = true,
            };
            var meetingData = new List<AhjoMeetingData>();
            meetingData.Add(new AhjoMeetingData(meeting));

            var storageMeeting = AhjoApiService.AhjoToStorageMapper.CreateStorageMeetingDTOs(meetingData).First();

            Assert.Equal(storageMeeting.MeetingID, meeting.MeetingID);
            Assert.Equal(storageMeeting.MeetingDate, meeting.DateMeeting);
            Assert.Equal(storageMeeting.Name, meeting.Name);
            Assert.Equal(storageMeeting.Location, meeting.Location);
        }
    }
}
