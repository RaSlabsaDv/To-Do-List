public record UserTaskDto
(
    int Id,
    string Label, 
    string? Description,
    DateTimeOffset? Deadline,
    DateTimeOffset? Reminder,
    RepeatState RepeatState,
    bool IsCompleted,
    CategoryDto? Category
);