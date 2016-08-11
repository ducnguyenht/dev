using DevExpress.XtraReports.UI;

namespace WebDemo1.Module.BusinessObjects
{
    public static class InTrucTiepGridControl
    {
        
        private static DevExpress.XtraReports.UI.WinControlContainer CopyGridControl(DevExpress.XtraGrid.GridControl grid)
        {
            DevExpress.XtraReports.UI.WinControlContainer winContainer = new DevExpress.XtraReports.UI.WinControlContainer();

            winContainer.Location = new System.Drawing.Point(0, 0);
            winContainer.Size = new System.Drawing.Size(200, 100);

            winContainer.WinControl = grid;
            return winContainer;
        }

        public static void XemVaIn(this DevExpress.XtraGrid.GridControl grid, DevExpress.XtraReports.UI.XtraReport rpt, System.Drawing.Printing.PaperKind PageKind, bool Landscape)
        {
            #region Thiết kế trước khi in

            if (grid != null)
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = grid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

                if (view != null)
                {
                    view.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.EvenRow.Options.UseFont = true;
                    view.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.FilterPanel.Options.UseFont = true;
                    view.AppearancePrint.FooterPanel.BorderColor = System.Drawing.Color.Black;
                    view.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.FooterPanel.Options.UseBorderColor = true;
                    view.AppearancePrint.FooterPanel.Options.UseFont = true;
                    view.AppearancePrint.GroupFooter.BorderColor = System.Drawing.Color.Black;
                    view.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.GroupFooter.Options.UseBorderColor = true;
                    view.AppearancePrint.GroupFooter.Options.UseFont = true;
                    view.AppearancePrint.GroupRow.BorderColor = System.Drawing.Color.Black;
                    view.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
                    view.AppearancePrint.GroupRow.Options.UseBorderColor = true;
                    view.AppearancePrint.GroupRow.Options.UseFont = true;
                    view.AppearancePrint.HeaderPanel.BorderColor = System.Drawing.Color.Black;
                    view.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
                    view.AppearancePrint.HeaderPanel.Options.UseBorderColor = true;
                    view.AppearancePrint.HeaderPanel.Options.UseFont = true;
                    view.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
                    view.AppearancePrint.HeaderPanel.Options.UseBackColor = true;
                    view.AppearancePrint.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
                    view.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    view.AppearancePrint.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    view.AppearancePrint.Lines.BackColor = System.Drawing.Color.Black;
                    view.AppearancePrint.Lines.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.Lines.Options.UseBackColor = true;
                    view.AppearancePrint.Lines.Options.UseFont = true;
                    view.AppearancePrint.OddRow.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.OddRow.Options.UseFont = true;
                    view.AppearancePrint.Preview.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.Preview.Options.UseFont = true;
                    view.AppearancePrint.Row.BorderColor = System.Drawing.Color.Black;
                    view.AppearancePrint.Row.Font = new System.Drawing.Font("Times New Roman", 11F);
                    view.AppearancePrint.Row.Options.UseBorderColor = true;
                    view.AppearancePrint.Row.Options.UseFont = true;

                    view.AppearancePrint.EvenRow.BackColor = System.Drawing.Color.MintCream;
                    view.OptionsPrint.EnableAppearanceEvenRow = true;
                }
            }

            #endregion Thiết kế trước khi in

            rpt.PaperKind = PageKind;
            rpt.Landscape = Landscape;
            rpt.Bands[DevExpress.XtraReports.UI.BandKind.Detail].Controls.Add(CopyGridControl(grid));

            rpt.ShowPreview();
        }
    }
}