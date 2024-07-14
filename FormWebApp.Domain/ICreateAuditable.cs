using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormWebApp.Domain
{
    public interface ICreateAuditable
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
