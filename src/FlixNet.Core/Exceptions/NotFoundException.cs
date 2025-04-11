namespace FlixNet.Core.Exceptions;

public class NotFoundException(string message) : DomainException(message);
