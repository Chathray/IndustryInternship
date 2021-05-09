$(document).on('ready', function () {
    var items = [];

    $.get("Home/GetAllDynamic", $.param({
        fields: ["Activity"]
    }, true)).done(function (json) {
        var acts = JSON.parse(JSON.stringify(json)).Activity

        if (acts.length > 0) {
            for (var i = 0; i < acts.length; i++) {
                items.push(`
                <!-- Step Item -->
                <li class="step-item">
                <div class="step-content-wrapper">
                    <span class="step-icon step-icon-pseudo step-icon-soft-dark"></span>

                    <div class="step-content">
                    <h5 class="mb-1">
                        <a class="text-dark">${acts[i].activityName}</a>
                    </h5>

                    <p class="font-size-sm mb-1">${acts[i].activityDescription}</p>

                    <small class="text-muted text-uppercase">${acts[i].updatedDate}</small>
                    </div>
                </div>
                </li>
                <!-- End Step Item -->`);
            }

            $('#user-activities-page').replaceWith(`
            <!-- Body -->
                <div class="card-body card-body-height" style="height: 30rem;">
                    <!-- Step -->
                    <ul class="step step-icon-xs">
                        ${items.join('')}
                    </ul>
                    <!-- End Step -->
                </div>
            <!-- End Body -->`);
        }
    });

    uuid = $('#uuid').attr('data-id');

    $.get("User/ProfileValue", {
        userId: uuid
    }).done(function (val) {

        $('#profile-value').html(`
            <div class="progress flex-grow-1">
                <div class="progress-bar bg-primary" role="progressbar" style="width: ${val}%" aria-valuenow="${val}" aria-valuemin="0" aria-valuemax="100"></div>
            </div>
            <span class="ml-4">${val}%</span>
        `);

    }).fail(function () {
        alert("Throw error when get profile value.");
    });
});