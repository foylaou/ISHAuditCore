// 選取與聊天機器人相關的 DOM 元素
const chatbotToggler = document.querySelector(".chatbot-toggler"); // 聊天機器人切換按鈕
const closeBtn = document.querySelector(".close-btn"); // 關閉按鈕
const chatInput = document.querySelector(".chat-input textarea"); // 聊天輸入框
const sendChatbtn = document.querySelector(".chat-input span"); // 送出聊天訊息按鈕
const chatbox = document.querySelector(".chatbox"); // 聊天框

let userMessage = null; // 儲存使用者訊息的變數

const inputInitHeight = chatInput.scrollHeight; // 輸入框的初始高度

// 創建聊天 <li> 元素，包含傳入的訊息和 class 名稱
const createChatLi = (message, className) => {
    const chatLi = document.createElement("li"); // 創建 <li> 元素
    chatLi.classList.add("chat", className); // 添加 class 名稱
    let chatContent = className === "outgoing" // 判斷是輸出訊息還是輸入訊息
        ? `<p></p>` // 如果是輸出訊息，創建 <p> 元素
        : `<span class="material-symbols-outlined"><svg xmlns="http://www.w3.org/2000/svg" fill-rule="evenodd" stroke-linejoin="round" stroke-miterlimit="2" clip-rule="evenodd" viewBox="0 0 500 500" id="star"><path fill="#ffd600" d="M250,25L317.308,182.692L442.308,250L317.308,317.308L250,475L182.692,317.308L57.692,250L182.692,182.692L250,25Z"></path></svg></span><p></p>`;
    // 如果是輸入訊息，創建 <span> 和 <p> 元素
    chatLi.innerHTML = chatContent; // 設置聊天內容的 HTML
    chatLi.querySelector("p").textContent = message; // 設置訊息內容
    return chatLi; // 返回創建的 <li> 元素
};
//傳送資料處理


// 生成機器人的隨機回應
const generateResponse = (incomingChatli) => {
    const API_URL = "https://api.openai.com/v1/chat/completions"; // API URL
    const messageElement = incomingChatli.querySelector("p"); // 選取訊息元素

    // const suggest_data = $('#example2').DataTable()
    const requestOptions = {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${API_KEY}`, // 授權標頭
        },
        body: JSON.stringify({
            model: "gpt-4o", // 使用的模型
            messages: [
                {
                    role: "system",
                    content: "你是具備工業安全衛生及化工相關知識的博士，請用繁體中文回答使用者相關的問題知識"
                    ,
                    role: "user",
                    content: userMessage, // 使用者的訊息
                },
            ],
        }),
    };

    // 發送 POST 請求到 API，取得回應並設置回應內容為段落文字
    fetch(API_URL, requestOptions)
        .then((res) => res.json())
        .then((data) => {
            messageElement.textContent = data.choices[0].message.content.trim(); // 設置回應訊息
        })
        .catch(() => {
            messageElement.classList.add("出現錯誤了"); // 添加錯誤樣式
            messageElement.textContent = "發生錯誤請檢查api_key並再次嘗試."; // 錯誤提示訊息
        })
        .finally(() => chatbox.scrollTo(0, chatbox.scrollHeight)); // 滾動到聊天框底部
};

// 處理聊天訊息
const handleChat = () => {
    userMessage = chatInput.value.trim(); // 取得使用者輸入的訊息並去除多餘的空白
    if (!userMessage) return; // 如果訊息為空，則返回

    // 清空輸入框並將其高度設置為初始高度
    chatInput.value = "";
    chatInput.style.height = `${inputInitHeight}px`;

    // 將使用者訊息添加到聊天框
    const outgoingChatli = createChatLi(userMessage, "outgoing");
    chatbox.appendChild(outgoingChatli);
    chatbox.scrollTo(0, chatbox.scrollHeight); // 滾動到聊天框底部

    setTimeout(() => {
        // 等待回應時顯示 "Typing..." 訊息
        const incomingChatli = createChatLi("輸入訊息中...", "incoming");
        chatbox.appendChild(incomingChatli);
        generateResponse(incomingChatli); // 生成機器人回應
    }, 600); // 600 毫秒後顯示 "Typing..."
};

// 輸入框內容變化時調整其高度
chatInput.addEventListener("input", () => {
    chatInput.style.height = `${inputInitHeight}px`; // 設置為初始高度
    chatInput.style.height = `${chatInput.scrollHeight}px`; // 調整為內容高度
});

// 按下 Enter 鍵時處理聊天訊息
chatInput.addEventListener("keydown", (e) => {
    // 如果按下 Enter 鍵且未按下 Shift 鍵並且視窗寬度大於 767px，則處理聊天訊息
    if (e.key === "Enter" && !e.shiftKey && window.innerWidth > 767) {
        e.preventDefault(); // 防止換行
        handleChat(); // 處理聊天訊息
    }
});

// 點擊送出按鈕時處理聊天訊息
sendChatbtn.addEventListener("click", handleChat);

// 點擊關閉按鈕時隱藏聊天機器人
closeBtn.addEventListener("click", () =>
    document.body.classList.remove("show-chatbot")
);

// 點擊聊天機器人切換按鈕時顯示或隱藏聊天機器人
chatbotToggler.addEventListener("click", () =>
    document.body.classList.toggle("show-chatbot")
);
