using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeHire.Models;

namespace CodeHire.BusinessLogic
{
    public abstract class FullBusinessLogicImplementor<T>: BasicBusinessLogicImplementor<T>
    {
        public FullBusinessLogicImplementor(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T GetOne(int id);

        //had to create special signatures with create and update in JobListings
        //that may not be applicable to other children

        public abstract bool Delete(int id);
    }
}
