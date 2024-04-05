using CommunityToolkit.Mvvm.ComponentModel;

namespace DungeonAssistant.Business.Commons.Models;

public class CloseWindowMessage
{
    public CloseWindowMessage(ObservableObject dataContext)
    {
        DataContext = dataContext;
    }

    public ObservableObject DataContext { get; set; }
}
