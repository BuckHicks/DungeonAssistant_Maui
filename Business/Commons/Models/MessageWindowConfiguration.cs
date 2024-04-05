namespace DungeonAssistant.Business.Commons.Models;

public class MessageWindowConfiguration
{
    public MessageWindowConfiguration()
    {

    }

    public MessageWindowConfiguration(string message)
    {
        Message = message;
        Title = string.Empty;
        IsOkVisible = true;
        IsTrueFalseVisible = false;
    }

    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsTrueFalseVisible { get; set; } = false;
    public bool IsOkVisible { get; set; } = true;
    public object Token { get; set; }
}
