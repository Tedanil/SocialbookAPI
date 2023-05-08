using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Domain.Entities.Common
{
    public class BaseEntity
    {
        virtual public Guid Id { get; set; }

        virtual public DateTime CreatedDate { get; set; }

        virtual public DateTime UpdatedDate { get; set; }
    }
}
