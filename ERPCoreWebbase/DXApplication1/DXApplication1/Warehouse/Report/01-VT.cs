using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit;

namespace WebModule.Warehouse.Report
{
    public partial class _01_VT : DevExpress.XtraReports.UI.XtraReport
    {
        private RichEditDocumentServer richEditDocumentServer;
        public _01_VT()
        {
            InitializeComponent();
        }

        //private void lbhotenShipper_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //if (!this OverrideRtfFormatting)
        //    //    return;

        //    richEditDocumentServer.RtfText = GetCurrentColumnValue("RtfContent").ToString();
        //    ApplyRTFModification(richEditDocumentServer);
        //    xrRichText1.Rtf = richEditDocumentServer.RtfText;
        //}

        //private void ApplyRTFModification(DevExpress.XtraRichEdit.RichEditDocumentServer server) {
        //    // Apply default formatting
        //    server.Document.DefaultCharacterProperties.FontName = "Arial";
        //    server.Document.DefaultCharacterProperties.FontSize = 9;
        //    server.Document.DefaultCharacterProperties.ForeColor = Color.FromArgb(120, 120, 120);
        //    server.Document.DefaultParagraphProperties.Alignment = ParagraphAlignment.Center;

        //    // Remove whitespaces from the end of RTF content
        //    DocumentRange[] dots = server.Document.FindAll(".", SearchOptions.None);
        //    DocumentPosition lastDot = dots[dots.Length - 1].End;
        //    server.Document.Delete(server.Document.CreateRange(lastDot, server.Document.Range.End.ToInt() - lastDot.ToInt()));
            
        //    // Append formatted word
        //    DocumentRange range = server.Document.InsertText(server.Document.Range.End, " [Approved]");
        //    CharacterProperties cp = server.Document.BeginUpdateCharacters(range);
        //    cp.FontName = "Courier New";
        //    cp.FontSize = 10;
        //    cp.ForeColor = Color.Red;
        //    cp.Underline = UnderlineType.Single;
        //    cp.UnderlineColor = Color.Red;
        //    server.Document.EndUpdateCharacters(cp);
        //}

    }
}
