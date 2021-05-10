// #region :Joint
function JointEvents(iid) {
    $.getJSON('calendar/getjointevents/?internId=' + iid)
        .done(function (joint_events) {
            var events = ""
            if (joint_events.length > 0)
                $.each(joint_events, function (i, data) {
                    events += `<li data-id = ${i}>${data}</li>`
                })
            else {
                events = "This person is not involved in any activities."
            }
            $.alert(events)
        });
}
function JointTrainings(iid) {
    $.getJSON('home/GetJointTrainings/?internId=' + iid)
        .done(function (model) {
            if (model.length > 0) {
                var tras = []
                for (var i = 0; i < model.length; i++) {
                    tras.push("<li data-id=" + i + ">" + model[i].traName + "</li>")
                }
                $.alert(tras.join(''))
            }
            else
                $.alert('No trainings to show')
        });
}
// #endregion :Joint


// #region :Intern
function InternDelete(iid) {
    $.confirm({
        title: 'Delete',
        icon: 'tio-delete-outlined',
        backgroundDismiss: true,
        type: 'red',
        buttons:
        {
            Yes: {
                action: function () {
                    $.post("home/deleteintern", { id: iid })
                        .done(function (data) {
                            $.alert({
                                title: 'Alert!',
                                content: 'Intern removed: ' + data,
                                buttons:
                                {
                                    OK: {
                                        text: 'Refresh',
                                        action: function () {
                                            window.location = window.location
                                        }
                                    },
                                }
                            });
                        }).fail(function () {
                            alert("Error");
                        });
                }
            },
            No: function () { }
        }
    });
}
function InternUpdate(iid) {
     update_avatar = false;
     $.getJSON('home/getinterninfo/?id=' + iid)
        .done(function (intern_info) {
            $('#cui-form').attr('action', '/internupdate/' + iid);

            InternSetModalData(intern_info);
            $("#exampleModal").modal();
        });
}
function InternEvaluate(iid) {
    $.ajax({
        method: "GET",
        url: "home/getpoint",
        data: { id: iid, withName: true }
    }).done(function (obj) {
        if (obj == null) {
            InternEvaluateFirstTime(iid); return;
        }

        var parsedJSON = JSON.parse(JSON.stringify(obj))
        var color;

        if (parsedJSON.passed) color = 'badge-soft-success';
        else color = 'badge-soft-danger';

        var item = `<tr data-id="${parsedJSON.internId}">
                <td data-field="index">${parsedJSON.internId}</td>
                <td data-field="techskill">${parsedJSON.technicalSkill}</td>
                <td data-field="softskill">${parsedJSON.softSkill}</td>
                <td data-field="attitude">${parsedJSON.attitude}</td>
                <td data="score"><i class="tio-star text-warning mr-1"></i> ${parsedJSON.score}</td>
                <td data="passed">
                    <span class="badge ${color} p-1">${parsedJSON.passed}</span
                </td>
                <td data="marker">${parsedJSON.marker}</td>
                <td>
                    <button type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>                  
                    <button id="removepoi" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`;

        $("#poi-tbody").html(item);
        if (!poitable)
            poitable = $.HSCore.components.HSDatatables.init($('#poitable'));

        $.getScript('/js/snips/poi-table-edits.js');
        $('#poiModal').modal('show');
    }).fail(function () {
        alert("Error");
    });
}
function InternEvaluateFirstTime(iid) {

    $.confirm({
        title: false,
        content: `<!-- Body -->
<div class="card-body">
    <h4 class="card-header-title mb-4">Internship evaluation<span class="text-black-50" style="float:right">ID: ${iid}</span></h4>
    <div class="row my-2">
        <label for="emailLabel" class="col-sm-7 col-form-label input-label">Technical skill: </label>
        <div class="col-md-4 ms-auto">
            <!-- Quantity Counter -->
            <div class="js-quantity-counter input-group-quantity-counter">
                <input id="techpoint" type="number" class="js-result form-control input-group-quantity-counter-control" min="0" value="1" max="10">
                <div class="input-group-quantity-counter-toggle">
                    <a class="js-minus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-remove"></i>
                    </a>
                    <a class="js-plus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-add"></i>
                    </a>
                </div>
            </div>
            <!-- End Quantity Counter -->
        </div>
    </div>
    <div class="row my-2">
        <label for="emailLabel" class="col-sm-7 col-form-label input-label">Soft skills: </label>
        <div class="col-md-4 ms-auto">
            <!-- Quantity Counter -->
            <div class="js-quantity-counter input-group-quantity-counter">
                <input id="softpoint"  type="number" class="js-result form-control input-group-quantity-counter-control" min="0" value="1" max="10">
                <div class="input-group-quantity-counter-toggle">
                    <a class="js-minus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-remove"></i>
                    </a>
                    <a class="js-plus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-add"></i>
                    </a>
                </div>
            </div>
            <!-- End Quantity Counter -->
        </div>
    </div>
    <div class="row my-2">
        <label for="emailLabel" class="col-sm-7 col-form-label input-label">Attitude: </label>
        <div class="col-md-4 ms-auto">
            <!-- Quantity Counter -->
            <div class="js-quantity-counter input-group-quantity-counter">
                <input id="attipoint" type="number" class="js-result form-control input-group-quantity-counter-control" min="0" value="1" max="10">
                <div class="input-group-quantity-counter-toggle">
                    <a class="js-minus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-remove"></i>
                    </a>
                    <a class="js-plus input-group-quantity-counter-btn" href="javascript:;">
                        <i class="tio-add"></i>
                    </a>
                </div>
            </div>
            <!-- End Quantity Counter -->
        </div>
    </div>
</div>
<!-- End Body -->`,
        backgroundDismiss: true,
        boxWidth: '430px',
        useBootstrap: false,
        onOpenBefore: function () {
            $.getScript('/js/snips/quantity-counter.js');
        },
        onContentReady: function () {
        },
        buttons:
        {
            Mark: {
                btnClass: 'btn-soft-success',
                action: function () {
                    var values = {
                        "InternId": iid,
                        "SoftSkill": $('#softpoint').val(),
                        "Attitude": $('#attipoint').val(),
                        "TechnicalSkill": $('#techpoint').val(),
                    }

                    $.post("home/evaluateintern", { model: values })
                        .done(function () {
                            $.alert('Evaluated: ' + iid)
                            RefreshPointView();
                        });
                }
            },
            Cancel: function () { }
        }
    });
}
function InternSetModalData(model) {
    $('#avatarImg').attr("src", "img/avatar/" + model.avatar);
    var filename = model.avatar;
    var ext = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();

    if (update_avatar) {
        var update_filename = model.email + "." + ext;
        $('#avatarName').val(update_filename);
    }
    else
        $('#avatarName').val(filename);

    $('#firstNameLabel').val(model.firstname);
    $('#lastNameLabel').val(model.lastname);

    eventBirth.setDate([moment(model.birth).format('YYYY-MM-DD')])

    $('#emailLabel').val(model.email);
    $('#phoneLabel').val(model.phone);
    $('#durationLabel').val(model.duration);

    var d = model.duration.split(' - ');
    eventDuration.setDate([moment(d[0]).format('YYYY-MM-DD'), moment(d[1]).format('YYYY-MM-DD')])

    $('#genderLabel').val(model.gender).change();
    $('#typeLabel').val(model.type).change();

    $('#orgInternSelector').val(model.organizationid).change();
    $('#depInternSelector').val(model.departmentid).change();
    $('#traInternSelector').val(model.trainingid).change();
}
// #endregion :Intern


