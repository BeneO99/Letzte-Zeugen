using Letzte_Zeugen.Db;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Letzte_Zeugen.Models
{
    public class Formular
    {

        public Modell Modell { get; set; }

        public Bautypus Bautypus { get; set; }
        public Gefaehrdung Gefaehrdung { get; set; }
        public String? BeteiligtePersonen { get; set; }
        public Eigentuemer Eigentuemer { get; set; }
        public Ort Erstellungsort { get; set; }
        public Ort Standort { get; set; }
        public InstitutHelper BeteiligteInstitute { get; set; }
        public InstitutHelper PruefInstitute { get; set; }
        public Material Material { get; set; }
        public ProjektHelper Projekt { get; set; }
        public Person Urheber { get; set; }
        public Existent Existent { get; set; }
        public Zustand Zustand { get; set; }
        public Messmodell Messmodell { get; set; }
        public Realisiert Realisiert { get; set; }
        public List<Person> Personenliste { get; set; }
        public Link Link { get; set; }
        public List<IFormFile>? Picture { get; set; }
		public List<IFormFile>? Files { get; set; }

	}


}
