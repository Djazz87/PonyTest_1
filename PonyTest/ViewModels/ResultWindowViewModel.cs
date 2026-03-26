using CommunityToolkit.Mvvm.ComponentModel;
using PonyTest.DB;

namespace PonyTest.ViewModels;

public partial class ResultWindowViewModel :  ViewModelBase
{
    [ObservableProperty] Result _result;
    [ObservableProperty] string _title;

    public ResultWindowViewModel(Result result, string testTitle)
    {
        Result = result;
        Title = testTitle;
    }
    
}