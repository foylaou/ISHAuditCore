function Bom(TreeName, data, eq_uuid) {
    $(TreeName).jstree({
        "core": {
            "animation": 0,
            "check_callback": true,
            "themes": {"stripes": true},
            "data": data
        },
        "types": {
            "root": {
                "icon": "jstree-folder"
            },
            "sub_unit": {
                "icon": "jstree-folder",
                "valid_children": ["parts", "folder", "file"]
            },
            "parts": {
                "icon": "jstree-folder",
                "valid_children": ["folder", "file"]
            },
            "file": {
                "icon": "jstree-file",
                "valid_children": []
            },
            "folder": {
                "icon": "jstree-folder",
                "valid_children": ["default", "folder", "file"]
            }
        },
        "plugins": [
            "contextmenu", "search",
            "state", "types", "wholerow"
        ],
        "contextmenu": {
            "items": function ($node) {
                var tree = $(TreeName).jstree(true);
                return {
                    "Create": {
                        "separator_before": false,
                        "separator_after": false,
                        "label": "�s�W",
                        "submenu": {
                            "Folder": {
                                "seperator_before": false,
                                "seperator_after": false,
                                "label": "�s�W�[�c",
                                "action": function (obj) {

                                    parent = $node.id;
                                    text = '�s�W����';
                                    alert(text);
                                    $.ajax({
                                        url: '/Equipment/SetBomFrame',
                                        type: "post",
                                        data: {parent: $node.id, text: text, eq_uuid: eq_uuid},
                                        headers: {
                                            'RequestVerificationToken': '@TokenHeaderValue()'
                                        },
                                        success: function (data) {
                                            tree.create_node(parent, {
                                                "id": data,
                                                "text": text,
                                                type: 'folder'
                                            }, "last");
                                            tree.deselect_all();
                                            tree.select_node(data);
                                            Toast.fire({
                                                icon: 'success',
                                                title: 'Success'
                                            })
                                        },
                                        error: function (XMLHttpRequest, textStatus) {
                                            Toast.fire({
                                                icon: 'error',
                                                title: '�[������'
                                            });
                                        }
                                    });
                                }
                            },
                            "File": {
                                "seperator_before": false,
                                "seperator_after": false,
                                "label": "Link�ƫ~",
                                action: function (obj) {
                                    $("#sel_material").modal('show');
                                }
                            }
                        }
                    },
                    "Rename": {
                        "separator_before": false,
                        "separator_after": false,
                        "label": "�s��",
                        "action": function (obj) {
                            if ($node.type == "sub_unit" || $node.type == "parts" || $node.type == "root") {
                                Toast.fire({
                                    icon: 'warning',
                                    title: '�w�]�ե�L�k�s��'
                                })
                            } else {
                                tree.edit($node);
                            }
                        }
                    },
                    "Remove": {
                        "separator_before": false,
                        "separator_after": false,
                        "label": "Remove",
                        "action": function (obj) {
                            if ($node.type == "sub_unit" || $node.type == "parts" || $node.type == "root") {
                                Toast.fire({
                                    icon: 'warning',
                                    title: '�w�]���صL�k�R��'
                                })
                            } else {
                                var sel_node = $(TreeName).jstree("get_selected", true);
                                $.ajax({
                                    url: '/Equipment/DeleteBom',
                                    type: "post",
                                    data: {id: sel_node[0].id},
                                    success: function (data) {
                                        if (data == "true") {
                                            tree.delete_node($node);
                                        } else {
                                            Toast.fire({
                                                icon: 'error',
                                                title: '�[������'
                                            });
                                        }
                                    },
                                    error: function (XMLHttpRequest, textStatus) {
                                        Toast.fire({
                                            icon: 'error',
                                            title: '�[������'
                                        });
                                    }
                                });
                            }
                        }
                    }
                };
            }
        }
    });
    $(TreeName).on('rename_node.jstree', function (e, data) {
        $.ajax({
            url: '/Equipment/SaveBomText',
            type: "post",
            data: {id: data.node.id, text: data.text},
            success: function (data) {
                if (data == "true") {
                } else {
                    Toast.fire({
                        icon: 'error',
                        title: '�[������'
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus) {
                Toast.fire({
                    icon: 'error',
                    title: '�[������'
                });
            }
        });
    });
}