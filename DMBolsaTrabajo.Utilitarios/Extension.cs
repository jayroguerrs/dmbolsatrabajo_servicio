using System.Data;
using System.Reflection;
using System.Xml.Serialization;

namespace DMBolsaTrabajo.Utilitarios.Extension
{
    public static class Extensions
    {
        /// <summary>
        /// Converts a DataTable to T type list.
        /// </summary>
        /// <remarks>
        /// v1.0 - 2023.07.20 - 65500 Edison M. Ramirez
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns>T type list</returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var objFieldNames = (from PropertyInfo aProp in typeof(T).GetProperties(flags)
                                 select new PropertyObjDb
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                                 })
                                 .ToList();

            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new PropertyObjDb
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     })
                                     .ToList();

            var commonFields = objFieldNames.Intersect(dataTblFieldNames, new PropObjDbEqualityComp()).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new T();

                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    if (propertyInfos == null) continue;
                    var value = (dataRow[aField.Name] == DBNull.Value) ? null : dataRow[aField.Name]; //if database field is nullable
                    if(propertyInfos.PropertyType.FullName.Equals("System.Int32"))
                        propertyInfos.SetValue(aTSource, Convert.ToInt32(value), null);
                    else if(propertyInfos.PropertyType.FullName.Equals("System.Int16"))
                        propertyInfos.SetValue(aTSource, Convert.ToInt16(value), null);
                    else if (propertyInfos.PropertyType.FullName.Equals("System.Decimal"))
                        propertyInfos.SetValue(aTSource, Convert.ToDecimal(value), null);
                    else if (propertyInfos.PropertyType.FullName.Equals("System.DateTime"))
                        propertyInfos.SetValue(aTSource, Convert.ToDateTime(value), null);
                    else if (propertyInfos.PropertyType.FullName.Equals("System.String"))
                        propertyInfos.SetValue(aTSource, value, null);
                    else if (propertyInfos.PropertyType.Name.Equals("Nullable`1") &&
                        propertyInfos.PropertyType.GenericTypeArguments[0].FullName.Equals("System.Int32"))
                        propertyInfos.SetValue(aTSource, Convert.ToInt32(value), null);
                    else
                        propertyInfos.SetValue(aTSource, value, null);
                }

                dataList.Add(aTSource);
            }
            return dataList;
        }

        /// <summary>
        /// Converts first row of DataTable to T type object
        /// </summary>
        /// <remarks>
        /// v1.0 - 2023.07.20 - 65500 Edison M. Ramirez
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns>T type object</returns>
        public static T ToObject<T>(this DataTable dataTable) where T : new()
        {
            var aTSource = new T();

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var objFieldNames = (from PropertyInfo aProp in typeof(T).GetProperties(flags)
                                 select new PropertyObjDb
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                                 })
                                 .ToList();

            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new PropertyObjDb
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     })
                                     .ToList();

            var commonFields = objFieldNames.Intersect(dataTblFieldNames, new PropObjDbEqualityComp()).ToList();

            DataRow dataRow = dataTable.Rows[0];            
            foreach (var aField in commonFields)
            {
                PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                if (propertyInfos == null) continue;
                var value = (dataRow[aField.Name] == DBNull.Value) ? null : dataRow[aField.Name]; //if database field is nullable
                if (propertyInfos.PropertyType.FullName.Equals("System.Int32"))
                    propertyInfos.SetValue(aTSource, Convert.ToInt32(value), null);
                else if (propertyInfos.PropertyType.FullName.Equals("System.Decimal"))
                    propertyInfos.SetValue(aTSource, Convert.ToDecimal(value), null);
                else
                    propertyInfos.SetValue(aTSource, value, null);
            }
            

            return aTSource;
        }

        /// <summary>
        /// Converts List to Xml
        /// </summary>
        /// <remarks>
        /// v1.0 - 2023.07.21 - 65500 Edison M. Ramirez
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstObjeto">Lista de objetos</param>
        /// <param name="cNodo">Nombre de los nodos del XML</param>
        /// <param name="cRaiz">Nombre de del nodo Raiz del XML</param>
        /// <returns>T type object</returns>
        public static string ToXml<T>(this IEnumerable<T> lstObjeto, string cNodo = "Nodo", string cRaiz = "Raiz")
        {
            var props = typeof(T).GetProperties();

            var dt = new DataTable(cNodo);
            dt.Columns.AddRange(
              props.Where(p => !p.PropertyType.GetCustomAttributes(false).Any(a=>a is XmlIgnoreAttribute)).
              Select(p => new DataColumn(p.Name, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType)).ToArray()
            );

            lstObjeto.ToList().ForEach(
                //i => dt.Rows.Add(props.Select(p => p.GetValue(i, null) ?? DBNull.Value).ToArray())
                i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );
            DataSet ds = new DataSet(cRaiz);
            ds.Tables.Add(dt);
            return ds.GetXml();
        }

        public class PropertyObjDb
        {
            public string Name { get; set; }
            public string NameOrigin { get; set; }
            public Type Type { get; set; }
        }
        public class PropObjDbEqualityComp : IEqualityComparer<PropertyObjDb>
        {
            public bool Equals(PropertyObjDb? x, PropertyObjDb? y)
            {
                if(x == null || y == null) return false;
                if(x.Name.Equals(y.Name)) { return true; }
                return false;
            }
            public int GetHashCode(PropertyObjDb x)
            {
                int nHashCode = x.Name.GetHashCode();
                return nHashCode;
            }
        }
    }
}
