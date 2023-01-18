using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    internal class JobController
    {
        AppContext app_context;
        public JobController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(String name_)
        {
            bool answer = true;
            try
            {
                app_context.Jobs.Add(new Job() { Name = name_ });
                app_context.SaveChanges();
            }
            catch (Exception exc){
                answer = false;
            }
            return answer;
        }
        public List<Job> GetAll()
        {
            return app_context.Jobs.AsQueryable().ToList();
        }
        public bool Update(int id, String name_)
        {
            var found = app_context.Jobs.Find(id);
            if (found == null) { return false; }
            found.Name = name_;
            app_context.Jobs.AddOrUpdate(found);
            app_context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
           var found = app_context.Jobs.Find(id);
           if (found == null) return false;
           app_context.Jobs.Remove(found);
           app_context.SaveChanges();
           return true;
        }
    }
}
