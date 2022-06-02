using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models.AulaVirtual
{
    public partial class MdlQuestionnaireResponse
    {
        public long Id { get; set; }
        public long SurveyId { get; set; }
        public long Submitted { get; set; }
        public string Complete { get; set; }
        public long Grade { get; set; }
        public string Username { get; set; }
    }
}
