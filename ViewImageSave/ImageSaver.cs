// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* ImageSaver.cs
 * https://revit-addins.blogspot.ru
 * © Inpad, 2021
 *
 * The external command.
 */
#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using InpadPlugins.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using WPF = System.Windows;

#endregion

namespace InpadPlugins
{
    /// <summary>
    /// Revit external command.
    /// </summary>	
	[Transaction(TransactionMode.Manual)]
    public sealed partial class ImageSaver : IExternalCommand
    {

        /// <summary>
        /// This method implements the external command within 
        /// Revit.
        /// </summary>
        /// <param name="commandData">An ExternalCommandData 
        /// object which contains reference to Application and 
        /// View needed by external command.</param>
        /// <param name="message">Error message can be returned
        /// by external command. This will be displayed only if
        /// the command status was "Failed". There is a limit 
        /// of 1023 characters for this message; strings longer
        /// than this will be truncated.</param>
        /// <param name="elements">Element set indicating 
        /// problem elements to display in the failure dialog. 
        /// This will be used only if the command status was 
        /// "Failed".</param>
        /// <returns>The result indicates if the execution 
        /// fails, succeeds, or was canceled by user. If it 
        /// does not succeed, Revit will undo any changes made 
        /// by the external command.</returns>    
        Result IExternalCommand.Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            Result result = Result.Failed;

            try
            {

                UIApplication ui_app = commandData?.Application;
                UIDocument ui_doc = ui_app?.ActiveUIDocument;
                Application app = ui_app?.Application;
                Document doc = ui_doc?.Document;

                /* Wrap all transactions into the transaction 
                 * group. At first we get the transaction group
                 * localized name. */
                var tr_gr_name = "image_saver";

                using (var tr_gr = new TransactionGroup(doc, tr_gr_name))
                {

                    if (TransactionStatus.Started == tr_gr.Start())
                    {

                        /* Here do your work or the set of 
                         * works... */
                        if (DoWork(commandData, ref message, elements))
                        {

                            if (TransactionStatus.Committed == tr_gr.Assimilate())
                            {

                                result = Result.Succeeded;
                            }
                        }
                        else
                        {

                            tr_gr.RollBack();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                TaskDialog.Show("Error", ex.Message);

                result = Result.Failed;
            }

            return result;
        }
    }
}
