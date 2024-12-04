using System.Net;

namespace Task_Managment_System.Models
{
    public class APIResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public List<string>? ErrorMessages { get; set; } = new List<string>();
        public T Result { get; set; }

    }
}
