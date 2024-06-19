// Fill the role after selecting user
$(function () {
    $("#SelectedUserId").on("change", function () {
        var selectedId = $(this).val();
        console.log(selectedId);
        if (selectedId == "") {
            $("#SelectedRoleId").val("");
        } else {
            $.ajax({
                url: `/Role/GetRoleByUserId/${selectedId}`,
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
