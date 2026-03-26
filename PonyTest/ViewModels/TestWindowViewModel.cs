using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PonyTest.DB;

namespace PonyTest.ViewModels;

public partial class TestWindowViewModel : ViewModelBase
{
    private readonly TestData _test;
    [ObservableProperty] private string _userName;
    [ObservableProperty] List<Question> _questions;
    [ObservableProperty] string _title;
    [ObservableProperty] Question _selectedQuestion;
    
    Dictionary<Question, char?> answers = new();

    [ObservableProperty] private bool? selectA;
    [ObservableProperty] private bool? selectB;
    [ObservableProperty] private bool? selectC;
    
    [ObservableProperty] private bool _allAnswersDone;

    void CheckAllAnswersDone()
    {
        
       AllAnswersDone = !answers.ContainsValue(null);
    }

    partial void OnSelectAChanged(bool? value)
    {
        if (value == true)
        {
            answers[SelectedQuestion] = 'A';
            CheckAllAnswersDone();
        }

    }
    partial void OnSelectBChanged(bool? value)
    {
        if (value == true)
        {
            answers[SelectedQuestion] = 'B';
            CheckAllAnswersDone();
        }
        
    }
    partial void OnSelectCChanged(bool? value)
    {
        if (value == true)
        {
            answers[SelectedQuestion] = 'C';
            CheckAllAnswersDone();
        }
    }

    partial void OnSelectedQuestionChanged(Question value)
    {
        switch (answers[SelectedQuestion])
        {
            case null:
                SelectA = null;
                SelectB = null;
                SelectC = null;
                break;
            case 'A':
                SelectA = true;
                break;
            case 'B':
                SelectB = true;
                break;
            case 'C':
                SelectC = true;
                break;
        }
    }
    
    
    public TestWindowViewModel(IServiceProvider serviceProvider, 
        QuestionRepository questionRepository,
        TestData test)
    {
        _test = test;
        Questions = questionRepository.GetQuestionsByTest(test);
        Title = test.Title;
        Questions.ForEach(s => answers.Add(s, null));
    }

    [RelayCommand]
    public void Previous()
    {
        if (SelectedQuestion == null)
            SelectedQuestion = Questions.First();
        else
        {
            int index = Questions.IndexOf(SelectedQuestion) - 1;
            if (index < 0)
                index = 0;
            SelectedQuestion = Questions[index];
        }
    }

    [RelayCommand]
    public void Next()
    {
        if (SelectedQuestion == null)
            SelectedQuestion = Questions.First();
        else
        {
            int index = Questions.IndexOf(SelectedQuestion) + 1;
            if (index > Questions.Count - 1)
                index = Questions.Count -1;
            SelectedQuestion = Questions[index];
        }
    }

    [RelayCommand]
    public void EndTest()
    {
        Result result = CalculateResult();
        
    }

    private Result CalculateResult()
    {
        Result result = new Result();
        result.Date = DateTime.Now;
        result.UserName = UserName;
        result.TestId = _test.Id;
        double right = 100.0 / Questions.Count;
        foreach (var q in Questions)
        {
            if (answers[q] == q.CorectOption)
                result.Score += right;
        }
        result.Grade = result.Score switch
        {
            >= 90 => "5", 
            >= 70 => "4",
            >= 50 => "3",
            _ => "2" 
        };
        return result;
    }
}