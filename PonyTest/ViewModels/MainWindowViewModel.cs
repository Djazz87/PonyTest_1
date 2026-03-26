using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using PonyTest.DB;
using PonyTest.Views;

namespace PonyTest.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private readonly IServiceProvider _provider;
    
    [ObservableProperty] string username;
    [ObservableProperty] List<TestData> testData;
    [ObservableProperty] TestData selectedTestData;
    
    public MainWindowViewModel(IServiceProvider provider, TestRepository repository)
    {
        _provider = provider;
        TestData = repository.GetAll();
    }

    [RelayCommand]
    public void StartTest()
    {
        if (SelectedTestData == null)
        {
            return;
        }
        var vm = ActivatorUtilities.CreateInstance<TestWindowViewModel>(_provider, SelectedTestData);
        vm.UserName = username;
        var win =  _provider.GetService<TestWindow>();
        win.DataContext = vm;
        win.Show();
    }
}