using Azure;

namespace Web.Models.Common
{
    public class ResponseData
    {
        public bool status { get; set; }
        public string message { get; set; }

        public ResponseData ErrorMessage(string errorMessage)
        {
            this.status = false;
            this.message = errorMessage;
            return this;

        }
        public ResponseData SuccessMessage(string successMessage)
        {
            this.status = true;
            this.message = successMessage;
            return this;

        }
    }
}
