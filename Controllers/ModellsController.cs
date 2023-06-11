using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Letzte_Zeugen.Db;
using Letzte_Zeugen.Models;
using Letzte_Zeugen.Helpers;

namespace Letzte_Zeugen.Controllers
{
	public class ModellsController : Controller
	{
		private readonly LetzteZeugenDB _context;

		public ModellsController(LetzteZeugenDB context)
		{
			_context = context;
		}

		//INDEX
		// GET: Modells
		//Liste auf startseite
		public async Task<IActionResult> Index()
		{
			List<Formular> formList = new List<Formular>();

			foreach (Modell modell in _context.Modell.ToList())
			{
				formList.Add(CreateFormByID(modell.ID));
			}

			return View(formList);
		}

		// Neue Methode für Dropdowns

		//public IActionResult DropdownZustand()
		//    {

		//   }

		// GET: Modells/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null || _context.Modell == null)
			{
				return NotFound();
			}



			return View(null);
		}

        public async Task<IActionResult> Pictures(long? id)
        {
            Modell modell = _context.Modell.Find(id);

			if(modell == null)
			{
				return null;
			}
			string picturepath = "~/Files/" + id.ToString() + "/Bilder/";
			List<string> picturenames = new List<string>();
			foreach(string item in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "database/image-data/" + id.ToString() + "/Bilder")))
			{
				picturenames.Add(Path.Combine(picturepath, System.IO.Path.GetFileName(item)));
			}

            Pictures pictures = new Pictures()
			{
				ModellName = modell.Bezeichnung,
				Images = picturenames

            };
            return View(pictures);
        }

        // GET: Modells/Create
        public IActionResult Create()
		{
			return View();
		}


		// POST: Modells/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

		//Zoe hier neue hinzufügen

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Formular formular)
		{


			//DOPPLUNGSCHECK ORTE
			// Dopplungscheck für Standort beginnen
			if (formular.Standort.Koordinate != null)
			{
				foreach (var item in _context.Ort.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Koordinate.Equals(formular.Standort.Koordinate)) //wenn gleich dann ID von gleichem 

					{
						formular.Modell.Standort = item.ID;
					}
				}

				if (formular.Modell.Standort == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.Standort); //hier wird Kontext erstellt
					_context.SaveChanges();
					formular.Modell.Standort = formular.Standort.ID;
				}
			}
			//Dopplungscheck fürs Standort fertig


			// Dopplungscheck für Erstellungsort beginnen
			if (formular.Erstellungsort.Koordinate != null)
			{
				foreach (var item in _context.Ort.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Koordinate.Equals(formular.Erstellungsort.Koordinate)) //wenn gleich dann ID von gleichem 

					{
						formular.Modell.Erstellungsort = item.ID;
					}
				}

				if (formular.Modell.Erstellungsort == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.Erstellungsort); //hier wird Kontext erstellt
					_context.SaveChanges();
					formular.Modell.Erstellungsort = formular.Erstellungsort.ID;
				}
			}
			//Dopplungscheck fürs Erstellungsort fertig


			// Dopplungscheck für BeteiligteInstitute.Ort beginnen
			if (formular.BeteiligteInstitute.Ort.Koordinate != null)
			{
				foreach (var item in _context.Ort.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Koordinate.Equals(formular.BeteiligteInstitute.Ort.Koordinate)) //wenn gleich dann ID von gleichem 

					{
						formular.BeteiligteInstitute.Institute.Koordinate = item.ID;
					}
				}

				if (formular.BeteiligteInstitute.Institute.Koordinate == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.BeteiligteInstitute.Ort); //hier wird Kontext erstellt
					_context.SaveChanges();
					formular.BeteiligteInstitute.Institute.Koordinate = formular.BeteiligteInstitute.Ort.ID;
				}
			}
			//Dopplungscheck fürs BeteiligteInstitute.Ort fertig



			// Dopplungscheck für Projekt.Bauwerksort beginnen
			if (formular.Projekt.Bauwerkort.Koordinate != null)
			{
				foreach (var item in _context.Ort.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Koordinate.Equals(formular.Projekt.Bauwerkort.Koordinate)) //wenn gleich dann ID von gleichem 

					{
						formular.Projekt.Projekt.Standort = item.ID;
					}
				}

				if (formular.Projekt.Projekt.Standort == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.Projekt.Bauwerkort); //hier wird Kontext erstellt
					_context.SaveChanges();
					formular.Projekt.Projekt.Standort = formular.Projekt.Bauwerkort.ID;
				}
			}
			//Dopplungscheck fürs Projekt.Bauwerksort fertig

			//DOPPLUNGSCHECK PERSONEN

			// Dopplungscheck für Eigentuemer beginnen
			if (formular.Eigentuemer.Name != null)
			{
				foreach (var item in _context.Eigentuemer.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Name.Equals(formular.Eigentuemer.Name)) //wenn gleich dann ID von gleichem 

					{
						formular.Modell.IDEigentuemer = item.ID; // wo fremdschlüssel liegt
					}
				}

				if (formular.Modell.IDEigentuemer == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.Eigentuemer); //hier wird Kontext erstellt
					await _context.SaveChangesAsync();
					formular.Modell.IDEigentuemer = formular.Eigentuemer.ID;
				}
			}
			//Dopplungscheck für Eigentuemer fertig


			// Dopplungscheck für Urheber beginnen
			if (formular.Urheber.Nachname != null)
			{
				foreach (var item in _context.Person.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Nachname.Equals(formular.Urheber.Nachname)) //wenn gleich dann ID von gleichem 

					{
						formular.Modell.IDUrheber = item.ID; // wo fremdschlüssel liegt
					}
				}

				if (formular.Modell.IDUrheber == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.Urheber); //hier wird Kontext erstellt
					_context.SaveChanges();
					formular.Modell.IDUrheber = formular.Urheber.ID;
				}
			}
			//Dopplungscheck für Urheber fertig


			//unterste ebene hinzufügen
			if (formular.Link.LinkPfad != null)
			{
				_context.Add(formular.Link);
			}

			_context.SaveChangesAsync(); //speichern um IDs automatisiert legen zu lassen

			//zweite ebene hinzufügen von den Helpern

			//Ids setzen um Verknüpfung zu haben
			if(formular.Link.LinkPfad != null)
			{
				formular.Modell.IDLink = formular.Link.ID;
			}
			if(formular.Projekt.Bauwerkort != null)
			{
				formular.Projekt.Projekt.Standort = formular.Projekt.Bauwerkort.ID;
			}

			/*if(formular.BeteiligteInstitute.Institute != null)
			{
				_context.Add(formular.BeteiligteInstitute.Institute);
			}*/
			_context.SaveChangesAsync();
			//DOPPLUNGSCHECK


			// Dopplungscheck für Projekt beginnen
			if (formular.Projekt.Projekt.Projekt1 != null && formular.Projekt.Projekt.Standort != null)
			{
				foreach (var item in _context.Projekt.ToList())

				{
					if (item.Projekt1.Equals(formular.Projekt.Projekt.Projekt1) && item.Standort == formular.Projekt.Projekt.Standort)

					{
						formular.Modell.IDProjekt = item.ID;
					}
				}

				if (formular.Modell.IDProjekt == null)

				{
					_context.Add(formular.Projekt.Projekt); //hier wird Kontext erstellt
					_context.SaveChanges();
					formular.Modell.IDProjekt = formular.Projekt.Projekt.ID;
				}
			}
			//Dopplungscheck fürs Projekt fertig 



			//BETEILIGTE INSTITUTE

			// Dopplungscheck für BeteilgiteInstitue beginnen
			if (formular.BeteiligteInstitute.Institute.Name != null)
			{
				foreach (var item in _context.Institute.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Name.Equals(formular.BeteiligteInstitute.Institute.Name)) //wenn gleich dann ID von gleichem 

					{
						formular.Modell.IDBeteiligteInstitute = item.ID; // wo fremdschlüssel liegt
					}
				}

				if (formular.Modell.IDBeteiligteInstitute == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.BeteiligteInstitute); //hier wird Kontext erstellt (DB)
					_context.SaveChanges();
					formular.Modell.IDBeteiligteInstitute = formular.BeteiligteInstitute.Institute.ID;
				}
			}
			//Dopplungscheck für BeteilgiteInstitue fertig





			_context.SaveChanges(); //speichern um IDs automatisiert legen zu lassen



			formular.Modell.IDBeteiligteInstitute = formular.BeteiligteInstitute.Institute.ID;



			_context.Add(formular.Modell);
			await _context.SaveChangesAsync();


			//Bilder speichern
			if (formular.Picture != null)
			{
				StoreHelper.SaveModellImage(formular.Picture, formular.Modell.ID, "Bilder");
			}
			if (formular.Files != null)
			{
				StoreHelper.SaveModellImage(formular.Files, formular.Modell.ID, "Dateien");
			}



			//Mehrfach Namen Eintragen bei Beteiligte Personen 
			if (formular.BeteiligtePersonen != null)
			{
				String[] personen = formular.BeteiligtePersonen.Split(";");
				Person[] arraypersonen = new Person[personen.Length];
				for (int i = 0; i < personen.Length; i++)

				{
					foreach (var item in _context.Person.ToList())
					{
						if (item.Nachname.Equals(personen[i]))
						{
							arraypersonen[i] = item;
						}
					}

					if (arraypersonen[i] == null)
					{
						arraypersonen[i] = new Person()

						{
							Nachname = personen[i]
						};

						_context.Add(arraypersonen[i]);
						await _context.SaveChangesAsync();
					}
					BeteiligtePersonen temp = new BeteiligtePersonen()
					{

						IDModell = formular.Modell.ID,
						IDPerson = arraypersonen[i].ID
					};
					_context.Add(temp);
				}
				await _context.SaveChangesAsync();
			}


			return RedirectToAction(nameof(Index));

		}



		// GET: Modells/Edit/5
		public async Task<IActionResult> Edit(long id)
		{


			return View(CreateFormByID(id));
		}

		// POST: Modells/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Formular formular)
		{

			Formular oldForm = CreateFormByID(formular.Modell.ID);




			//DOPPLUNGSCHECK ORTE

			if (formular.Standort.Koordinate != null && oldForm.Standort != null)
			{
				if (oldForm.Standort.Koordinate == formular.Standort.Koordinate)
				{
					formular.Modell.Standort = oldForm.Modell.Standort;
				}
			}
			if (formular.Erstellungsort.Koordinate != null && oldForm.Erstellungsort != null)
			{
				if (oldForm.Erstellungsort.Koordinate == formular.Erstellungsort.Koordinate)
				{
					formular.Modell.Erstellungsort = oldForm.Modell.Erstellungsort;
				}
			}
			if (formular.BeteiligteInstitute.Ort.Koordinate != null && oldForm.BeteiligteInstitute.Ort != null)
			{
				if (oldForm.BeteiligteInstitute.Ort.Koordinate == formular.BeteiligteInstitute.Ort.Koordinate)
				{
					formular.BeteiligteInstitute.Institute.Koordinate = oldForm.BeteiligteInstitute.Institute.Koordinate;
				}
			}
			if (formular.Projekt.Bauwerkort.Koordinate != null && oldForm.Projekt.Bauwerkort != null)
			{
				if (oldForm.Projekt.Bauwerkort.Koordinate == formular.Projekt.Bauwerkort.Koordinate)
				{
					formular.Projekt.Projekt.Standort = oldForm.Projekt.Projekt.Standort;
				}
			}


			foreach (var item in _context.Ort.ToList())
			{
				//Dopplungscheck Standort
				if (formular.Standort.Koordinate != null && oldForm.Standort.Koordinate != formular.Standort.Koordinate && item.Koordinate.Equals(formular.Standort.Koordinate))
				{
					formular.Modell.Standort = item.ID;
				}
				//Dopplungscheck Erstellungsort
				if (formular.Erstellungsort.Koordinate != null && oldForm.Erstellungsort.Koordinate != formular.Erstellungsort.Koordinate && item.Koordinate.Equals(formular.Erstellungsort.Koordinate))
				{
					formular.Modell.Erstellungsort = item.ID;
				}
				//Dopplungscheck Beteiligte Institute.Ort
				if (formular.BeteiligteInstitute.Ort.Koordinate != null && oldForm.BeteiligteInstitute.Ort.Koordinate != formular.BeteiligteInstitute.Ort.Koordinate && item.Koordinate.Equals(formular.BeteiligteInstitute.Ort.Koordinate))
				{
					formular.BeteiligteInstitute.Institute.Koordinate = item.ID;
				}
				//Dopplungscheck Projekt.Bauwerksort
				if (formular.Projekt.Bauwerkort.Koordinate != null && oldForm.Projekt.Bauwerkort.Koordinate != formular.Projekt.Bauwerkort.Koordinate && item.Koordinate.Equals(formular.Projekt.Bauwerkort.Koordinate))
				{
					formular.Projekt.Projekt.Standort = item.ID;
				}
			}

			if (formular.Modell.Standort == null) // Abfrage, wenn nicht Vorhanden neue ID

			{
				_context.Add(formular.Standort); //hier wird Kontext erstellt
				_context.SaveChanges();
				formular.Modell.Standort = formular.Standort.ID;
			}

			if (formular.Modell.Erstellungsort == null) // Abfrage, wenn nicht Vorhanden neue ID

			{
				_context.Add(formular.Erstellungsort); //hier wird Kontext erstellt
				_context.SaveChanges();
				formular.Modell.Erstellungsort = formular.Erstellungsort.ID;
			}

			if (formular.BeteiligteInstitute.Institute.Koordinate == null) // Abfrage, wenn nicht Vorhanden neue ID

			{
				_context.Add(formular.BeteiligteInstitute.Ort); //hier wird Kontext erstellt
				_context.SaveChanges();
				formular.BeteiligteInstitute.Institute.Koordinate = formular.BeteiligteInstitute.Ort.ID;
			}

			if (formular.Projekt.Projekt.Standort == null) // Abfrage, wenn nicht Vorhanden neue ID

			{
				_context.Add(formular.Projekt.Bauwerkort); //hier wird Kontext erstellt
				_context.SaveChanges();
				formular.Projekt.Projekt.Standort = formular.Projekt.Bauwerkort.ID;
			}

			//DOPPLUNGSCHECK PERSONEN

			if (formular.Eigentuemer.Name != null && oldForm.Eigentuemer.Name != null)
			{
				if (formular.Eigentuemer.Name == oldForm.Eigentuemer.Name)
				{
					formular.Modell.IDEigentuemer = oldForm.Modell.IDEigentuemer;
				}
			}
			if (formular.Urheber.Nachname != null && oldForm.Urheber.Nachname != null)
			{
				if (formular.Urheber.Nachname == oldForm.Urheber.Nachname)
				{
					formular.Modell.IDUrheber = oldForm.Modell.IDUrheber;
				}
			}


			foreach (var item in _context.Person.ToList())
			{
				//Dopplungscheck Urheber
				if (formular.Urheber.Nachname != null && formular.Urheber.Nachname != oldForm.Urheber.Nachname && item.Nachname.Equals(formular.Urheber.Nachname))
				{
					formular.Modell.IDUrheber = item.ID;
				}

			}

			foreach (var item in _context.Eigentuemer.ToList())
			{
				if (formular.Eigentuemer.Name != null && formular.Eigentuemer.Name != oldForm.Eigentuemer.Name && item.Name.Equals(formular.Eigentuemer.Name))
				{
					formular.Modell.IDEigentuemer = item.ID;
				}
			}

			if (formular.Modell.IDEigentuemer == null) // Abfrage, wenn nicht Vorhanden neue ID

			{
				_context.Add(formular.Eigentuemer); //hier wird Kontext erstellt
				await _context.SaveChangesAsync();
				formular.Modell.IDEigentuemer = formular.Eigentuemer.ID;
			}

			if (formular.Modell.IDUrheber == null) // Abfrage, wenn nicht Vorhanden neue ID

			{
				_context.Add(formular.Urheber); //hier wird Kontext erstellt
				_context.SaveChanges();
				formular.Modell.IDUrheber = formular.Urheber.ID;
			}



			//unterste ebene hinzufügen

			if (formular.Link != oldForm.Link)
			{
				_context.Remove(oldForm.Link);
				_context.Add(formular.Link);
			}
			else
			{
				formular.Link = oldForm.Link;
			}
			_context.SaveChangesAsync(); //speichern um IDs automatisiert legen zu lassen

			//zweite ebene hinzufügen von den Helpern

			//Ids setzen um Verknüpfung zu haben

			formular.Projekt.Projekt.Standort = formular.Projekt.Bauwerkort.ID;
			formular.Modell.IDLink = formular.Link.ID;
			if (formular.BeteiligteInstitute.Institute != null)
			{
				_context.Update(formular.BeteiligteInstitute.Institute);
				_context.SaveChangesAsync();
			}



			//DOPPLUNGSCHECK


			// Dopplungscheck für Projekt beginnen
			if (formular.Projekt.Projekt.Projekt1 != null && formular.Projekt.Projekt.Standort != null && (formular.Projekt.Projekt.Projekt1 != oldForm.Projekt.Projekt.Projekt1 || formular.Projekt.Projekt.Standort != oldForm.Projekt.Projekt.Standort))
			{
				foreach (var item in _context.Projekt.ToList())

				{
					if (item.Projekt1.Equals(formular.Projekt.Projekt.Projekt1) && item.Standort == formular.Projekt.Projekt.Standort)

					{
						formular.Modell.IDProjekt = item.ID;
					}
				}

				if (formular.Modell.IDProjekt == null)

				{
					_context.Add(formular.Projekt.Projekt); //hier wird Kontext erstellt
					_context.SaveChanges();
					formular.Modell.IDProjekt = formular.Projekt.Projekt.ID;
				}
			}
			else
			{
				formular.Projekt.Projekt.Projekt1 = oldForm.Projekt.Projekt.Projekt1;
				formular.Projekt.Projekt.Standort = oldForm.Projekt.Projekt.Standort;
			}
			//Dopplungscheck fürs Projekt fertig 



			//BETEILIGTE INSTITUTE

			// Dopplungscheck für BeteilgiteInstitue beginnen
			if (formular.BeteiligteInstitute.Institute.Name != null && formular.BeteiligteInstitute.Institute != oldForm.BeteiligteInstitute.Institute)
			{
				foreach (var item in _context.Institute.ToList()) // Abfrage, ob es schon in Db existiert 

				{
					if (item.Name.Equals(formular.BeteiligteInstitute.Institute.Name)) //wenn gleich dann ID von gleichem 

					{
						formular.Modell.IDBeteiligteInstitute = item.ID; // wo fremdschlüssel liegt
					}
				}

				if (formular.Modell.IDBeteiligteInstitute == null) // Abfrage, wenn nicht Vorhanden neue ID

				{
					_context.Add(formular.BeteiligteInstitute.Institute);
					_context.SaveChanges();
					formular.Modell.IDBeteiligteInstitute = formular.BeteiligteInstitute.Institute.ID;
				}
			}
			else
			{
				formular.BeteiligteInstitute.Institute = oldForm.BeteiligteInstitute.Institute;
			}
			//Dopplungscheck für BeteilgiteInstitue fertig





			_context.SaveChanges(); //speichern um IDs automatisiert legen zu lassen



			formular.Modell.IDBeteiligteInstitute = formular.BeteiligteInstitute.Institute.ID;
			//formular.Modell.ID = oldForm.Modell.ID;

			_context.Remove(oldForm.Modell);
			_context.Add(formular.Modell);


			await _context.SaveChangesAsync();


			//Mehrfach Namen Eintragen bei Beteiligte Personen 

			String[] personen = formular.BeteiligtePersonen.Split(";");
			Person[] arraypersonen = new Person[personen.Length];
			for (int i = 0; i < personen.Length; i++)

			{
				foreach (var item in _context.Person.ToList())
				{
					if (item.Nachname.Equals(personen[i]))
					{
						arraypersonen[i] = item;
					}
				}

				if (arraypersonen[i] == null)
				{
					arraypersonen[i] = new Person()
					{
						Nachname = personen[i]
					};

					_context.Add(arraypersonen[i]);
					await _context.SaveChangesAsync();
				}
				bool doubled = false;

				BeteiligtePersonen temp = new BeteiligtePersonen()
				{

					IDModell = formular.Modell.ID,
					IDPerson = arraypersonen[i].ID
				};
				foreach (var item in _context.BeteiligtePersonen)
				{
					if (temp.IDPerson == item.IDPerson && temp.IDModell == item.IDModell)
					{
						doubled = true;
					}
				}
				if (!doubled)
					_context.Add(temp);
			}
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));


		}

		// GET: Modells/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null || _context.Modell == null)
			{
				return NotFound();
			}


			return View(null);
		}

		// POST: Modells/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			if (_context.Modell == null)
			{
				return Problem("Entity set 'LetzteZeugenDB.Modell'  is null.");
			}
			var modell = await _context.Modell.FindAsync(id);
			if (modell != null)
			{
				_context.Modell.Remove(modell);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ModellExists(long id)
		{
			return (_context.Modell?.Any(e => e.ID == id)).GetValueOrDefault();
		}

		private Formular CreateFormByID(long id)
		{
			if (id == null || _context.Modell == null)
			{
				return null;
			}

			Modell modell = _context.Modell.Find(id);
			if (modell == null)
			{
				return null;
			}



			Institute beteiligteInstitute = _context.Institute.Find(modell.IDBeteiligteInstitute);
			Institute pruefInstitute = _context.Institute.Find(modell.IDPruefinstitut);
			Projekt projekt = _context.Projekt.Find(modell.IDProjekt);

			if (projekt == null)
			{
				projekt = new Projekt();
			}
			if (pruefInstitute == null)
			{
				pruefInstitute = new Institute();
			}
			if (beteiligteInstitute == null)
			{
				beteiligteInstitute = new Institute();
			}

			String Personenliste = "";

			foreach (var item in _context.BeteiligtePersonen.ToList())
			{
				if (item.IDModell == modell.ID)

				{
					Personenliste += _context.Person.Find(item.IDPerson).Nachname + "; ";
				}

			}
			if (Personenliste.Length > 2)
			{
				Personenliste = Personenliste.Substring(0, Personenliste.Length - 2);
			}


			Formular formular = new Formular()
			{
				Modell = modell,
				Bautypus = _context.Bautypus.Find(modell.IDBautypus),
				Gefaehrdung = _context.Gefaehrdung.Find(modell.IDGefaehrdung),
				BeteiligtePersonen = Personenliste,
				Eigentuemer = _context.Eigentuemer.Find(modell.IDEigentuemer),
				Erstellungsort = _context.Ort.Find(modell.Erstellungsort),
				Standort = _context.Ort.Find(modell.Standort),
				BeteiligteInstitute = new InstitutHelper() { Institute = beteiligteInstitute, Ort = _context.Ort.Find(beteiligteInstitute.Koordinate) },
				PruefInstitute = new InstitutHelper() { Institute = pruefInstitute, Ort = _context.Ort.Find(pruefInstitute.Koordinate) },
				Material = _context.Material.Find(modell.IDMaterial),
				Projekt = new ProjektHelper() { Projekt = projekt, Bauwerkort = _context.Ort.Find(projekt.Standort) },
				Urheber = _context.Person.Find(modell.IDUrheber),
				Existent = _context.Existent.Find(modell.IDExistent),
				Zustand = _context.Zustand.Find(modell.IDZustand),
				Messmodell = _context.Messmodell.Find(modell.IDMessmodell),
				Realisiert = _context.Realisiert.Find(projekt.IDRealisiert),
				Link = _context.Link.Find(modell.IDLink),
			};
			return formular;
		}
	}

}
