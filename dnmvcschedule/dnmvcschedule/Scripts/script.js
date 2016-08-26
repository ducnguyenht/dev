var OutlookController = {

    MessagesPaneName: "MessageList",
    MessageBodyName: "messageBody",

    ContentSplitterPaneResized: function (s, e) {
        if(typeof (MessagesGrid) != "undefined") {
            if(e.pane.name === OutlookController.MessagesPaneName) {
                MessagesGrid.SetHeight(e.pane.GetClientHeight());
            }
        }
    },

    MessagesGridRowChanged: function (s, e) {
        var currentRow = s.GetFocusedRowIndex();
        s.GetRowValues(currentRow, 'Text', OutlookController.OnGetRowValues);
    },

    MailFilterNodeClick: function (s, e) {
        var filter = e.node.text;

        if(e.node.text === "Inbox" || !e.node.parent) {
            filter = "%";
        }

		// DXCOMMENT: Define the filter row's name
        MessagesGrid.ApplyFilter("[Folder] LIKE '" + filter + "'");
    },

    MessagesGridEndCallback: function (s, e) {
        ContentSplitter.GetPaneByName(OutlookController.MessagesPaneName).RaiseResizedEvent();
    },

    OnGetRowValues: function (values) {
        document.getElementById(OutlookController.MessageBodyName).innerHTML = values;
    }

}