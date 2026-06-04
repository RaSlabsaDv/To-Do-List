public record UpdateUserTaskDto
(
    string? Label, 
    string? Description,
    DateTimeOffset? Deadline,
    DateTimeOffset? Reminder,
    RepeatState? RepeatState,
    int? CategoryId
);