using WebAPI.Data.DTO;
using WebAPI.Model;

namespace WebAPI.Data.Extensions
{
    public static class InvoiceExtensions
    {
        public static InvoiceDTO ToDto(this Invoice invoice)
        {
            if (invoice == null)
                return new InvoiceDTO();

            var dto = new InvoiceDTO
                          {
                              InvoiceNumber = invoice.InvoiceNumber,
                              InvoiceDate = invoice.InvoiceDate
                          };

            return dto;
        }
    }
}
 