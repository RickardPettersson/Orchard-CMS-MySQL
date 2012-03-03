using System;
using System.Linq;
using Orchard.Projections.Models;

namespace Orchard.Projections.Services {
    public class FieldIndexService : IFieldIndexService {

        public void Set(FieldIndexPart part, string partName, string fieldName, string valueName, object value, Type valueType) {
            var propertyName = String.Join(".", partName, fieldName, valueName ?? "");

            var typeCode = Type.GetTypeCode(valueType);

            if(valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                typeCode = Type.GetTypeCode(Nullable.GetUnderlyingType(valueType));
            }

            switch (typeCode) {
                case TypeCode.Char:
                case TypeCode.String:
                    var stringRecord = part.Record.StringFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    if (stringRecord == null) {
                        stringRecord = new StringFieldIndexRecord { PropertyName = propertyName };
                        part.Record.StringFieldIndexRecords.Add(stringRecord);
                    }

                    stringRecord.Value = value == null ? null : value.ToString();
                    break;
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    var integerRecord = part.Record.IntegerFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    if (integerRecord == null) {
                        integerRecord = new IntegerFieldIndexRecord { PropertyName = propertyName };
                        part.Record.IntegerFieldIndexRecords.Add(integerRecord);
                    }

                    integerRecord.Value = value == null ? default(long?) : Convert.ToInt64(value);
                    break;
                case TypeCode.DateTime:
                    var dateTimeRecord = part.Record.IntegerFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    if (dateTimeRecord == null) {
                        dateTimeRecord = new IntegerFieldIndexRecord { PropertyName = propertyName };
                        part.Record.IntegerFieldIndexRecords.Add(dateTimeRecord);
                    }

                    dateTimeRecord.Value = value == null ? default(long?) : ((DateTime)value).Ticks;
                    break;
                case TypeCode.Boolean:
                    var booleanRecord = part.Record.IntegerFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    if (booleanRecord == null) {
                        booleanRecord = new IntegerFieldIndexRecord { PropertyName = propertyName };
                        part.Record.IntegerFieldIndexRecords.Add(booleanRecord);
                    }

                    booleanRecord.Value = value == null ? default(long?) : Convert.ToInt64((bool)value);
                    break;
                case TypeCode.Decimal:
                    var decimalRecord = part.Record.DecimalFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    if (decimalRecord == null) {
                        decimalRecord = new DecimalFieldIndexRecord { PropertyName = propertyName };
                        part.Record.DecimalFieldIndexRecords.Add(decimalRecord);
                    }

                    decimalRecord.Value = value == null ? default(decimal?) : Convert.ToDecimal((decimal)value);
                    break;
                case TypeCode.Single:
                case TypeCode.Double:
                    var doubleRecord = part.Record.DoubleFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    if (doubleRecord == null) {
                        doubleRecord = new DoubleFieldIndexRecord { PropertyName = propertyName };
                        part.Record.DoubleFieldIndexRecords.Add(doubleRecord);
                    }

                    doubleRecord.Value = value == null ? default(double?) : Convert.ToDouble(value);
                    break;
                default:
                    break;
            }
        }

        public T Get<T>(FieldIndexPart part, string partName, string fieldName, string valueName) {
            var propertyName = String.Join(".", partName, fieldName, valueName ?? "");

            var typeCode = Type.GetTypeCode(typeof(T));

            switch (typeCode) {
                case TypeCode.Char:
                case TypeCode.String:
                    var stringRecord = part.Record.StringFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    return stringRecord != null ? (T)Convert.ChangeType(stringRecord.Value, typeof(T)) : default(T);
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    var integerRecord = part.Record.IntegerFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    return integerRecord != null ? (T)Convert.ChangeType(integerRecord.Value, typeof(T)) : default(T);
                case TypeCode.Decimal:
                    var decimalRecord = part.Record.DecimalFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    return decimalRecord != null ? (T)Convert.ChangeType(decimalRecord.Value, typeof(T)) : default(T);
                case TypeCode.Single:
                case TypeCode.Double:
                    var doubleRecord = part.Record.DoubleFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    return doubleRecord != null ? (T)Convert.ChangeType(doubleRecord.Value, typeof(T)) : default(T);
                case TypeCode.DateTime:
                    var dateTimeRecord = part.Record.IntegerFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    return dateTimeRecord != null ? (T)Convert.ChangeType(new DateTime(Convert.ToInt64(dateTimeRecord.Value)), typeof(T)) : default(T);
                case TypeCode.Boolean:
                    var booleanRecord = part.Record.IntegerFieldIndexRecords.Where(r => r.PropertyName == propertyName).FirstOrDefault();
                    return booleanRecord != null ? (T)Convert.ChangeType(booleanRecord.Value, typeof(T)) : default(T);
                default:
                    return default(T);
            }
        }
    }
}
