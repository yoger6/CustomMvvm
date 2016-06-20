using System;
using System.Windows.Forms;

namespace CustomMvvm.Utilities
{
    public sealed class WindowsFolderBrowserDialog : IFolderBrowserDialog, IDisposable
    {
        private FolderBrowserDialog _dialog;

        public string SelectedPath => _dialog.SelectedPath;

        public WindowsFolderBrowserDialog()
        {
            _dialog = new FolderBrowserDialog();
        }

        public DialogResult ShowDialog()
        {
            return _dialog.ShowDialog();
        }

        public void Dispose()
        {
            _dialog.Dispose();
            _dialog = null;
        }
    }
}