// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    function AddItemId(hidden,itemId) {
        let values = $(hidden).val().split(',').filter(x => x !== "");
        if (values.indexOf(itemId) < 0) {
            values.push(itemId);
            $(hidden).val(values.join(','));
            return true;
        }
        return false;
    }
    function RemoveItemId(hidden, itemId) {
        let values = $(hidden).val().split(',').filter(x => x !== "");
        let index = values.indexOf(itemId);
        if (index >= 0) {
            values.splice(index, 1);
            $(hidden).val(values.join(','));
            return true;
        }
        return false;
    }
    function removeTag() {
        let container = $(this).closest("div#platforms,div#musicians,div#musicLabels");
        let hidden = container.find("input[type=hidden]");
        let tag = $(this).closest("li");
        let itemId = tag.data("id");
        if (RemoveItemId(hidden, ""+itemId)) {
            tag.remove();
        }
    }
    function renderTag() {
        let id = $("#contactList").val();
        let selectedItem = $('#contactList').find(":selected");
        let name = selectedItem.data("name");
        let type = selectedItem.data("type");
        let setval = (hidden, boxId, itemId, name) => {
            if (AddItemId(hidden,itemId)) {
                let tag = $("#contactTag").clone();
                tag.removeClass("hidden").removeAttr("id");
                $(".tags", boxId).append(tag);
                tag.children(".badge").text(name);
                tag.data("id", itemId);
                $("i.fa-remove", tag).on('click', removeTag);
            }
        };
        switch (type) {
            case "MusicLabel": setval("#musicLabelContacts", "#musicLabels", id, name); break;
            case "Musician": setval("#musicianContacts", "#musicians", id, name); break;
            case "Platform": setval("#platformContacts", "#platforms", id, name); break;
        }
    }

    $(".tags i.fa-remove").on('click', removeTag);
    $("#addButton").on('click', renderTag );
})