function block() {
    $.blockUI({
        message: '<h1>Please wait...</h1>',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function blockMessage(message) {
    $.blockUI({
        message: '<h5>' + message + '</h5>',
        fadeIn: 0,
        fadeOut: 0,
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function unblock() {
    setTimeout($.unblockUI, 1);
}