// #region :Training
function TrainingCreate() {
    $.confirm({
        title: false,
        content: `<!-- Body -->
              <div class="card-body">
                <h2 class="card-header-title mb-2">Create a training</h2>
                <!-- Form Group -->
                <div class="form-group">
                  <label for="crtrainingLabel" class="input-label">Training name</label>

                  <div class="input-group input-group-merge">
                    <div class="input-group-prepend">
                      <div class="input-group-text">
                        <i class="tio-briefcase-outlined"></i>
                      </div>
                    </div>
                    <input type="text" class="form-control" name="trainingName" id="crtrainingLabel" placeholder="Enter training name here" aria-label="Enter training name here">
                  </div>
                </div>
                <!-- End Form Group -->

                <!-- Quill -->
                <label class="input-label">Training content</label>

                <div class="quill-custom">
                  <div id="qlToolbarContainer">
                    <div class="row align-items-center">
                      <div class="col-sm">
                        <!-- Toolbar List -->
                        <ul class="list-inline ql-toolbar-list">
                          <li class="list-inline-item">
                            <button class="ql-bold"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-italic"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-underline"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-strike"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-link"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-image"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-blockquote"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-code"></button>
                          </li>
                          <li class="list-inline-item">
                            <button class="ql-list" value="bullet"></button>
                          </li>
                        </ul>
                        <!-- End Toolbar List -->
                      </div>
                    </div>
                    <!-- End Row -->
                  </div>

                  <div class="js-quill ql-editor-content"
                       data-hs-quill-options='{
                          "placeholder": "Type your content...",
                          "toolbarBottom": true,
                          "modules": {
                            "toolbar": "#qlToolbarContainer"
                          }
                         }'>
                  </div>
                </div>
                <!-- End Quill -->
              </div>
              <!-- End Body -->`,
        columnClass: 'large',
        container: 'main',
        containerFluid: false,
        dragWindowBorder: false,
        backgroundDismiss: true,
        onOpenBefore: function () {
            $.getScript('/js/snips/quilljs-editor.js');
        },
        buttons:
        {
            Cancel: function () { },
            Create: {
                btnClass: 'btn-soft-success',
                action: function () {
                    var traName = $('#crtrainingLabel').val()
                    var traData = JSON.stringify(quill.getContents())

                    $.post("home/inserttraining", { model: { 'TraName': traName, 'TraData': traData } })
                        .done(function (data) {
                            $.alert("Result: " + data)
                            RefreshTrainingCount()
                        })
                        .fail(function () {
                            alert("Error");
                        });
                }
            },
        }
    });
}
// #endregion :Training


