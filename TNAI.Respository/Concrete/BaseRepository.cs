using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI.Model;

namespace TNAI.Respository.Concrete
{
    public class BaseRepository
    {
        protected AppDbContext Context;

        public BaseRepository()
        {
            Context = AppDbContext.Create();
        }
    }
}
