using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.ComponentModel;
using DungeonAssistant.Business.Commons.Models;

namespace DungeonAssistant.Business.Commons.ViewModels;

public partial class MessageWindowViewModel : ObservableObject
{
    [ObservableProperty] private string title;
    [ObservableProperty] private string message;
    [ObservableProperty] private bool isTrueFalseVisible;
    [ObservableProperty] private bool isOkVisible;
 
    public MessageWindowViewModel(MessageWindowConfiguration config)
    {
        Title = config.Title;
        Message = config.Message;
        IsTrueFalseVisible = config.IsTrueFalseVisible;
        IsOkVisible = config.IsOkVisible;
        Token = config.Token;
    }

    [RelayCommand]    
    private void Ok()
    {
        Close();
    }

    [RelayCommand]
    private void True()
    {
        // Send true message
        //Messenger.Default.Send(new MessageWindowResponse 
        //{
        //    Response = true,
        //    Token = Token
        //});
        
        //BAH - 2021-09-14 - Changed to use the new Register method
        //Messenger.Register<MessageWindowViewModel, MessageWindowResponse>(this, static (r, m) => r.Response = true);

        Close();
    }

    [RelayCommand]
    private void False()
    {
        // Send false message
        //Messenger.Default.Send(new MessageWindowResponse
        //{
        //    Response = false,
        //    Token = Token
        //});
        Close();
    }

    private void Close()
    {
        //Messenger.Default.Send(new CloseWindowMessage(this));            
    }

    public object Token { get; set; }
}
