using AutoMapper;
using Objects.DTOs;

namespace Objects.Models
{
    public class MyMappingProfile:Profile
    {
        public MyMappingProfile()
        {
            CreateMap<Category,CategoryDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<ImportProduct,ImportProductDTO>().ReverseMap();
            CreateMap<Invoice,InvoiceDTO>().ReverseMap();
            CreateMap<InvoiceDetail,InvoiceDetailDTO>().ReverseMap();
            CreateMap<Customer,CustomerDTO>().ReverseMap();
        }
    }
}