// #region :Refresh
function RefreshInternCount() {
    $.get("countbyindex/4", function (data) {
        $('#interns-count').html(data);
    });
}
function RefreshPointView() {
    $.get("countbyindex/6", function (data) {
        $('#points-count').html(data);
    });
    RefreshPassed();
}
function RefreshPassed() {
    $.get("home/getpassedcount").done(function (data) {
        $('#passed-count').html(data);
    }).fail(function () {
        alert("Have error when get passed.");
    });
}

function RefreshTrainingCount() {
    $.get("countbyindex/8", function (data) {
        $('#trainings-count').html(data - 1);
    });
}
function RefreshDepartmentCount() {
    $.get("countbyindex/1", function (data) {
        $('#departments-count').html(data);
    });
}
function RefreshOrganizationCount() {
    $.get("countbyindex/5", function (data) {
        $('#organizations-count').html(data);
    });
}
// #endregion :Count


function AppendDep() {
    $('#dep-tbody').append(`<tr data-id="-1">
                <td>New</td>
                <td data-field="name"></td>
                <td data-field="location"></td>
                <td>
                    <button id="depnew" type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>                  
                    <button id="removedep" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`);
    $.getScript('/js/snips/dep-table-edits.js');
    //code before the pause
    setTimeout(function () {
        $('#depnew').click();
    }, 300);
}

