// Fill the role after selecting user
$(function () {
    $("#SelectedUserId").on("change", function () {
        var selectedId = $(this).val();
        if (selectedId == "") {
            $("#SelectedRoleId").val("");
        } else {
            $.ajax({
                url: `/Role/GetUserRoleById/${selectedId}`,
                type: "get",
                success: function (data) {
                    $("#SelectedRoleId").val(data);
                },
                error: function () {
                    toastr["error"]("Something went wrong");
                }
            });
        }
    });
});
