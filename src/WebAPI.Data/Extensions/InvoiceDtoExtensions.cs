using WebAPI.Data.DTO;
using WebAPI.Model;

namespace WebAPI.Data.Extensions
{
    public static class InvoiceDtoExtensions
    {
        public static Invoice ToEntity(this InvoiceDTO dto)
        {
            var entity = new Invoice
                             {
                                 InvoiceNumber = dto.InvoiceNumber,
                                 InvoiceDate = dto.InvoiceDate
                             };

            return entity;
        }
    }
}
