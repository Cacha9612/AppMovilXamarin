using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class StringToListConverter : JsonConverter
{
	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(List<string>);
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		// Si el valor es un token de tipo string, lo convierte en una lista de un solo elemento
		if (reader.TokenType == JsonToken.String)
		{
			return new List<string> { (string)reader.Value };
		}
		// Si es un array, lo deserializa como lista de cadenas normalmente
		return serializer.Deserialize<List<string>>(reader);
	}

	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		serializer.Serialize(writer, value);
	}
}
