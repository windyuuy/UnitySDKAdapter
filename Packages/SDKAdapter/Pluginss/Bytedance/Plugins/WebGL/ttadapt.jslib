mergeInto(LibraryManager.library, {
  GDK_Bytedance_ReportEvent: function (eventid, data) {
    try {
      var eventidStr = UTF8ToString(eventid);
      var dataStr = UTF8ToString(data);
      console.log(`[埋点] eventId=${eventidStr}, data=${dataStr}`)
      var dataObj = JSON.parse(dataStr);
      tt.reportAnalytics(eventidStr, dataObj);
    } catch (error) {
      console.error("GDK_Bytedance_ReportEvent-failed:", error)
    }
  },

  GDK_Bytedance_ShowLoading: function (sessionId, optionsJson) {
    try {
      var optionsStr = UTF8ToString(optionsJson);
      var options = JSON.parse(optionsStr);
      tt.showLoading({
        title: options.title,
        success: function (errMsg) {
          var resultStr = JSON.stringify({
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
      })
    } catch (error) {
      console.error("GDK_Bytedance_ShowLoading-failed:", error)
    }
  },

  GDK_Bytedance_HideLoading: function (sessionId) {
    try {
      tt.hideLoading({
        success: function (errMsg) {
          var resultStr = JSON.stringify({
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
      })
    } catch (error) {
      console.error("GDK_Bytedance_HideLoading-failed:", error)
    }
  },

  GDK_Bytedance_ShowModal: function (sessionId, optionsJson) {
    try {
      var optionsStr = UTF8ToString(optionsJson);
      var options = JSON.parse(optionsStr);
      tt.showModal({
        cancelColor: options.cancelColor,
        cancelText: options.cancelText,
        confirmColor: options.confirmColor,
        confirmText: options.confirmText,
        content: options.content,
        editable: options.editable,
        placeholderText: options.placeholderText,
        showCancel: options.showCancel,
        title: options.title,
        success: function (errMsg) {
          var resultStr = JSON.stringify({
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
      })
    } catch (error) {
      console.error("GDK_Bytedance_ShowModal-failed:", error)
    }
  },

  GDK_Bytedance_ShowToast: function (sessionId, optionsJson) {
    try {
      var optionsStr = UTF8ToString(optionsJson);
      var options = JSON.parse(optionsStr);
      tt.showToast({
        title: options.title,
        duration: options.duration,
        icon: options.icon,
        success: function (errMsg) {
          var resultStr = JSON.stringify({
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
      })
    } catch (error) {
      console.error("GDK_Bytedance_ShowLoading-failed:", error)
    }
  },

  GDK_Bytedance_HideToast: function (sessionId) {
    try {
      tt.hideToast({
        success: function (errMsg) {
          var resultStr = JSON.stringify({
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          var bufferSize = lengthBytesUTF8(resultStr) + 1;
          var buffer = _malloc(bufferSize);
          stringToUTF8(resultStr, buffer, bufferSize);
          SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
        },
      })
    } catch (error) {
      console.error("GDK_Bytedance_HideToast-failed:", error)
    }
  },

  GDK_Bytedance_GetUserDataPath: function () {
    try {
      let resultStr = tt.env.USER_DATA_PATH;
      let bufferSize = lengthBytesUTF8(resultStr) + 1;
      let buffer = _malloc(bufferSize);
      stringToUTF8(resultStr, buffer, bufferSize);
      SendMessage('JsCallbackListener', "ReceiveCallback", buffer);
    } catch (error) {
      console.error("GDK_Bytedance_GetUserDataPath-failed:", error)
    }
  },

});
