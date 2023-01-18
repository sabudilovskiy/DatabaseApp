using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    internal class WorkerController
    {
        AppContext app_context;
        public WorkerController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(String fio_, int job_id, int workshop_id, float discount)
        {
            bool answer = true;
            try
            {
                app_context.Workers.Add(new Worker() {
                    FIO = fio_,
                    JobId = job_id,
                    WorkshopId = workshop_id,
                    Discount = discount
                });
                app_context.SaveChanges();
            }
            catch (Exception exc){
                Console.WriteLine(exc.ToString());
                answer = false;
            }
            return answer;
        }
        public List<Worker> GetAll()
        {
            return app_context.Workers.AsQueryable().ToList();
        }
        public bool Update(int id, String fio_, int job_id, int workshop_id, float discount)
        {
            var found = app_context.Workers.Find(id);
            if (found == null) { return false; }
            try
            {
                found.FIO = fio_;
                found.JobId = job_id;
                found.WorkshopId = workshop_id;
                found.Discount = discount;
                app_context.Workers.AddOrUpdate(found);
                app_context.SaveChanges();
                return true;
            }
            catch (Exception exc) {
                return false;
            }
            
        }
        public bool Delete(int id)
        {
           var found = app_context.Workers.Find(id);
           if (found == null) return false;
           app_context.Workers.Remove(found);
           app_context.SaveChanges();
           return true;
        }
    }
}
