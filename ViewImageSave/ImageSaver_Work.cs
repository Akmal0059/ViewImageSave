// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* ExternalCommand_Work.cs
 * https://revit-addins.blogspot.ru
 * © Inpad, 2021
 *
 * This file contains the methods which are used by the 
 * command.
 */
#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using InpadPlugins.ViewImageSave.ViewModels;
using InpadPlugins.ViewImageSave.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Application = Autodesk.Revit.ApplicationServices.Application;
using MessageBox = System.Windows.Forms.MessageBox;

#endregion


namespace InpadPlugins
{
    public sealed partial class ImageSaver
    {

        private bool DoWork(ExternalCommandData commandData, ref String message, ElementSet elements)
        {

            if (null == commandData)
            {

                throw new ArgumentNullException(nameof(commandData));
            }

            if (null == message)
            {

                throw new ArgumentNullException(nameof(message));
            }

            if (null == elements)
            {

                throw new ArgumentNullException(nameof(elements));
            }

            UIApplication ui_app = commandData.Application;
            UIDocument ui_doc = ui_app?.ActiveUIDocument;
            Application app = ui_app?.Application;
            Document doc = ui_doc?.Document;

            var tr_name = "image_saver";

            try
            {
                using (var tr = new Transaction(doc, tr_name))
                {

                    if (TransactionStatus.Started == tr.Start())
                    {

                        // ====================================
                        // TODO: delete these code rows and put
                        // your code here.
                        ImageSaverViewModel viewModel = new ImageSaverViewModel();
                        ImageSaverUI imageSaverUI = new ImageSaverUI();
                        imageSaverUI.DataContext = viewModel;
                        if (imageSaverUI.ShowDialog() == false)
                            return false;

                        var views = new FilteredElementCollector(doc)
                                             .OfClass(typeof(ViewDrafting))
                                             .Cast<ViewDrafting>()
                                             .ToList();
                        if (views.Count == 0)
                        {
                            MessageBox.Show("В этом документе нет чертёжных видов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        IList<ElementId> ImageExportList = new List<ElementId>(views.Select(x => x.Id));

                        if (!Directory.Exists(viewModel.Path))
                            Directory.CreateDirectory(viewModel.Path);

                        var BilledeExportOptions = new ImageExportOptions
                        {
                            ZoomType = ZoomFitType.FitToPage,
                            PixelSize = 1024,
                            FilePath = viewModel.Path + "\\img",
                            FitDirection = FitDirectionType.Horizontal,
                            HLRandWFViewsFileType = ImageFileType.JPEGLossless,
                            ImageResolution = ImageResolution.DPI_600,
                            ExportRange = ExportRange.SetOfViews,
                        };
                        BilledeExportOptions.SetViewsAndSheets(ImageExportList);

                        try
                        {
                            doc.ExportImage(BilledeExportOptions);
                        }
                        catch { }
                        // ====================================

                        return TransactionStatus.Committed == tr.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                /* TODO: Handle the exception here if you need 
                 * or throw the exception again if you need. */
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
            }

            return false;
        }
    }
}
