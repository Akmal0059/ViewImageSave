// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* ViewImageSave
 * ExternalApplication.cs
 * https://inpad.ru
 * © Inpad, 2021
 *
 * The external application. This is the entry point of the
 * 'ViewImageSave' (Revit add-in).
 */
#region Namespaces
using Autodesk.Revit.UI;
using InpadPlugins.ViewImageSave.Properties;
using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace InpadPlugins
{
    /// <summary>
    /// Revit external application.
    /// </summary>  
    public sealed partial class ExternalApplication
        : IExternalApplication
    {
        public static readonly string PATH_LOCACTION = Assembly.GetExecutingAssembly().Location;

        /// <summary>
        /// This method will be executed when Autodesk Revit 
        /// will be started.
        /// 
        /// WARNING
        /// Don't use the RevitDevTools.dll features directly 
        /// in this method. You are to call other methods which
        /// do it instead of.
        /// </summary>
        /// <param name="uic_app">A handle to the application 
        /// being started.</param>
        /// <returns>Indicates if the external application 
        /// completes its work successfully.</returns>
        Result IExternalApplication.OnStartup(
            UIControlledApplication uic_app)
        {

            Result result = Result.Succeeded;

            try
            {
                Initalize(uic_app);

                // TODO: put your code here.
            }
            catch (Exception ex)
            {

                TaskDialog.Show("Error", ex.Message);

                result = Result.Failed;
            }

            return result;
        }

        /// <summary>
        /// This method will be executed when Autodesk Revit 
        /// shuts down.</summary>
        /// <param name="uic_app">A handle to the application 
        /// being shut down.</param>
        /// <returns>Indicates if the external application 
        /// completes its work successfully.</returns>
        Result IExternalApplication.OnShutdown(
            UIControlledApplication uic_app)
        {

            ResourceManager res_mng = new ResourceManager(
                  GetType());
            ResourceManager def_res_mng = new ResourceManager(
                typeof(ViewImageSave.Properties.Resources));

            Result result = Result.Succeeded;

            try
            {

            }
            catch (Exception ex)
            {

                TaskDialog.Show(def_res_mng.GetString("_Error")
                    , ex.Message);

                result = Result.Failed;
            }
            finally
            {

                res_mng.ReleaseAllResources();
                def_res_mng.ReleaseAllResources();
            }

            return result;
        }

        /// <summary>
        /// This method creates a button in the Autodesk Revit UI
        /// </summary>
        private void Initalize(UIControlledApplication application)
        {
            // Create tabs, panels, and buttons
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("ViewExporter");

            PushButtonData pushButtonData1 = new PushButtonData("ViewExporter", "ViewExporter", PATH_LOCACTION, "InpadPlugins.ImageSaver");
            pushButtonData1.AvailabilityClassName = "InpadPlugins.ExternalCommandAvailability";

            RibbonItem item = ribbonPanel.AddItem(pushButtonData1);
            PushButton optionsBtn = item as PushButton;

            optionsBtn.LargeImage = ImageSourceFromBitmap(Resources.imageIcon);
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
