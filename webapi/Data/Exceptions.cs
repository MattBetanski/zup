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

public class ObjectNameInUseException : Exception {
    public ObjectNameInUseException() { }
    public ObjectNameInUseException(string message) : base(message) { }
}

public class UserAlreadyInGroupException : Exception {
    public UserAlreadyInGroupException() { }
    public UserAlreadyInGroupException(string message) : base(message) { }
}

public class InviteExpiredException : Exception {
    public InviteExpiredException() { }
    public InviteExpiredException(string message) : base(message) { }
}


