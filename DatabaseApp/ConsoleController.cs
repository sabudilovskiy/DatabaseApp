using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseApp
{
    internal class ConsoleController
    {
        protected Commands cur_command;
        protected Types cur_type;
        protected DistributionController distributionController;
        protected InvoiceController invoiceController;
        protected JobController jobController;
        protected WearTypeController wearTypeController;
        protected WorkerController workerController;
        protected WorkshopController workshopController;
        protected WorkWearController workWearController;

        protected enum Commands { 
            Stop = 0,
            Create = 1,
            GetAll = 2,
            Update = 3,
            Delete = 4,
            Error = 5
        }
        protected enum Types
        {
            Invoice = 0,
            Job = 1,
            WearType = 2,
            Workshop = 3,
            WorkWear = 4,
            Worker = 5,
            Distribution = 6,
            Error = 7
        }
        public ConsoleController(AppContext appContext)
        {
            distributionController= new DistributionController(appContext);
            invoiceController= new InvoiceController(appContext);
            jobController= new JobController(appContext);
            wearTypeController= new WearTypeController(appContext);
            workerController= new WorkerController(appContext);
            workshopController = new WorkshopController(appContext);
            workWearController= new WorkWearController(appContext);
            cur_command = Commands.Create;
        }
        protected void ConvertToCommand(String input)
        {
            if (!Enum.TryParse(input, true, out Commands my_command)) my_command = Commands.Error;
            cur_command = my_command;
        }
        protected void ConvertToType(String input)
        {
            if (!Enum.TryParse(input, true, out Types my_type)) my_type = Types.Error;
            cur_type = my_type;
        }

        protected static int SafeInputInt(String prev)
        {
            Console.WriteLine(prev);
            int result;
            try
            {
                result = int.Parse(Console.ReadLine());
                return result;
            }
            catch (Exception e) { }
            while (true)
            {
                try
                {
                    Console.WriteLine("Допущена ошибка, повторите ввод:");
                    result = int.Parse(Console.ReadLine());
                    return result;
                }
                catch(Exception e) { }
            }
        }

        protected static float SafeInputFloat(String prev)
        {
            Console.WriteLine(prev);
            float result;
            try
            {
                result = float.Parse(Console.ReadLine());
                return result;
            }
            catch (Exception e) { }
            while (true)
            {
                try
                {
                    Console.WriteLine("Допущена ошибка, повторите ввод:");
                    result = float.Parse(Console.ReadLine());
                    return result;
                }
                catch (Exception e) { }
            }
        }
        protected static bool SafeInputBool(String prev)
        {
            Console.WriteLine(prev);
            bool result;
            try
            {
                result = bool.Parse(Console.ReadLine());
                return result;
            }
            catch (Exception e) { }
            while (true)
            {
                try
                {
                    Console.WriteLine("Допущена ошибка, повторите ввод:");
                    result = bool.Parse(Console.ReadLine());
                    return result;
                }
                catch (Exception e) { }
            }
        }

        protected static String InputString(String prev)
        {
            Console.WriteLine(prev);
            return Console.ReadLine();
        }

        protected void Dispatch()
        {
            switch (cur_type)
            {
                case Types.Job: JobInput(); break;
                case Types.Workshop: WorkshopInput(); break;
                case Types.Worker: WorkerInput(); break;
                case Types.Invoice: InvoiceInput(); break;
                case Types.WearType: WearTypeInput(); break;
                case Types.WorkWear: WorkWearInput(); break;
                case Types.Distribution: DistributionInput(); break;
            }
        }

        protected void JobInput()
        {
            if (cur_command == Commands.Create)
            {
                Console.WriteLine("Введите name: ");
                String name = Console.ReadLine();
                if (jobController.Create(name)) Console.WriteLine("Успешно создано");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                Console.WriteLine("Введите name: ");
                String name = Console.ReadLine();
                if (jobController.Update(id, name)) Console.WriteLine("Успешно обновлено");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                if (jobController.Delete(id)) Console.WriteLine("Успешно удалено");
                else Console.WriteLine("Произошла ошибка");
            }
            else
            {
                var listAll = jobController.GetAll();
                foreach (var item in listAll)
                {
                    String elem = "id: " + item.Id + " name: " + item.Name;
                    Console.WriteLine(elem);
                }
            }
        }
        protected void WorkshopInput()
        {
            if (cur_command == Commands.Create)
            {
                Console.WriteLine("Введите name: ");
                String name = Console.ReadLine();
                if (workshopController.Create(name)) Console.WriteLine("Успешно создано");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                Console.WriteLine("Введите name: ");
                String name = Console.ReadLine();
                if (workshopController.Update(id, name)) Console.WriteLine("Успешно обновлено");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                if (workshopController.Delete(id)) Console.WriteLine("Успешно удалено");
                else Console.WriteLine("Произошла ошибка");
            }
            else
            {
                var listAll = workshopController.GetAll();
                foreach (var item in listAll)
                {
                    String elem = "id: " + item.Id + " name: " + item.Name;
                    Console.WriteLine(elem);
                }
            }
        }
        protected void WearTypeInput()
        {
            if (cur_command == Commands.Create)
            {
                Console.WriteLine("Введите name: ");
                String name = Console.ReadLine();
                if (wearTypeController.Create(name)) Console.WriteLine("Успешно создано");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                Console.WriteLine("Введите name: ");
                String name = Console.ReadLine();
                if (wearTypeController.Update(id, name)) Console.WriteLine("Успешно обновлено");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                if (wearTypeController.Delete(id)) Console.WriteLine("Успешно удалено");
                else Console.WriteLine("Произошла ошибка");
            }
            else
            {
                var listAll = wearTypeController.GetAll();
                foreach (var item in listAll)
                {
                    String elem = "id: " + item.Id + " name: " + item.Name;
                    Console.WriteLine(elem);
                }
            }
        }
        protected void WorkerInput()
        {
            if (cur_command == Commands.Create)
            {
                Console.WriteLine("Введите FIO: ");
                String fio = Console.ReadLine();
                int job_id = SafeInputInt("Введите job_id: ");
                int workshop_id = SafeInputInt("Введите workshop_id: ");
                float discount = SafeInputFloat("Введите скидку(%): ");
                if (workerController.Create(fio, job_id, workshop_id, discount)) Console.WriteLine("Успешно создано");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                Console.WriteLine("Введите name: ");
                String fio = Console.ReadLine();
                int job_id = SafeInputInt("Введите job_id: ");
                int workshop_id = SafeInputInt("Введите workshop_id: ");
                float discount = SafeInputFloat("Введите скидку(%): ");
                if (workerController.Update(id, fio, job_id, workshop_id, discount)) Console.WriteLine("Успешно обновлено");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                if (workerController.Delete(id)) Console.WriteLine("Успешно удалено");
                else Console.WriteLine("Произошла ошибка");
            }
            else
            {
                var listAll = workerController.GetAll();
                foreach (var item in listAll)
                {
                    String elem = 
                        "id: " + item.Id + 
                        " fio: " + item.FIO + 
                        " job_id: " + item.JobId + 
                        " workshop_id: " + item.WorkshopId + 
                        " discount: " + item.Discount;
                    Console.WriteLine(elem);
                }
            }
        }
        protected void InvoiceInput()
        {
            if (cur_command == Commands.Create)
            {
                int worker_id = SafeInputInt("Введите worker_id: ");
                if (invoiceController.Create(worker_id)) Console.WriteLine("Успешно создано");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                int worker_id = SafeInputInt("Введите worker_id: ");
                if (invoiceController.Update(id, worker_id)) Console.WriteLine("Успешно обновлено");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                if (invoiceController.Delete(id)) Console.WriteLine("Успешно удалено");
                else Console.WriteLine("Произошла ошибка");
            }
            else
            {
                var listAll = invoiceController.GetAll();
                foreach (var item in listAll)
                {
                    String elem = "id: " + item.Id + " worker_id: " + item.WorkerId + " time: " + item.Time;
                    Console.WriteLine(elem);
                }
            }
        }

        protected void WorkWearInput()
        {
            if (cur_command == Commands.Create)
            {
                String name = InputString("Введите name: ");
                int wear_type_id = SafeInputInt("Введите wear_type_id: ");
                float cost = SafeInputFloat("Введите cost: ");
                if (workWearController.Create(wear_type_id, name, cost)) Console.WriteLine("Успешно создано");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                String name = InputString("Введите name: ");
                int wear_type_id = SafeInputInt("Введите wear_type_id: ");
                float cost = SafeInputFloat("Введите cost: ");
                if (workWearController.Update(id, wear_type_id, name, cost)) Console.WriteLine("Успешно обновлено");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                if (workWearController.Delete(id)) Console.WriteLine("Успешно удалено");
                else Console.WriteLine("Произошла ошибка");
            }
            else
            {
                var listAll = workWearController.GetAll();
                foreach (var item in listAll)
                {
                    String elem =
                        "id: " + item.Id +
                        " wear_type_id: " + item.WearTypeId +
                        " name: " + item.Name +
                        " cost: " + item.Cost;
                    Console.WriteLine(elem);
                }
            }
        }
        protected void DistributionInput()
        {
            if (cur_command == Commands.Create)
            {
                int invoice_id = SafeInputInt("Введите invoice_id: ");
                int workwear_id = SafeInputInt("Введите workwear_id: ");
                if (distributionController.Create(invoice_id, workwear_id)) Console.WriteLine("Успешно создано");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Update)
            {
                int id = SafeInputInt("Введите id: ");
                int invoice_id = SafeInputInt("Введите invoice_id: ");
                int workwear_id = SafeInputInt("Введите workwear_id: ");
                bool issued = SafeInputBool("Введите issued: ");
                if (distributionController.Update(id, invoice_id, workwear_id, issued)) 
                    Console.WriteLine("Успешно обновлено");
                else Console.WriteLine("Произошла ошибка");
            }
            else if (cur_command == Commands.Delete)
            {
                int id = SafeInputInt("Введите id: ");
                if (distributionController.Delete(id)) Console.WriteLine("Успешно удалено");
                else Console.WriteLine("Произошла ошибка");
            }
            else
            {
                var listAll = distributionController.GetAll();
                foreach (var item in listAll)
                {
                    String elem = 
                        "id: " + item.Id +
                        " invoice_id: " + item.InvoiceId +
                        " workwear_id: " + item.WorkWearId +
                        " issued: " + item.Issued
                        ;
                    Console.WriteLine(elem);
                }
            }
        }
        public void Start()
        {
            String input;
            while (cur_command != Commands.Stop)
            {
                Console.WriteLine("Введите команду: Stop, Create, Update, GetAll, Delete: ");
                input = Console.ReadLine();
                ConvertToCommand(input);
                while (cur_command == Commands.Error)
                {
                    Console.WriteLine("Ошибка ввода, повторите ввод: ");
                    input = Console.ReadLine();
                    ConvertToCommand(input);
                }
                if (cur_command == Commands.Stop) {
                    continue;
                }
                Console.WriteLine("Введите таблицу: Job, Workshop, Worker, Invoice, WearType, WorkWear, Distribution: ");
                input = Console.ReadLine();
                ConvertToType(input);
                while (cur_type == Types.Error)
                {
                    Console.WriteLine("Ошибка ввода, повторите ввод: ");
                    input = Console.ReadLine();
                    ConvertToCommand(input);
                }
                Dispatch();
            }
        }
        
    }
}
