$(document).ready(function() {
    $("#titleButton").click(function() {
         $(".titlec.displayNone").toggle();
         $(".halfTitle").toggle();
     });
});
$(document).ready(function () {
    $("#createdButton").click(function () {
        $(".createdc.displayNone").toggle();
        $(".halfCreatedBy").toggle();
    });
});
$(document).ready(function() {
    $("#emailButton").click(function() {
        $(".emailt.displayNone").toggle();
        $(".halfEmail").toggle();
    });
});
$(document).ready(function() {
    $("#urlButton").click(function() {
        $(".urlu.displayNone").toggle();
        $(".halfUrl").toggle();
    });
});
       
$(document).ready(function() {
    $(".Expire").click(function(e) {

        var link = $(e.target);        
        bootbox.dialog({
            title: "Confirm",
            message: "Are you sure you want to expire this job?",
            buttons: {
                no: {
                    label: 'No',
                    className: 'btn-default',
                    callback: function() {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: 'Yes',
                    className: 'btn-danger',
                    callback: function() {
                        $.ajax({
                                url: "/api/jobs/" + link.attr("data-job-id"),
                                method: "POST",
                                async: false
                            })
                            .done(function() {
                                link.closest("a").attr('disabled', 'disabled');                                                                                
                                        
                            })
                            .fail(function() {
                                alert("Try again!");
                            });
                    }
                }
            }

        });
    });
});

$(document).ready(function() {
    $(".Delete").click(function(e) {

        var link = $(e.target);                
        bootbox.dialog({
            title: "Confirm",
            message: "Are you sure you want to delete this job?",
            buttons: {
                no: {
                    label: 'No',
                    className: 'btn-default',
                    callback: function() {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: 'Yes',
                    className: 'btn-danger',
                    callback: function() {
                        $.ajax({
                                url: "/api/jobs/" + link.attr("data-job-id"),
                                method: "GET",
                                async: false
                            })
                            .done(function() {
                                link.closest("tr").fadeOut(function () {
                                    $(this).remove();
                                });
                            })
                            .fail(function() {
                                alert("Try again!");
                            });
                    }
                }
            }

        });
    });
});
