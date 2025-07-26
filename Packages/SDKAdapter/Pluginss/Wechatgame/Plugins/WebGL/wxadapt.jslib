mergeInto(LibraryManager.library, {
  GDK_Wechatgame_ReportEvent: function (eventid, data) {
    try {
      var eventidStr = UTF8ToString(eventid);
      var dataStr = UTF8ToString(data);
      console.log(`[埋点] eventId=${eventidStr}, data=${dataStr}`)
      var dataObj = JSON.parse(dataStr);
      wx.reportEvent(eventidStr, dataObj);
    } catch (error) {
      console.error("reportEvent-failed:", error)
    }
  },
});
