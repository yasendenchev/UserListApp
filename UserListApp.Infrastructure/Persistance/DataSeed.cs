using UserListApp.Domain.Entities;

namespace UserListApp.Infrastructure.Persistance;

public static class DataSeed
{
    public static List<User> Users { get; set; } = new List<User>()
    {
        new User{ Id = 1, Name = "Alice", Email = "alice@example.com", PhoneNumber = "123-456-7890" },
        new User{ Id = 2, Name = "Bob", Email = "bob@example.com", PhoneNumber = "234-567-8901" },
        new User{ Id = 3, Name = "Charlie", Email = "charlie@example.com", PhoneNumber = "345-678-9012" },
        new User{ Id = 4, Name = "David", Email = "david@example.com", PhoneNumber = "456-789-0123" },
        new User{ Id = 5, Name = "Eva", Email = "eva@example.com", PhoneNumber = "567-890-1234" },
        new User{ Id = 6, Name = "Frank", Email = "frank@example.com", PhoneNumber = "678-901-2345" },
        new User{ Id = 7, Name = "Grace", Email = "grace@example.com", PhoneNumber = "789-012-3456" },
        new User{ Id = 8, Name = "Hank", Email = "hank@example.com", PhoneNumber = "890-123-4567" },
        new User{ Id = 9, Name = "Ivy", Email = "ivy@example.com", PhoneNumber = "901-234-5678" },
        new User{ Id = 10, Name = "Jack", Email = "jack@example.com", PhoneNumber = "012-345-6789" },
        new User{ Id = 11, Name = "Kara", Email = "kara@example.com", PhoneNumber = "123-456-7891" },
        new User{ Id = 12, Name = "Leo", Email = "leo@example.com", PhoneNumber = "234-567-8902" }
    };
}
