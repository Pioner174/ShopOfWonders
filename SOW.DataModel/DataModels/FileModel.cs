namespace SOW.DataModels;

public class FileModel
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; } 

    public long Size { get; set; }

    public string Path { get; set; }

    public long AuthorId { get; set; }

    public User Author { get; set; }
}
