using EnvDTE;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Text;
using System.Reflection;
using System.Security.Policy;

namespace GDToolTips
{
    [Command(PackageIds.Signature)]
    internal sealed class Signature : BaseCommand<Signature>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {

            string user = GDToolTips.Vsix.Author;

            string message = "/*==============================\n" + 
                             " Made By " + user  + "\n" + 
                             " " + DateTime.Now.Year.ToString() + "\n" + 
                             " All rights reserved\n"+
                             "==============================*/" + "";

            await Package.JoinableTaskFactory.SwitchToMainThreadAsync();
            DocumentView docView = await VS.Documents.GetActiveDocumentViewAsync();
            if (docView?.TextView == null) return;
            SnapshotPoint position = docView.TextView.Caret.Position.BufferPosition;
            docView.TextBuffer?.Insert(position, message);
        }
    }
}
