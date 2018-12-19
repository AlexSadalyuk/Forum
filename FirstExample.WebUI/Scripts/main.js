$(".edit").on("click",function () {
    $("#commentId").val($(this).data("comment"));
    $("#commetText").val($(this).data("text"));
    $("#PostId").val($(this).data("post"));
    $("#PageId").val($(this).data("page"));
});

