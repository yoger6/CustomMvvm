using System.Windows.Forms;

namespace CustomMvvm.Utilities
{
    public interface IFolderBrowserDialog
    {
        DialogResult ShowDialog();
        string SelectedPath { get; }
    }
}