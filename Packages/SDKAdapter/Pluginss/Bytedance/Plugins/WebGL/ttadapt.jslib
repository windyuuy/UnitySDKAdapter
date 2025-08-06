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
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_ShowLoading", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_ShowLoading", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
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
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_HideLoading", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_HideLoading", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
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
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_ShowModal", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_ShowModal", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
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
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_ShowToast", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_ShowToast", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
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
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_HideToast", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
        },
        fail: function (errMsg) {
          var resultStr = JSON.stringify({
            sessionId,
            errMsg: errMsg,
          })
          console.log("BytedanceGDK_JsCallbackListener:", "GDK_Bytedance_HideToast", resultStr)
          SendMessage('BytedanceGDK_JsCallbackListener', "ReceiveCallback", resultStr);
        },
      })
    } catch (error) {
      console.error("GDK_Bytedance_HideToast-failed:", error)
    }
  },

  GDK_Bytedance_GetUserDataPath: function () {
    try {
      let resultStr = tt.env.USER_DATA_PATH;
      console.log("tt.env.USER_DATA_PATH:", resultStr);
      let bufferSize = lengthBytesUTF8(resultStr) + 1;
      let buffer = _malloc(bufferSize);
      stringToUTF8(resultStr, buffer, bufferSize);
      return buffer;
    } catch (error) {
      console.error("GDK_Bytedance_GetUserDataPath-failed:", error)
      return null;
    }
  },
  GDK_Bytedance_Openlink: function (openlink) {
    if (wx.createPageManager) {
      if (openlink == null || openlink == "") {
        openlink = "-SSEykJvFV3pORt5kTNpS06Hq3gJrqJzZSmH4s-t_mjH0A_vwnACsknOYfhugKp0LfE9WBcjiw5ejlTSnlp1RzqjH_ZcBH_CZtDfO4Fgi1isdKcIlJRF8kir6RDRNkMwPBzUzCjErxOpUeZ6vyYW4sKaMBfsIksbixLxCAX3l43557LnPiRaYtugfq10R-InXELR5IcF559mGY5ptAmZJbT1zElEqJ1aKACrPzFPi1JPYprzdjleaQRNwHtwITt3WNcAUdUvccwVsIcu7UcGVNGKUHNiBi_kj1R-rk1BgX3fu5AWiDkUxS8_lrqXXJ-OUqXszGd0G6k8CBnMNyXhUw" // 由不同渠道获得的OPENLINK值
      }
      console.log("wxOpenlink: " + openlink);
      const pageManager = wx.createPageManager();

      pageManager
        .load({
          openlink: openlink
        })
        .then((res) => {
          // 加载成功，res 可能携带不同活动、功能返回的特殊回包信息（具体请参阅渠道说明）
          console.log(res);

          // 加载成功后按需显示
          pageManager.show();
        })
        .catch((err) => {
          // 加载失败，请查阅 err 给出的错误信息
          console.error(err);
        });
    } else {
      // 如果希望用户在最新版本的客户端上体验您的小程序，可以这样子提示
      wx.showModal({
        title: '提示',
        content: '当前微信版本过低，无法使用该功能，请升级到最新微信版本后重试。'
      })
    }
  },

});
