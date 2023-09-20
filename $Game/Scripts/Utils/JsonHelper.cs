using System;
using Godot;
using Newtonsoft.Json;

public static class JsonHelper
{
    public static T DeserializeObjectFromPath<T> (string path)
    {
        Json jsonObject = GD.Load<Json>(path);

        if (jsonObject == null)
        {
            throw new NullReferenceException(
                $"Json object in the specified path is null! Path: {path}"
            );
        }
        string rawObject = jsonObject.Data.ToString();

        if (string.IsNullOrEmpty(rawObject))
        {
            throw new NullReferenceException(
                $"Json object data in the specified path is null or empty! Path: {path}"
            );
        }
        
        return JsonConvert.DeserializeObject<T>(rawObject);
    }
}