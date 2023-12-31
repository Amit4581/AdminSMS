
    // Function to update current date and time
function updateDateTime() {
    var dateTimeElement = $('#currentDateTime');
    var currentDate = new Date(); // Get the current date and time

    // Format the date and time (dd-MMM-yyyy HH:MM:SS)
    var formattedDateTime = currentDate.getDate().toString().padStart(2, '0') + '-' +
        getMonthAbbreviation(currentDate.getMonth()) + '-' +
        currentDate.getFullYear() + ' ' +
        currentDate.getHours().toString().padStart(2, '0') + ':' +
        currentDate.getMinutes().toString().padStart(2, '0') + ':' +
        currentDate.getSeconds().toString().padStart(2, '0');

    // Update the HTML element with the formatted date and time
    dateTimeElement.text( formattedDateTime);
}

// Function to get month abbreviation
function getMonthAbbreviation(monthIndex) {
    var monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return monthNames[monthIndex];
}

// Call the function to update date and time when the document is ready
$(document).ready(function () {
    updateDateTime(); // Initial update

    // Update date and time every second (1000 milliseconds)
    setInterval(function () {
        updateDateTime();
    }, 1000);
});