using Microsoft.AspNetCore.Identity;
using FormWebApp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormApp.Domain
{
    public class User : IdentityUser<int>, IBaseEntity, ISoftDeletable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        
        public List <UserRole> Roles { get; set; }

        public List<Form> Forms { get; set; }

        public bool IsDeleted { get; set; }
    }
}
