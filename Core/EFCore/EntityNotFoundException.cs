using System;

namespace Core.EFCore;

public class EntityNotFoundException(
    string message
) : Exception(message);