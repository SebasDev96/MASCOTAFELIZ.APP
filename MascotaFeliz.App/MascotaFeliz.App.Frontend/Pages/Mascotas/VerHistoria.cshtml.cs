using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MascotaFeliz.App.Dominio;
using MascotaFeliz.App.Persistencia;

namespace MascotaFeliz.App.Frontend.Pages
{
    public class VerHistoriaModel : PageModel
    {
        private readonly IRepositorioMascota _repositorioMascota;
        private readonly IRepositorioHistoria _repositorioHistoria;
        [BindProperty]
        public Mascota Mascota {get;set;}
        public List<VisitaPyP> ListaVisitaspyp {get;set;}
        public Historia Historia {get;set;}

        public VerHistoriaModel()
        {
            this._repositorioMascota=new RepositorioMascota(new Persistencia.AppContext());
            this._repositorioHistoria=new RepositorioHistoria(new Persistencia.AppContext());
        }
        public IActionResult OnGet(int mascotaId)
        {
            ListaVisitaspyp=new List<VisitaPyP>();
           

            Mascota=_repositorioMascota.GetMascota(mascotaId);
            
            Historia=_repositorioHistoria.GetHistoria(Mascota.Historia.Id);
            
            foreach (var h in Historia.VisitasPyP)
            {
                ListaVisitaspyp.Add(h);
            }

            return Page();


        }
    }
}
