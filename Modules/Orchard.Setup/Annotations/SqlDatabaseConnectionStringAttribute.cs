using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Orchard.Setup.Annotations {
    public class SqlDatabaseConnectionStringAttribute : ValidationAttribute {
        public override bool IsValid(object value) {
            if (value is string && ((string)value).Length > 0) {
                try {
                    // TODO: This validation need to be added has fix for MySQL if it should work, so i comment out the row below
                    // var connectionStringBuilder = new SqlConnectionStringBuilder(value as string);

                    //TODO: (erikpo) Should the keys be checked here to ensure that a valid combination was entered? Needs investigation.
                }
                catch {
                    return false;
                }
            }

            return true;
        }
    }
}