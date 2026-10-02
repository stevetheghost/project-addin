# MS Project Importer add-in

A VSTO add-in (C#, .NET Framework, Windows only). It adds an **Importer** ribbon tab with Dev, Test and Production buttons. Each button opens an import dialog.

## Setup (Windows, Visual Studio with the "Office/SharePoint development" workload)

1. Create a new project from the **Project VSTO Add-in** template. Name it `ProjectImporter`.
2. Replace the template's `ThisAddIn.cs` with the one in `ProjectImporter/`.
3. Add `Ribbon.cs`, `ImportDialog.cs` and `Importer.cs` to the project.
4. Make sure the project references `System.Windows.Forms`. The template already includes the Office and MSProject interop references.
5. Press F5. MS Project starts with the **Importer** tab.

## Your code

Fill in `Importer.Import(...)` in `Importer.cs`. It receives the environment, whether it's a template, the calendar name and the project ID. Throw an exception to show an error to the user and keep the dialog open.

## Showing the Dev and Test buttons

Dev and Test are on their own tabs ("Importer (Dev)" and "Importer (Test)"), hidden by default. To show them, go to File > Options > Customize Ribbon and tick the tab under Main Tabs. Untick it to hide it again.

If those tabs don't appear in the Customize Ribbon list, remove `visible='false'` from them in `Ribbon.cs`. They will then show by default, and you can untick them once.
# project-addin
