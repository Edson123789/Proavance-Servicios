using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models.AulaVirtual
{
    public partial class MdlQuestionnaireResponseText
    {
        public long Id { get; set; }
        public long ResponseId { get; set; }
        public long QuestionId { get; set; }
        public string Response { get; set; }
    }
}
