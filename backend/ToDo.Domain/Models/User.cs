public class User
{
    public int Id {get; private set;}
    public string Name {get; private set;} = string.Empty;
    public string PasswordHash {get; private set;} = string.Empty;
    public string Email {get; private set;} = string.Empty;
    
    private List<UserTask> _userTasks = new ();
    public IReadOnlyCollection<UserTask> Tasks => _userTasks;

    private List<Category> _categories = new ();
    public IReadOnlyCollection<Category> Categories => _categories;

    private User (){}

    public User(string name, string passwordHash, string email)
    {
        Name = Validator.RequiredString(name, nameof(Name));
        PasswordHash = Validator.RequiredString(passwordHash, nameof(PasswordHash));
        Email = Validator.RequiredString(email, nameof(Email));
    }

    public void AddTask(UserTask userTask)
    {
        if (userTask == null)
            throw new Exception($"Cannot add empty task!");
        
        _userTasks.Add(userTask);
    }

    public void RemoveTask(int taskId)
    {
        if (!_userTasks.Any())
            throw new Exception($"Cannot remove from empty list!");

        var userTask = _userTasks.FirstOrDefault(t => t.Id == taskId);

        if (userTask == null)
            throw new Exception($"Cannot delete task with id {taskId} do not exist!");

        _userTasks.Remove(userTask);
    }
}