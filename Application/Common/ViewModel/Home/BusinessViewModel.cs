using Domain.Entities.System;

namespace Application.Common.ViewModel.Home
{
    public class BusinessViewModel
    {
        public Guid Id { get; set; }
        public string? AccountName { set; get; }
        public string? BusinessPictureImage { set; get; }
        public string? ProvinceName { set; get; }
        public string? CityName { set; get; }
        public string? CategoryName { set; get; }

        public long? Code { set; get; }
    }
}
