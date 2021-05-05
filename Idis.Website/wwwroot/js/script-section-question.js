let current_qa = 0;

$('.question-item').on('contextmenu', function (e) {
    current_qa = $(this).attr("data-id")

    var top = e.pageY - 10;
    var left = e.pageX - 90;
    $("#context-menu").css({
        display: "block",
        top: top,
        left: left
    }).addClass("show");
    return false; //blocks default Webbrowser right click menu
}).on("click", function () {
    $("#context-menu").removeClass("show").hide();
});

$("#context-menu a").on("click", function () {
    switch ($(this).attr('data-id')) {
        case '1':
            $.confirm({
                title: 'Confirm!',
                content: 'Delete confirm!',
                buttons: {
                    OK: function () {
                        $.post("home/deletequestion", {
                            id: current_qa
                        }).done(function (data) {
                            window.location = window.location
                        }).fail(function () {
                            $.alert("Error");
                        });
                    },
                    cancel: function () {}
                }
            }); break;
        case '2':
            $.get("home/getquestion", {
                id: current_qa
            }).done(function (data) {
                CreateQuestion("home/updatequestion", current_qa)
                var obj = JSON.parse(JSON.stringify(data));

                setTimeout(function () {
                    $('#txtGroup').val(obj.group)
                    $('#txtQuestion').val(obj.inData)
                    $('#txtAnswers').val(obj.outData)
                }, 10);

            }).fail(function () {
                $.alert("Error");
            });
            break;
    }

    $(this).parent().removeClass("show").hide();
});


function CreateQuestion(ex_action = "home/insertquestion", qid = null) {  

    $.confirm({
        title: false,
        content: `<!-- Body -->
              <div class="card-body">
                <h2 class="card-header-title mb-2">Create one</h2>
                <!-- Form Group -->
                <div class="form-group">
                  <label for="txtGroup" class="input-label">Group</label>
                  <div class="input-group input-group-merge">
                    <div class="input-group-prepend">
                      <div class="input-group-text">
                        <i class="tio-briefcase-outlined"></i>
                      </div>
                    </div>
                    <input id="txtGroup" type="text" class="form-control" placeholder="Enter group name" aria-label="Enter training name here">
                  </div>
                </div>
                <!-- End Form Group -->
                <div class="form-group">
                  <label for="txtQuestion" class="input-label">Question</label>
                  <div class="input-group input-group-merge">
                    <div class="input-group-prepend">
                      <div class="input-group-text">
                        <i class="tio-help-outlined"></i>
                      </div>
                    </div>
                    <input id="txtQuestion" type="text" class="form-control" placeholder="Enter question here" aria-label="Enter training name here">
                  </div>
                </div>
                <!-- End Form Group -->

                <div class="form-group">
                    <label for="txtAnswers" class="input-label">Answers</label>
                    <textarea id="txtAnswers" class="form-control" placeholder="Answers content" style="height: 150px;"></textarea>
                </div>
              </div>
              <!-- End Body -->`,
        columnClass: 'medium',
        container: 'main',
        containerFluid: false,
        dragWindowBorder: false,
        backgroundDismiss: true,
        onOpenBefore: function () {

        },
        buttons:
        {
            Cancel: function () { },
            Submit: {
                btnClass: 'btn-soft-success',
                action: function () {
                    var gr = $('#txtGroup').val()
                    var inD = $('#txtQuestion').val()
                    var outD = $('#txtAnswers').val()

                    $.post(ex_action, {
                        QuestionId: qid,
                        Group: gr,
                        InData: inD,
                        OutData: outD
                    }).done(function (data) {
                        alert("Done, refresh now!");
                        window.location = window.location
                    }).fail(function () {
                        $.alert("Error");
                    });
                }
            },
        }
    });
}



$(document).on('ready', function () {

    $('.question-item').on('contextmenu', function (e) {
        var top = e.pageY - 10;
        var left = e.pageX - 90;
        $("#context-menu").css({
            display: "block",
            top: top,
            left: left
        }).addClass("show");
        return false; //blocks default Webbrowser right click menu
    }).on("click", function () {
        $("#context-menu").removeClass("show").hide();
    });

    $("#context-menu a").on("click", function () {
        $(this).parent().removeClass("show").hide();
    });
});