using System;
using System.IO;
using System.Collections.Generic;

public static class Saving
{
    static string? localProjectPath;
    static string? roamingProjectPath;

    public static UpdateValue()
    {
        localProjectpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        roamingProjectpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //vgg
    }
}