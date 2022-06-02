using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models.AulaVirtual
{
    public partial class MdlQuestionnaireQuestion
    {
        public long Id { get; set; }
        public long SurveyId { get; set; }
        public string Name { get; set; }
        public long TypeId { get; set; }
        public long? ResultId { get; set; }
        public long Length { get; set; }
        public long Precise { get; set; }
        public long Position { get; set; }
        public string Content { get; set; }
        public string Required { get; set; }
        public string Deleted { get; set; }
        public long Dependquestion { get; set; }
        public long Dependchoice { get; set; }
    }
}
