public class Category
{
    public int Id {get; private set;}
    public string Name {get; private set;} = string.Empty;

    public int UserId {get; private set;}
    public User User {get; private set;} = null!;

    private Category(){}

    public Category(string name, int userId)
    {
        SetName(name);
        UserId = userId; 
    }

    public void SetName(string name)
    {
        Name = Validator.RequiredString(name, nameof(Name));
    }
}