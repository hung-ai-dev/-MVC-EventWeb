var AttendanceService = function () {
    var createAttendance = function (gigId, success, error) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            type: "POST",
            success: success,
            error: error
        });
    }

    var deleteAttendance = function (gigId, success, error) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            type: "DELETE",
            success: success,
            error: error
        });
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();