function AppendOrg() {
    $('#org-tbody').append(`<tr data-id="-1">
                <td>New</td>
                <td data-field="name"></td>
                <td data-field="phone"></td>
                <td data-field="address"></td>
                <td>
                    <button id="orgnew" type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>
                    <button id="removeorg" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`);
    $.getScript('/js/snips/org-table-edits.js');
    //code before the pause
    setTimeout(function () {
        $('#orgnew').trigger("click");
    }, 300);
}

function ReadAvatarName(input) {
    update_avatar = true;
    var filename = input.files[0]['name'];
    $('#avatarName').val(filename);
}


$(document).on('submit', '#cui-form', function () {
    if (!update_avatar) return;

    var str = $('#avatarName').val();
    var em = $('#emailLabel').val();
    var ex = str.substring(str.lastIndexOf(".") + 1);

    var imgname = em + "." + ex;
    var img64 = $('#avatarImg').attr("src").split(",").pop();

    $('#avatarName').val(imgname)

    $.post("home/uploadavatar", {
        ImgStr: img64,
        ImgName: imgname
    }).done(function () {
        alert("Done, refresh now?");
    }).fail(function () {
        alert("Error");
    });
});

let update_avatar = false;
let current_intern_id = 0;
let eventBirth;
let eventDuration;

