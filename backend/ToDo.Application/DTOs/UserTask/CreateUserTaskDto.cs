public record CreateUserTaskDto
(
    string Label, 
    string? Description,
    DateTimeOffset? Deadline,
    DateTimeOffset? Reminder,
    RepeatState RepeatState,
    int? CategoryId
);