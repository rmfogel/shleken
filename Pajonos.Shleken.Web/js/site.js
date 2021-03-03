window.ModalClose = function () {
    $('#modal').modal('hide');
};

window.ModalCloseAndRefresh = function () {
    $('#modal').modal('hide');
    window.location.reload();
};


$(function () {


    $(".btn-search").click(function () {
        $(".search-panel").slideToggle();
    });
    if (window.location.href.indexOf("?search.") >= 0)
        $(".search-panel").show();
    var showModel = $("#modalRolesNew").attr("data-show-model");


    $("a[data-popup]").click(function (e) {
        e.preventDefault();

        var title = $(this).attr("title");
        var href = $(this).attr("href");

        $("#modal .modal-title").text(title);
        $("#modal iframe").attr("src", href);
        $("#modal").modal("show");
    });

    $("button[data-popup-close]").click(function (e) {
        e.preventDefault();
        window.parent.ModalClose();
    });

    $(".sidebar-toggle").click(function () {
        $(".main > .left").toggleClass("mini");
    });

    $('[data-toggle="popover"]').popover({
        html: true, container: "body"
    });

    //$("form .btn").click(function () {
    //    $(this).button('loading');
    //});

    $(".sortable").sortable({
        handle: ".sortable-handle"
    });

    $(".list-checkbox input:checkbox").change(function () {
        $(this).closest("li").find(".text").toggleClass("line-through", $(this).is(":checked"));
    });

    $(".list-checkbox .text").click(function () {
        $(this).closest("li").find("input:checkbox").click();
    });

    $(".delete-item").click(function (e) {
        if (confirm("Are you sure you want delete this item?") == false) {
            e.preventDefault();
        }
    });

    function removeBeforesave($element) { // <-- changes
        debugger;
        if (confirm("Are you sure you want delete this item?") == true) {
            $element.closest('tr').remove();
        }
        return false;
    };

    $(".delete-item1").click(function (e) {
        e.preventDefault();
        if (confirm("Are you sure you want delete this item?") == true) {
            e.continueDefault();
        }
    });
    //Tasks
    $(document).ready(function () {
        TasksInput();
    });

    $("#addTasks").click(function () {
        var RolesCount = $("#table").attr('data-count');
        var ProjectId = parseInt($("#table").attr('data-Projects-id'));
        var url = '/project/GetRoles/' + ProjectId + '';

        var o = "";

        $(".Tasks-input").off("change");
        $(".Tasks-input").off("click");
        $('.delete-item-before-save').off("click");

        $.ajax({
            type: 'GET',
            url: url,
            dataType: 'json',
            success: function (data) {

                for (i = 0; i < parseInt(RolesCount); i++) {
                    o += '<td><input type="number" min="0" class="input-table-number Tasks-input hour-input " data-hours="' + data[i].Id + '" />H</td>+'
                }
                //data - hours="' + Roles[i].RoleId + '"

                $('#Tasks').append(function () {
                    return '<tr class="row-resource" data-id="0">'
                        + '<td data-name><input class="input-table" type="text"/></td>'
                        + '<td data-description><input class="input-table" type="text"/></td>'
                        + '<td data-show-client><input type="checkbox" /></td>'
                        + `<td data-status><select id="a@(item.Id)">
                          
    
                                <option value="new">new</option>
                                <option value="in progress">in progress</option>
                                <option value="done">done</option>


                         
                        </select></td>`
                        + '<td></td>'
                        + o
                        + '<td class="client-hours"></td>'
                        + '<td class="total-cost"></td>'
                        + '<td class="delete-item-before-save" type="button"><i class="glyphicon glyphicon-trash fa-fw" aria-hidden="true"></i></td>'
                        + '</tr>'
                })
                $('.delete-item-before-save').click(function () {
                    removeBeforesave($(this));
                });

                $(".Tasks-input").change(function () {
                    TasksInput();

                    $(".Tasks-input").click(function () {
                        TasksInput();
                    });
                });

            }
        });


        //  var len = Roles.length;



    });




    //$("#addFixes").click(function () {
    //    var RolesCount = $("#table").attr('data-count');
    //    $(".Tasks-input").off("change");
    //    $(".Tasks-input").off("click");
    //    $('.delete-item-before-save').off("click");
    //    var o = "";
    //    for (i = 0; i < RolesCount; i++) {
    //        o += '<td></td>+'
    //    }

    //    $(".Fixes").last().after(function () {
    //        return '<tr data-id="0" class="row-Fixes Fixes">'
    //            + '<td data-name><input class="input-table" type="text"  /></td>'
    //            + '<td data-description><input class="input-table" type="text"/></td>'
    //            + '<td data-show-client><input type="checkbox" /></td>'
    //            + '<td data-value><input type="number" min="0" class="input-table-number Tasks-input"/>$</td>'
    //            + o
    //            + '<td></td>'
    //            + '<td class="cost"></td>'
    //            + '<td class="delete-item-before-save" type="button"><i class="glyphicon glyphicon-trash fa-fw" aria-hidden="true"></i></td>'
    //            + '</tr>'
    //    });
    //    $('.delete-item-before-save').click(function () {
    //        removeBeforesave($(this));
    //    });

    //    $(".Tasks-input").change(function () {
    //        TasksInput();
    //    });

    //    $(".Tasks-input").click(function () {
    //        TasksInput();
    //    });
    //});

    //$("#addGlobals").click(function () {
    //    $(".Tasks-input").off("change");
    //    $(".Tasks-input").off("click");
    //    $('.delete-item-before-save').off("click");
    //    var RolesCount = $("#table").attr('data-count');
    //    var o = "";
    //    for (i = 0; i < RolesCount; i++) {
    //        o += '<td></td>+'
    //    }

    //    $(".jobs").last().after(function () {
    //        return '<tr class="row-Globals jobs" data-id="0">'
    //            + '<td data-name><input class="input-table" type="text" /></td>'
    //            + '<td data-description><input class="input-table" type="text"/></td>'
    //            + '<td data-show-client><input type="checkbox" /></td>'
    //            + '<td data-value><input type="number" data-hours=""  min="0" class="input-table-number Tasks-input hour-input" />%</td>'
    //            + o
    //            + '<td></td>'
    //            + '<td class="internal-cost"></td>'
    //            + '<td class="delete-item-before-save" type="button"><i class="glyphicon glyphicon-trash fa-fw" aria-hidden="true"></i></td>'
    //            + '</tr>'
    //    });
    //    $('.delete-item-before-save').click(function () {
    //        removeBeforesave($(this));
    //    });

    //    $(".Tasks-input").change(function () {
    //        TasksInput();

    //        $(".Tasks-input").click(function () {
    //            TasksInput();
    //        });
    //    });
    //});

    var TaskTotalCosts = 0;

    $(".Tasks-input").change(function () {
        TasksInput();
    });

    $(".Tasks-input").click(function () {
        TasksInput();
    });

    function TasksInput() {
        var RolesCount = $("#table").attr('data-count');
        if ($("#modalRolesNew").attr("data-show-model") == 1) {
            $("#modalRolesNew").modal('show');
        };
        $("#table").find(".row-resource").each(function () {
            var rowTotalCost = 0;
            var rowTotalHours = 0;
            var cnt = 5;
            $(this).find(".Tasks-input").each(function () {
                if (!isNaN(this.value) && this.value.length != 0) {
                    var attrCost = $('#table').find("th:eq(" + cnt + ")").attr("data-price");
                    rowTotalCost += parseInt(attrCost) * parseInt(this.value);
                    rowTotalHours += parseInt(this.value);
                    cnt++;
                }
            })
            $(this).find(".client-hours").text(rowTotalHours);
            $(this).find(".total-cost").text(rowTotalCost);
            TaskTotalCosts += rowTotalCost
            $("#total-cost").html(TaskTotalCosts)
            //cost += rowTotalCost;
            //console.log($(".Tasks-total-cost"));
            //$(".Tasks-total-cost").each(console.log(this.innerHTML))


        });

        for (i = 4; i < parseInt(RolesCount) + 6; i++) {
            var sumInput = 0;
            var sum = 0;
            $('#Tasks').find(".row-resource").each(function () {
                var totalColInput = $(this).find("td:eq(" + i + ") input").val();
                var totalCol = $(this).find("td:eq(" + i + ")").text();
                if (!isNaN(totalColInput) && totalColInput.length != 0)
                    sumInput += parseInt(totalColInput);
                if (!isNaN(totalCol) && totalCol.length != 0)
                    sum += parseInt(totalCol);

            });
            if (sumInput > 0)
                $("#table").find("tr.row-resource-culculate td:eq(" + i + ")").text(sumInput);
            else
                $("#table").find("tr.row-resource-culculate td:eq(" + i + ")").text(sum);
        }

        $('#table').find(".row-Fixes").each(function () {
            var Fixestotal = 0;
            $(this).find('.Tasks-input').each(function () {
                if (!isNaN(this.value) && this.value.length != 0) {
                    Fixestotal = this.value;
                }
            });
            $(this).find(".cost").text(Fixestotal);
        });

        var i = parseInt(RolesCount) + 5;
        var sum = 0;
        $('#table').find(".row-Fixes").each(function () {
            var totalCol = $(this).find("td:eq(" + i + ")").text();
            if (!isNaN(totalCol) && totalCol.length != 0)
                sum += parseInt(totalCol);
        });
        $("#table").find("tr.row-Fixes-culculate td:eq(" + i + ")").text(sum);


        var internalCost = 0;
        $("#table").find(".calculate-Tasks-internal-cost").each(function () {
            internalCost += parseInt($(this).text());
        })

        $("#table").find(".row-Globals").each(function () {
            var rowGlobals = 0;
            rowGlobals = $(this).find(".Tasks-input").val();
            $(this).find(".internal-cost").text(rowGlobals / 100 * internalCost);
        });


        for (i = parseInt(RolesCount) + 4; i < parseInt(RolesCount) + 6; i++) {
            var sum = 0;
            $('#table').find(".row-Globals").each(function () {
                var totalCol = $(this).find("td:eq(" + i + ")").text();
                if (!isNaN(totalCol) && totalCol.length != 0)
                    sum += parseInt(totalCol);
            });
            $("#table").find("tr.row-Globals-culculate td:eq(" + i + ")").text(sum);
        }

        for (i = parseInt(RolesCount) + 4; i < parseInt(RolesCount) + 6; i++) {
            var sum = 0;
            $('#table').find(".sum-total").each(function () {
                var totalCol = $(this).find("td:eq(" + i + ")").text();
                if (!isNaN(totalCol) && totalCol.length != 0)
                    sum += parseInt(totalCol);
            });
            $("#table").find("tr.total td:eq(" + i + ")").text(sum);
        }

        //$(".default input").each(function () {
        //    $(this).attr("disabled", "true");
        //}
        //);


    }
    function saveTaskstable() {
        var listTasks = [];
        var urlTasks = '/Project/Tasksave';
        var TaskTotalHours = parseInt($(".Tasks-total-hours").text());
        //var TaskTotalCosts = parseInt($(".Tasks-total-cost").text());

        var ProjectId = $('#table').attr('data-Projects-id');

        $("#table").find(".row-resource").each(function () {
            var name = $(this).find("td[data-name] input").val();
            var id = parseInt($(this).attr('data-id'));
            var description = $(this).find('td[data-description] input').val();
            var showClient = $(this).find('td[data-show-client] input').is(':checked');

            var status = $("#a" + id).val();

            var TasksRoles = [];
            $(this).find(".hour-input").each(function () {
                var value = parseInt($(this).val());
                var Roles = parseInt($(this).attr('data-hours'));
                var item = {
                    RoleId: Roles,
                    TaskId: 0,
                    Value: value,
                    Status: status
                }
                TasksRoles.push(item);
            });
            var model = {
                Name: name,
                Description: description,
                ProjectId: ProjectId,
                ShowClient: showClient,
                Id: id,
                TasksRoles: TasksRoles,
                Status: status

            };
            listTasks.push(model);
        });
        //var data = JSON.stringify({
        //    'listTasks': listTasks,
        //    'TaskTotalHours': TaskTotalHours,
        //    'TaskTotalCosts': TaskTotalCosts
        //});
        //$.ajax({
        //    type: "POST",
        //    data: JSON.stringify({
        //        listTasks: listTasks,
        //        TaskTotalHours: TaskTotalHours,
        //        TaskTotalCosts: TaskTotalCosts,
        //        ProjectId: ProjectId
        //    }),
        //    url: urlTasks,
        //    contentType: "application/json"
        //    //   Error: (function () { window.location.reload(); }) 
        //}).done(function () { window.location.reload(); })

        var listFixes = [];
        var urlFixes = '/Project/FixesSave';
        $("#table").find(".row-Fixes").each(function () {
            var name = $(this).find("td[data-name] input").val();
            var id = parseInt($(this).attr('data-id'));
            var description = $(this).find('td[data-description] input').val();
            var value = $(this).find('td[data-value] input').val();
            var ProjectId = $('#table').attr('data-Projects-id');
            var showClient = $(this).find('td[data-show-client] input').is(':checked');
            var status = $("#a" + id).val();

            var model = {
                Name: name,
                Description: description,
                Value: value,
                ProjectId: ProjectId,
                ShowClient: showClient,
                Id: id,
                Status: status
            };
            listFixes.push(model);
        });
        //$.ajax({
        //    type: "POST",
        //    data: JSON.stringify(listFixes),
        //    url: urlFixes,
        //    contentType: "application/json",
        //    //   Error: function () { window.location.reload(); }
        //}).done(function (res) {
        //    window.location.reload();
        //});

        var listGlobals = [];
        var urlGlobals = '/Project/Globalsave';
        $("#table").find(".row-Globals").each(function () {
            var name = $(this).find("td[data-name] input").val();
            var id = parseInt($(this).attr('data-id'));
            var description = $(this).find('td[data-description] input').val();
            var value = $(this).find('td[data-value] input').val();
            var ProjectId = $('#table').attr('data-Projects-id');
            var showClient = $(this).find('td[data-show-client] input').is(':checked');
            var status = $("#a" + id).val();

            var model = {
                Name: name,
                Description: description,
                Value: value,
                ProjectId: ProjectId,
                ShowClient: showClient,
                Id: id,
                Status: status
            };
            listGlobals.push(model);
        });
        //$.ajax({
        //    type: "POST",
        //    data: JSON.stringify(listGlobals),
        //    url: urlGlobals,
        //    contentType: "application/json",
        //    //  Error: function () { window.location.reload(); }
        //}).done(function (res) {
        //    window.location.reload();
        //});

        $.when(
            $.ajax({
                type: "POST",
                data: JSON.stringify({
                    listTasks: listTasks,
                    TaskTotalHours: TaskTotalHours,
                    TaskTotalCosts: TaskTotalCosts,
                    ProjectId: ProjectId
                }),
                url: urlTasks,
                contentType: "application/json"
                //   Error: (function () { window.location.reload(); }) 
            }).then(function () {
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(listFixes),
                    url: urlFixes,
                    contentType: "application/json",
                    Error: function () {/* window.location.reload(); */ }
                }).then(function () {
                    $.ajax({
                        type: "POST",
                        data: JSON.stringify(listGlobals),
                        url: urlGlobals,
                        contentType: "application/json",
                        Error: function () { /*window.location.reload();*/ }
                    }).then(function () {
                        window.location.reload();
                    })
                });
            }));

        //        $.ajax({
        //           type: "POST",
        //           data: JSON.stringify(listFixes),
        //           url: urlFixes,
        //           contentType: "application/json",
        //              Error: function () {/* window.location.reload(); */}
        //       }),

        //   $.ajax({
        //       type: "POST",
        //       data: JSON.stringify(listGlobals),
        //       url: urlGlobals,
        //       contentType: "application/json",
        //         Error: function () { /*window.location.reload();*/ }
        //   })


        //   ).then(function () {

        ////     window.location.reload();

        //   });
    }
    $("#saveTasksTableNow").click(function (e) {
        saveTaskstable();
        //   window.location.reload();
    });
    //$("#saveRoles").click(function (e) {
    //    saveTaskstable()
    //});

    //Teams
    $(document).ready(function () {
        TeamsInput();
    });

    $(".Teams-input").click(function () {
        TeamsInput();
    });

    $(".Teams-input").change(function () {
        TeamsInput();
    });

    function TeamsInput() {
        var diff = $('#TeamsTable').attr("data-Projects-month");
        var rows = 0;
        $('#TeamsTable').find(".Teams-row").each(function () {
            rows++;
            var rowTotalCost = 0;
            var rowTotalHours = 0;
            var cnt = 4;
            var attrCost = $(this).attr("data-price");
            $(this).find(".Teams-input").each(function () {
                if (!isNaN(this.value) && this.value.length != 0) {
                    rowTotalCost += parseInt(attrCost) * parseInt(this.value);
                    rowTotalHours += parseInt(this.value);
                    cnt++;
                }
            })
            $(this).find(".total-hours").text(rowTotalHours);
            $(this).find(".total-cost").text(rowTotalCost);
        });

        $("#TeamsTable").find('.cntUsers').text(rows + " Users");

        for (i = 2; i < parseInt(diff) + 4; i++) {

            var totalSumHours = 0;
            var totalSumCostMonth = 0;
            var totalhours = 0;

            $('#TeamsTable').find(".Teams-row").each(function () {
                var Hours = $(this).find("td:eq(" + i + ") input").val();
                var attrCost = $('#TeamsTable').find("th:eq(" + i + ")").attr("data-price");
                var total = $(this).find("td:eq(" + i + ")").text();
                if (!isNaN(Hours) && Hours.length != 0) {
                    totalSumHours += parseInt(Hours);
                    totalSumCostMonth += parseInt(Hours) * parseInt(attrCost);
                }

                if (!isNaN(total) && total.length != 0)
                    totalhours += parseInt(total);
            });
            if (totalSumHours > 0)
                $("#TeamsTable").find("tr.sum-hours td:eq(" + i + ")").text(totalSumHours);
            else
                $("#TeamsTable").find("tr.sum-hours td:eq(" + i + ")").text(totalhours);

            $("#TeamsTable").find("tr.sum-cost-month td:eq(" + (i - 2) + ")").text(totalSumCostMonth);
        }
        $('#TeamsTable').find('.default').each(function () {
            var inputTeams = $(this).find('input');

        })

    }
    $("#cencelModel").click(function () {
        $("form#TeamsForm")[0].reset();
        $("#showAlert").text("");
        $('#modelResorcesNew').modal('hide');
    });
    $('#addResorces').click(function (e) {
        debugger;
        var ProjectsMonth = $('#TeamsTable').attr("data-Projects-month");
        var Users = $("#UsersName option:selected").text();
        var UserId = parseInt($("#UsersName option:selected").val());
        var RoleId = parseInt($("#Roleslist option:selected").val());
        var Roles = $("#Roleslist option:selected").text();
        var price = 100;
        var ProjectId = parseInt($('#TeamsTable').attr('data-Projects-id'));
        var url = '/Project/GetDatesProjects/' + ProjectId + '';
        $(".Teams-input").off("change");
        $(".Teams-input").off("click");
        $('.delete-item-before-save').off("click");

        var n = "";
        var o = "";

        $.ajax({
            type: 'GET',
            url: url,
            dataType: 'json',
            success: function (data) {
                var findSameItem;
                //   var date = $('#TeamsTable').attr("data-date-now");
                var r = jQuery.now();
                //    var date = $('#TeamsTable').attr("data-date-now");
                //  date.getDate();
                //  date = moment(date).format("DD-MM-YYYY");
                r = moment(r).format("DD/MM/YYYY");
                var new_val = r.split('/');
                dateYearNow = parseInt(new_val[2]);
                //    date = moment(date).getFullYear();
                //date = date.format("MM-DD-YYYY");
                // date.getDate();
                for (i = 0; i < data.length; i++) {

                    data[i] = moment(data[i]).format("DD/MM/YYYY");
                    var newYear = data[i].split('/');
                    dateYear = parseInt(newYear[2]);
                    //dateYear == dateYearNow;
                    if (data[i] > r) {
                        o += '<td class="default"><input type="number" min="0" class="Teams-input input-table-number" disabled data-id="0" value="0"  data-date="' + data[i] + '"/></td>+'
                    }
                    else {
                        o += '<td><input type="number" min="0" class="Teams-input input-table-number"  data-id="0" value="0"  data-date="' + data[i] + '"/></td>+'

                    }
                }
                $("#TeamsTable").find(".Teams-row").each(function () {
                    var id = parseInt($(this).attr('data-Users-id'));
                    var Roles = parseInt($(this).attr('data-Roles'));
                    if (id == UserId && Roles == RoleId) {
                        findSameItem = 1;
                    }
                });

                if (findSameItem == 1) {
                    $("#showAlert").text("this item is exist");
                    // window.location.reload();
                }
                else {
                    var isFind = $("#TeamsTable").find("." + Roles);
                    if (isFind.length != 0) {

                        $('.' + Roles).last().after(function () {
                            return '<tr data-price=' + price + ' data-Users-id=' + UserId + '  data-Roles=' + RoleId + '  class="Teams-row ' + Roles + '">'
                                + '<td>' + Roles + '</td>'
                                + '<td>' + Users + ' ' + price + '</td>'
                                + o
                                + '<td class="total-hours"></td>'
                                + '<td class="total-cost"></td>'
                                + '<td class="delete-item-before-save" type="button"><i class="glyphicon glyphicon-trash fa-fw" aria-hidden="true"></i></td>'
                                + '</tr>'
                        })
                    }
                    else {
                        $('#TeamsTable').append(function () {
                            return '<tr data-price=' + price + ' data-Users-id=' + UserId + ' data-Roles=' + RoleId + '  data-id-item="0"  class="Teams-row ' + Roles + '">'
                                + '<td>' + Roles + '</td>'
                                + '<td>' + Users + ' ' + price + '</td>'
                                + o
                                + '<td class="total-hours"></td>'
                                + '<td class="total-cost"></td>'
                                + '<td class="delete-item-before-save" type="button"><i class="glyphicon glyphicon-trash fa-fw" aria-hidden="true"></i></td>'
                                + '</tr>'
                        });
                    }
                    TeamsInput();
                    $(".Teams-input").click(function () {
                        TeamsInput();
                    });
                    $(".Teams-input").change(function () {
                        TeamsInput();
                    });
                    $('.delete-item-before-save').click(function () {
                        removeBeforesave($(this));
                    });

                    $("form#TeamsForm")[0].reset();
                    $("#showAlert").text("");
                    $('#modelResorcesNew').modal('hide');
                }
            }

        });
    }
    );




    $("#saveTeamsTable").click(function (e) {
        debugger;
        var TeamTotalHours = parseInt($("#TeamsTable").find('.Teams-total-hours').text());
        var TeamTotalCost = parseInt($("#TeamsTable").find('.Teams-total-cost').text());
        var ProjectId = parseInt($('#TeamsTable').attr('data-Projects-id'));
        var listTeams = [];
        var url = '/Project/Teamsave';
        $("#TeamsTable").find(".Teams-row").each(function () {
            //var price = parseInt($(this).attr('data-price'));
            var UserId = parseInt($(this).attr('data-Users-id'));
            var RoleId = parseInt($(this).attr('data-Roles'));
            $(this).find(".Teams-input").each(function () {
                var date = $(this).attr('data-date');
                var hours = parseInt($(this).val());
                var id = parseInt($(this).attr('data-id'));
                var model = {
                    RoleId: RoleId,
                    UserId: UserId,
                    ProjectId: ProjectId,
                    Id: id,
                    Date: date,
                    Hours: hours
                };
                listTeams.push(model);
            });
        });
        $.ajax({
            type: "POST",
            data: JSON.stringify({ listTeams: listTeams, TeamTotalHours: TeamTotalHours, TeamTotalCost: TeamTotalCost, ProjectId: ProjectId }),
            url: url,
            contentType: "application/json"
        }).done(function (res) {
            window.location.reload();
        });
    });
})



