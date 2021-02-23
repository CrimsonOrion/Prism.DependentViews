using Prism.Commands;
using Prism.Mvvm;

using System;

namespace Prism.DependentViews.Module.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _title = "View A";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DelegateCommand UpdateCommand { get; private set; }

        public ViewAViewModel() => UpdateCommand = new DelegateCommand(UpdateTitle);

        private void UpdateTitle() => Title = $"Updated {DateTime.Now}";
    }
}