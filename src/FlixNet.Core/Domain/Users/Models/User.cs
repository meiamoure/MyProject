using FlixNet.Core.Domain.Users.Data;

namespace FlixNet.Core.Domain.Users.Models;

public class User
{
    private User() { }

    public User(Guid id, string name, string email, string pictureUrl)
    {
        Id = id;
        Name = name;
        Email = email;
        PictureUrl = pictureUrl;
    }

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string PictureUrl { get; private set; } = string.Empty;

    public static User Create(string name, string email, string pictureUrl)
    {
        return new User(Guid.NewGuid(), name, email, pictureUrl);
    }

    public void UpdateProfile(UpdateUserData data)
    {
        Name = data.Name;
        PictureUrl = data.PictureUrl;
    }
}

