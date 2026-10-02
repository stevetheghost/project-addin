using System;

namespace ProjectImporter
{
    public static class Importer
    {
        // Called when the user clicks Import and all fields are valid.
        // calendar is one of: "5 Day Calendar", "7 Day Calendar", "24 Hour Calendar".
        // The running MS Project instance is Globals.ThisAddIn.Application.
        // Throw an exception to show an error message to the user and keep the dialog open.
        public static void Import(TargetEnvironment environment, bool isTemplate, string calendar, string projectId)
        {
            throw new NotImplementedException("Importer.Import has not been implemented yet.");
        }
    }
}
