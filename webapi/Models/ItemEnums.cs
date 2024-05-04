namespace webapi.Models;

public enum Type {
    Epic,
    Feature,
    Requirement,
    Task,
    Bug
}

public enum State {
    Open,
    InProgess,
    Blocked,
    Completed
}