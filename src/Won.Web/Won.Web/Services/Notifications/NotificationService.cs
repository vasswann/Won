namespace Won.Web.Services.Notifications;

public class NotificationService
{
    public event Action? OnChange;

    public List<NotificationMessage> Notifications { get; } = [];

    public void ShowSuccess(string message) => Show(message, NotificationType.Success);

    public void ShowError(string message) => Show(message, NotificationType.Error);

    public void ShowInfo(string message) => Show(message, NotificationType.Info);

    public void ShowWarning(string message) => Show(message, NotificationType.Warning);

    public void Remove(Guid id)
    {
        var notification = Notifications.FirstOrDefault(x => x.Id == id);

        if (notification is null)
        {
            return;
        }

        Notifications.Remove(notification);
        NotifyStateChanged();
    }

    private void Show(string message, NotificationType type)
    {
        var notification = new NotificationMessage
        {
            Id = Guid.NewGuid(),
            Message = message,
            Type = type
        };

        Notifications.Add(notification);
        NotifyStateChanged();

        _ = AutoRemoveAsync(notification.Id);
    }

    private async Task AutoRemoveAsync(Guid id)
    {
        await Task.Delay(7000);
        Remove(id);
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}