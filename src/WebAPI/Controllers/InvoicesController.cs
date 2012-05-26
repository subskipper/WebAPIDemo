using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class InvoicesController : ApiController
    {
        private static readonly List<Invoice> m_InvoiceRepository = new List<Invoice>();  

        // GET /api/invoices
        public HttpResponseMessage<List<Invoice>> Get()
        {
            return new HttpResponseMessage<List<Invoice>>(m_InvoiceRepository);
        }

        // GET /api/invoices/5
        public HttpResponseMessage<Invoice> Get(string invoiceNumber)
        {
            //TODO: Serve a 404 or 204 instead? 
            var invoice = m_InvoiceRepository.FirstOrDefault(inv => inv.InvoiceNumber == invoiceNumber);
            if (invoice == null)
                return new HttpResponseMessage<Invoice>(HttpStatusCode.OK);

            return new HttpResponseMessage<Invoice>(invoice);
        }

        // POST /api/invoices
        public HttpResponseMessage Post(Invoice invoice)
        {
            m_InvoiceRepository.Add(invoice);
            var response = new HttpResponseMessage<Invoice>(invoice, HttpStatusCode.Created);

            response.Headers.Location = new Uri(Request.RequestUri + invoice.InvoiceNumber);

            return response;
        }

        // PUT /api/invoices/5
        public HttpResponseMessage Put(string invoiceNumber, string newValue)
        {

            //TODO: Should update via EF.
            var invoice = m_InvoiceRepository.FirstOrDefault(inv => inv.InvoiceNumber == invoiceNumber);

            if (invoice == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            invoice.InvoiceNumber = newValue;

            m_InvoiceRepository.RemoveAll(inv => inv.InvoiceNumber == invoiceNumber);
            m_InvoiceRepository.Add(invoice);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE /api/invoice/5
        public HttpResponseMessage Delete(string invoiceNumber)
        {
            var invoice = m_InvoiceRepository.FirstOrDefault(inv => inv.InvoiceNumber == invoiceNumber);

            if (invoice == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            //ToDo: No content for Deletes, right?
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}