using Prism.Commands;
using Prism.Mvvm;

using System;

namespace Prism.DependentViews.Module.ViewModels
{
    public class ViewBViewModel : BindableBase
    {
        private string _title = "View B";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DelegateCommand UpdateCommand { get; private set; }

        public ViewBViewModel() => UpdateCommand = new DelegateCommand(UpdateTitle);

        private void UpdateTitle() => Title = $"Updated {DateTime.Now}";
    }
}