﻿
let SinglePage = {};

SinglePage.LoadModal = function () {
    let url = window.location.hash.toLowerCase();
    if (!url.startsWith("#showmodal")) {
        return;
    }
    url = url.split("showmodal=")[1];
    $.ajax({
        url: url,
        type: "get",
        dataType: "html"
    })
        .done(function (htmlPage, statusText) {
            const modal = $("#ModalContent").html(htmlPage);
            $.validator.unobtrusive.parse(modal);
            showModal();
        })
        .fail(function (error) {
            alert("خطایی رخ داده، لطفا با مدیر سیستم تماس بگیرید.");
        })
};

function showModal() {
    $("#MainModal").modal("show");
}

function hideModal() {
    $("#MainModal").modal("hide");
}

$(document).ready(function () {
    window.onhashchange = function () {
        SinglePage.LoadModal();
    };

    $("#MainModal").on("shown.bs.modal",
        function () {
            window.location.hash = "##";
            $('.persianObserveCalnder').persianDatepicker({
                format: 'YYYY/MM/DD',
                autoClose: true
            });
        });

    $("#MainModal").on("submit",
        'form[data-ajax="true"]',
        function (e) {
            e.preventDefault();
            const form = $(this);
            const method = form.attr("method").toLocaleLowerCase();
            const url = form.attr("action");
            const action = form.attr("data-action");

            if (method === "get") {
                const data = form.serializeArray();
                $.get(
                    url,
                    data,
                    function (data) {
                        CallBackHandler(data, action, form);
                    });
            }
            else {
                const formData = new FormData(this);
                $.ajax({
                    url: url,
                    type: "post",
                    dataType: "json",
                    enctype: "multipart/form-data",
                    contentType: false,
                    processData: false,
                    data: formData
                })
                    .done(function (operationResult) {
                        CallBackHandler(operationResult, action, form)
                    })
                    .fail(function (xhr) {
                        alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
                    })
            }
            return false;
        });
});

function CallBackHandler(data, action, form) {
    switch (action) {
        case "Message":
            alert(data.Message);
            break;
        case "Refresh":
            if (data.isSucceeded) {
                window.location.reload();
            } else {
                alert(data.message);
            }
            break;
        case "RefereshList":
            {
                hideModal();
                const refereshUrl = form.attr("data-refereshurl");
                const refereshDiv = form.attr("data-refereshdiv");
                get(refereshUrl, refereshDiv);
            }
            break;
        case "setValue":
            {
                const element = form.data("element");
                $(`#${element}`).html(data);
            }
            break;
        default:
    }
}

function get(url, refereshDiv) {
    const searchModel = window.location.search;
    $.get(url,
        searchModel,
        function (result) {
            $("#" + refereshDiv).html(result);
        });
}

function makeSlug(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(convertToSlug(value));
}

var convertToSlug = function (str) {
    var $slug = '';
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return $slug.toLowerCase();
};

function checkSlugDuplication(url, dist) {
    const slug = $('#' + dist).val();
    const id = convertToSlug(slug);
    $.get({
        url: url + '/' + id,
        success: function (data) {
            if (data) {
                sendNotification('error', 'top right', "خطا", "اسلاگ نمی تواند تکراری باشد");
            }
        }
    });
}

function fillField(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(value);
}

$(document).on("click",
    'button[data-ajax="true"]',
    function () {
        const button = $(this);
        const form = button.data("request-form");
        const data = $(`#${form}`).serialize();
        let url = button.data("request-url");
        const method = button.data("request-method");
        const field = button.data("request-field-id");
        if (field !== undefined) {
            const fieldValue = $(`#${field}`).val();
            url = url + "/" + fieldValue;
        }
        if (button.data("request-confirm") == true) {
            if (confirm("آیا از انجام این عملیات اطمینان دارید؟")) {
                handleAjaxCall(method, url, data);
            }
        } else {
            handleAjaxCall(method, url, data);
        }
    });

function handleAjaxCall(method, url, data) {
    if (method === "post") {
        $.post(url,
            data,
            "application/json; charset=utf-8",
            "json",
            function (data) {

            }).fail(function (error) {
                alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.");
            });
    }
}

$.validator.addMethod("maxFileSize", function (value, element, params) {
    const size = element.files[0].size;
    const maxSize = 300 * 1024;
    return size <= maxSize;
});

$.validator.addMethod("fileType", function (value, element, params) {
    const type = element.files[0].type;
    const fileTypes = ["image/jpg", "image/jpeg", "image/png"]
    return fileTypes.includes(type);
});

$.validator.unobtrusive.adapters.addBool("maxFileSize");

$.validator.unobtrusive.adapters.addBool("fileType");