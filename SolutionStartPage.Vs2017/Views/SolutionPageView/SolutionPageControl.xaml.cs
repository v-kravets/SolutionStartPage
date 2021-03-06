﻿using SolutionStartPage.Shared.Helpers;

namespace SolutionStartPage.Vs2017.Views.SolutionPageView
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;
    using Shared.Models;
    using Shared.Views;
    using Shared.Views.SolutionPageView;
    using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

    /// <summary>
    /// Interaction logic for SolutionPageControl.xaml
    /// </summary>
    public partial class SolutionPageControl : ISolutionPageView
    {
        /////////////////////////////////////////////////////////

        #region Constructors

        public SolutionPageControl()
        {
            InitializeComponent();
        }

        #endregion

        /////////////////////////////////////////////////////////

        #region Event Handler

        private void AlterPage_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            AlterPageCanExecute?.Invoke(sender, e);
        }

        private void AlterPage_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            AlterPageExecuted?.Invoke(sender, e);
        }

        #endregion

        /////////////////////////////////////////////////////////

        #region ISolutionPageView Member

        public event EventHandler<CanExecuteRoutedEventArgs> AlterPageCanExecute;
        public event EventHandler<ExecutedRoutedEventArgs> AlterPageExecuted;

        void IView<ISolutionPageViewModel>.ConnectDataSource(ISolutionPageViewModel viewModel)
        {
            DataContext = viewModel;
        }

        string ISolutionPageView.BrowseBulkAddRootFolder()
        {
            string selectedPath = null;
            var fsd = new FolderSelectDialog
                      {
                          Title = @"Browse for a root folder...",
                          InitialDirectory = Environment.SpecialFolder.Desktop.ToString()
                      };
            if (fsd.ShowDialog(IntPtr.Zero))
                selectedPath = fsd.SelectedFolderFullPath;

            return selectedPath;
        }

        string ISolutionPageView.BrowseSolution(SolutionGroup solutionGroup)
        {
            var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".sln",
                Filter = @"Solution (*.sln)|*.sln|" +
                         @"All files (*.*)|*.*",
                AddExtension = true,
                Multiselect = false,
                ValidateNames = true,
                Title = @"Browse for solution or other file..."
            };

            return ofd.ShowDialog() == true
                ? ofd.FileName
                : null;
        }

        #endregion
    }
}