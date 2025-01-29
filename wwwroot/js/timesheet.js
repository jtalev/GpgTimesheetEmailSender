document.getElementById("timesheet-form").addEventListener("submit", function (event) {
    event.preventDefault()

    let timesheetData = {
        first_name: document.querySelector("#first-name").value || "",
        last_name: document.querySelector("#last-name").value || "",
        week_start_date: document.querySelector("#week-start-date").value || "",
        timesheets: [],
        passcode: document.querySelector("#passcode").value || "",
    }

    for (let i = 0; i < 7; i++) {
        timesheetData.timesheets.push({
            hours: parseInt(document.querySelector(`#hrs-${i}`).value || "0", 10),
            minutes: parseInt(document.querySelector(`#mins-${i}`).value || "0", 10),
            day: document.querySelector(`#day-${i}`).value
        })
    }

    let headers = new Headers()
    headers.append('Content-Type', 'application/json')

    fetch("/timesheet/send", {
        method: "POST",
        body: JSON.stringify(timesheetData),
        headers: headers
    }).then(response => {
        if (response.status === 200) {
            document.getElementById('send-btn').disabled = true
        }
        if (response.headers.get('Content-Type').includes('application/json')) {
            return response.json()
        } else {
            return response.text()
        }
    }).then(data => {
        if (typeof data === 'object') {
            document.getElementById('message').innerHTML = JSON.stringify(data.message)
        } else {
            document.getElementById('message').innerHTML = data
        }
    }).catch(error => console.error('Error:', error))
})