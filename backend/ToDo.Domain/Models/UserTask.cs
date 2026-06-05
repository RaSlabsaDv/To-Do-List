public class UserTask
{
    public int Id {get; private set;}
    public int UserId {get; private set;}
    public User User { get; private set; } = null!;
    public string Label {get; private set;} = string.Empty;
    public string? Description {get; private set;}
    public DateTimeOffset? Deadline {get; private set;}
    public DateTimeOffset? Reminder {get; private set;}
    public RepeatState RepeatState {get; private set;} = RepeatState.NoRepeat;
    public bool IsCompleted {get; private set;} = false;
    public int? CategoryId {get; private set;}
    public Category? Category {get; private set;}

    private UserTask(){}

    public UserTask
    (
        int userId,
        string label, 
        DateTimeOffset? deadline,
        DateTimeOffset? reminder,
        RepeatState repeatState,
        Category? category = null,
        string? description = null,
        bool isCompleted = false
    )
    {
        UserId = userId;
        SetLabel(label);
        SetDeadline(deadline);
        SetReminder(reminder);
        SetRepeatState(repeatState);
        SetCategory(category);
        SetDescription(description);
        IsCompleted = isCompleted;
    }

    public void SetLabel(string label) =>
        Label = Validator.RequiredString(label, nameof(Label));

    public void SetDescription(string? description)
    {
        Description = description != null 
            ? Validator.RequiredString(description, nameof(Description))
            : null;
    }

    public void SetDeadline(DateTimeOffset? deadline)
    {
        Deadline = deadline;
    }

    public void SetReminder(DateTimeOffset? reminder)
    {
        Reminder = reminder;
    }

    public void SetRepeatState(RepeatState repeatState)
    {
        RepeatState = repeatState;
    }

    public void SetCategory(Category? category)
    {
        Category = category;
        CategoryId = category?.Id;
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}