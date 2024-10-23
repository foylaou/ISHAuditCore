function switchContainer(hideId, showId, direction = 'right') {
    const hideContainer = document.getElementById(hideId);
    const showContainer = document.getElementById(showId);

    document.querySelectorAll('.login-container').forEach(container => {
        container.classList.remove('active');
    });

    hideContainer.classList.add(`slide-${direction}`);
    showContainer.classList.remove('hidden');

    showContainer.offsetHeight;

    showContainer.classList.add(direction === 'right' ? 'slide-left' : 'slide-right');
    showContainer.classList.add('active');

    setTimeout(() => {
        hideContainer.classList.add('hidden');
        hideContainer.classList.remove(`slide-${direction}`);
        showContainer.classList.remove(direction === 'right' ? 'slide-left' : 'slide-right');
    }, 600);
}



function returnToUsername() {
    $('#Nickname').empty();
    switchContainer('stepPassword', 'stepUsername', 'left');
}

function otherOptions() {
    switchContainer('stepPassword', 'stepOtherOptions', 'right');
}

function backToPassword() {
    switchContainer('stepOtherOptions', 'stepPassword', 'left');
}

function verifyByEmail() {
    alert('此功能上未開放...');
}

function verifyBySMS() {
    alert('此功能上未開放...');
}

function verifyByDevice() {
    alert('此功能上未開放...');
}

window.onload = function() {
    document.getElementById('stepEmail').classList.add('active');
}
function nextStep() {
    $('.loader-container:eq(0) .loader').show();
    $('#errorMessage').empty();
    $.ajax({
        type: 'POST',
        url: '/Login/QueryUser',
        data: { username: $('#usernameInput').val() }, // 傳送輸入框中的值
        dataType: "json",
        success: function (data) {
            if (data.success) {
                $('.loader-container:eq(0) .loader').hide();
                // 切換到下一步，這裡的 'right' 是動畫方向
                $('#Nickname').text(data.Nickname);
                switchContainer('stepUsername', 'stepPassword', 'right');
            } else {
                $('.loader-container:eq(0) .loader').hide();
                // 將伺服器回應的錯誤訊息顯示在指定的元素中
                $('#errorMessage').html('<i class="fas fa-exclamation-circle"></i> ' + data.Message);
            }
        },
        error: function (xhr, status, error) {
            console.log("error");
            // 處理 AJAX 請求的錯誤
            $('.loader-container:eq(0) .loader').hide();
            console.error("發生錯誤：" + error);
        }
    });
}

var widgetId;

// 當 CAPTCHA 加載後，保存 widgetId
window.onload = function() {
    widgetId = turnstile.render('#captcha', {
        sitekey: '0x4AAAAAAAgCSXub-CbDkEzy', // 替換為您的 sitekey
        callback: function(token) {
            // 將 CAPTCHA 的 token 存入隱藏的 input 中
            document.getElementById('cf-turnstile-response').value = token;
            console.log("CAPTCHA passed, token: " + token);
        }
    });
};
// 當表單提交失敗後刷新 CAPTCHA
function refreshCaptcha() {
    turnstile.reset(widgetId);  // 使用 widgetId 來重置 CAPTCHA
}

function Login() {
    $('.loader-container:eq(0) .loader').show();
    
    // 確保 CAPTCHA 驗證通過，並檢查是否有 token
    var token = $('#cf-turnstile-response').val();
    if (!token) {
        $('.loader-container:eq(0) .loader').hide();
        Swal.fire({
            icon: 'error',
            title: '錯誤',
            text: '請通過 CAPTCHA 驗證',
            confirmButtonText: '確定'
        });
        return;
    }
    
    $.ajax({
        type: 'POST',
        url: '/Login',
        data: {
            username: $('#usernameInput').val(),
            password: $('#passwordInput').val(),
            captchaToken: token,
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()  // 傳送 CSRF Token
        },
        dataType: "json",
        success: function (data) {
            if (data.success) {
                $('.loader-container:eq(0) .loader').hide();
                // 登入成功，導向首頁或其他指定的頁面
                window.location.href = data.redirectUrl;
            } else {
                $('.loader-container:eq(0) .loader').hide();
                // 將伺服器回應的錯誤訊息顯示在指定的元素中
                $('#errorPassword').html('<i class="fas fa-exclamation-circle"></i> '+data.Message); // 正確的賦值方式是 .text() 或 .html()，而不是 .val()
            }
        },
        error: function (xhr, status, error) {
            // 處理 AJAX 請求的錯誤
            $('.loader-container:eq(0) .loader').hide();
            console.error("發生錯誤：" + error);
        }
    });
}