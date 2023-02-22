using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entites.Concrete
{
    public class Permission
    {
        public Guid Id { get; set; }

        public PermissionTypes PermissionTypes { get; set; }

        public DateTime RequestTime { get; set; }

        public DateTime? ResponseTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Day { get; set; }

        public StatusTypes Status { get; set; }

        public string PersonnelId { get; set; }

        public Employee Personnel { get; set; }
    }
}
