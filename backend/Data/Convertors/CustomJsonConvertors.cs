using Newtonsoft.Json;
using NetTopologySuite.Geometries;

public class GeometryJsonConverter : JsonConverter<Geometry>
{
    public override void WriteJson(JsonWriter writer, Geometry value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        // Serialize geometry as GeoJSON (or WKT string)
        //var geoJson = value.AsText(); // WKT format - or use GeoJSON serializer if you prefer
        writer.WriteValue(value.AsText());
    }
        public override Geometry ReadJson(JsonReader reader, Type objectType, Geometry existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // You can implement this if needed
        throw new NotImplementedException("Deserialization not supported.");
    }
}

    // public override Geometry ReadJson(JsonReader reader, Type objectType, Geometry existingValue, bool hasExistingValue, JsonSerializer serializer)
    // {
    //     // Implement if you need deserialization from WKT or GeoJSON string back to Geometry
    //     throw new NotImplementedException();
    // 


