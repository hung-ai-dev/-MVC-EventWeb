var GigController = function (attendanceService) {
    var $button;

    var init = function () {
        $(".js-toggle-attendance").click(toggleAttendance);
    };

    var toggleAttendance = function toggleAttendance(e) {
        $button = $(e.target);
        var gigId = $button.attr("data-gig-info");
        if ($button.hasClass("btn-default")) {
            attendanceService.createAttendance(gigId, success, error);
        } else {
            attendanceService.deleteAttendance(gigId, success, error);
        }
    }

    var success = function success() {
        var text = ($button.text() == "Going") ? "Going ?" : "Going";
        $button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    var error = function error() {
        alert("Something failed!");
    }

    return {
        init: init
    }
}(AttendanceService);