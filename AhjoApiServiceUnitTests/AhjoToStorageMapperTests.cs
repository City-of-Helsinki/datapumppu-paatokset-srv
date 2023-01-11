using AhjoApiService;
using AhjoApiService.AhjoApi.DTOs;
using AhjoApiService.AhjoApi.Models;
using System.Reflection;
using Xunit;

namespace AhjoApiServiceUnitTests
{
    public class AhjoToStorageMapperTests
    {
        [Fact]
        public void CreateStorageMeeting_StorageAgendaDTO_HasCorrectValues()
        {
            var fullMeeting = new AhjoFullMeetingDTO
            {
                MeetingID = "1",
                Name = "Valtuuston kokous",
                DateMeeting = DateTime.Now,
                MeetingSequenceNumber = 1,
                Agenda = new AhjoAgendaItemDTO[]
                {
                        new AhjoAgendaItemDTO
                        {
                            AgendaPoint = 44,
                            Section = "sfas12312d",
                            AgendaItem = "otsikko1",
                            CaseIDLabel = "s42124121",
                            Pdf = new AhjoAttachmentDTO
                            {
                                Language = "sv"
                            },
                            Html = "<html lang=\"SV\"><head><META content=\"text/html; charset=UTF-8\" http-equiv=\"Content-Type\"><META name=\"DhId\" content=\"{2D20B30D-078D-C4EC-8A09-850A lang=\"fi\"",
                            DecisionHistoryHTML = "s2341232d",
                        },
                        new AhjoAgendaItemDTO
                        {
                            AgendaPoint = 66,
                            Section = "shgfasdf",
                            AgendaItem = "otsikko2",
                            CaseIDLabel = "s3asdf12",
                            Pdf = new AhjoAttachmentDTO
                            {
                                Language = "fi"
                            },
                            Html = "<html ",
                            DecisionHistoryHTML = "sdfasdfsdd",
                        }
                }
            };

            var meetingData = new List<AhjoMeetingData>();
            meetingData.Add(new AhjoMeetingData(fullMeeting, null));

            var storageMeeting = AhjoApiService.AhjoToStorageMapper.CreateStorageMeetingDTOs(meetingData).First();

            Assert.Equal(44, storageMeeting.Agendas?[0].AgendaPoint);
            Assert.Equal("sfas12312d", storageMeeting.Agendas?[0].Section);
            Assert.Equal("otsikko1", storageMeeting.Agendas?[0].Title);
            Assert.Equal("s42124121", storageMeeting.Agendas?[0].CaseIDLabel);
            Assert.Equal("s2341232d", storageMeeting.Agendas?[0].DecisionHistoryHTML);
            Assert.Equal("sv", storageMeeting.Agendas?[0].Language);
            Assert.Equal("fi", storageMeeting.Agendas?[1].Language);
        }

        [Fact]
        public void CreateStorageMeeting_StorageMeetingDTO_HasCorrectValues()
        {
            var now = DateTime.Now;
            var fullMeeting = new AhjoFullMeetingDTO
            {
                MeetingID = "1",
                Name = "Valtuuston kokous",
                DateMeeting = now,
                MeetingSequenceNumber = 1,
                Location = "sali",
            };

            var meetingData = new List<AhjoMeetingData>();
            meetingData.Add(new AhjoMeetingData(fullMeeting));

            var storageMeeting = AhjoApiService.AhjoToStorageMapper.CreateStorageMeetingDTOs(meetingData).First();

            Assert.Equal("1", storageMeeting.MeetingID);
            Assert.Equal(now, storageMeeting.MeetingDate);
            Assert.Equal("Valtuuston kokous", storageMeeting.Name);
            Assert.Equal("sali", storageMeeting.Location);
        }
    }
}
