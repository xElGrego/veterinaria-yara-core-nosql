
namespace veterinaria_yara_core_nosql.application.models.exceptions
{
    public class VeterinariaYaraNoSqlException : BaseCustomException
    {
        public VeterinariaYaraNoSqlException(string message = "Exception", string desciption = "", int statuscode = 500) : base(message, desciption, statuscode)
        {

        }
    }
}
