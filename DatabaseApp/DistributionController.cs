using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    internal class DistributionController
    {
        AppContext app_context;
        public DistributionController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(int invoice_id, int workwear_id)
        {
            bool answer = true;
            try
            {
                app_context.Distributions.Add(new Distribution() {
                    InvoiceId = invoice_id, 
                    WorkWearId = workwear_id, 
                    Issued = false});
                app_context.SaveChanges();
            }
            catch (Exception exc){
                answer = false;
            }
            return answer;
        }
        public List<Distribution> GetAll()
        {
            return app_context.Distributions.AsQueryable().ToList();
        }
        public bool Update(int id, int invoice_id, int workwear_id, bool issued)
        {
            var found = app_context.Distributions.Find(id);
            if (found == null) { return false; }
            try
            {
                found.InvoiceId = invoice_id;
                found.WorkWearId = workwear_id;
                found.Issued = issued;
                app_context.Distributions.AddOrUpdate(found);
                app_context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
           var found = app_context.Distributions.Find(id);
           if (found == null) return false;
           app_context.Distributions.Remove(found);
           app_context.SaveChanges();
           return true;
        }
    }
}
