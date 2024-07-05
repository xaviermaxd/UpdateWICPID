using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ExecutionUpdateWicPID
    {
        public int ExecutionUpdateWicPIDID { get; set; }
        public DateTime ExecutionDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? LastExecutionDate { get; set; }
        public string TypeBatch { get; set; }
        public string Error { get; set; }
        public bool Successful { get; set; }
        public int NotFoundCount { get; set; } // Nueva propiedad para contar participantes no encontrados
    }
}