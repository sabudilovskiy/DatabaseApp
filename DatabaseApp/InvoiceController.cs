using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    internal class InvoiceController
    {
        AppContext app_context;
        public InvoiceController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(int worker_id)
        {
            bool answer = true;
            try
            {
                app_context.Invoices.Add(new Invoice() { WorkerId = worker_id, Time = DateTime.Now });
                app_context.SaveChanges();
            }
            catch (Exception exc){
                answer = false;
            }
            return answer;
        }
        public List<Invoice> GetAll()
        {
            return app_context.Invoices.AsQueryable().ToList();
        }
        public bool Update(int id, int worker_id)
        {
            var found = app_context.Invoices.Find(id);
            if (found == null) { return false; }
            try
            {
                found.WorkerId = worker_id;
                app_context.Invoices.AddOrUpdate(found);
                app_context.SaveChanges();
                return true;
            }
            catch (Exception e) { return false; }
        }
        public bool Delete(int id)
        {
           var found = app_context.Invoices.Find(id);
           if (found == null) return false;
           app_context.Invoices.Remove(found);
           app_context.SaveChanges();
           return true;
        }
    }
}
