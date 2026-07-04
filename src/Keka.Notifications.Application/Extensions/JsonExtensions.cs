// -----------------------------------------------------------------------
// <copyright file="JsonExtensions.cs" company="Keka Inc">
//     Copyright (c) Keka.com. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Keka.Notifications.Application.Extensions;

/// <summary>
/// Represent a Json Extension.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Converts to json.
    /// </summary>
    /// <typeparam name="T">source type.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns>
    /// The Json string.
    /// </returns>
    public static string ToJson<T>(this T source)
        where T : class
    {
        return source == default(T) ? string.Empty : JsonSerializer.Serialize(source);
    }

    /// <summary>
    /// Froms the json.
    /// </summary>
    /// <typeparam name="T">destination type.</typeparam>
    /// <param name="str">The string.</param>
    /// <returns>
    /// The object.
    /// </returns>
    public static T FromJson<T>(this string str)
    {
        return string.IsNullOrWhiteSpace(str) ? default : JsonSerializer.Deserialize<T>(str);
    }

    /// <summary>
    /// Deserializes the json.
    /// </summary>
    /// <param name="message">The string.</param>
    /// <param name="obj">When this method returns, contains the deserialized object of type <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <returns>
    /// Boolean value.
    /// </returns>
    public static bool TryDeserialize<T>(this string message, out T obj)
    {
        obj = default(T);
        try
        {
            obj = message.FromJson<T>();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Flattens a JSON string into a dictionary of key-value pairs.
    /// </summary>
    /// <param name="json">The JSON string to flatten.</param>
    /// <returns>A dictionary where keys are the names of properties and values are the string representations of those values.</returns>
    public static Dictionary<string, object> FlattenJsonToDictionary(this string json)
    {
        var result = new Dictionary<string, object>();
        using (JsonDocument doc = JsonDocument.Parse(json))
        {
            FlattenElement(doc.RootElement, result);
        }

        return result;
    }

    /// <summary>
    /// Recursively processes a <see cref="JsonElement"/> and adds its key-value pairs to the dictionary.
    /// </summary>
    /// <param name="element">The <see cref="JsonElement"/> to process.</param>
    /// <param name="dictionary">The dictionary to which the flattened key-value pairs are added.</param>
    private static void FlattenElement(JsonElement element, Dictionary<string, object> dictionary)
    {
        foreach (var property in element.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.Object)
            {
                // Create a nested dictionary for the object and recursively process it
                var nestedDictionary = new Dictionary<string, object>();
                FlattenElement(property.Value, nestedDictionary);
                dictionary[property.Name] = nestedDictionary;
            }
            else if (property.Value.ValueKind == JsonValueKind.Array)
            {
                // Create a list to store array elements
                var list = new List<object>();
                foreach (var item in property.Value.EnumerateArray())
                {
                    if (item.ValueKind == JsonValueKind.Object)
                    {
                        var nestedDictionary = new Dictionary<string, object>();
                        FlattenElement(item, nestedDictionary);
                        list.Add(nestedDictionary);
                    }
                    else
                    {
                        list.Add(item.ToString());
                    }
                }

                dictionary[property.Name] = list;
            }
            else
            {
                // Add simple key-value pairs to the dictionary
                dictionary[property.Name] = property.Value.ToString();
            }
        }
    }
}
