namespace NonconformityControl.Api.ViewModels
{
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResultViewModel(bool success, object data, string messsage) 
        {
            Success = success;
            Data = data;
            Message = messsage;
        }
    }

}