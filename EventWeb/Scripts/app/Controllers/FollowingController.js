var FollowingController = function (followingService) {
    var $button;

    var init = function(container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    }

    var toggleFollowing = function(e) {
        $button = $(e.target);
        var followeeId = $button.attr("data-user-info");

        if ($button.hasClass("btn-link")) {
            followingService.follow(followeeId, success, error);
        } else {
            followingService.unfollow(followeeId, success, error);
        }
    }

    var success = function() {
        var text = ($button.text() == "Following") ? "Follow" : "Following";
        $button.toggleClass("btn-link").toggleClass("btn-info").text(text);
    }

    var error  = function() {
        alert("Something failed");
    }

    return {
        init: init
    }
}(FollowingService)