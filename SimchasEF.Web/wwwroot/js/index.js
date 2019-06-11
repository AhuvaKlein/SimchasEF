$(() => {

    $('#new-simcha-btn').on('click', function () {
        //console.log('hello');
        $('#addSimchaModal').modal('show');
    });

    $("#new-contributor").on('click', function () {
        //console.log('contribute');
        $("#addContributorModal").modal('show');

    });

    $(".deposit").on('click', function () {

        const button = $(this);
        const id = button.data('id');
        $("#contributorId").val(id);

        $("#addDepositModal").modal('show');

    });

    $(".editContributor").on('click', function () {

        const button = $(this);
        const name = button.data('name');
        const id = button.data('id');
        const cell = button.data('cell');
        const dateJoined = button.data('datejoined');
        const alwaysInclude = button.data('alwaysinclude');
        console.log(id);

        $("#Name").val(name);
        $("#contributorIdModal").val(id);
        $("#cell").val(cell);
        $("#dateJoined").val(dateJoined);
        if (alwaysInclude === "True") {
            $("#alwaysInclude").prop('checked', true);
        }
        else {
            $("#alwaysInclude").prop('checked', false);
        }


        $("#editContributorModal").modal('show');

    });


})