using System;

namespace Core.Configuration;

[AttributeUsage(AttributeTargets.Class)]
public class OptionsPathAttribute(
    string optionsPath
) : Attribute
{
    public string OptionsPath { get; } = optionsPath;
}