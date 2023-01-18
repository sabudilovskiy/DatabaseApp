using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseApp
{
    internal class WorkshopController
    {
        AppContext app_context;
        public WorkshopController(AppContext app_context)
        {
            this.app_context = app_context;
        }
        public bool Create(String name_)
        {
            bool answer = true;
            try
            {
                app_context.Workshops.Add(new Workshop() { Name = name_ });
                app_context.SaveChanges();
            }
            catch (Exception exc)
            {
                answer = false;
            }
            return answer;
        }
        public List<Workshop> GetAll()
        {
            return app_context.Workshops.AsQueryable().ToList();
        }
        public bool Update(int id, String name_)
        {
            Workshop found = app_context.Workshops.Find(id);
            if (found == null) { return false; }
            found.Name = name_;
            app_context.Workshops.AddOrUpdate(found);
            app_context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var found = app_context.Workshops.Find(id);
            if (found == null) return false;
            app_context.Workshops.Remove(found);
            app_context.SaveChanges();
            return true;
        }
    }
}
