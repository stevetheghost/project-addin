using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;

namespace ProjectImporter
{
    public enum TargetEnvironment { Dev, Test, Prod }

    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        // Dev and Test live on their own tabs, hidden by default (visible='false').
        // Show them via File > Options > Customize Ribbon by ticking the tab.
        private const string RibbonXml = @"
<customUI xmlns='http://schemas.microsoft.com/office/2009/07/customui'>
  <ribbon>
    <tabs>
      <tab id='tabProd' label='Importer'>
        <group id='grpProd' label='Import From'>
          <button id='btnProd' label='Production' size='large' imageMso='FileOpen'
                  onAction='OnProdClick' />
        </group>
      </tab>
      <tab id='tabTest' label='Importer (Test)' visible='false'>
        <group id='grpTest' label='Import From'>
          <button id='btnTest' label='Test' size='large' imageMso='FileOpen'
                  onAction='OnTestClick' />
        </group>
      </tab>
      <tab id='tabDev' label='Importer (Dev)' visible='false'>
        <group id='grpDev' label='Import From'>
          <button id='btnDev' label='Dev' size='large' imageMso='FileOpen'
                  onAction='OnDevClick' />
        </group>
      </tab>
    </tabs>
  </ribbon>
</customUI>";

        public string GetCustomUI(string ribbonID)
        {
            return RibbonXml;
        }

        public void OnDevClick(Office.IRibbonControl control)
        {
            ShowImportDialog(TargetEnvironment.Dev);
        }

        public void OnTestClick(Office.IRibbonControl control)
        {
            ShowImportDialog(TargetEnvironment.Test);
        }

        public void OnProdClick(Office.IRibbonControl control)
        {
            ShowImportDialog(TargetEnvironment.Prod);
        }

        private static void ShowImportDialog(TargetEnvironment environment)
        {
            using (var dialog = new ImportDialog(environment))
            {
                dialog.ShowDialog();
            }
        }
    }
}
