using System;

public class CredentialsInUseException : Exception {
    public CredentialsInUseException() { }
    public CredentialsInUseException(string message) : base(message) { }
}

public class InvalidCredentialsException : Exception {
    public InvalidCredentialsException() { }
    public InvalidCredentialsException(string message) : base(message) { }
}

public class AccessNotAllowedException : Exception {
    public AccessNotAllowedException() { }
    public AccessNotAllowedException(string message) : base(message) { }
}

public class DataNotFoundException : Exception {
    public DataNotFoundException() { }
    public DataNotFoundException(string message) : base(message) { }
}

public class ProjectNameInUseException : Exception {
    public ProjectNameInUseException() { }
    public ProjectNameInUseException(string message) : base(message) { }
}


