using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    internal class WorkWearController
    {
        AppContext app_context;
        public WorkWearController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(int wear_type_id, String name_, float cost)
        {
            bool answer = true;
            try
            {
                app_context.WorkWears.Add(new WorkWear() {
                    Name = name_,
                    Cost = cost,
                    WearTypeId = wear_type_id
                });
                app_context.SaveChanges();
            }
            catch (Exception exc){
                answer = false;
            }
            return answer;
        }
        public List<WorkWear> GetAll()
        {
            return app_context.WorkWears.AsQueryable().ToList();
        }
        public bool Update(int id, int wear_type_id, String name_, float cost)
        {
            var found = app_context.WorkWears.Find(id);
            if (found == null) { return false; }
            try
            {
                found.WearTypeId = wear_type_id;
                found.Name = name_;
                found.Cost = cost;
                app_context.WorkWears.AddOrUpdate(found);
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
           var found = app_context.WorkWears.Find(id);
           if (found == null) return false;
           app_context.WorkWears.Remove(found);
           app_context.SaveChanges();
           return true;
        }
    }
}
