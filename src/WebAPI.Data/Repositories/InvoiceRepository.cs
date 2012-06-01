using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WebAPI.Data.Contracts;
using WebAPI.Data.DTO;
using WebAPI.Model;

namespace WebAPI.Data.Repositories
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private WebAPIDemodbEntities m_DbContext;

        public InvoiceRepository()
        {
            m_DbContext = new WebAPIDemodbEntities();
        }

        public Invoice GetById(int id)
        {
            return m_DbContext.Invoices.FirstOrDefault(inv => inv.Id == id);
        }

        public IEnumerable<Invoice> GetAll()
        {
            return m_DbContext.Invoices.AsEnumerable();
        }

        public void Add(Invoice entity)
        {
            
            m_DbContext.Invoices.AddObject(entity);
            m_DbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = m_DbContext.Invoices.FirstOrDefault(c => c.Id == id);

            if (entity == null)
                return;

            m_DbContext.Invoices.DeleteObject(entity);
            m_DbContext.SaveChanges();
        }

        public void DeleteByInvoiceNumber(string invoiceNumber)
        {
            var entity = m_DbContext.Invoices.FirstOrDefault(c => c.InvoiceNumber == invoiceNumber);

            if (entity == null)
                return;

            m_DbContext.Invoices.DeleteObject(entity);
            m_DbContext.SaveChanges();
        }

        public Invoice FindByInvoiceNumber(string invoiceNumber)
        {
            return m_DbContext.Invoices.FirstOrDefault(c => c.InvoiceNumber == invoiceNumber);
        }

        public void UpdateInvoice(InvoiceDTO invoice)
        {
            var dbInvoice = m_DbContext.Invoices.FirstOrDefault(inv => inv.InvoiceNumber == invoice.InvoiceNumber);

            //ToDO: Fix this
            if (dbInvoice == null)
                return;

            dbInvoice.InvoiceNumber = invoice.InvoiceNumber;
            dbInvoice.InvoiceDate = invoice.InvoiceDate;

            m_DbContext.SaveChanges();
        }
    }
}