$(document).on('ready', function () {

    // INITIALIZATION OF QUILLJS EDITOR
    // =======================================================
    var quill2 = $.HSCore.components.HSQuill.init('.js-quill-modal-eg');


    // INITIALIZATION OF FLATPICKR
    // =======================================================
    eventDuration = $.HSCore.components.HSFlatpickr.init($('#durationLabel'));
    eventBirth = $.HSCore.components.HSFlatpickr.init($('#birthLabel'));

    $.HSCore.components.HSFlatpickr.init($('#start-dateFlatpickr'));
    $.HSCore.components.HSFlatpickr.init($('#end-dateFlatpickr'));


    // INITIALIZATION OF DATATABLES
    // =======================================================
    var datatable = $.HSCore.components.HSDatatables.init($('#datatable'), {
        language: {
            zeroRecords: '<div class="text-center p-4">' +
                '<img class="mb-3" src="/img/sorry.svg" alt="Image Description" style="width: 7rem;">' +
                '<p class="mb-0">No data to show</p>' +
                '</div>'
        },
        pageLength: 36
    });


    // ADD EVENT LISTENER FOR OPENING AND CLOSING DETAILS
    // =======================================================
    $('#datatable tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = datatable.row(tr);

        var internId = tr.attr("data-id");

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {

            let internData = "";

            $.ajax({
                method: "GET",
                url: "home/getinterndetail",
                data: { id: internId }
            }).done(function (json) {
                internData = JSON.parse(json);
                // Open this row
                row.child(`<div class="col-sm-3">
                      <h5>Intern info:</h5>
                      <ul class="font-size-sm">
                        <li>Full name: ${internData.fullname}</li>
                        <li>Date of birth: ${internData.birth}</li>
                        <li>Gender: ${internData.gender}</li>
                        <li>Email: <a href="mailto:${internData.email}">${internData.email}</a></li>
                        <li>Phone number: <a href="tel:${internData.phone}">${internData.phone}</a></li>
                        <li>Address 1:
                            <a href="javascript:;" onclick="PopupCenter('https://www.google.com/maps/search/?api=1&query=${internData.address1}', '', 960, 720)">${internData.address1}</a></li>
                        <li>Address 2:
                            <a href="javascript:;" onclick="PopupCenter('https://www.google.com/maps/search/?api=1&query=${internData.address2}', '', 960, 720)">${internData.address2}</a></li>
                        <li>Join Date: ${internData.joindate}</li>
                      </ul>
                    </div>
                    <div class="col-sm-3">
                      <h5>Relative info:</h5>
                      <ul class="font-size-sm">
                        <li>Time schedule: ${internData.type}</li>
                        <li>Duration: ${internData.duration}</li>
                        <li>Training: ${internData.training}</li>
                        <li>Department: ${internData.department}</li>
                        <li>Organization: ${internData.organization}</li>
                        <li>Mentor: ${internData.mentor}</li>
                      </ul>
                </div>`).show();
                tr.addClass('shown');
            });
        }
    });


    var urlParams = new URLSearchParams(location.search);
    var page_size = urlParams.get('size');
    if (page_size > 14) {
        var calHeight = $('.footer').height() * 13.54;
        $('#main-docker').css('height', calHeight + 'px');
    }

    $('.js-datatable-filter').on('change', function () {
        var $this = $(this),
            elVal = $this.val(),
            targetColumnIndex = $this.data('target-column-index');

        datatable.column(targetColumnIndex).search(elVal).draw();
    });


    $('.js-datatable-search').on('change', function () {
        var elVal = $(this).val();

        if (elVal == 0) window.location = window.location;
    });


    $('#trainingSelector').on('change', function () {
        var tid = $(this).val();

        $.get("home/gettrainingcontent", {
            id: tid
        }).done(function (data) {
            quill2.setContents(JSON.parse(data), 'api');
        }).fail(function () {
            alert("Error");
        });
    });


    $('#delTraining').on('click', function () {
        var trainingId = $('#trainingSelector').val()

        $.post("home/deletetraining", {
            id: trainingId
        }).done(function (result) {
            Refresh(true, result);
        }).fail(function () {
            alert("Error");
        });
    });


    $('#change-training').on('click', function () {
        var trainingId = $('#trainingSelector').val()
        var deparray = $('#depSharedSelector').val()

        var traName = $("#trainingSelector option:selected").text();

        $.post("home/updatetraining", {
            model: {
                'TrainingId': trainingId,
                'TraName': traName,
                'TraData': JSON.stringify(quill2.getContents())
            }
        }).done(function (data) {

            $.post("home/setsharedtraining", {
                sharedId: trainingId,
                depArray: deparray
            }).done(function (data) {
                $.alert("Result: " + data)
            }).fail(function () {
                alert("Error");
            });

        }).fail(function () {
            alert("Error");
        });
    });


    $('#datatableSearch').on('keyup', function (e) {
        if (event.keyCode != 13)
            return;

        var searchOn = $('.js-datatable-search').val();

        var params = new URLSearchParams(window.location.search);

        var $this = $(this),
            elVal = $this.val();

        if (elVal != '') {
            params.set('search_string', elVal);
            params.set('search_on', searchOn);
        }
        else {
            params.delete('search_string');
            params.delete('search_on');
        }

        window.location = "?" + params.toString();
    });


    $('.js-datatable-sort').on('change', function () {
        var $this = $(this),
            elVal = $this.val();

        var params = new URLSearchParams(window.location.search);
        params.set('sort', elVal);

        window.location = "?" + params.toString();
    });


    $('#datatableEntries').on('change', function () {
        var $this = $(this),
            elVal = $this.val();

        var params = new URLSearchParams(window.location.search);
        params.set('size', elVal);

        window.location = "?" + params.toString();
    });


    $('.paginate_button').on('click', function () {
        var params = new URLSearchParams(window.location.search);
        var curr = $(this).html()

        if (isNaN(curr)) {
            params.set('page', $(this).attr("data-id"));
        }
        else
            params.set('page', curr);

        window.location = "?" + params.toString();
    });


    $('#ad-filter-submit').on('click', function () {
        var passed = $('#pass-filter').val();
        var dateFilter = $('#due-date-mode').val();

        var params = new URLSearchParams(window.location.search);

        if (passed) {
            params.set('on_passed', passed);
        }
        if (dateFilter) {
            params.set('date_filter', dateFilter);

            var st = $('#start-date').val();
            var en = $('#end-date').val();

            if (st)
                params.set('start_date', st);
            if (en)
                params.set('end_date', en);
        }
        if (passed || dateFilter)
            window.location = "?" + params.toString();
    });


    //Sync sort,size
    var params = new URLSearchParams(window.location.search);
    $('#datatableEntries').val(params.get("size") ? params.get("size") : 7);
    $('.js-datatable-sort').val(params.get("sort") ? params.get("sort") : 1);
    $('.js-datatable-search').val(params.get("search_on") ? params.get("search_on") : 9);
    $('#datatableSearch').val(params.get("search_string") ? params.get("search_string") : "");


    var org_opt = [];
    var dep_opt = [];
    var tra_opt = [];

    $.get("Home/GetAllDynamic", $.param({
        fields: ["Department", "Organization", "Training"]
    }, true)).done(function (data) {
        var parsedJSON = JSON.parse(JSON.stringify(data))

        var orgs = parsedJSON.Organization;
        for (var i = 0; i < orgs.length; i++) {
            org_opt.push(`<option value="${orgs[i].organizationId}">${orgs[i].orgName}</option>`);
        }

        var tras = parsedJSON.Department;
        for (var i = 0; i < tras.length; i++) {
            dep_opt.push(`<option value="${tras[i].departmentId}">${tras[i].depName}</option>`);
        }

        var deps = parsedJSON.Training;
        for (var i = 0; i < deps.length; i++) {
            tra_opt.push(`<option value="${deps[i].trainingId}">${deps[i].traName}</option>`);
        }

        $('#orgInternSelector').html(org_opt.join(""))
        $('#depInternSelector').html(dep_opt.join(""))
        $('#traInternSelector').html(tra_opt.join(""))

    }).fail(function () {
        alert("Error on fetch relative data");
    });


    $('#addibtn').on("click", function (e) {
        //var type = $("#cui-submit").text(); //For button
        update_avatar = false;
        $('#cui-form').attr('action', '/');

        $('#firstNameLabel').val("");
        $('#lastNameLabel').val("");
        $('#birthLabel').val("");
        $('#emailLabel').val("");
        $('#phoneLabel').val("");
        $('#durationLabel').val("");

        $('#genderLabel').val("").change();
        $('#typeLabel').val("").change();

        $('#orgInternSelector').val('').change();
        $('#depInternSelector').val('').change();
        $('#traInternSelector').val('').change();
    });

    RefreshPassed()

    // CR:Init for one thing, not two, 3hours to fix bug!
    var orgtable;
    var deptable;

    ////////////  ORGANIZATION
    $(document).on("click", '#organization-now', function (e) {
        var items = []

        $.get("Home/GetAllDynamic", $.param({
            fields: ["Organization"]
        }, true)).done(function (json) {
            var orgs = JSON.parse(JSON.stringify(json)).Organization

            for (var i = 0; i < orgs.length; i++) {
                items.push(`<tr data-id="${orgs[i].organizationId}">
                <td data-field="index">${orgs[i].organizationId}</td>
                <td data-field="name">${orgs[i].orgName}</td>
                <td data-field="phone">${orgs[i].orgPhone}</td>
                <td data-field="address">${orgs[i].orgAddress}</td>
                <td>
                    <button type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>
                    <button id="removeorg" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`);
            }
            $("#org-tbody").html(`${items.join("")}`);
            if (!orgtable)
                orgtable = $.HSCore.components.HSDatatables.init($('#orgtable'));

            $.getScript('/js/snips/org-table-edits.js');
            $('#orgModal').modal('show');
        });
    });


    ////////////  DEPARTMENT
    $(document).on("click", '#department-now', function (e) {
        var items = []

        $.ajax({
            method: "GET",
            url: "Home/GetAllDynamic",
            data: $.param({ fields: ["Department"] }, true)
        }).done(function (json) {
            var deps = JSON.parse(JSON.stringify(json)).Department

            for (var i = 0; i < deps.length; i++) {
                items.push(`<tr data-id="${deps[i].departmentId}">
                <td data-field="index">${deps[i].departmentId}</td>
                <td data-field="name">${deps[i].depName}</td>
                <td data-field="location">${deps[i].depLocation}</td>
                <td>
                    <button type="button" class="js-edit btn btn-soft-info btn-icon btn-xs">
                        <i class="tio-edit js-edit-icon"></i>
                    </button>                  
                    <button id="removedep" type="button" class="ml-2 btn btn-soft-danger btn-icon btn-xs">
                        <i class="tio-remove js-remove-icon"></i>
                    </button>                  
                </td>
                </tr>`);
            }
            $("#dep-tbody").html(`${items.join("")}`);
            if (!deptable)
                deptable = $.HSCore.components.HSDatatables.init($('#deptable'));

            $.getScript('/js/snips/dep-table-edits.js');
            $('#depModal').modal('show');
        });
    });


    ////////////  TRAINING
    $(document).on("click", '#training-now', function (e) {
        var tra_options = ['<option label="empty"></option>'];
        var dep_options = [];
        $.get("home/gettrainingmanagerdata").done(function (data) {
            var parsedJSON = JSON.parse(JSON.stringify(data))

            var tras = parsedJSON.Training;
            for (var i = 0; i < tras.length; i++) {
                tra_options.push(`<option value="${tras[i].trainingId}">${tras[i].traName}</option>`);
            }

            var deps = parsedJSON.Department;
            for (var i = 0; i < deps.length; i++) {
                dep_options.push(`<option value="${deps[i].departmentId}">${deps[i].depName}</option>`);
            }

            $('#trainingSelector').html(tra_options.join(''))
            $('#depSharedSelector').html(dep_options.join(''))

            quill2.setContents(null);

            $('#traModal').modal('show');

        }).fail(function () {
            alert("Error");
        });
    });


    // #region :Remove on -now
    $(document).on("click", '#removedep', function (e) {
        var tr = $(this).closest('tr');
        var id = tr.attr("data-id");

        $.post("home/deletedepartment", {
            id: id
        }).done(function (result) {
            Refresh(true, result);
        }).fail(function () {
            alert("Error");
        });
    });


    $(document).on("click", '#removeorg', function (e) {
        var tr = $(this).closest('tr');
        var id = tr.attr("data-id");

        $.post("home/deleteorganization", {
            id: id
        }).done(function (result) {
            Refresh(true, result);
        }).fail(function () {
            alert("Error");
        });
    });


    $(document).on("click", '#removepoi', function (e) {
        var tr = $(this).closest('tr');
        var id = tr.attr("data-id");

        $.post("home/deletepoint", {
            id: id
        }).done(function (data) {
            $.alert("Result: " + data);
            tr.remove()
            RefreshPointView()
        }).fail(function () {
            alert("Error");
        });
    });

    // #endregion


    $('.intern-row').on('contextmenu', function (e) {
        current_intern_id = $(this).attr('data-id')

        var top = e.pageY - 10;
        var left = e.pageX - 90;
        $("#intern-menu").css({
            display: "block",
            top: top,
            left: left
        }).addClass("show");
        return false; //blocks default Webbrowser right click menu
    }).on("click", function () {
        $("#intern-menu").removeClass("show").hide();
    });

    $("#intern-menu a").on("click", function () {
        switch ($(this).attr('data-id')) {
            case '1':
                InternUpdate(current_intern_id)
                break;
            case '2':
                InternEvaluate(current_intern_id)
                break;
            case '3':
                JointEvents(current_intern_id)
                break;
            case '4':
                JointTrainings(current_intern_id)
                break;
            case '5':
                InternDelete(current_intern_id)
                break;
        }
        $(this).parent().removeClass("show").hide();
    });

    $("#datatable thead tr th").on("click", function () {
        // CR: Client sort enable
    });
});
