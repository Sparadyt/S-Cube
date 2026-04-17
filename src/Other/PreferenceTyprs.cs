using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace S_Cube;

public record Preference
{
    public string Name {get; set;}
    public string Info{ get; set; }
    public string Type {get; set;}

    [JsonConstructor]
    public Preference(string name, string info)
    {
        Name = name;
        Info = info;
    }
}

public record StringPr : Preference
{
    public string Value;

    public StringPr(string name, string value, string info) : base(name, info)
    {
        Type = "string";
        Value = value;
    }
}

public record IntPr : Preference
{
    public int MinValue;
    public int Value;
    public int? MaxValue;

    public IntPr(string name, int minValue, int value, int? maxValue,  string info) : base(name, info)
    {
        Type = "int";
        MinValue = minValue;
        Value = value;
        MaxValue = maxValue;

        if (string.IsNullOrWhiteSpace(maxValue.ToString()))
            MaxValue = int.MaxValue;
    }
}

public record BoolPr : Preference
{
    public bool Value;

    public BoolPr(string name, bool value, string info) : base(name, info)
    {
        Type = "bool";
        Value = value;
    }
}

public record MultipleChoicePr : Preference
{
    public string ChoicesType;
    public string Value;
    public List<string> Choices;

    public MultipleChoicePr(string name, string type, string value, string info, List<string> choices) : base(name, info)
    {
        Type = "multiple_choice";
        ChoicesType = type;
        Value = value;
        Choices = choices;
    }
}