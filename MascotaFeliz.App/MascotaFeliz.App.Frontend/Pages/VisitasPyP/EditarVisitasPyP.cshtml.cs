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
    public class EditarVisitasPyPModel : PageModel
    {
        private readonly IRepositorioVisitaPyP _repoVisitaPyP;
        private readonly IRepositorioVeterinario _repoVeterinario;
        private readonly IRepositorioMascota _repoMascota;
        private readonly IRepositorioHistoria _repoHistoria;
        [BindProperty]
        public VisitaPyP visitaPyP { get; set; }
        public IEnumerable<Mascota> mascotas {get; set;}
        public IEnumerable<Veterinario> veterinarios {get;set;}
        public int mascotaId;

        public EditarVisitasPyPModel()
        {
            this._repoVisitaPyP = new RepositorioVisitaPyP(new Persistencia.AppContext());
            this._repoVeterinario = new RepositorioVeterinario(new Persistencia.AppContext());
            this._repoMascota = new RepositorioMascota(new Persistencia.AppContext());
            this._repoHistoria = new RepositorioHistoria(new Persistencia.AppContext());

          }
                public void OnGet(int? visitaPyPId)
          {
            mascotas = _repoMascota.GetAllMascotas();
            veterinarios = _repoVeterinario.GetAllVeterinarios();

            if (visitaPyPId.HasValue)
            {
                visitaPyP = _repoVisitaPyP.GetVisitaPyP(visitaPyPId.Value);
            }
            else 
            {
                visitaPyP = new VisitaPyP{FechaVisita = DateTime.Today};
               
            }
            if (visitaPyP == null)
            {
              RedirectToPage("./NotFound");
            }
             else 
              {
                 Page();
              }
        }

        public IActionResult OnPost(int vetId, int mascotaId)
        {
            
            if (!ModelState.IsValid)
                {
               return  Page();
            
                }
                 
                if (visitaPyP.Id > 0)
                {
                       
                   visitaPyP= _repoVisitaPyP.UpdateVisitaPyP(visitaPyP);
                }
                else
                {
                 visitaPyP.IdVeterinario = vetId;  
                 visitaPyP = _repoVisitaPyP.AddVisitaPyP(visitaPyP);
                 var mascota = _repoMascota.GetMascota(mascotaId);
                 var historia = _repoHistoria.GetHistoria(mascota.Historia.Id);
                 _repoHistoria.AsignarVisita(historia, visitaPyP);
                }
                                          
                 return RedirectToPage("../Index");;
            }
        }
              
    }
