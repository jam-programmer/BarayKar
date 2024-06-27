namespace Application.Common.ViewModel.Home
{
    public class EmploymentViewModel
    {
        public Guid Id { get; set; }    
        public long Code { set; get; }
        public string? Title { set; get; }
        public string? Salary { set; get; }
        public string? BusinessAccountName { set; get; }
        public string? ProvinceName { set; get; }
        public string? CityName { set; get; }
        public string? Image { set; get; }
    }
}
