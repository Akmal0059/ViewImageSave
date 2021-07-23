// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

/* ViewImageSave
 * ExternalCommandAvailability.cs
 * https://inpad.ru
 * © Inpad, 2021
 *
 * The 'ExternalCommand' command availability.
 */
#region Namespaces
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

#endregion

namespace InpadPlugins
{
    /// <summary>
    /// This class provides an accessibility checking for an 
    /// external command of Revit.
    /// </summary>	
	public sealed partial class ExternalCommandAvailability
        : IExternalCommandAvailability
    {

        /// <summary>
        /// This method provides the control over whether an 
        /// external command is enabled or disabled.
        /// </summary>
        /// <param name="applicationData">An 
        /// ApplicationServices.Application object which 
        /// contains reference to Application needed by 
        /// external command.</param>
        /// <param name="selectedCategories">An list of 
        /// categories of the elements which have been selected
        /// in Revit in the active document, or an empty set if
        /// no elements are selected or there is no active 
        /// document.</param>
        /// <returns>Indicates whether Revit should enable or 
        /// disable the corresponding external command.
        /// </returns>
        bool IExternalCommandAvailability.IsCommandAvailable(
            UIApplication applicationData,
            CategorySet selectedCategories)
        {
            bool result = false;

            try
            {

                // ============================================
                // TODO: delete these code rows and put your code 
                // here.
                if (applicationData.ActiveUIDocument != null &&
                selectedCategories.IsEmpty)
                {

                    result = true;
                }
                // ============================================
            }
            catch (Exception ex)
            {

                TaskDialog.Show("Error", ex.Message);

                result = false;
            }

            return result;
        }
    }
}
