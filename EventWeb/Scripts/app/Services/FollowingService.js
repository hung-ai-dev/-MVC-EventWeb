var FollowingService = function () {
    var follow = function(followeeId, success, error) {
        $.ajax({
            url: "/api/followings",
            type: "POST",
            data: { followeeId: followeeId },
            success: success,
            error: error
        });
    }

    var unfollow = function(followeeId, success, error) {
        $.ajax({
            url: "/api/followings",
            type: "DELETE",
            data: { followeeId: followeeId },
            success: success,
            error: error
        });
    }

    return {
        follow: follow,
        unfollow: unfollow
    }
}();