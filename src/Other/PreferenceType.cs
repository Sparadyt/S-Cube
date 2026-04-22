using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace S_Cube;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(StringPr), typeDiscriminator: "string")]
[JsonDerivedType(typeof(IntPr), typeDiscriminator: "int")]
[JsonDerivedType(typeof(DoublePr), typeDiscriminator: "double")]
[JsonDerivedType(typeof(BoolPr), typeDiscriminator: "bool")]
[JsonDerivedType(typeof(MultipleChoicePr), typeDiscriminator: "multiple_choice")]
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
    public string Value { get; set; }

    public StringPr(string name, string value, string info) : base(name, info)
    {
        Type = "string";
        Value = value;
    }
}

public record IntPr : Preference
{
    public int MinValue { get; set; }
    public int Value {get; set;}
    public int? MaxValue {get; set;}

    public IntPr(string name, int minValue, int value, int? maxValue,  string info) : base(name, info)
    {
        Type = "int";
        MinValue = minValue;
        Value = value;
        MaxValue = maxValue ?? int.MaxValue;
    }
}

public record DoublePr : Preference
{
    public double MinValue {get; set;}
    public double Value {get; set;}
    public double? MaxValue {get; set;}

    public DoublePr(string name, double minValue, double value, double? maxValue,  string info) : base(name, info)
    {
        Type = "double";
        MinValue = minValue;
        Value = value;
        MaxValue = maxValue ?? double.MaxValue;
    }
}

public record BoolPr : Preference
{
    public bool Value {get; set;}

    public BoolPr(string name, bool value, string info) : base(name, info)
    {
        Type = "bool";
        Value = value;
    }
}

public record MultipleChoicePr : Preference
{
    public string ChoicesType {get; set;}
    public string Value {get; set;}
    public List<string> Choices {get; set;}

    public MultipleChoicePr(string name, string type, string value, string info, List<string> choices) : base(name, info)
    {
        Type = "multiple_choice";
        ChoicesType = type;
        Value = value;
        Choices = choices;
    }
}