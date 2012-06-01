using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Data.DTO;
using WebAPI.Data.Extensions;
using WebAPI.Data.Repositories;

namespace WebAPI.Controllers
{
    public class InvoicesController : ApiController
    {
        //private static readonly List<InvoiceDTO> m_InvoiceRepository = new List<InvoiceDTO>();  
        
        private InvoiceRepository m_InvoiceRepository = new InvoiceRepository();

        // GET /api/invoices
        public HttpResponseMessage<IEnumerable<InvoiceDTO>> Get()
        {
            var invoices = m_InvoiceRepository.GetAll();

            var dtoList = invoices.Select(invoice => invoice.ToDto()).ToList();

            return new HttpResponseMessage<IEnumerable<InvoiceDTO>>(dtoList);
        }

        // GET /api/invoices/5
        public HttpResponseMessage<InvoiceDTO> Get(string invoiceNumber)
        {
            //TODO: Serve a 404 or 204 instead? 
            var invoice = m_InvoiceRepository.FindByInvoiceNumber(invoiceNumber).ToDto();
            
            if (invoice == null)
                return new HttpResponseMessage<InvoiceDTO>(HttpStatusCode.OK);

            return new HttpResponseMessage<InvoiceDTO>(invoice);
        }

        // POST /api/invoices
        public HttpResponseMessage Post(InvoiceDTO invoice)
        {
            m_InvoiceRepository.Add(invoice.ToEntity());
            var response = new HttpResponseMessage<InvoiceDTO>(invoice, HttpStatusCode.Created);

            response.Headers.Location = new Uri(Request.RequestUri + invoice.InvoiceNumber);

            return response;
        }

        // PUT
        //TODO: Make a request object instead.
        public HttpResponseMessage Put(string invoiceNumber, DateTime invoiceDate)
        {
            var invoice = m_InvoiceRepository.FindByInvoiceNumber(invoiceNumber).ToDto();

            if (invoice == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            invoice.InvoiceNumber = invoiceNumber;
            invoice.InvoiceDate = invoiceDate;

            m_InvoiceRepository.UpdateInvoice(invoice);

            //TODO: Create a proper update flow with a useful response.
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE /api/invoice/5
        public HttpResponseMessage Delete(string invoiceNumber)
        {
            //ToDo: Need to get some response back here
            m_InvoiceRepository.DeleteByInvoiceNumber(invoiceNumber);

            //ToDo: No content for Deletes, right?
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}