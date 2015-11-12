using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace rvvrb3.Models
{
    [JsonConverter(typeof(DataTableConverter))]

    public class RvvrbDataTable : IXmlSerializable
    {
        
        public RvvrbDataTable(DataTable dataTable)
        {
            this.Data = dataTable;
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (DataRow row in Data.Rows)
            {
                writer.WriteStartElement(Data.TableName);
                foreach (DataColumn column in row.Table.Columns)
                {
                    writer.WriteElementString(column.ColumnName, row[column].ToString());
                }
                writer.WriteEndElement();
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public DataTable Data { get; set; }
    }

    public class DataTableConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return typeof(RvvrbDataTable).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            RvvrbDataTable myDataTable = value as RvvrbDataTable;
            DataTable dt = myDataTable.Data;

            writer.WriteStartObject();
            writer.WritePropertyName("Data");
            writer.WriteStartArray();


            foreach (DataRow row in dt.Rows)
            {
                writer.WriteStartObject();
                foreach (DataColumn column in row.Table.Columns)
                {
                    writer.WritePropertyName(column.ColumnName);
                    serializer.Serialize(writer, row[column]);
                }
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}