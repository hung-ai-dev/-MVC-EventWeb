var FollowingController = function (followingService) {
    var $button;

    var init = function() {
        $(".js-toggle-follow").click(toggleFollowing);
    }

    var toggleFollowing = function(e) {
        $button = $(e.target);
        var followeeId = $button.attr("data-user-info");

        if ($button.hasClass("btn-default")) {
            followingService.follow(followeeId, success, error);
        } else {
            followingService.unfollow(followeeId, success, error);
        }
    }

    var success = function() {
        var text = ($button.text() == "Following") ? "Follow" : "Following";
        $button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    }

    var error  = function() {
        alert("Something failed");
    }

    return {
        init: init
    }
}(FollowingService)