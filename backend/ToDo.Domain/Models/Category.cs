public class Category
{
    public int Id {get; private set;}
    public string Name {get; private set;} = string.Empty;

    public int UserId {get; private set;}
    public User User {get; private set;} = null!;
}