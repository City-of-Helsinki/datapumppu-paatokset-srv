const express = require('express');
const app = express();
const port = process.env.PORT || 3000;

// Logging middleware
app.use((req, res, next) => {
    console.log(`[${new Date().toISOString()}] ${req.method} ${req.url}`);
    next();
});

// Helper to generate mock agenda items
function generateAgendaItems(count) {
    const items = [];
    for (let i = 1; i <= count; i++) {
        items.push({
            agendaPoint: i,
            section: `§ ${i}`,
            agendaItem: `Draft Proposal ${i} for Datapumppu Integration`,
            caseIDLabel: `HEL 2026-0000${i}`,
            pdf: {
                nativeId: `agenda-doc-${i}`,
                title: `Draft Proposal ${i} Document PDF`,
                language: "fi",
                fileURI: `http://localhost:3000/files/agenda-doc-${i}.pdf`
            }
        });
    }
    return items;
}

// 1. Get Meetings List
app.get('/ahjo-proxy/meetings', (req, res) => {
    // Generate dates in 2023
    const publishedDate = new Date('2023-06-23T15:00:00.000Z');
    const unpublishedDate = new Date('2023-06-28T15:00:00.000Z');

    res.json({
        meetings: [
            {
                dateMeeting: publishedDate.toISOString(),
                meetingID: "0290020238",
                decisionMaker: "Kaupunginvaltuusto",
                decisionMakerID: "02900",
                name: "Kaupunginvaltuuston kokous (Minutes Unpublished - 11 Items)",
                location: "Kaupungintalo",
                agendaPublished: true,
                minutesPublished: false // Set to false so paatokset-srv ingests its 11 agenda items
            },
            {
                dateMeeting: unpublishedDate.toISOString(),
                meetingID: "0290020239",
                decisionMaker: "Kaupunginvaltuusto",
                decisionMakerID: "02900",
                name: "Kaupunginvaltuuston kokous (Minutes Unpublished - 1 Agenda Item)",
                location: "Kaupungintalo",
                agendaPublished: true,
                minutesPublished: false
            }
        ]
    });
});

// 2. Get Single Meeting Details
app.get('/ahjo-proxy/meetings/single/:meetingId', (req, res) => {
    const meetingId = req.params.meetingId || '';
    const isPublished = false; // Both are unpublished to let the service ingest agenda items

    const date = meetingId.includes('238') ? new Date('2023-06-23T15:00:00.000Z') : new Date('2023-06-28T15:00:00.000Z');

    let agendaItems = null;
    if (meetingId.includes('238')) {
        agendaItems = generateAgendaItems(11);
    } else {
        agendaItems = generateAgendaItems(1);
    }

    const response = {
        dateMeeting: date.toISOString(),
        meetingID: meetingId,
        decisionMaker: "Kaupunginvaltuusto",
        decisionMakerID: "02900",
        name: meetingId.includes('238') ? "Kaupunginvaltuuston kokous (Minutes Unpublished - 11 Items)" : "Kaupunginvaltuuston kokous (Minutes Unpublished - 1 Item)",
        location: "Kaupungintalo",
        agendaPublished: true,
        minutesPublished: isPublished,
        meetingSequenceNumber: meetingId.includes('238') ? 8 : 9,
        status: "scheduled",
        agenda: agendaItems
    };

    res.json({ meetings: [response] });
});

// 3. Get Decisions List for a meeting
app.get('/ahjo-proxy/decisions', (req, res) => {
    const meetingId = req.query.meeting_id || '';
    const count = meetingId.includes('238') ? 11 : 1;
    
    const decisions = [];
    for (let i = 1; i <= count; i++) {
        decisions.push({
            nativeId: `decision-doc-${meetingId.includes('238') ? '238' : '239'}-${i}`,
            title: `Approved Proposal ${i} for Datapumppu Integration`,
            caseIDLabel: `HEL 2026-0000${i}`,
            caseID: `1234${i}`,
            section: `§ ${i}`
        });
    }
    res.json({ decisions });
});

// 4. Get Single Decision details
app.get('/ahjo-proxy/decisions/single/:nativeId', (req, res) => {
    const nativeId = req.params.nativeId;
    res.json({
        decisions: [
            {
                nativeId: nativeId,
                title: `Approved Proposal for Datapumppu Integration`,
                caseIDLabel: "HEL 2026-012345",
                caseID: "12345",
                section: "§ 5",
                content: `<h2>Decision Body HTML</h2><p>This is the official mock decision text for ${nativeId}.</p>`,
                motion: "<p>The Mayor proposes that the system be upgraded.</p>",
                classificationCode: "10 01 02",
                classificationTitle: "IT Infrastruktuuri",
                pdf: {
                    nativeId: `dec-pdf-${nativeId}`,
                    title: `Official Decision PDF for ${nativeId}`,
                    language: "fi",
                    fileURI: `http://localhost:3000/files/dec-pdf-${nativeId}.pdf`
                }
            }
        ]
    });
});

// 5. Get Single Agenda Item details
app.get('/ahjo-proxy/agenda-item/:meetingId/:nativeId', (req, res) => {
    const nativeId = req.params.nativeId || '';
    
    // Extract number from nativeId (e.g. "agenda-doc-5" -> 5)
    const match = nativeId.match(/\d+/);
    const index = match ? parseInt(match[0], 10) : 1;

    res.json({
        agenda_item: {
            agendaPoint: index,
            section: `§ ${index}`,
            agendaItem: `Agenda item ${index} from paatokset-api-mock`,
            caseIDLabel: `HEL 2026-0000${index}`,
            html: `<h2>Agenda Proposal ${index} HTML</h2><p>This is mock text of the proposed agenda item ${index}.</p>`,
            decisionHistoryHTML: `<p>History trace shows earlier discussions for item ${index}.</p>`,
            pdf: {
                nativeId: nativeId,
                title: `Draft Proposal ${index} Document PDF`,
                language: "fi",
                fileURI: `http://localhost:3000/files/${nativeId}.pdf`
            }
        }
    });
});

app.listen(port, () => {
    console.log(`Ahjo API Mock server running on port ${port}`);
});
