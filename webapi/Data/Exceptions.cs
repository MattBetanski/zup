using System;

public class CredentialsInUseException : Exception {
    public CredentialsInUseException() { }
    public CredentialsInUseException(string message) : base(message) { }
}

public class InvalidCredentialsException : Exception {
    public InvalidCredentialsException() { }
    public InvalidCredentialsException(string message) : base(message) { }
}



